using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 720f;

    [Header("Jump & Gravity")]
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 2f;

    [Header("Low Friction Simulation")]
    [Tooltip("Simula baja fricción: cuanto menor el valor, más 'resbaloso' será el movimiento.")]
    [Range(0f, 1f)][SerializeField] private float friction = 0.05f;

    [Header("Optional")]
    [Tooltip("Si se asigna, el movimiento será relativo a esta cámara (ej: cámara orbital).")]
    public Camera mouseOrbitCamera;

    private CharacterController controller;
    private Animator anim;

    private Vector2 moveInput;
    private Vector3 velocity;
    private Vector3 currentHorizontalVelocity; // ← velocidad con fricción
    private bool isGroundedByTag = false; // ← Detecta si está tocando algo con tag "Ground"

    private static readonly int VelX = Animator.StringToHash("velX");
    private static readonly int VelY = Animator.StringToHash("velY");
    private static readonly int JumpTrigger = Animator.StringToHash("jump");

    [SerializeField] private float animDamp = 0.05f;
    private float velXCur, velYCur;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // === NUEVO INPUT SYSTEM ===
    public void OnMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }

    private void Update()
    {
        // === MOVIMIENTO ===
        Vector3 input = new Vector3(moveInput.x, 0f, moveInput.y);

        Vector3 moveWorld;
        if (mouseOrbitCamera != null && mouseOrbitCamera.gameObject.activeInHierarchy)
        {
            Vector3 camFwd = mouseOrbitCamera.transform.forward;
            camFwd.y = 0f;
            camFwd.Normalize();

            Vector3 camRight = mouseOrbitCamera.transform.right;
            camRight.y = 0f;
            camRight.Normalize();

            moveWorld = camRight * input.x + camFwd * input.z;
        }
        else
        {
            moveWorld = transform.right * input.x + transform.forward * input.z;
        }

        // --- Simulación de baja fricción (aceleración y desaceleración suave) ---
        Vector3 targetVelocity = moveWorld * moveSpeed;
        currentHorizontalVelocity = Vector3.Lerp(currentHorizontalVelocity, targetVelocity, 1f - friction);

        controller.Move(currentHorizontalVelocity * Time.deltaTime);

        // === ROTACIÓN ===
        Vector3 lookDir = new Vector3(currentHorizontalVelocity.x, 0f, currentHorizontalVelocity.z);
        if (lookDir.sqrMagnitude > 0.0001f)
        {
            Quaternion targetRot = Quaternion.LookRotation(lookDir, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
        }

        // === SALTO CON ESPACIO + TAG GROUND ===
        if (isGroundedByTag)
        {
            if (velocity.y < 0f)
                velocity.y = -2f;

            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                if (anim != null)
                    anim.SetTrigger(JumpTrigger);
            }
        }

        // === GRAVEDAD ===
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // === ANIMACIONES ===
        velXCur = Mathf.Lerp(velXCur, moveInput.x, animDamp);
        velYCur = Mathf.Lerp(velYCur, moveInput.y, animDamp);
        if (anim != null)
        {
            anim.SetFloat(VelX, velXCur);
            anim.SetFloat(VelY, velYCur);
        }
    }

    // === DETECCIÓN DEL SUELO POR TAG "Ground" ===
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("Ground"))
            isGroundedByTag = true;
        else
            isGroundedByTag = false;
    }
}
