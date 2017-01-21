using UnityEngine;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
	public RadarScreen radar;

	public WaveScreen wave;

    //public TurretScreen turret;

    public int maxLives = 5;

    private int lives;

    public GameObject title;

    public GameObject mainMenu;

    public GameObject pauseMenu;

    public GameObject HitBlocker;

    public CameraController cameraController;

    public Transform StartPosition;

    public Transform BackPosition;

    public float damageMagnitude;

    public float damageDuration;

    private static GameController instance = null;

    private bool isPaused = false;

    private bool hasStarted = false;

    private bool isInHelp = false;

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

		if (Input.GetMouseButtonDown(0))
			Damage(1);
    }

    public static GameController GetInstance()
    {

        return instance;
    }

    public void Pause()
    {

        Time.timeScale = 0;
        
        pauseMenu.SetActive(true);
        HitBlocker.SetActive(true);

        isPaused = !isPaused;
    }

    public void Continue()
    {

        Time.timeScale = 1;

        pauseMenu.SetActive(false);
        HitBlocker.SetActive(false);

        isPaused = !isPaused;
    }

    public void Restart()
    {

        Time.timeScale = 1;

        pauseMenu.SetActive(false);
        HitBlocker.SetActive(false);

        cameraController.MoveTo(BackPosition);

        cameraController.EnableColliders();

        isPaused = !isPaused;
    }

    public void Stop()
    {

        Continue();

        hasStarted = false;

        mainMenu.SetActive(true);

        HitBlocker.SetActive(true);

        cameraController.MoveTo(StartPosition);

        title.SetActive(true);
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

    public void setPaused(bool newState)
    {
        isPaused = newState;
    }

    public void setStarted(bool newState)
    {
        hasStarted = newState;
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

    public void GameOver()
    {

    }

    public int GetLives()
    {
        return lives;
    }
}

