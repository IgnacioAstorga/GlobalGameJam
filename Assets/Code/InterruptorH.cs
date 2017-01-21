using UnityEngine;
using System.Collections.Generic;

public class InterruptorH : MonoBehaviour
{
    //define el estado del interruptor. puede ser true o false
    public bool estado;

    //valor que representa posicion horizontal
    public int pos;

    private void Start()
    {
        estado = false;
    }

    private void Update()
    {
        this.onMouseUp(){
            Debug.Log("hit!! My pos is " + pos);
            estado = true;

        }
    }

    
}

