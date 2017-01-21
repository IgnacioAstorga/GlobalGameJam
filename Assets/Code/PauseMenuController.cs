using UnityEngine;
using System.Collections;

public class PauseMenuController : MonoBehaviour {

    private CameraController cameraController;

    // Use this for initialization
    void Start()
    {
        cameraController = Camera.main.transform.parent.GetComponent<CameraController>();
    }

    public void Pause()
    {
        GameController.GetInstance().Pause();
    }

    public void Continue()
    {
        GameController.GetInstance().Continue();
    }

    public void Restart()
    {
        GameController.GetInstance().Restart();
    }

    public void Exit()
    {
        GameController.GetInstance().Stop();

    }

    public void ChangeVolume()
    {

    }

}
