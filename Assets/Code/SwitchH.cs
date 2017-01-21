using UnityEngine;
using System.Collections.Generic;

public class SwitchH : MonoBehaviour
{
    //define el estado del interruptor. si esta conectado. puede ser true o false
    public bool switched;

    //valor que representa posicion vertical
    public int value;

    //por defecto esta apagado
    private void Start()
    {
        switched = false;
    }

    //en cada frame
    public void Update() {
        if (!switched)
        {
            Vector2 vec2 = new Vector2(Input.mousePosition.y, Input.mousePosition.z);
            this.transform.position = vec2;
        }
    }

    //al tocar cambia el estado a true o false
    private void OnMouseUp()
    {
        if (switched)
        {
            switched = false;
            gameObject.GetComponent<Renderer>().material.color = Color.red;

        }
        else
        {
            switched = true;
            gameObject.GetComponent<Renderer>().material.color = Color.green;

        }
        //Debug.Log("Tower "+value+" is " + switched);

    }
    //devuelve el valor del interruptor
    public int GetValue()
    {
        return value;
    }

}

