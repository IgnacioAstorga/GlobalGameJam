using UnityEngine;
using System.Collections.Generic;

public class Plug : MonoBehaviour
{

    //posibles valores radar
    public enum radarCoord { HOR, VER };
    //define si elo interruptor apunta a la coordenada X o Y del radar
    public radarCoord coord;
    //esta conectado a un switch o no
    public bool switched;
    //valor numerico de la coordenada
    public int value;

    public void Start() {
        switched = false;
    }

    public void Update() {
        
    }

}