using UnityEngine;

public class WaveScreen : MonoBehaviour {

	public LineRenderer targetLineRenderer;
	public Wave[] targetWaves;

	public LineRenderer userLineRenderer;
	public Wave[] userWaves;

	public Regulator[] amplitudeRegulators;
	public Regulator[] frequencyRegulators;

	public int waveResolution = 100;

	public float speed = 1.0f;

	public float maxAmplitude = 0.5f;
	public float maxFrequency = 10.0f;
	public float toleranceFactor = 0.05f;

	public float minChangeTime = 30.0f;
	public float maxChangeTime = 60.0f;

	private float _nextTimeToChange;
	private float _elapsedTime;

	[Header("Light")]
	public Renderer lightObject;
	private Light _light;

	public Color lightOnColor = Color.yellow;
	public Color lightOffColor = Color.red;

	public Vector2 dimensions = new Vector2(1.0f, 1.0f);

	private bool _playing = false;

	public void Play() {
		_playing = true;
		ChangeWave();
	}

	public void Stop() {
		_playing = false;
	}

	private void Awake() {
		_light = lightObject.GetComponentInChildren<Light>();
	}

	private void Start() {
		targetLineRenderer.SetVertexCount(waveResolution);
		userLineRenderer.SetVertexCount(waveResolution);

		ChangeWave();
	}

	private void Update() {
		for (int i = 0; i < userWaves.Length; i++) {
			userWaves[i].amplitude = amplitudeRegulators[i].GetOutput();
			userWaves[i].frequency = frequencyRegulators[i].GetOutput();
		}

		DrawWave(targetLineRenderer, targetWaves);
		DrawWave(userLineRenderer, userWaves);

		if (WavesFit()) {
			TurnLightOn();
			GameController.GetInstance().radar.radarOn = true;

			if (_playing) {
				_elapsedTime += Time.deltaTime;
				if (_elapsedTime > _nextTimeToChange)
					ChangeWave();
			}
		}
		else {
			TurnLightOff();
			GameController.GetInstance().radar.radarOn = false;
		}
	}

	public void ChangeWave() {
		_elapsedTime = 0;
		_nextTimeToChange = Random.Range(minChangeTime, maxChangeTime);

		foreach (Wave wave in targetWaves) {
			wave.amplitude = Random.value * maxAmplitude;
			wave.frequency = Random.value * maxFrequency;
		}
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
		return Mathf.Abs(waveA.amplitude - waveB.amplitude) < maxAmplitude * toleranceFactor
			&& Mathf.Abs(waveA.frequency - waveB.frequency) < maxFrequency * toleranceFactor;
	}
}