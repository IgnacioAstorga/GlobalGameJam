using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public GameObject child;

    public GameObject[] screens;

    public Transform backPostion;

    public float movementDuration = 1.0f;

    public GameObject lightChain;

    public float lightPhysicsFactor = 10;

    private float startTime;

    private Vector3 startPostion;

    private Vector3 endPostion;

    private Quaternion startRotation;

    private Quaternion endRotation;

    private float shakeTime;

    private float shakeMagnitude;

    private float magnitudeDecretion;

    private Transform childTransform;

    private Rigidbody lightChainRB;

    // Use this for initialization
    void Start ()
    {
        endPostion = transform.position = backPostion.transform.position;
        startRotation = endRotation = transform.rotation = backPostion.transform.rotation;

        childTransform = child.GetComponent<Transform>();

        lightChainRB = lightChain.GetComponent<Rigidbody>();
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
            ShakeCamera(0.05f, 2.0f);
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
            float cameraShakeAngle = Random.Range(0.0f, 2.0f * Mathf.PI);
            childTransform.localPosition = new Vector3(Mathf.Cos(cameraShakeAngle), Mathf.Sin(cameraShakeAngle), 0.0f) * shakeMagnitude;

            float lightShakeAngle = Random.Range(0.0f, 2.0f * Mathf.PI);
            lightChainRB.AddForce(new Vector3(Mathf.Cos(lightShakeAngle), 0, Mathf.Sin(lightShakeAngle)) * shakeMagnitude * lightPhysicsFactor);

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
