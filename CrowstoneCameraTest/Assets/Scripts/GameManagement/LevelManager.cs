using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

    private static LevelManager _instance;

    public static LevelManager Instance { get { return _instance; } }

    public delegate void newSceneLoaded();
    public static event newSceneLoaded onNewSceneLoaded;

    public delegate void fadeInFinished(string scenename);
    public static event fadeInFinished onFadeInFinished;

    public Canvas loadingScreen;
    public Canvas blackFade;
    public float loadingScreenFadeSpeed = 0.3f;
    public float changeAreaFadeSpeed = 2.0f;
    public float minSecondsOnLoadingScreen = 2.0f;

    private bool blackFadeInComplete;
    private bool blackFadeOutComplete;
    private bool loadingScreenIn;
    private bool loadingScreenOut;
    private string currentArea;
    private string previousArea;
    private bool loadScene;
    private bool loadArea;
    private string sceneToLoad;

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

        string scenename = SceneManager.GetActiveScene().name;

        //dont call this if it is start menu or town
        if (!scenename.Equals("Start Menu") && !scenename.Equals("Town"))
        {
            if (onNewSceneLoaded != null)
            {
                onNewSceneLoaded();
            }
        }

        loadingScreenBackground = loadingScreen.GetComponentInChildren<Image>();
        loadingScreenText = loadingScreen.GetComponentInChildren<Text>();
        blackFadeBackground = blackFade.GetComponentInChildren<Image>();

        blackFadeInComplete = false;
        blackFadeOutComplete = false;
        loadScene = false;
	}

    private void OnEnable()
    {
        onNewSceneLoaded += ActivateByDay;
        onNewSceneLoaded += DeactivatePickedupItems;
    }

    private void OnDisable()
    {
        onNewSceneLoaded -= ActivateByDay;
        onNewSceneLoaded -= DeactivatePickedupItems;
    }
	
	// Update is called once per frame
	void Update () {
        if (loadScene)
        {
            StartCoroutine(loadAScene());
            loadScene = false;

        }
        else if (loadArea)
        {
            StartCoroutine(EnterArea());
            loadArea = false;
        }
	}

    private void DeactivatePickedupItems()
    {
        string[] items = GameManager.gameManager.getAllItems();

        if (items == null) return;
        else
        {
            if(items != null){
                return;
            }

            if (items.Length > 1 && items[0].Equals("empty"))
            {
                return;
            }

            for (int i = 0; i < items.Length; i++)
            {
                                string nameOfObject = items[i];
                GameObject objectToDeactivate = GameObject.Find(nameOfObject);
                Debug.Log("object to deactivate is called : " + nameOfObject);
                if (objectToDeactivate != null)
                {
                    objectToDeactivate.gameObject.SetActive(false);
                }

            }
        }
    }

    private void ActivateByDay()
    {
        int dayAsInt = GameManager.gameManager.getCurrentDay();

        GameObject[] days = GameObject.FindGameObjectsWithTag("Day");

        foreach (GameObject day in days)
        {
            switch (dayAsInt)
            {
                case 1:
                    if (day.name.Equals("Day1")) day.gameObject.SetActive(true);
                    else day.gameObject.SetActive(false);
                    break;
                case 2:
                    if (day.name.Equals("Day2")) day.gameObject.SetActive(true);
                    else day.gameObject.SetActive(false);
                    break;
                case 3:
                    if (day.name.Equals("Day3")) day.gameObject.SetActive(true);
                    else day.gameObject.SetActive(false);
                    break;
                default:
                    GameManager.gameManager.setCurrentDay(1);
                    Debug.Log("Current day was reset to " + GameManager.gameManager.getCurrentDay());
                    ActivateByDay();
                    break;
            }
        }
    }

    public void startLoadSpecificScene(string name)
    {
        sceneToLoad = name;
        loadScene = true;
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

        if(onNewSceneLoaded != null)
        {
            onNewSceneLoaded();
        }

        //fade out black
        blackFadeOutComplete = false;
        StartCoroutine(FadeBlackOut(changeAreaFadeSpeed));

        while (!blackFadeOutComplete)
        {
            yield return null;
        }

        if (onFadeInFinished != null)
        {
            onFadeInFinished(SceneManager.GetActiveScene().name);
        }

        yield return null;
    }

    private IEnumerator loadAScene()
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
            loadingScreenOut = false;
            string wordToDisplay = "";
            if(sceneToLoad.Equals("Start Menu"))
            {
                wordToDisplay = "Loading";
            }
            else
            {
                wordToDisplay = GetDayStringFromInt(GameManager.gameManager.getCurrentDay());
            }

            StartCoroutine(UpdateLoadingText(wordToDisplay));
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
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneToLoad);

        while (!async.isDone)
        {
            yield return null;
        }

        StartCoroutine(FadeLoadingScreenOut());

        while (!loadingScreenOut)
        {
            yield return null;
        }

        if(onFadeInFinished != null)
        {
            onFadeInFinished(SceneManager.GetActiveScene().name);
        }

        yield return null;
    }

    private string GetDayStringFromInt(int day)
    {
        string dayText;
        switch (day)
        {
            case 1:
                dayText = "Day One";
                break;
            case 2:
                dayText = "Day Two";
                break;
            case 3:
                dayText = "Day Three";
                break;
            default:
                dayText = "No day";
                break;
        }

        return dayText;
    }

    IEnumerator UpdateLoadingText(string word)
    {
        float waitTime = 0.5f;
        while (!loadingScreenOut)
        {
            loadingScreenText.text = word;
            yield return new WaitForSeconds(waitTime);
            loadingScreenText.text = word + ".";
            yield return new WaitForSeconds(waitTime);
            loadingScreenText.text = word + "..";
            yield return new WaitForSeconds(waitTime);
            loadingScreenText.text = word + "...";
            yield return new WaitForSeconds(waitTime);

            yield return null;
        }

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

        loadingScreenOut = true;

        yield return null;
    }

}
