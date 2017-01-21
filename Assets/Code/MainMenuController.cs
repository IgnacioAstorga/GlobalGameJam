using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour {

    public GameObject title;

    public GameObject mainMenu;

    public GameObject mainLayout;

    public GameObject creditsLayout;

    public GameObject howToLayout;

    public GameObject hitLocker;

    public Transform backPosition;

    public Transform initPosition;

    private CameraController cameraController;

    // Use this for initialization
    void Start()
    {
        cameraController = Camera.main.transform.parent.GetComponent<CameraController>();
    }

    public void NewGame()
    {
        Debug.Log("NewGame");
        GameController.GetInstance().Play();
        mainMenu.SetActive(false);
        title.SetActive(false);
        cameraController.MoveTo(backPosition);
        hitLocker.SetActive(false);
    }

    public void HowTo()
    {
        howToLayout.SetActive(true);
        mainLayout.SetActive(false);
        hitLocker.SetActive(false);
        title.SetActive(false);
        cameraController.MoveTo(backPosition);
        GameController.GetInstance().setInHelp(true);
    }

    public void Credits()
    {
        title.SetActive(false);
        mainLayout.SetActive(false);
        creditsLayout.SetActive(true);
    }

    public void BackToMain()
    {
        title.SetActive(true);
        mainLayout.SetActive(true);
        creditsLayout.SetActive(false);
        howToLayout.SetActive(false);
        hitLocker.SetActive(true);
        cameraController.MoveTo(initPosition);
        GameController.GetInstance().setInHelp(false);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
