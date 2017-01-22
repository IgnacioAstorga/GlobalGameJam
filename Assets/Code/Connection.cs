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
            PositionFromPrime();
            scaleZ = (to.transform.position - tFrom.transform.position).magnitude;
        }
        else
        {
            PositionFrom();
            scaleZ = (to.transform.position - from.transform.position).magnitude;

        }

        PointingTo();
        ScaleZ();
    }

    //Apunta a un objeto
    public void PointingTo()
    {
        /*
        Vector3 relativePos = xx.transform.position - (transform.position);
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        transform.rotation = rotation;
        */
        transform.LookAt(to.transform.position);
    }

    //Parte des de un objeto
    public void PositionFrom()
    {
        Vector3 relativePos = from.transform.position;
        this.transform.position = relativePos;
    }
    //Parte des de un objeto version PRIME
    public void PositionFromPrime()
    {
        Vector3 relativePos = tFrom.transform.position;
        this.transform.position = relativePos;
    }
    //se adapta a la distancia de los objetos
    public void ScaleZ() {
       transform.localScale = new Vector3(1, 1, scaleZ/2);
    }

}