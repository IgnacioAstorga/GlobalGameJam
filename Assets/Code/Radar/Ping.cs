using UnityEngine;

public class Ping : MonoBehaviour {

	public Renderer model;

	public float duration = 0.5f;

	[HideInInspector]
	public int x;
	[HideInInspector]
	public int y;
	[HideInInspector]
	public float radius;

	[HideInInspector]
	public RadarScreen radar;

	private float _elapsedTime;

	private void Start() {
		_elapsedTime = 0.0f;
	}

	private void Update() {
		_elapsedTime += Time.deltaTime;
		if (_elapsedTime > duration)
			Destroy(gameObject);

		float timeFactor = _elapsedTime / duration;
		float pingRadius = radius * Mathf.Sqrt(timeFactor);
		radar.Ping(x, y, pingRadius);

		model.transform.localScale = 2.0f * pingRadius * Vector3.one;
		
		Color color = model.material.GetColor("_TintColor");
		color *= 1 - timeFactor * timeFactor * timeFactor * timeFactor;
		model.material.SetColor("_TintColor", color);
	}
}