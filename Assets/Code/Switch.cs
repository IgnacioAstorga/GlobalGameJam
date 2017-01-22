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
    
    //ultimo valor correcto
    //public int lastKnownValue;
    //ultima posicion correcta
    //public Vector3 lastKnownPos;
    //posicion a la que se va a mover si se suelta el boton
    //public Vector3 tempPos;
    //posicion a la que se va a mover si se suelta el boton
    //public int tempValue;

    //plug inicial
    public Plug initialPlug;
    //plug temporal
    private Plug tempPlug;
    //ultimo plug guardado
    private Plug lastKnownPlug;


    //por defecto esta apagado
    private void Start()
    {
        tempPlug = null;
        lastKnownPlug = initialPlug;
        tempPlug = initialPlug;
        switched = true;
        transform.position = initialPlug.transform.position;
        
        
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
        if (tempPlug != null)
        {
            switched = true;
            lastKnownPlug = tempPlug;
            tempPlug = null;
        }
        transform.position = lastKnownPlug.transform.position;
        switched = true;


    }




    //devuelve el valor del interruptor
    public int GetValue()
    {
        return lastKnownPlug.value;
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
                    tempPlug = other.GetComponent<Plug>();
                    other.GetComponent<Plug>().switched = true;
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
            tempPlug = null;
        }



    }


}

