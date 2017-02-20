using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

    private static LevelManager _instance;

    public static LevelManager Instance { get { return _instance; } }

    public Canvas loadingScreen;
    public Canvas blackFade;
    public float fadeSpeed = 0.3f;
    public float minSecondsOnLoadingScreen = 2.0f;

    private bool blackFadeInComplete;
    private bool blackFadeOutComplete;
    private bool loadingScreenIn;
    private int currentLevel;
    private bool loadFirst;
    private bool loadCurrent;
    private bool loadNext;

    private Image loadingScreenBackground;
    private Image blackFadeBackground;
    private Text loadingScreenText;

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

	// Use this for initialization
	void Start () {
        loadingScreenBackground = loadingScreen.GetComponentInChildren<Image>();
        loadingScreenText = loadingScreen.GetComponentInChildren<Text>();
        blackFadeBackground = blackFade.GetComponentInChildren<Image>();

        blackFadeInComplete = false;
        blackFadeOutComplete = false;
        loadFirst = false;
        loadCurrent = false;
        loadNext = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (loadFirst)
        {
            currentLevel = 1;
            StartCoroutine(loadLevel(currentLevel));
            loadFirst = false;

        }
        else if (loadCurrent)
        {
            StartCoroutine(loadLevel(currentLevel));
            loadCurrent = false;
        }
        else if (loadNext)
        {
            StartCoroutine(loadLevel(++currentLevel));
            loadNext = false;
        }
	}

    public void startLoadFirstLevel()
    {
        loadFirst = true;
    }

    public void startLoadCurrentLevel()
    {
        loadCurrent = true;
    }

    public void startLoadNextLevel()
    {
        loadNext = true;
    }

    public float getFadeSpeed()
    {
        return fadeSpeed;
    }

    private IEnumerator loadLevel(int index)
    {
        //fade in black
        if(blackFade != null)
        {
            blackFadeInComplete = false;
            blackFade.gameObject.SetActive(true);
            StartCoroutine(FadeBlackIn());

            while (!blackFadeInComplete)
            {
                yield return null;
            }
        }
        else
        {
            Debug.Log("no black fade set");
        }

        //activate loading screen
        if(loadingScreen != null)
        {
            loadingScreen.gameObject.SetActive(true);
            loadingScreenBackground.color = new Color(loadingScreenBackground.color.r,
                loadingScreenBackground.color.g, loadingScreenBackground.color.b, 1);
            loadingScreenText.color = new Color(loadingScreenText.color.r,
                loadingScreenText.color.g, loadingScreenText.color.b, 1);
        }
        else
        {
            Debug.Log("no loading screen set");
        }
        //fade out black
        blackFadeOutComplete = false;
        StartCoroutine(FadeBlackOut());

        while (!blackFadeOutComplete)
        {
            yield return null;
        }

        //wait min time
        yield return new WaitForSeconds(minSecondsOnLoadingScreen);

        //load level async
        AsyncOperation async = SceneManager.LoadSceneAsync(index);

        while (!async.isDone)
        {
            yield return null;
        }

        StartCoroutine(FadeLoadingScreenOut());

        yield return null;
    }

    IEnumerator FadeBlackIn()
    {
        float alpha = 0.0f;
        while (alpha < 1)
        {

            blackFadeBackground.color = new Color(blackFadeBackground.color.r, blackFadeBackground.color.g, blackFadeBackground.color.b, alpha);
            alpha += Time.deltaTime * fadeSpeed;

            yield return null;
        }
        blackFadeInComplete = true;

        yield return null;
    }

    IEnumerator FadeBlackOut()
    {
        float alpha = blackFadeBackground.color.a;
        while (alpha > 0)
        {
            alpha -= Time.deltaTime * fadeSpeed;
            blackFadeBackground.color = new Color(blackFadeBackground.color.r, blackFadeBackground.color.g, blackFadeBackground.color.b, alpha);
            yield return null;
        }
        blackFadeOutComplete = true;

        yield return null;
    }

    IEnumerator FadeLoadingScreenOut()
    {
        float panelAlpha = loadingScreenBackground.color.a;
        float textAlpha = loadingScreenText.color.a;

        while (panelAlpha > 0 || textAlpha > 0)
        {
            panelAlpha -= Time.deltaTime * fadeSpeed;
            textAlpha -= Time.deltaTime * fadeSpeed;
            loadingScreenBackground.color = new Color(loadingScreenBackground.color.r, loadingScreenBackground.color.g, loadingScreenBackground.color.b, panelAlpha);
            loadingScreenText.color = new Color(loadingScreenText.color.r, loadingScreenText.color.g, loadingScreenText.color.b, textAlpha);
            yield return null;
        }
        loadingScreenBackground.color = new Color(loadingScreenBackground.color.r, loadingScreenBackground.color.g, loadingScreenBackground.color.b, 0);
        loadingScreenText.color = new Color(loadingScreenText.color.r, loadingScreenText.color.g, loadingScreenText.color.b, 0);

        yield return null;
    }

}
