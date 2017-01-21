using UnityEngine;
using System;

[Serializable]
public class Wave {
	public float amplitude;
	public float frequency;
	public float phase;

	public float EvaluateWaveFunction(float x) {
		return amplitude * Mathf.Sin(frequency * x + phase);
	}
}