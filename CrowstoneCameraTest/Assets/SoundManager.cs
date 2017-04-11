using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour {

    public AudioClip enterDoor;
    public float doorVolume = 1.0f;
    public AudioClip walking;
    public float walkingVolume = 1.0f;
    public float walkingPitch = 1.25f;
    public AudioClip running;
    public float runningVolume = 1.0f;
    public float RunningPitch = 2.0f;

    public AudioClip itemPickup;
    public float itemPickupVolume = 1.0f;

    private static SoundManager _instance;

    private AudioSource player;
    private float defaultPitch = 1.0f;

    public static SoundManager Instance { get { return _instance; } }

    // Use this for initialization
    void Start()
    {
        player = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!LevelManager.Instance.getScenename().Equals("Start Menu"))
        {
            if (GameManager.gameManager.getInUI() == false && GameManager.gameManager.getPause() == false)
            {

                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
                {
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        playRunning();
                    }
                    else
                    {
                        playWalking();
                    }
                }
                else
                {
                    player.Stop();
                }

                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    player.Stop();
                    playRunning();
                }

                if (Input.GetKeyUp(KeyCode.LeftShift))
                {
                    player.Stop();
                    playWalking();
                }
            }
        }
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

            player.Stop();
            player.pitch = defaultPitch;
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
            if (player.isPlaying) return;

            player.pitch = walkingPitch;
            Debug.Log("walking");
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
            if (player.isPlaying) return;

            player.pitch = RunningPitch;
            Debug.Log("running");
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
            player.Stop();
            player.pitch = defaultPitch;
            player.PlayOneShot(itemPickup, itemPickupVolume);

        }
        else
        {
            Debug.LogError("item pick up sound not set");
        }
    }
}
