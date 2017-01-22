using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour {

    public AudioMixer mixer;

    public Slider volumeSlider;

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
        mixer.SetFloat("Volume", volumeSlider.value);
    }

}
