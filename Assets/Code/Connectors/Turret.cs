using UnityEngine;
using System.Collections.Generic;

public class Turret : MonoBehaviour
{
    //la conexion contiene lso dos switches y consulta si estan conectados
    public  Connection conn;

    //la coordenada x a la que dispara
    private int coordenateX;
    //la coordenada y a la que dispara
    private int coordenateY;

    private void Start()
    {
        SetX(-1);
        SetY(-1);
    }

    /*en cada frame consulta si la conexion es correcta y 
    en tal caso llama al disparador del radar con las coordenadas*/
    public void Update()
    {
        if (conn.CheckConn())
        {
            SetX(conn.GetX());
            SetY(conn.GetY());
            //Radar.dispara(connection.sh.value, connection sh.value);
            Debug.Log("apuntando a X: " + coordenateX + "y: " + coordenateY);
            gameObject.GetComponent<Renderer>().material.color = Color.green;
        }else
        {
            gameObject.GetComponent<Renderer>().material.color = Color.red;

        }
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
}


