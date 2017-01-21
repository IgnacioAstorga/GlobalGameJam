using UnityEngine;
using System.Collections.Generic;

public class Interruptor : MonoBehaviour
{
    //define el estado del interruptor. puede ser true o false
    public bool estado;

    //valor que representa posicion vertical
    public int pos;

    private void Start()
    {
        estado = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            estado = true;
        }
    }
}


