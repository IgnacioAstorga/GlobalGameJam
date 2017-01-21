using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public GameObject child;

    public GameObject[] screens;

    public Transform backPostion;

    public float movementDuration = 1.0f;

    private float startTime;

    private Vector3 startPostion;

    private Vector3 endPostion;

    private Quaternion startRotation;

    private Quaternion endRotation;

    private float shakeTime;

    private float shakeMagnitude;

    private float magnitudeDecretion;

    private Transform childTransform;

    // Use this for initialization
    void Start ()
    {
        endPostion = transform.position = backPostion.transform.position;
        startRotation = endRotation = transform.rotation = backPostion.transform.rotation;

        childTransform = child.GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Pressed right click.");
            EnableColliders();
            MoveTo(backPostion, false);
        }
        if (Input.GetButtonDown("Jump"))
        {
            ShakeCamera(.5f,1);
        }

        float t = (Time.time - startTime) / movementDuration;
        if (t <= 1f)
        {
            transform.position = SmoothStep(startPostion, endPostion, t);
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);
        }
        
        if(shakeTime > 0)
        {
            shakeTime -= Time.deltaTime;
            float spawnAngle = Random.Range(0.0f, 2.0f * Mathf.PI);
            childTransform.localPosition = new Vector3(Mathf.Cos(spawnAngle), Mathf.Sin(spawnAngle), 0.0f) * shakeMagnitude;
            shakeMagnitude -= magnitudeDecretion * Time.deltaTime;
        }
        else
        {
            childTransform.localPosition = Vector3.zero;
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

    public void ShakeCamera(float magnitude, float duration)
    {
        Debug.Log("Shake");
        shakeTime = duration;
        shakeMagnitude = magnitude;
        magnitudeDecretion = magnitude / duration;
    }

    public static Vector3 SmoothStep(Vector3 value1, Vector3 value2, float amount)
    {
        return new Vector3(
            Mathf.SmoothStep(value1.x, value2.x, amount),
            Mathf.SmoothStep(value1.y, value2.y, amount),
            Mathf.SmoothStep(value1.z, value2.z, amount));
    }

}
