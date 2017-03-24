using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class DayManager : MonoBehaviour {

    // Class members
    public TextAsset _inkAsset;
    public static Story _dayStory;
    private static string _partner;

	// Use this for initialization
	void Awake () {
        if(_inkAsset != null) _dayStory = new Story(_inkAsset.text);
        DontDestroyOnLoad(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public static KeyValuePair<string, List<Choice>> StartParsing(string partner)
    {
        _partner = partner;
        string partnerDialogue = "";
        List<Choice> outputList = new List<Choice>();

        _dayStory.ChoosePathString(partner);
        
        while (_dayStory.canContinue)
        {
            partnerDialogue += _dayStory.Continue().Trim();
        }
        Debug.Log("Partner: " + partnerDialogue);
        if (_dayStory.currentChoices.Count > 0)
        {
            for (int i = 0; i < _dayStory.currentChoices.Count; i++)
            {
                Choice choice = _dayStory.currentChoices[i];
                outputList.Add(choice);
            }
        }
        Debug.Log("Your choices: ");
        foreach (Choice choice in outputList)
            Debug.Log(choice.text + "\n");
        return new KeyValuePair<string, List<Choice>>(partnerDialogue, outputList);
    }

    public static KeyValuePair<string, List<Choice>> ContinueParsing(Choice c)
    {
        _dayStory.ChooseChoiceIndex(c.index);

        string partnerDialogue = "";
        List<Choice> outputList = new List<Choice>();

        while (_dayStory.canContinue)
        {
            partnerDialogue += _dayStory.Continue().Trim();
        }
        Debug.Log("Partner: " + partnerDialogue);
        if (_dayStory.currentChoices.Count > 0)
        {
            for (int i = 0; i < _dayStory.currentChoices.Count; i++)
            {
                Choice choice = _dayStory.currentChoices[i];
                outputList.Add(choice);
            }
        }
        else
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponentInChildren<FirstPersonController>().enabled = true;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            GameObject UI = GameObject.FindGameObjectWithTag("DialogueUI");
            DestroyObject(UI);
            GameManager.gameManager.flipInUI();
        }
        Debug.Log("Your choices: ");
        foreach (Choice choice in outputList)
            Debug.Log(choice.text + "\n");
        return new KeyValuePair<string, List<Choice>>(partnerDialogue, outputList);
    }
}
