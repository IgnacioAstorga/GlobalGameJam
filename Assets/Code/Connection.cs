using UnityEngine;
using System.Collections.Generic;

public class Connection : MonoBehaviour
{
    //
    public SwitchH sh;
    public SwitchV sv;
    private bool stablished;

    public void Start() {

    }

    public void Update() {
      
    }
    //comprueba que la conexion es correcta
    public bool CheckConn() {
        //comprueba que lso dos interruptores estan conectados
        if (sh.switched && sv.switched)
        {
            stablished = true;
        }
        else {
            stablished = false;
        }
        return stablished;
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