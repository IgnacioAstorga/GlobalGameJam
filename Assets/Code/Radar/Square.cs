using UnityEngine;

public class Square : MonoBehaviour {

	public ParticleSystem system;
	public float delay = 1.0f;

	private int x;
	private int y;

	private float time;

	private RadarScreen radar;

	private bool pingPong;

	private void Update() {
		time -= Time.deltaTime;
	}

	private void OnMouseUp() {
		// Hace un ping en esta casilla
		radar.CreatePing(x, y, radar.pingRadius);
	}

	public void Initialize(RadarScreen radar, int x, int y) {
		this.radar = radar;
		this.x = x;
		this.y = y;
	}

	public void PlayEffect() {
		if (time <= 0.0f) {
			time = delay;
			system.Emit(1);
		}
	}
}