using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    public GameObject title;
    public GameObject PressAnyKeyPanel;
    public GameObject MenuPanel;
    public float fadeSpeed = 1.0f;

    private bool fadingOutInProcess;
    private bool fadingInInProcess;
    private bool onTitleScreen; 

	// Use this for initialization
	void Start () {
        onTitleScreen = true;
        title.gameObject.SetActive(true);
        PressAnyKeyPanel.gameObject.SetActive(true);
        MenuPanel.gameObject.SetActive(false);

        fadingOutInProcess = false;
        fadingInInProcess = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (onTitleScreen)
        {
            if (Input.anyKey)
            {
                FadeToMenu();
                onTitleScreen = false;
            }
        }
    }

    public void FadeToMenu()
    {
        if (!fadingInInProcess && !fadingOutInProcess)
        {
            //fade out pressanykey screen
            Text pressKeyText = PressAnyKeyPanel.GetComponentInChildren<Text>();
            StartCoroutine(FadeOutText(pressKeyText));
            StartCoroutine(DeactivatePanel(PressAnyKeyPanel));

            //fade in menu
            MenuPanel.gameObject.SetActive(true);

            Text[] texts = MenuPanel.GetComponentsInChildren<Text>();

            foreach (Text txt in texts)
            {
                if (txt.name.Equals("Continue"))
                {
                    txt.GetComponent<Button>().interactable = false;
                }
                StartCoroutine(FadeInText(txt));
            }
        }
    }

    public void MainMenuStart()
    {
        if(!fadingInInProcess && !fadingOutInProcess)
        {
            //change menu fade speed to that of loading screen
            fadeSpeed = LevelManager.Instance.getLoadingFadeSpeed();

            //fade out menu and title
            Text titleText = title.GetComponent<Text>();
            StartCoroutine(FadeOutText(titleText));

            Text[] menuTexts = MenuPanel.GetComponentsInChildren<Text>();
            foreach (Text txt in menuTexts)
            {
                StartCoroutine(FadeOutText(txt));
            }

            //load level

            GameManager.gameManager.setCurrentDay(1);
            LevelManager.Instance.startLoadSpecificScene("Town");
        }
    }

    public void MainMenuContinue()
    {
        Debug.Log("Continue clicked");
    }

    public void MainMenuQuit()
    {
        if(!fadingInInProcess && !fadingOutInProcess)
        {
            Application.Quit();

        }
    }

    IEnumerator FadeOutText(Text text)
    {
        fadingOutInProcess = true;

        float alpha = text.color.a;
        while (text.color.a > 0)
        {
            alpha -= Time.deltaTime * fadeSpeed;
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
            yield return null;
        }

        fadingOutInProcess = false;

        yield return null;
    }

    IEnumerator FadeInText(Text text)
    {
        fadingInInProcess = true;

        float alpha = 0.0f;
        while (alpha < 1)
        {

            alpha += Time.deltaTime * fadeSpeed;
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
            alpha -= Time.deltaTime * fadeSpeed;
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
            alpha += Time.deltaTime * fadeSpeed;
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
            yield return null;
        }

        fadingInInProcess = false;

        yield return null;
    }

    IEnumerator DeactivatePanel(GameObject panel)
    {
        yield return new WaitForSeconds(1.0f / fadeSpeed);

        panel.gameObject.SetActive(false);
    }
}
