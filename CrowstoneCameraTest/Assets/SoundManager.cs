using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{

    public AudioClip enterDoor;
    public float doorVolume = 1.0f;
    public float doorPitch = 1.0f;
    public AudioClip walking;
    public float walkingVolume = 1.0f;
    public float walkingPitch = 1.25f;
    public AudioClip running;
    public float runningVolume = 1.0f;
    public float RunningPitch = 2.0f;
    public AudioClip menuClick;
    public float menuClickVol = 1.0f;
    public float menuClickPitch = 1.0f;
    public AudioClip menuStartClicked;
    public float menuStartClickedVol = 1.0f;
    public float menuStartClickedPitched = 1.0f;
    public AudioClip itemPickup;
    public float itemPickupVolume = 1.0f;
    public float itemPickupPitch = 1.0f;
    public AudioClip pauseMenuOnOff;
    public float pauseOnOffVol = 1.0f;
    public float pauseOnOffPitch = 1.0f;
    public AudioClip piano;
    public float pianoVol = 1.0f;
    public float pianoPitch = 1.0f;
    public AudioClip wind;
    public float windVol = 1.0f;
    public float windPitch = 1.0f;
    public AudioClip horse;
    public float horseVol = 1.0f;
    public float horsePitch = 1.0f;
    public AudioClip crow;
    public float crowVol = 1.0f;
    public float crowPitch = 1.0f;
    public AudioClip schoolAmb;
    public float schoolVol = 1.0f;
    public AudioClip bankAmb;
    public float bankVol = 1.0f;
    public Slider mainSlider;
    public int townAmbDelay = 5;
    private bool determining;

    private static SoundManager _instance;

    private float globalSoundVolume = 1.0f;

    private AudioSource fxPlayer;
    private AudioSource movementPlayer;
    private AudioSource ambiencePlayer;
    private AudioSource ambPlayer2;
    private float defaultPitch = 1.0f;
    private bool playHorse;

    public static SoundManager Instance { get { return _instance; } }

    private void Awake()
    {
        //make sure there is only ever one of these 
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    // Use this for initialization
    void Start()
    {
        AudioSource[] sources = GetComponents<AudioSource>();

        fxPlayer = sources[0];
        movementPlayer = sources[1];
        ambiencePlayer = sources[2];
        ambPlayer2 = sources[3];
        if (mainSlider != null)
        {
            mainSlider.onValueChanged.AddListener(delegate { updateGlobalSound(); });

        }

        playHorse = false;

        StartCoroutine(determineAnimalSound());
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("ambience is playing: " + ambiencePlayer.isPlaying);

        //walking and running
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
                    movementPlayer.Stop();
                }

                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    movementPlayer.Stop();
                    playRunning();
                }

                if (Input.GetKeyUp(KeyCode.LeftShift))
                {
                    movementPlayer.Stop();
                    playWalking();
                }
            }
        }

        //ambience 
        string scenename = LevelManager.Instance.getScenename();
        if (scenename.Equals("Town") || scenename.Equals("Start Menu"))
        {
            if (!ambiencePlayer.isPlaying)
            {
                if (wind != null)
                {
                    ambiencePlayer.PlayOneShot(wind, windVol * globalSoundVolume);

                }
            }

            if (!ambPlayer2.isPlaying)
            {
                if (!determining)
                {
                    StartCoroutine(determineAnimalSound());

                }
            }
        }
        else if (scenename.Equals("Jail"))
        {

        }
        else if (scenename.Equals("Saloon"))
        {
            if (!ambiencePlayer.isPlaying)
            {
                if (piano != null)
                {
                    ambiencePlayer.pitch = pianoPitch;
                    ambiencePlayer.PlayOneShot(piano, pianoVol * globalSoundVolume);
                }

            }
        }
        else if (scenename.Equals("BankInterior"))
        {
            if (!ambiencePlayer.isPlaying)
            {
                PlayBankAmb();
            }
        }
        else if (scenename.Equals("SchoolInterior"))
        {
            if (!ambiencePlayer.isPlaying)
            {
                PlaySchoolAmb();
            }
        }
    }

    public void changeSceneAmbience()
    {
        if(ambiencePlayer != null)
        {
            ambiencePlayer.Stop();

        }
        if (ambPlayer2 != null)
        {
            StopAllCoroutines();
            ambPlayer2.Stop();


        }
    }

    private void OnEnable()
    {
        LevelManager.onOpeningDoor += playEnterDoor;
        PickUpItem.onItemPickedUp += playItemPickup;
        LevelManager.onNewSceneLoaded += changeSceneAmbience;
    }

    private void OnDisable()
    {
        LevelManager.onOpeningDoor -= playEnterDoor;
        PickUpItem.onItemPickedUp -= playItemPickup;
        LevelManager.onNewSceneLoaded -= changeSceneAmbience;
    }

    public void playEnterDoor()
    {
        if (enterDoor != null)
        {
            fxPlayer.pitch = doorPitch;
            fxPlayer.PlayOneShot(enterDoor, doorVolume * globalSoundVolume);
        }
        else
        {
            Debug.LogError("enter door sound not set");
        }
    }

    public void playMenuClick()
    {
        if (menuClick != null)
        {
            fxPlayer.pitch = menuClickPitch;
            fxPlayer.PlayOneShot(menuClick, menuStartClickedVol * globalSoundVolume);
        }
    }

    public void playMenuStartClicked()
    {
        if (menuStartClicked != null)
        {
            fxPlayer.pitch = menuStartClickedPitched;
            fxPlayer.PlayOneShot(menuStartClicked, menuStartClickedVol * globalSoundVolume);
        }
    }

    public void playWalking()
    {
        if (walking != null)
        {
            if (movementPlayer.isPlaying) return;

            movementPlayer.pitch = walkingPitch;
            movementPlayer.PlayOneShot(walking, walkingVolume * globalSoundVolume);

        }
        else
        {
            Debug.LogError("walking sound not set");
        }
    }

    public void playRunning()
    {
        if (running != null)
        {
            if (movementPlayer.isPlaying) return;

            movementPlayer.pitch = RunningPitch;
            movementPlayer.PlayOneShot(running, runningVolume * globalSoundVolume);

        }
        else
        {
            Debug.LogError("running sound not set");
        }
    }

    public void updateGlobalSound()
    {
        if (mainSlider != null)
        {
            globalSoundVolume = mainSlider.value;

        }
    }

    public void playPauseOnOff()
    {
        if (pauseMenuOnOff != null)
        {
            fxPlayer.pitch = pauseOnOffPitch;
            fxPlayer.PlayOneShot(pauseMenuOnOff, pauseOnOffVol * globalSoundVolume);
        }
    }

    public void playItemPickup()
    {
        if (itemPickup != null)
        {
            fxPlayer.pitch = itemPickupPitch;
            fxPlayer.PlayOneShot(itemPickup, itemPickupVolume * globalSoundVolume);

        }
        else
        {
            Debug.LogError("item pick up sound not set");
        }
    }

    IEnumerator determineAnimalSound()
    {
        determining = true;

        if (playHorse)
        {
            yield return new WaitForSeconds(townAmbDelay);
            PlayHorse();

            playHorse = !playHorse;
        }
        else
        {
            yield return new WaitForSeconds(townAmbDelay);
            PlayCrow();

            playHorse = !playHorse;
        }
        determining = false;
        yield return null;
    }

    public void PlayHorse()
    {
        if (horse != null)
        {
            ambPlayer2.PlayOneShot(horse, horseVol * globalSoundVolume);
        }
    }

    public void PlayCrow()
    {
        if (crow != null)
        {
            ambPlayer2.PlayOneShot(crow, crowVol * globalSoundVolume);
        }
    }

    public void PlaySchoolAmb()
    {
        if (schoolAmb != null)
        {
            ambiencePlayer.PlayOneShot(schoolAmb, schoolVol * globalSoundVolume);
        }
    }

    public void PlayBankAmb()
    {
        if (bankAmb != null)
        {
            ambiencePlayer.PlayOneShot(bankAmb, bankVol * globalSoundVolume);
        }
    }
}

