using UnityEngine;
using System.Collections.Generic;

public class Turret : MonoBehaviour
{
        //la coordenada x a la que dispara
    private int coordenateX;
    //la coordenada y a la que dispara
    private int coordenateY;
    //torreta
    public Switch sh;

    public Switch sv;

    private bool stablished;

    private void Start()
    {
        SetX(-1);
        SetY(-1);
    }

    /*en cada frame consulta si la conexion es correcta y 
    en tal caso llama al disparador del radar con las coordenadas*/
    public void Update()
    {

        if (CheckConn())
        {
            SetX(GetX());
            SetY(GetY());
            GameController.GetInstance().radar.DestroyEnemiesAtPosition(coordenateX, coordenateY);
            Debug.Log("Disparando a "+coordenateX+" "+coordenateY);
        }        
    }

    public bool CheckConn()
    {
        //comprueba que lso dos interruptores estan conectados
        if (sh.switched && sv.switched)
        {
            stablished = true;
        }
        else
        {
            stablished = false;
        }
        return stablished;
    }

    //da un valor a X
    public void SetX(int xx)
    {
        coordenateX = xx;
    }
    //da un valor a Y
    public void SetY(int yy)
    {
        coordenateY = yy;
    }
    //devuelve el valor del interruptorH
    public int GetX()
    {
        return sh.GetValue();
    }

    //devuelve el valor del interruptorV
    public int GetY()
    {
        return sv.GetValue();
    }
}


