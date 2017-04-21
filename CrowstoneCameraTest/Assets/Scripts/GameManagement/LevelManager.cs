//Created by Jared Shaw

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// The is a static class that remains through the whole game
/// manages all changing of scenes and level specific functions
/// </summary>
public class LevelManager : MonoBehaviour {

    private static LevelManager _instance;

    public static LevelManager Instance { get { return _instance; } }

    /// <summary>
    /// this is an event that is triggered the moment a scene is loaded
    /// </summary>
    public delegate void newSceneLoaded();
    public static event newSceneLoaded onNewSceneLoaded;

    public delegate void openingDoor();
    public static event openingDoor onOpeningDoor;

    /// <summary>
    /// this is an event that is triggered after a scene is loaded
    /// and the screen faded in is complete
    /// </summary>
    /// <param name="scenename"></param>
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
        //make sure there is only ever one of these 
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
        if (!scenename.Equals("Start Menu"))
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

    /// <summary>
    /// add functions to events
    /// </summary>
    private void OnEnable()
    {
        onNewSceneLoaded += ActivateByDay;
        onNewSceneLoaded += DeactivatePickedupItems;
    }

    /// <summary>
    /// removes functions from events
    /// </summary>
    private void OnDisable()
    {
        onNewSceneLoaded -= ActivateByDay;
        onNewSceneLoaded -= DeactivatePickedupItems;
    }
	
	// Update is called once per frame
	void Update () {

        //start loading the town from the start menu
        if (loadScene)
        {
            StartCoroutine(loadAScene());
            if (!GameManager.gameManager.getFirstHUDActive()) {
                GameManager.gameManager.setHUDActive();
                GameManager.gameManager.setFirstHUDActive();
            }
            loadScene = false;

        }
        //start loading any scene, saloon, jail, town, etc
        else if (loadArea)
        {
            StartCoroutine(EnterArea());
            GameManager.gameManager.setHUDActive();
            loadArea = false;
        }
	}

    /// <summary>
    /// looks through the game objects and deactivates
    /// any items that are already in inventory
    /// </summary>
    private void DeactivatePickedupItems()
    {

        string[] items = GameManager.gameManager.getAllItems();

        if (items == null) return;
        else
        {
            if (items.Length == 0) return;

            if (items.Length > 1 && items[0].Equals("empty"))
            {
                Debug.Log("No items in inventory");
                return;
            }
            else
            {
                for (int i = 0; i < items.Length; i++)
                {
                    string nameOfObject = items[i];
                    Debug.Log(nameOfObject + " was found in inventory");
                    GameObject objectToDeactivate = GameObject.Find(nameOfObject);
                    if (objectToDeactivate != null)
                    {
                        objectToDeactivate.gameObject.SetActive(false);
                    }

                }
            }
        }
    }

    /// <summary>
    /// looks through the game objects and disables the parent day objects
    /// depending on which day it is
    /// </summary>
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

    /// <summary>
    /// this is called primarily to load the town from the menu
    /// </summary>
    /// <param name="name"></param>
    public void startLoadSpecificScene(string name)
    {
        sceneToLoad = name;
        loadScene = true;
    }

    /// <summary>
    /// returns the speed of fading on the load screen
    /// </summary>
    /// <returns>float</returns>
    public float getLoadingFadeSpeed()
    {
        return loadingScreenFadeSpeed;
    }

    /// <summary>
    /// this is called primarily between scenes in game
    /// such as saloon->town->jail etc
    /// </summary>
    /// <param name="scenename"></param>
    public void SwitchArea(string scenename)
    {
        //save previous scene index
        previousArea = SceneManager.GetActiveScene().name;
        currentArea = scenename;
        loadArea = true;
    }

    public string getScenename()
    {
        Scene scene;
        scene = SceneManager.GetActiveScene();
        return scene.name;
    }

    /// <summary>
    /// a co-routine that runs in the background
    /// this fades in black, then loads the desired scene
    /// then fades the black out
    /// </summary>
    /// <returns></returns>
    private IEnumerator EnterArea()
    {
        //play sound
        onOpeningDoor();

        //activate and fade in black fade screen
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

        //wait for level to load
        while (!async.isDone)
        {
            yield return null;
        }

        //send off loaded scene event
        if(onNewSceneLoaded != null)
        {
            onNewSceneLoaded();
        }

        //fade out black screen
        blackFadeOutComplete = false;
        StartCoroutine(FadeBlackOut(changeAreaFadeSpeed));

        //wait for black screen to fade out
        while (!blackFadeOutComplete)
        {
            yield return null;
        }
        
        //fire off fade in screen event
        if (onFadeInFinished != null)
        {
            onFadeInFinished(SceneManager.GetActiveScene().name);
        }

        //make sure objects are disabled after use is complete
        if (loadingScreen != null)
        {
            loadingScreen.gameObject.SetActive(false);
        }

        if(blackFade != null)
        {
            blackFade.gameObject.SetActive(false);
        }

        yield return null;
    }

    /// <summary>
    /// This is a co-routine that runs in the background
    /// called to load the town from menu
    /// fades in black->activates loading screen->fades out black->loads level->fades out loading screen
    /// </summary>
    /// <returns></returns>
    private IEnumerator loadAScene()
    {
        //activate and fade in black screen
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

            //if you are loading the start menu then display "loading" otherwise display the current day
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
        //fade out black screen
        blackFadeOutComplete = false;
        StartCoroutine(FadeBlackOut(loadingScreenFadeSpeed));

        while (!blackFadeOutComplete)
        {
            yield return null;
        }

        //this is used to pause on the loading screen
        //if you take away this line you will not see the loading screen 
        //because the scenes load too quick
        yield return new WaitForSeconds(minSecondsOnLoadingScreen);

        //load level async
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneToLoad);

        //wait for level to load
        while (!async.isDone)
        {
            yield return null;
        }

        //send off loaded scene event
        if (onNewSceneLoaded != null)
        {
            onNewSceneLoaded();
        }

        //start fading out the loading screen using a co-routine
        StartCoroutine(FadeLoadingScreenOut());

        //wait for loading screen to completely fade out
        while (!loadingScreenOut)
        {
            yield return null;
        }

        //fire off fade in event
        if(onFadeInFinished != null)
        {
            onFadeInFinished(SceneManager.GetActiveScene().name);
        }

        //deactivate objects after use
        if(loadingScreen != null)
        {
            loadingScreen.gameObject.SetActive(false);

        }

        if(blackFade != null)
        {
            blackFade.gameObject.SetActive(false);
        }

        yield return null;
    }

    /// <summary>
    /// returns a string format of integer for current day
    /// </summary>
    /// <param name="day">int</param>
    /// <returns>string</returns>
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

    /// <summary>
    /// this is run as a co-routine in the background
    /// it basically just adds periods periodically to the end of the given word
    /// which should be "Loading" or "Day One" etc
    /// </summary>
    /// <param name="word"></param>
    /// <returns></returns>
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

    /// <summary>
    /// changes the alpha value of the black screen from 0 to 1 using the fade speed
    /// </summary>
    /// <param name="fade"></param>
    /// <returns></returns>
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

    /// <summary>
    /// changes the alpha value of the black screen from whatever it is to 0 using the fade speed
    /// </summary>
    /// <param name="fade"></param>
    /// <returns></returns>
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

    /// <summary>
    /// changes the alpha value of the loading screen from whatever it is to 0 using loading screen fade speed
    /// </summary>
    /// <returns></returns>
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
