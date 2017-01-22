using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour {

    public float splashWait = 2f;

    public float videoDuration = 10f;

    public float tittleDuration = 2f;

    public GameObject video;

    public GameObject tittleImage;

    public bool canSkip = false;

    public AudioSource audioSource;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(IntroTimerController());
        StartCoroutine(WaitingToSkip());

    }

    void Update()
    {
        if (canSkip && Input.GetMouseButtonUp(0))
        {
            StartCoroutine(SkipIntro());
        }
    }

    private IEnumerator IntroTimerController()
    {
        while (true)
        {
            yield return new WaitForSeconds(splashWait);

            video.SetActive(true);
            ((MovieTexture)video.GetComponent<RawImage>().texture).Play();
            audioSource.clip = ((MovieTexture)video.GetComponent<RawImage>().texture).audioClip;
            audioSource.Play();

            yield return new WaitForSeconds(videoDuration);

            tittleImage.SetActive(true);

            yield return new WaitForSeconds(tittleDuration);

            SceneManager.LoadScene("Main");
        }
    }

    private IEnumerator SkipIntro()
    {
        while (true)
        {
            ((MovieTexture)video.GetComponent<RawImage>().texture).Stop();

            tittleImage.SetActive(true);

            yield return new WaitForSeconds(tittleDuration);

            SceneManager.LoadScene("Main");
        }
    }

    private IEnumerator WaitingToSkip()
    {
        while (true)
        {
            yield return new WaitForSeconds(splashWait * 2);

            canSkip = true;
        }
    }
}
