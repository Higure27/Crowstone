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
    public float loadingScreenFadeSpeed = 0.3f;
    public float changeAreaFadeSpeed = 2.0f;
    public float minSecondsOnLoadingScreen = 2.0f;

    private bool blackFadeInComplete;
    private bool blackFadeOutComplete;
    private bool loadingScreenIn;
    private string currentArea;
    private string previousArea;
    private bool loadTheTown;
    private bool loadArea;

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
        loadTheTown = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (loadTheTown)
        {
            StartCoroutine(loadTown());
            loadTheTown = false;

        }
        else if (loadArea)
        {
            StartCoroutine(EnterArea());
            loadArea = false;
        }
	}

    public void startLoadTown()
    {
        loadTheTown = true;
    }

    public float getLoadingFadeSpeed()
    {
        return loadingScreenFadeSpeed;
    }

    public void SwitchArea(string scenename)
    {
        //save previous scene index
        previousArea = SceneManager.GetActiveScene().name;
        currentArea = scenename;
        loadArea = true;
    }

    private IEnumerator EnterArea()
    {
        //fade in black
        if (blackFade != null)
        {
            blackFadeInComplete = false;
            blackFade.gameObject.SetActive(true);
            StartCoroutine(FadeBlackIn(changeAreaFadeSpeed));

            while (!blackFadeInComplete)
            {
                yield return null;
            }
        }
        else
        {
            Debug.Log("no black fade set");
        }

        //load level async
        AsyncOperation async = SceneManager.LoadSceneAsync(currentArea);

        while (!async.isDone)
        {
            yield return null;
        }

        //fade out black
        blackFadeOutComplete = false;
        StartCoroutine(FadeBlackOut(changeAreaFadeSpeed));

        while (!blackFadeOutComplete)
        {
            yield return null;
        }

        yield return null;
    }

    private IEnumerator loadTown()
    {
        //fade in black
        if(blackFade != null)
        {
            blackFadeInComplete = false;
            blackFade.gameObject.SetActive(true);
            StartCoroutine(FadeBlackIn(loadingScreenFadeSpeed));

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
        StartCoroutine(FadeBlackOut(loadingScreenFadeSpeed));

        while (!blackFadeOutComplete)
        {
            yield return null;
        }

        //wait min time
        yield return new WaitForSeconds(minSecondsOnLoadingScreen);

        //load level async
        AsyncOperation async = SceneManager.LoadSceneAsync("Town");

        while (!async.isDone)
        {
            yield return null;
        }

        StartCoroutine(FadeLoadingScreenOut());

        yield return null;
    }

    IEnumerator FadeBlackIn(float fade)
    {
        float alpha = 0.0f;
        while (alpha < 1)
        {

            blackFadeBackground.color = new Color(blackFadeBackground.color.r, blackFadeBackground.color.g, blackFadeBackground.color.b, alpha);
            alpha += Time.deltaTime * fade;

            yield return null;
        }
        blackFadeBackground.color = new Color(blackFadeBackground.color.r, blackFadeBackground.color.g, blackFadeBackground.color.b, 1);
        blackFadeInComplete = true;

        yield return null;
    }

    IEnumerator FadeBlackOut(float fade)
    {
        float alpha = blackFadeBackground.color.a;
        while (alpha > 0)
        {
            alpha -= Time.deltaTime * fade;
            blackFadeBackground.color = new Color(blackFadeBackground.color.r, blackFadeBackground.color.g, blackFadeBackground.color.b, alpha);
            yield return null;
        }
        blackFadeBackground.color = new Color(blackFadeBackground.color.r, blackFadeBackground.color.g, blackFadeBackground.color.b, 0);
        blackFadeOutComplete = true;

        yield return null;
    }

    IEnumerator FadeLoadingScreenOut()
    {
        float panelAlpha = loadingScreenBackground.color.a;
        float textAlpha = loadingScreenText.color.a;

        while (panelAlpha > 0 || textAlpha > 0)
        {
            panelAlpha -= Time.deltaTime * loadingScreenFadeSpeed;
            textAlpha -= Time.deltaTime * loadingScreenFadeSpeed;
            loadingScreenBackground.color = new Color(loadingScreenBackground.color.r, loadingScreenBackground.color.g, loadingScreenBackground.color.b, panelAlpha);
            loadingScreenText.color = new Color(loadingScreenText.color.r, loadingScreenText.color.g, loadingScreenText.color.b, textAlpha);
            yield return null;
        }
        loadingScreenBackground.color = new Color(loadingScreenBackground.color.r, loadingScreenBackground.color.g, loadingScreenBackground.color.b, 0);
        loadingScreenText.color = new Color(loadingScreenText.color.r, loadingScreenText.color.g, loadingScreenText.color.b, 0);

        yield return null;
    }

}
