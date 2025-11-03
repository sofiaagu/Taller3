using TMPro;
using UnityEngine;

public class InfernoEffect : MonoBehaviour
{
    public TextMeshProUGUI tmp;
    public float speed = 3f;
    public float intensity = 0.3f;

    void Update()
    {
        if (tmp == null) return;

        float t = Mathf.Sin(Time.time * speed);
        Color glow = new Color(1f, 0.4f + 0.3f * Mathf.Abs(t), 0f);
        tmp.outlineColor = glow;
        tmp.outlineWidth = 0.15f + intensity * Mathf.Abs(t);
    }
}