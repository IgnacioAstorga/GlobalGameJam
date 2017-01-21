using UnityEngine;
using System.Collections;

public class SineLine : MonoBehaviour {

    public Color c1 = Color.yellow;
    public Color c2 = Color.red;
    public int lengthOfLineRenderer = 20;
    void Start()
    {
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        lineRenderer.SetColors(c1, c2);
        lineRenderer.SetWidth(0.2F, 0.2F);
        lineRenderer.SetVertexCount(lengthOfLineRenderer);
    }

    public float size = 5.0f;
    public float x = 1;
    public float y = 2;
    public float d = 0;

    void Update()
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetVertexCount(lengthOfLineRenderer);
        
        for (int i = 0; i < lengthOfLineRenderer; i++)
        {
            Vector3 pos = Vector3.zero;
            float value = (float)i / lengthOfLineRenderer;
            pos.x = value * size;
            pos.y = y * Mathf.Sin(value * x + d);
            lineRenderer.SetPosition(i, pos);
        }
    }
}
