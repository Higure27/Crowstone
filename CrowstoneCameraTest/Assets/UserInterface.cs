using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour {

    public float displayTime = 2.0f;
    public float textFadeSpeed = 0.8f;
    public GameObject panel;
    public Text sceneNameText;

    private bool fadingOutInProcess;
    private bool fadingInInProcess;
    

	// Use this for initialization
	void Start () {
		if(panel != null)
        {
            panel.gameObject.SetActive(false);
        }
	}

    private void OnEnable()
    {
        LevelManager.onFadeInFinished += startDisplaySceneName;
    }

    private void OnDisable()
    {
        LevelManager.onFadeInFinished -= startDisplaySceneName;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void startDisplaySceneName(string name)
    {
        StartCoroutine(displaySceneName(name));
    }

    private IEnumerator displaySceneName(string name)
    {
        //set text
        panel.gameObject.SetActive(true);
        sceneNameText.text = name;

        //fade in text
        sceneNameText.color = new Color(sceneNameText.color.r, sceneNameText.color.g, sceneNameText.color.b, 0);
        StartCoroutine(FadeInText(sceneNameText));

        while (fadingInInProcess)
        {
            yield return null;
        }

        //wait given time
        yield return new WaitForSeconds(displayTime);

        //fade out text
        StartCoroutine(FadeOutText(sceneNameText));

        yield return null;
    }

    IEnumerator FadeOutText(Text text)
    {
        fadingOutInProcess = true;

        float alpha = text.color.a;
        while (text.color.a > 0)
        {
            alpha -= Time.deltaTime * textFadeSpeed;
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

            alpha += Time.deltaTime * textFadeSpeed;
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
            yield return null;
        }

        fadingInInProcess = false;
        yield return null;
    }
}
