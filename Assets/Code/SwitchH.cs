using UnityEngine;
using System.Collections.Generic;

public class SwitchH : MonoBehaviour
{
    //define el estado del interruptor. si esta conectado. puede ser true o false
    public bool switched;

    //valor que representa posicion vertical
    public static int value;

    //por defecto esta apagado
    private void Start()
    {
        switched = false;
    }

    //al tocar cambia el estado a true o false
    private void OnMouseUp()
    {
        if (switched)
        {
            switched = false;
        }
        else
        {
            switched = true;
        }
    }
    //devuelve el valor del interruptor
    public int GetValue()
    {
        return value;
    }

}

