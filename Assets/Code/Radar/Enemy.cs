using UnityEngine;

public class Enemy : MonoBehaviour {

	public float visibleTime = 10.0f;
	
	public float speed = 0.5f;

	public float fadeTime = 4.5f;

	private float timeToHide;

	private bool visible;

	private float alpha;

	private Transform _transform;

	private Renderer _renderer;

	private ParticleSystem _particleSystem;

	[HideInInspector]
	public Radar radar;

	private void Awake() {
		_transform = transform;
		_renderer = GetComponent<Renderer>();
		_particleSystem = GetComponent<ParticleSystem>();
	}

	private void Start() {
		Hide();
	}

	private void Update() {
		if (visible) {
			// Hace fade al objeto
			alpha -= Time.deltaTime / timeToHide;
			Color fadeColor = _renderer.material.color;
			fadeColor.a = alpha;
			_renderer.material.color = fadeColor;

			// Tras cierto tiempo, se oculta
			timeToHide -= Time.deltaTime;
			if (timeToHide < 0.0f)
				Hide();
		}

		// Mueve al enemigo hacia el centro (asume [0, 0])
		_transform.localPosition = Vector3.MoveTowards(_transform.localPosition, Vector3.zero, speed * Time.deltaTime);

		// Si está suficientemente cerca, hace daño
		if (_transform.localPosition.sqrMagnitude < radar.centerRadius * radar.centerRadius)
			radar.Damage(this, 1);
	}

	public void Show() {
		visible = true;
		timeToHide = visibleTime;
		_renderer.enabled = true;
		alpha = 1.0f;
	}

	public void Hide() {
		visible = false;
		timeToHide = -1.0f;
		_renderer.enabled = false;
	}

	public void HeartBeat() {
		if (visible) {
			alpha = 1.0f;
			_particleSystem.Emit(1);
		}
	}

	public bool IsVisble() {
		return visible;
	}

	public bool IsInSquare(int x, int y) {
		return x == Mathf.Floor(_transform.localPosition.x) && y == Mathf.Floor(_transform.localPosition.y);
	}
}