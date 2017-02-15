using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        foreach(KeyValuePair<string,string> labelToDialogue in dialogueOptions)
        {
            var b = Instantiate(buttonPrefab);
            //b.tag = labelToDialogue.Key;
            b.name = labelToDialogue.Key;
            Text text = b.gameObject.GetComponentInChildren<Text>();
            text.text = labelToDialogue.Value;
            b.transform.parent = gameObject.transform;
            Button button = b.GetComponent<Button>();
            button.onClick.AddListener(() => UpdateDialogueButtonList(b.name));
        }
    }
	
	void Update () {
		
	}

    private void ClearDialogueButtonList()
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);
    }

    public void UpdateDialogueButtonList(string from)
    {
        ClearDialogueButtonList();
        Dictionary<string, string> dialogueOptions = conversation.ListDialogueConnections(from);
        foreach (KeyValuePair<string, string> labelToDialogue in dialogueOptions)
        {
            var b = Instantiate(buttonPrefab);
            //b.tag = labelToDialogue.Key;
            b.name = labelToDialogue.Key;
            Text text = b.gameObject.GetComponentInChildren<Text>();
            text.text = labelToDialogue.Value;
            b.transform.parent = gameObject.transform;
            Button button = b.GetComponent<Button>();
            button.onClick.AddListener(() => UpdateDialogueButtonList(b.name));
        }
    }
}
