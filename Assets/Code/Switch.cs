using UnityEngine;
using System.Collections.Generic;

public class Switch : MonoBehaviour
{
    //posibles valores radar
    public enum radarCoord { HOR, VER};
    //define si elo interruptor apunta a la coordenada X o Y del radar
    public radarCoord coord; 
    //define el estado del interruptor. si esta conectado. puede ser true o false
    public bool switched;

    //valor que representa posicion vertical
    public int value;

    //por defecto esta apagado
    private void Start()
    {
        switched = false;
        value = -1;
    }

    //en cada frame
    public void Update() {
        
        if (switched)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.green;
        }
        else
        {
            gameObject.GetComponent<Renderer>().material.color = Color.red;

        }
    }
    //al mantenerlo apretado sigue al raton
    void OnMouseDown()
    {
        switched = false;
    }
    void OnMouseDrag() {
        MouseMove();
    }

    //al dejar de draggarlo revisa si siene valor
    private void OnMouseUp()
    {
        if (value != -1)
        {
            switched = true;
            //gameObject.GetComponent<Renderer>().material.color = Color.green;
        }
        else {
            //POSICION INICIAL
        }
    }
    
    //devuelve el valor del interruptor
    public int GetValue()
    {
        return value;
    }

    //sigue al raton
    public void MouseMove()
    {
        var pos = Input.mousePosition;
        pos.z = 5f;
        pos = Camera.main.ScreenToWorldPoint(pos);
        transform.position = pos;   
    }

    //devuelve el valor del enchufe al colisionar con el
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Plug"))
        {
            this.value = other.GetComponent<Plug>().value;
            Debug.Log("im colliding with " + other.GetComponent<Plug>().value);
        }

    }
    
    //al dejar de estar en contacto con el enchuve el valor se borra
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Plug"))
        {
            value = -1;
        }

    }


}

