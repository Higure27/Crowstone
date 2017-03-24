using UnityEngine;
using System.Collections.Generic;
using Ink.Runtime;

public class InteractWithNPC : MonoBehaviour {

    Transform playerTransform;
    public GameObject UI;
    public string NPC;
    public float distanceToTrigger = 3.5f;

    [SerializeField]
    private Transform DialogueUI;

    private bool inRange;
    private GameObject player;
    private RaycastHit hit;

    // Use this for initialization
    void Start () {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        inRange = false;
        player = GameObject.FindGameObjectWithTag("Player");
        UI.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (DistanceBetweenThisAndPlayer() <= distanceToTrigger && !GameManager.gameManager.getPause() && !GameManager.gameManager.getInUI()) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100)) {
                UI.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E)) {
                    //if (Cursor.visible == false)
                   // {
                        //TODO: Fill With Conversation stuff
                        var dialogueTransform = Instantiate(DialogueUI);
                        NewConversationUI dialogueUI = dialogueTransform.GetComponent<NewConversationUI>();
                        KeyValuePair<string, List<Choice>> dialogues = DayManager.StartParsing(NPC);
                        dialogueUI.UpdateDialogue(dialogues);
                        GameManager.gameManager.flipInUI();
                        player.GetComponentInChildren<FirstPersonController>().enabled = false;

                        Cursor.visible = true;
                        Cursor.lockState = CursorLockMode.None;

                        UI.SetActive(false);
                    //}
                    //else
                    //{
                    //    GameObject player = GameObject.FindGameObjectWithTag("Player");
                    //    player.GetComponentInChildren<FirstPersonController>().enabled = true;
                    //    Cursor.visible = false;
                    //    Cursor.lockState = CursorLockMode.Locked;

                    //    GameObject UI = GameObject.FindGameObjectWithTag("DialogueUI");
                    //    DestroyObject(UI);
                    //}
                }
            }
            else {
                UI.SetActive(false);
            }
        }
        else {
            UI.SetActive(false);
        }
    }

    public static KeyValuePair<string, List<Choice>> UpdateDialogueUI(Choice c)
    {
        return DayManager.ContinueParsing(c);
    }

    public void EndDialogue()
    {
        DestroyObject(DialogueUI.gameObject);
    }

    private float DistanceBetweenThisAndPlayer() {
        float result = 0.0f;

        float x1 = transform.position.x;
        float y1 = transform.position.y;
        float z1 = transform.position.z;

        float x2 = playerTransform.position.x;
        float y2 = playerTransform.position.y;
        float z2 = playerTransform.position.z;

        //distance formula
        result = Mathf.Sqrt(Mathf.Pow((x1 - x2), 2) + Mathf.Pow((y1 - y2), 2) + Mathf.Pow((z1 - z2), 2));

        return result;
    }
}
