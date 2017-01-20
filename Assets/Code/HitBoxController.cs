using UnityEngine;
using System.Collections;

public class HitBoxController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseUp()
    {
        Debug.Log(gameObject.name);
        Camera.main.GetComponent<CameraController>().DisableColliders();
        Camera.main.GetComponent<CameraController>().MoveTo(transform, true);
    }
}
