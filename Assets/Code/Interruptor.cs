using UnityEngine;
using System.Collections.Generic;

public class Interruptor : MonoBehaviour
{
    //define el estado del interruptor. puede estar ON o OFF
    public string estado;

    private void Start()
    {
        estado = "off";
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            Debug.Log("Hit me");
        }
    }
}

