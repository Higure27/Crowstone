using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour {

    public float displayTime = 2.0f;
    public float textFadeSpeed = 0.8f;
    public GameObject gameUI;
    public GameObject pauseScreen;
    public Text sceneNameText;
    public float pauseScreenFadeSpeed = 0.8f;

    private bool fadingOutInProcess;
    private bool fadingInInProcess;
    

	// Use this for initialization
	void Start () {
		if(gameUI != null)
        {
            gameUI.gameObject.SetActive(false);
        }
        if(pauseScreen != null)
        {
            pauseScreen.gameObject.SetActive(false);
        }
	}

    private void OnEnable()
    {
        LevelManager.onFadeInFinished += startDisplaySceneName;
        InputManager.onPausePressed += pausePressed;
    }

    private void OnDisable()
    {
        LevelManager.onFadeInFinished -= startDisplaySceneName;
        InputManager.onPausePressed -= pausePressed;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void startDisplaySceneName(string name)
    {
        StartCoroutine(displaySceneName(name));
    }

    private void pausePressed()
    {
        if(!fadingOutInProcess && !fadingInInProcess)
        {
            if (pauseScreen.gameObject.activeSelf)
            {
                fadingOutInProcess = true;
                StartCoroutine(undisplayPauseScreen());
            }
            else
            {
                fadingInInProcess = true;
                StartCoroutine(displayPauseScreen());
            }
        }


    }

    public void PauseScreenResumeClicked()
    {
        if(!fadingOutInProcess && !fadingInInProcess)
        {
            fadingOutInProcess = true;
            StartCoroutine(undisplayPauseScreen());
        }
    }

    public void PauseScreenMainMenuClicked()
    {
        if (!fadingOutInProcess && !fadingInInProcess)
        {
            fadingOutInProcess = true;
            LevelManager.Instance.startLoadSpecificScene("Start Menu");
        }
    }

    public void PauseScreenQuit()
    {
        Application.Quit();
    }

    private IEnumerator displayPauseScreen()
    {
        //fade in screen
        fadingInInProcess = true;
        Image background = pauseScreen.GetComponentInChildren<Image>();
        Text[] texts = pauseScreen.GetComponentsInChildren<Text>();

        StartCoroutine(FadeInImage(background));
        foreach(Text txt in texts)
        {
            StartCoroutine(FadeInText(txt, pauseScreenFadeSpeed));
        }

        //stop movement of player and camera
        
        yield return null;
    }

    private IEnumerator undisplayPauseScreen()
    {
        //fade out screen
        fadingOutInProcess = true;
        Image background = pauseScreen.GetComponentInChildren<Image>();
        Text[] texts = pauseScreen.GetComponentsInChildren<Text>();

        StartCoroutine(FadeOutImage(background));
        foreach (Text txt in texts)
        {
            StartCoroutine(FadeOutText(txt, pauseScreenFadeSpeed));
        }

        //restart movement of player and camera

        yield return null;
    }

    private IEnumerator displaySceneName(string name)
    {
        //set text
        gameUI.gameObject.SetActive(true);
        sceneNameText.text = name;

        //fade in text
        sceneNameText.color = new Color(sceneNameText.color.r, sceneNameText.color.g, sceneNameText.color.b, 0);
        StartCoroutine(FadeInText(sceneNameText, textFadeSpeed));

        while (fadingInInProcess)
        {
            yield return null;
        }

        //wait given time
        yield return new WaitForSeconds(displayTime);

        //fade out text
        StartCoroutine(FadeOutText(sceneNameText, textFadeSpeed));

        yield return null;
    }

    IEnumerator FadeOutText(Text text, float speed)
    {
        fadingOutInProcess = true;

        float alpha = text.color.a;
        while (text.color.a > 0)
        {
            alpha -= Time.deltaTime * speed;
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
            yield return null;
        }

        fadingOutInProcess = false;

        yield return null;
    }

    IEnumerator FadeInText(Text text, float speed)
    {
        fadingInInProcess = true;

        float alpha = 0.0f;
        while (alpha < 1)
        {

            alpha += Time.deltaTime * speed;
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
            yield return null;
        }

        fadingInInProcess = false;
        yield return null;
    }

    IEnumerator FadeOutImage(Image image)
    {
        fadingOutInProcess = true;

        float alpha = image.color.a;
        while (image.color.a > 0)
        {
            alpha -= Time.deltaTime * pauseScreenFadeSpeed;
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
            yield return null;
        }

        fadingOutInProcess = false;

        yield return null;
    }

    IEnumerator FadeInImage(Image image)
    {
        fadingInInProcess = true;

        float alpha = 0.0f;
        while (alpha < 1)
        {
            alpha += Time.deltaTime * pauseScreenFadeSpeed;
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
            yield return null;
        }

        fadingInInProcess = false;

        yield return null;
    }
}
