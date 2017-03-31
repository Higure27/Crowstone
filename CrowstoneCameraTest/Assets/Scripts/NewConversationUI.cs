using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;

/**
 * Controls the dialogue prefab which allows users to traverse conversations.
 **/
public class NewConversationUI : MonoBehaviour {

    /**
     * A prefab which should be instantiated to dispaly the player's dialogue options. This can
     * be set in the inspector. Se eselfDialogueOptionsViewport for further info on how to use this.
     **/
    [SerializeField]
    private Transform dialogueButtonPrefab;

    /**
     * An object that gives access to the viewport that contains all of the player's dialogue options.
     * Dialogue options should be instantiated as children of this object using the
     * dialogueButtonPrefab object.
     **/
    private Transform selfDialogueOptionsViewport;

    /**
     * The text script which gives access to the partner's dialogue. Text can be changed by
     * simply changing this object's .text property.
     **/
    private Text partnerDialogue;

    /**
     * 
     **/
    public delegate void DialogueEvent(Choice dialogueChoice);
    public static event DialogueEvent dialogueChosen;

    // Use this for initialization
    void Awake () {
        selfDialogueOptionsViewport = transform.GetChild(2).GetChild(1).GetChild(0);
        partnerDialogue = transform.GetChild(1).GetComponent<Text>();
	}

    /**
     * Updates the dialogue state. Strings in "self" are set to the dialogue option buttons
     * and "partner" is set to the partner's dialogue
     **/
    public void UpdateDialogue(KeyValuePair<string, List<Choice>> kvp)
    {
        partnerDialogue.text = kvp.Key;
        ClearPlayerDialogueOptions();
        foreach (Choice dialogueOption in kvp.Value)
        {
            var b = Instantiate(dialogueButtonPrefab) as Transform;
            b.GetComponentInChildren<Text>().text = dialogueOption.text;
            b.transform.parent = selfDialogueOptionsViewport;
            Button button = b.GetComponent<Button>();
            button.onClick.AddListener(() => dialogueChosen(dialogueOption));
        }
    }

    void ClearPlayerDialogueOptions()
    {
        foreach (Transform child in selfDialogueOptionsViewport)
            Destroy(child.gameObject);
    }

    public void EndDialogue(string s)
    {
        ClearPlayerDialogueOptions();
        partnerDialogue.text = s;
        var b = Instantiate(dialogueButtonPrefab) as Transform;
        b.GetComponentInChildren<Text>().text = "*End Conversation*";
        b.transform.parent = selfDialogueOptionsViewport;
        Button button = b.GetComponent<Button>();
        button.onClick.AddListener(() => ActuallyEndDialogue());
        //DestroyObject(gameObject);
    }

    public void ActuallyEndDialogue()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponentInChildren<FirstPersonController>().enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        GameManager.gameManager.flipInUI();
        Destroy(gameObject);
    }
}
