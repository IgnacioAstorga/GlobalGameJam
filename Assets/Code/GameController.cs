using UnityEngine;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    public int maxLives = 5;

	public float gameOverDuration = 4.0f;
	public float gameOverFadeIn = 2.0f;
	public float gameOverFadeOut = 0.5f;

	public float damageMagnitude;

	public float damageDuration;

	public GameObject title;

    public GameObject mainMenu;

    public GameObject pauseMenu;

    public GameObject HitBlocker;

    public CameraController cameraController;

    public Transform StartPosition;

    public Transform BackPosition;
	
	public RadarScreen radar;

	public WaveScreen wave;

	//public TurretScreen turret;

	public FadeElement gameOverText;

	public FadeElement blackFade;

	private static GameController instance = null;

	private int lives;

	private bool isPaused = false;

    private bool hasStarted = false;

    private bool isInHelp = false;

	private bool gameOver = false;
	private float gameOverRemainingTime;

	void Awake()
    {
        instance = this;
        lives = maxLives;
    }

    void Update()
    {
        if (hasStarted && Input.GetButtonDown("Cancel")) {

            if (!isPaused)
                Pause();
            else
                Continue();
        }

		if (gameOver) {
			gameOverRemainingTime -= Time.deltaTime;
			if (gameOverRemainingTime < 0) {
				gameOver = false;
				gameOverText.FadeOut(gameOverFadeOut);
				blackFade.FadeOut(gameOverFadeOut);
				Stop(true);
			}
		}
    }

    public static GameController GetInstance()
    {

        return instance;
    }

    public void Pause() {
        Time.timeScale = 0;
        
        pauseMenu.SetActive(true);
        HitBlocker.SetActive(true);

        isPaused = true;
    }

    public void Continue() {
        Time.timeScale = 1;

        pauseMenu.SetActive(false);
        HitBlocker.SetActive(false);

        isPaused = false;
	}

	public void Play() {
		Continue();

		hasStarted = true;

		mainMenu.SetActive(false);
		HitBlocker.SetActive(false);
		title.SetActive(false);

		cameraController.MoveTo(BackPosition);
		cameraController.EnableColliders();

		lives = maxLives;

		radar.Play();
		wave.Play();
		turret.Play();
	}

	public void Stop(bool showMenu = true) {
		Continue();

		hasStarted = false;

		mainMenu.SetActive(showMenu);
		HitBlocker.SetActive(showMenu);
		title.SetActive(showMenu);

		if (showMenu) {
			cameraController.MoveTo(StartPosition);
			cameraController.DisableColliders();
		}

		radar.Stop();
		wave.Stop();
		turret.Stop();
	}

	public void Restart() {
		Stop();
		Play();
	}

	public void GameOver() {
		Stop(false);

		cameraController.ShakeCamera(maxLives * damageMagnitude, gameOverDuration);

		gameOverText.FadeIn(gameOverFadeIn);
		blackFade.FadeIn(gameOverFadeIn);

		gameOver = true;
		gameOverRemainingTime = gameOverDuration;
	}

	public bool isGamePaused()
    {
        return isPaused;
    }

    public bool hasGameStarted()
    {
        return hasStarted;
    }

    public bool isInHelpMode()
    {
        return isInHelp;
    }

    public void setInHelp(bool newState)
    {
        isInHelp = newState;
    }

    public void Damage(int damagePoints)
    {
        lives -= damagePoints;

		if (lives <= 0) {
			lives = 1;
			GameOver();
		}
        else
        {
            cameraController.ShakeCamera((maxLives - lives) * damageMagnitude, damageDuration);
		}
    }

    public int GetLives()
    {
        return lives;
    }
}

