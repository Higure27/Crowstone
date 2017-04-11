using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioClip enterDoor;
    public float doorVolume = 1.0f;
    public AudioClip walking;
    public float walkingVolume = 1.0f;
    public AudioClip running;
    public float runningVolume = 1.0f;
    public AudioClip itemPickup;
    public float itemPickupVolume = 1.0f;

    private static SoundManager _instance;

    private AudioSource player;

    public static SoundManager Instance { get { return _instance; } }

    // Use this for initialization
    void Start()
    {
        player = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        LevelManager.onOpeningDoor += playEnterDoor;
        PickUpItem.onItemPickedUp += playItemPickup;
    }

    private void OnDisable()
    {
        LevelManager.onOpeningDoor -= playEnterDoor;
        PickUpItem.onItemPickedUp -= playItemPickup;
    }

    public void playEnterDoor()
    {
        if(enterDoor != null)
        {
            player.PlayOneShot(enterDoor, doorVolume);

        }
        else
        {
            Debug.LogError("enter door sound not set");
        }
    }

    public void playWalking()
    {
        if(walking != null)
        {
            player.PlayOneShot(walking, walkingVolume);

        }
        else
        {
            Debug.LogError("walking sound not set");
        }
    }

    public void playRunning()
    {
        if(running != null)
        {
            player.PlayOneShot(running, runningVolume);

        }
        else
        {
            Debug.LogError("running sound not set");
        }
    }

    public void playItemPickup()
    {
        if(itemPickup != null)
        {
            player.PlayOneShot(itemPickup, itemPickupVolume);

        }
        else
        {
            Debug.LogError("item pick up sound not set");
        }
    }
}
