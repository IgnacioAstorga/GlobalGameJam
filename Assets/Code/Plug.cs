using UnityEngine;
using System.Collections.Generic;

public class Plug : MonoBehaviour
{

    //posibles valores radar
    public enum radarCoord { HOR, VER };
    //define si elo interruptor apunta a la coordenada X o Y del radar
    public radarCoord coord;
    //esta conectado a un switch o no
    private bool switched;
    //valor numerico de la coordenada
    public int value;

    public void Start() {
        switched = false;
    }

    public void setSwitched(bool newValue)
    {
        switched = newValue;
        Debug.Log(value);
    }
    public bool getSwitched()
    {
        return switched;
    }

}