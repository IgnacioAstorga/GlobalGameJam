using UnityEngine;

public class Square : MonoBehaviour {

	private int x;
	private int y;

	private RadarScreen radar;

	// TODO: BOMBA
	bool ultra;

	private void Update() {
		ultra = Input.GetKey(KeyCode.LeftShift);
	}

	private void OnMouseUp() {
		// Hace un ping en esta casilla
		radar.CreatePing(x, y, radar.pingRadius);

		if (ultra)
			radar.DestroyEnemiesAtPosition(x, y);
	}

	public void Initialize(RadarScreen radar, int x, int y) {
		this.radar = radar;
		this.x = x;
		this.y = y;
	}
}