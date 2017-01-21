using UnityEngine;
using System.Collections;

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
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("im colliding");
    }
    void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }

}