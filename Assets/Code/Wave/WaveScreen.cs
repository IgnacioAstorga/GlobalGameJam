using UnityEngine;
using System.Collections.Generic;

public class WaveScreen : MonoBehaviour {

	public LineRenderer targetLineRenderer;
	public Wave[] targetWaves;

	public LineRenderer userLineRenderer;
	public Wave[] userWaves;

	public Regulator[] amplitudeRegulators;
	public Regulator[] frequencyRegulators;

	public int waveResolution = 100;

	public float speed = 1.0f;

	public float amplitudeTolerance = 0.05f * 0.5f;
	public float frequencyTolerance = 0.05f * 10.0f;

	[Header("Light")]
	public Renderer lightObject;
	private Light _light;

	public Color lightOnColor = Color.yellow;
	public Color lightOffColor = Color.red;

	public Vector2 dimensions = new Vector2(1.0f, 1.0f);

	private void Awake() {
		_light = lightObject.GetComponentInChildren<Light>();
	}

	private void Start() {
		targetLineRenderer.SetVertexCount(waveResolution);
		userLineRenderer.SetVertexCount(waveResolution);
	}

	private void Update() {
		for (int i = 0; i < userWaves.Length; i++) {
			userWaves[i].amplitude = amplitudeRegulators[i].GetOutput();
			userWaves[i].frequency = frequencyRegulators[i].GetOutput();
		}

		DrawWave(targetLineRenderer, targetWaves);
		DrawWave(userLineRenderer, userWaves);

		if (WavesFit())
			TurnLightOn();
		else
			TurnLightOff();
	}

	private void TurnLightOn() {
		lightObject.material.SetColor("_EmissionColor", lightOnColor);
		_light.color = lightOnColor;
		_light.enabled = true;
	}

	private void TurnLightOff() {
		lightObject.material.SetColor("_EmissionColor", lightOffColor);
		_light.color = lightOffColor;
		_light.enabled = false;
	}

	private void DrawWave(LineRenderer lineRenderer, params Wave[] waves) {
		foreach (Wave wave in waves)
			wave.phase += speed * Time.deltaTime;
		for (int i = 0; i < waveResolution; i++) {
			float distance = (float)i / waveResolution;
			Vector3 position = Vector3.zero;
			position.x = distance * dimensions.x - dimensions.x / 2.0f;
			position.y += AddWaves(distance, waves) * dimensions.y;
			lineRenderer.SetPosition(i, position);
		}
	}

	private float AddWaves(float x, params Wave[] waves) {
		float sum = 0;
		foreach (Wave wave in waves)
			sum += wave.EvaluateWaveFunction(x);
		return sum;
	}

	public bool WavesFit() {
		foreach (Wave targetWave in targetWaves) {
			bool fit = false;
			foreach (Wave userWave in userWaves) {
				if (WavesFitTogether(targetWave, userWave)) {
					fit = true;
					break;
				}
			}
			if (!fit)
				return false;
		}
		return true;
	}

	public bool WavesFitTogether(Wave waveA, Wave waveB) {
		return Mathf.Abs(waveA.amplitude - waveB.amplitude) < amplitudeTolerance
			&& Mathf.Abs(waveA.frequency - waveB.frequency) < frequencyTolerance;
	}
}