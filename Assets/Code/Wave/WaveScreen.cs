using UnityEngine;

public class WaveScreen : MonoBehaviour {

	public LineRenderer targetLineRenderer;
	public Wave[] targetWaves;

	public LineRenderer userLineRenderer;
	public Wave[] userWaves;

	public int waveResolution = 100;

	public Vector2 dimensions = new Vector2(1.0f, 1.0f);

	private void Start() {
		targetLineRenderer.SetVertexCount(waveResolution);
		userLineRenderer.SetVertexCount(waveResolution);
	}

	private void Update() {
		DrawWave(targetLineRenderer, targetWaves);
		DrawWave(userLineRenderer, userWaves);
	}

	private void DrawWave(LineRenderer lineRenderer, params Wave[] waves) {
		foreach (Wave wave in waves)
			wave.phase = Time.time;
		for (int i = 0; i < waveResolution; i++) {
			float distance = (float)i / waveResolution;
			Vector3 position = Vector3.zero;
			position.x = distance * dimensions.x - dimensions.x / 2.0f;
			foreach (Wave wave in waves)
				position.y += wave.EvaluateWaveFunction(distance) * dimensions.y;
			lineRenderer.SetPosition(i, position);
		}
	}
}