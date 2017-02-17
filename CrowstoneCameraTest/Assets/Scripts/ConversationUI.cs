using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ConversationUI : MonoBehaviour {

    Conversation conversation = new Conversation();
    List<Button> dialogueButtonList = new List<Button>();

    public Transform buttonPrefab;
    public string fileInput;
    public string startingLabel;

	void Start () {
        //conversation.ParseXML(fileInput);
        conversation.ParseFile(fileInput);
        Dictionary<string, string> dialogueOptions = conversation.ListDialogueConnections(startingLabel);
        dialogueButtonList = new List<Button>(dialogueOptions.Count);
        ClearDialogueButtonList();
        Transform content = gameObject.GetComponentInChildren<ScrollRect>().gameObject.GetComponentInChildren<Mask>().gameObject.GetComponentInChildren<VerticalLayoutGroup>().gameObject.transform;
        foreach (KeyValuePair<string,string> labelToDialogue in dialogueOptions)
        {
            var b = Instantiate(buttonPrefab);
            b.name = labelToDialogue.Key;
            Text text = b.gameObject.GetComponentInChildren<Text>();
            text.text = labelToDialogue.Value;
            b.transform.parent = content;
            Button button = b.GetComponent<Button>();
            button.onClick.AddListener(() => UpdateDialogueButtonList(b.name));
        }
        transform.Find("DialogueHeader").GetComponent<Text>().text = conversation.GetDialogueB(startingLabel);
    }
	
	void Update () {
		
	}

    private void ClearDialogueButtonList()
    {
        Transform content = gameObject.GetComponentInChildren<ScrollRect>().gameObject.GetComponentInChildren<Mask>().gameObject.GetComponentInChildren<VerticalLayoutGroup>().gameObject.transform;
        foreach (Transform child in content)
            Destroy(child.gameObject);
    }

    public void UpdateDialogueButtonList(string from)
    {
        if (!conversation.GetExitValue(from))
        {
            ClearDialogueButtonList();
            Dictionary<string, string> dialogueOptions = conversation.ListDialogueConnections(from);
            Transform content = gameObject.GetComponentInChildren<ScrollRect>().gameObject.GetComponentInChildren<Mask>().gameObject.GetComponentInChildren<VerticalLayoutGroup>().gameObject.transform;
            foreach (KeyValuePair<string, string> labelToDialogue in dialogueOptions)
            {
                var b = Instantiate(buttonPrefab);
                b.name = labelToDialogue.Key;
                Text text = b.gameObject.GetComponentInChildren<Text>();
                text.text = labelToDialogue.Value;
                b.transform.parent = content;
                Button button = b.GetComponent<Button>();
                button.onClick.AddListener(() => UpdateDialogueButtonList(b.name));
            }
            transform.Find("DialogueHeader").GetComponent<Text>().text = conversation.GetDialogueB(from);
        }
        else
        {
            try
            {
                GetComponentInParent<SwitchToBartender>().SwitchCamera();
                transform.parent.GetComponentInParent<BoxCollider>().enabled = true;
                Destroy(transform.parent.gameObject);
            }
            catch (Exception e)
            {
                {
                    GetComponentInParent<SwitchToGambler>().SwitchCamera();
                    transform.parent.GetComponentInParent<BoxCollider>().enabled = true;
                    Destroy(transform.parent.gameObject);
                }
            }
        }
    }
}
