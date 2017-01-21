using UnityEngine;
using System.Collections.Generic;

public class Plug : MonoBehaviour
{
    //
    public int value;

    public void Start() {
        value = 0;
    }

    public void Update() {
        
    }

    //devuelve el valor del enchufeal colisionar con el
   void OnTriggerEnter(Collider other)
    {
        Debug.Log("im colliding");
    }

}