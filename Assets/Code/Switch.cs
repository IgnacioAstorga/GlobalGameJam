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
    //ultimo valor correcto
    public int lastKnownValue;
    //ultima posicion correcta
    public Vector3 lastKnownPos;

    //por defecto esta apagado
    private void Start()
    {
        switched = false;
        value = -1;
        lastKnownValue = this.value;
        if (coord == radarCoord.HOR) {
            lastKnownPos = new Vector3(0, 0, 0);
            transform.position = lastKnownPos;
        }else
        {
            lastKnownPos = new Vector3(0, 0, 4);
            transform.position = lastKnownPos;
        }
        
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
        value = lastKnownValue;
        transform.position = lastKnownPos;
        if (value != -1)
        {
            switched = true;
        }
       
    }
    
    //devuelve el valor del interruptor
    public int GetValue()
    {
        return value;
    }

    //sigue al raton
    public void MouseMove() {
        Plane plane = new Plane(-transform.parent.forward, transform.parent.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float distance;
        if (plane.Raycast(ray, out distance)) {
            transform.position = ray.GetPoint(distance);
        }
		
    }

    //devuelve el valor del enchufe al colisionar con el
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Plug") )
        {
            if (other.GetComponent<Plug>().switched == false )
            {
                if (other.GetComponent<Plug>().coord.ToString().Equals(this.coord.ToString()) == true)
                {
                    other.GetComponent<Plug>().switched = true;
                    this.value = other.GetComponent<Plug>().value;
                    transform.position = other.transform.position;

                    lastKnownValue = other.GetComponent<Plug>().value;
                    lastKnownPos = other.transform.position;
                }
               
            }

        }

    }
    
    //al dejar de estar en contacto con el enchuve el valor se borra
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Plug"))
        {
            other.GetComponent<Plug>().switched = false;
            value = -1;
        }



    }


}

