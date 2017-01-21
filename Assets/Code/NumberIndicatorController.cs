using UnityEngine;
using System.Collections;

public class NumberIndicatorController : MonoBehaviour {

    public Renderer materialRenderer;

    public TextMesh text;

    public void Set(Color color, int number, bool isSelected = false)
    {
        materialRenderer.material.color = color;

        text.text = number.ToString();
    }

    public void SetSelected(bool isSelected)
    {

        if (isSelected)
        {
            materialRenderer.material.SetColor("_EmissionColor", materialRenderer.material.color);
        }
        else
        {
            materialRenderer.material.SetColor("_EmissionColor", Color.black);
        }
    }
}
