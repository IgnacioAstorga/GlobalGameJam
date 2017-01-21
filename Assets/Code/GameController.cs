using UnityEngine;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    public int vidas = 5;

    public GameObject title;

    public GameObject mainMenu;

    public GameObject pauseMenu;

    public GameObject HitBlocker;

    public CameraController cameraController;

    public Transform StartPosition;

    public Transform BackPosition;

    private static GameController instance = null;

    private bool isPaused = false;

    private bool hasStarted = false;

    private bool isInHelp = false;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (hasStarted && Input.GetButtonDown("Cancel")) {

            if (!isPaused)
                Pause();
            else
                Continue();
        }
    }

    public static GameController GetInstance()
    {

        return instance;
    }

    public void Pause()
    {

        Debug.Log("Pause");

        Time.timeScale = 0;
        
        pauseMenu.SetActive(true);
        HitBlocker.SetActive(true);

        isPaused = !isPaused;
    }

    public void Continue()
    {

        Debug.Log("Continue");

        Time.timeScale = 1;

        pauseMenu.SetActive(false);
        HitBlocker.SetActive(false);

        isPaused = !isPaused;
    }

    public void Restart()
    {
        Debug.Log("Restart");

        Time.timeScale = 1;

        pauseMenu.SetActive(false);
        HitBlocker.SetActive(false);

        cameraController.MoveTo(BackPosition);

        cameraController.EnableColliders();

        isPaused = !isPaused;
    }

    public void Stop()
    {
        Debug.Log("Stop");

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
}

