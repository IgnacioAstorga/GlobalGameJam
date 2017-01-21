using UnityEngine;

public class Square : MonoBehaviour {

	private int x;
	private int y;

	private RadarScreen radar;

	private void OnMouseUp() {
		// Hace un ping en esta casilla
		radar.CreatePing(x, y, radar.pingRadius);
	}

	public void Initialize(RadarScreen radar, int x, int y) {
		this.radar = radar;
		this.x = x;
		this.y = y;
	}
}