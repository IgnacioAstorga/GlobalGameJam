﻿using UnityEngine;
using System.Collections;

public class LivesIndicator : MonoBehaviour {

    public NumberIndicatorController numberIndicator;

    public Color initColor = Color.green;

    public Color endColor = Color.red;

    private NumberIndicatorController[] arrayIndicators;

	// Use this for initialization
	void Start () {
        int lives = GameController.GetInstance().maxLives;

        arrayIndicators = new NumberIndicatorController[lives];

        for(int i = 0; i < lives; ++i)
        {
            arrayIndicators[i] = Instantiate(numberIndicator);
            arrayIndicators[i].Set(Color.Lerp(initColor, endColor, (float)i /((float)lives -1)), i + 1, false);
            arrayIndicators[i].transform.SetParent(transform, false);
            arrayIndicators[i].transform.localPosition = new Vector3(i + 0.5f - lives / 2.0f, 0, 0);
        }
	}


	public void Update ()
    {
        int lives = GameController.GetInstance().maxLives;

        for (int i = 0; i < lives; ++i)
        {
            arrayIndicators[i].SetSelected(lives - GameController.GetInstance().GetLives() == i);
        }
    }
}
