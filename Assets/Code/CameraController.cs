using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	public GameObject child;

	public GameObject[] screens;

	public Transform backPostion;

	public Transform initPosition;

	public float movementDuration = 1.0f;

	public GameObject lightChain;

	public ParticleSystem dustParticles;

	public float lightPhysicsFactor = 10;

	public GameObject text1;

	public GameObject text2;

	public GameObject text3;

	public GameObject mainText;

	private float startTime;

	private Vector3 startPosition;

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
		startPosition = endPostion = transform.position = initPosition.transform.position;
		startRotation = endRotation = transform.rotation = initPosition.transform.rotation;

		childTransform = child.GetComponent<Transform>();

		lightChainRB = lightChain.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButtonDown(1) && 
			!GameController.GetInstance().isGamePaused() &&
			GameController.GetInstance().hasGameStarted())
		{
			EnableColliders();
			MoveTo(backPostion);
		}

		if (Input.GetMouseButtonDown(1) && 
			GameController.GetInstance().isInHelpMode())
		{
			EnableColliders();
			MoveTo(backPostion);
			DisableTexts();
		}

		float t = (Time.time - startTime) / movementDuration;
		if (t <= 1f)
		{
			transform.position = SmoothStep(startPosition, endPostion, t);
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

			dustParticles.Play(true);
		}
		else
		{
			childTransform.localPosition = Vector3.zero;

			dustParticles.Stop(true);
		}
	}

	public void MoveTo(Transform targetTransform)
	{

		startTime = Time.time;

		endPostion = targetTransform.position;
		endRotation = targetTransform.rotation;

		startPosition = transform.position;
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

	public void DisableTexts()
	{
		text1.SetActive(false);
		text2.SetActive(false);
		text3.SetActive(false);
		mainText.SetActive(true);
	}

}
