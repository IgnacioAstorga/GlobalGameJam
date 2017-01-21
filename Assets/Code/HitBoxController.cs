using UnityEngine;
using System.Collections;

public class HitBoxController : MonoBehaviour {

    private CameraController cameraController;

    // Use this for initialization
    void Start () {
        cameraController = Camera.main.transform.parent.GetComponent<CameraController>();

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseUp()
    {
        Debug.Log(gameObject.name);
        cameraController.DisableColliders();
        cameraController.MoveTo(transform, true);
    }
}
