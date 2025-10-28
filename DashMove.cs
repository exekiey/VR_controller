using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class DashedLine : MonoBehaviour
{
    public float dashSpeed = 2f;
    public Color dashColor = Color.white;

    private LineRenderer lineRenderer;
    private Material material;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        material = new Material(lineRenderer.material);
        lineRenderer.material = material;
        material.color = dashColor;
        material.mainTextureScale = new Vector2(1f, 1f);
    }

    void Update()
    {
        Vector2 offset = material.mainTextureOffset;
        offset.x += dashSpeed * Time.deltaTime;
        material.mainTextureOffset = offset;
    }
}