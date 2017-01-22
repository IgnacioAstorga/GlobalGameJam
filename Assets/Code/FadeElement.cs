using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Graphic))]
public class FadeElement : MonoBehaviour {

	private float duration;
	private float elapsedTime;
	private float targetAlpha;

	private Graphic _renderer;

	private void Awake() {
		_renderer = GetComponent<Graphic>();
	}

	private void Start() {
		FadeOut(0.0f);
	}

	private void Update() {
		elapsedTime += Time.deltaTime;
		Color color = _renderer.color;
		if (duration == 0)
			color.a = targetAlpha;
		else
			color.a = Mathf.Lerp(color.a, targetAlpha, elapsedTime / duration);
		_renderer.color = color;
	}

	public void FadeIn(float duration) {
		Fade(duration, 1.0f);
	}

	public void FadeOut(float duration) {
		Fade(duration, 0.0f);
	}

	public void Fade (float duration, float alpha) {
		this.duration = duration;
		elapsedTime = 0.0f;
		targetAlpha = alpha;
	}
}