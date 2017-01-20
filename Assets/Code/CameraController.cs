using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public Transform cameraStartPosition;

    public GameObject[] screens;

    public Transform backPostion;

    public float movementDuration = 1.0f;

    public float rotationSpeed = 1F;

    private float startTime;

    private Vector3 startPostion;

    private Vector3 endPostion;

    private Quaternion startRotation;

    private Quaternion endRotation;

    // Use this for initialization
    void Start ()
    {
        endPostion = transform.position = cameraStartPosition.transform.position;
        endRotation = transform.rotation = cameraStartPosition.transform.rotation;
        transform.rotation = cameraStartPosition.transform.rotation;

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Pressed right click.");
            EnableColliders();
            MoveTo(backPostion, false);
        }

        float t = (Time.time - startTime) / movementDuration;
        if (t <= 1f)
        {
            transform.position = SmoothStep(startPostion, endPostion, t);
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);
        }
    }

    public void MoveTo(Transform targetTransform, bool moveIn)
    {

        startTime = Time.time;
        if (moveIn)
        {
            endPostion = targetTransform.position;
            endRotation = targetTransform.rotation;
        }else
        {
            endPostion = backPostion.position;
            endRotation = backPostion.rotation;
        }
        startPostion = transform.position;
        startRotation = transform.rotation;
    }

    public void DisableColliders()
    {
        foreach ( GameObject screen in screens)
        {
            screen.SetActive(false);
        }
    }

    public void EnableColliders()
    {
        foreach (GameObject screen in screens)
        {
            screen.SetActive(true);
        }
    }

    public void ShakeCamera()
    {

    }

    public static Vector3 SmoothStep(Vector3 value1, Vector3 value2, float amount)
    {
        return new Vector3(
            Mathf.SmoothStep(value1.x, value2.x, amount),
            Mathf.SmoothStep(value1.y, value2.y, amount),
            Mathf.SmoothStep(value1.z, value2.z, amount));
    }

}
