using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class DayManager : MonoBehaviour {

    // Class members
    public TextAsset _inkAsset;
    public static Story _dayStory;
    private static string _partner;

    [SerializeField]
    private Transform DialogueUI;
    private NewConversationUI dialogueUI;
    private bool dayLoaded = false;
    private int dayComplete;
    private int day = -1;

    public static DayManager dayManager; 


    // Use this for initialization
    void Awake () {
        if (dayManager == null) {
            if (_inkAsset != null) _dayStory = new Story(_inkAsset.text);
            DontDestroyOnLoad(gameObject);
            InteractWithNPC.dialogueStarted += StartDialogue;
            NewConversationUI.dialogueChosen += ContinueParsing;
            dayManager = this;
        }
    }

    private void Start() {
        
    }

    void OnDestroy()
    {
        InteractWithNPC.dialogueStarted -= StartDialogue;
        NewConversationUI.dialogueChosen -= ContinueParsing;
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    public void setDay() {
        if (GameManager.gameManager != null)
            day = GameManager.gameManager.getCurrentDay();
    }

    public void StartParsing(string partner)
    {
        _partner = partner;
        string partnerDialogue = "";
        List<Choice> outputList = new List<Choice>();

        _dayStory.ChoosePathString(partner);
        
        while (_dayStory.canContinue)
        {
            partnerDialogue += _dayStory.Continue();
        }
        //Debug.Log("Partner: " + partnerDialogue);
        if (_dayStory.currentChoices.Count > 0)
        {
            for (int i = 0; i < _dayStory.currentChoices.Count; i++)
            {
                Choice choice = _dayStory.currentChoices[i];
                outputList.Add(choice);
            }
            dialogueUI.UpdateDialogue(new KeyValuePair<string, List<Choice>>(partnerDialogue, outputList));
        }
        else
        {
            dialogueUI.EndDialogue(partnerDialogue);
        }
    }

    public void ContinueParsing(Choice c)
    {
        _dayStory.ChooseChoiceIndex(c.index);

        string partnerDialogue = "";
        //Debug.Log(_dayStory.Continue());
        List<Choice> outputList = new List<Choice>();

        while (_dayStory.canContinue)
        {
            partnerDialogue += _dayStory.Continue();
        }
        //Debug.Log("Partner: " + partnerDialogue);
        if (_dayStory.currentChoices.Count > 0)
        {
            for (int i = 0; i < _dayStory.currentChoices.Count; i++)
            {
                Choice choice = _dayStory.currentChoices[i];
                outputList.Add(choice);
            }
            dialogueUI.UpdateDialogue(new KeyValuePair<string, List<Choice>>(partnerDialogue, outputList));
        }
        else
        {
            dialogueUI.EndDialogue(partnerDialogue);
        }     
    }

    public void StartDialogue(string partner)
    {
        var dialogueTransform = Instantiate(DialogueUI);
        dialogueUI = dialogueTransform.GetComponent<NewConversationUI>();
        StartParsing(partner);
        GameManager.gameManager.flipInUI();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponentInChildren<FirstPersonController>().enabled = false;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        StartParsing(partner);
    }

    public static void ItemPickup(string item)
    {
        _dayStory.ChoosePathString(item);
        while (_dayStory.canContinue)
            _dayStory.Continue();
    }

    void changeDay() {
        int day = GameManager.gameManager.getCurrentDay() + 1;
        GameManager.gameManager.setCurrentDay(day);
        GameManager.gameManager.resetLocations();
        LevelManager.Instance.startLoadSpecificScene("Town");
        DayManager._dayStory.variablesState["day"] = day;
    }
}
