using UnityEngine;

public class Enemy : MonoBehaviour {

	public float visibleTime = 10.0f;

	private float timeToHide;

	private bool visible;

	private void Start() {
		Hide();
	}

	private void Update() {
		if (visible) {
			// Tras cierto tiempo, se oculta
			timeToHide -= Time.deltaTime;
			if (timeToHide < 0.0f)
				Hide();
		}
	}

	public void Show() {
		visible = true;
		timeToHide = visibleTime;
		gameObject.SetActive(true);
	}

	public void Hide() {
		visible = false;
		timeToHide = -1.0f;
		gameObject.SetActive(false);
	}

	public bool IsVisble() {
		return visible;
	}
}