using UnityEngine;

public class Square : MonoBehaviour {

	private int x;
	private int y;

	private Radar radar;

	private void OnMouseUp() {
		// Hace un ping en esta casilla
		radar.Ping(x, y, radar.pingRadius);
	}

	public void Initialize(Radar radar, int x, int y) {
		this.radar = radar;
		this.x = x;
		this.y = y;
	}
}