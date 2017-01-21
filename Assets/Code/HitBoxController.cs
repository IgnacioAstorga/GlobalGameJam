using UnityEngine;
using System.Collections;

public class HitBoxController : MonoBehaviour {

    public GameObject help;

    public GameObject mainHelp;

    private CameraController cameraController;

    // Use this for initialization
    void Start () {
        cameraController = Camera.main.transform.parent.GetComponent<CameraController>();

    }

    void OnMouseUp()
    {
        cameraController.DisableColliders();
        cameraController.MoveTo(transform);
        if (GameController.GetInstance().isInHelpMode())
        {
            help.SetActive(true);
            mainHelp.SetActive(false);
        }
    }
}
