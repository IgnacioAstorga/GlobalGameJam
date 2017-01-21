using UnityEngine;
using System.Collections.Generic;

public class Connection : MonoBehaviour
{
    //posibles valores radar
    public enum type { PRIME, ORDINARY };
    //define si elo interruptor apunta a la coordenada X o Y del radar
    public type towerType;
    public Turret tFrom;
    public Switch from;
    public Switch to;
    public float scaleZ;
    /*
    public Switch sv;

    private bool stablished;
    */
   

    public void Update() {
        if (towerType == type.PRIME) {
            PositionFrom(tFrom);
            scaleZ = (to.transform.position - tFrom.transform.position).magnitude;
        }
        else
        {
            PositionFrom(from);
            scaleZ = (to.transform.position - from.transform.position).magnitude;

        }

        PointingTo(to);
        ScaleZ();
    }

    //Apunta a un objeto
    public void PointingTo(MonoBehaviour xx)
    {
        /*
        Vector3 relativePos = xx.transform.position - (transform.position);
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        transform.rotation = rotation;
        */
        transform.LookAt(xx.transform.position);
    }

    //Parte des de un objeto
    public void PositionFrom(MonoBehaviour xx)
    {
        Vector3 relativePos = xx.transform.position;
        this.transform.position = relativePos;
    }
    //se adapta a la distancia de los objetos
    public void ScaleZ() {
       transform.localScale = new Vector3(1, 1, scaleZ/2);
    }

}