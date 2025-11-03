using TMPro;
using UnityEngine;

public class FrostEffect : MonoBehaviour
{
    public TextMeshProUGUI tmp;
    public float speed = 2f;
    public float intensity = 0.25f;

    void Update()
    {
        if (tmp == null) return;

        float t = (Mathf.Sin(Time.time * speed) + 1f) / 2f;
        Color glow = Color.Lerp(new Color(0.5f, 0.8f, 1f), Color.white, t);
        tmp.outlineColor = glow;
        tmp.outlineWidth = 0.1f + intensity * t;
    }
}