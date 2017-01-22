using UnityEngine;

public class Square : MonoBehaviour {

	private int x;
	private int y;

	private RadarScreen radar;

	private bool pingPong;
	private ParticleSystem system;

	private void Awake() {
		system = GetComponent<ParticleSystem>();
	}

	private void Update() {
		if (!pingPong)
			StopEffect();
		pingPong = false;
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
		system.Play();
		pingPong = true;
	}

	public void StopEffect() {
		system.Stop();
	}
}