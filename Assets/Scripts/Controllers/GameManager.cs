using Cinemachine;
using InControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour {
    //Load selected character
    public List<GameObject> characterList = new List<GameObject>();

    //Kart variables
    public GameObject P1;

    //Intro variables
    public GameObject levelUI;
    public GameObject pauseUI;
    public GameObject introUI;
    public GameObject finishUI;

    public PlayableDirector tl;
    public GameObject mainvcam;
    public GameObject introvcam;
    public float introTime;

    // Count down variables
    public AudioSource one;
    public AudioSource two;
    public AudioSource three;
    public AudioSource GoAudio;
    public AudioSource BGM;
    public LapTimeManager LapTimer;
    public GameObject CDBox;
    public Animator cd;
    public bool enableCountdown = true;
    public bool raceStarted = false;
    public bool raceFinished = false;
    public bool paused = false;

    public float time = 1;
    public float ltime = 1;

    public bool recordWaypoints = false;
    public FinishPlaces finishPlaces;
    public KartData kartData;
    public CinemachineVirtualCamera[] cameras;

    private static GameManager instance;

    public static GameManager Instance => instance;

    private void Awake() {
        instance = this;
        raceStarted = false;
        LapTimer.GetComponent<LapTimeManager>().enabled = false;
        CDBox.SetActive(false);
        BGM.Stop();
        PlayerPrefs.GetInt("P1", 0);
        loadP1();
        introUI.SetActive(true);
        levelUI.SetActive(false);
    }

    private void Start() {
        tl.Play();
        if (enableCountdown) {
            StartCoroutine(CountStart());
        } else {
            raceStarted = true;
        }
    }

    private IEnumerator CountStart() {

        yield return new WaitForSeconds(introTime);
        levelUI.SetActive(true);
        introUI.SetActive(false);
        mainvcam.SetActive(true);
        introvcam.SetActive(false);

        CDBox.SetActive(true);
        cd.Play("CountDown");
        yield return new WaitForSeconds(.9f);
        if (one != null) {
            one.Play();
        }

        yield return new WaitForSeconds(0.9f);
        if (two != null) {
            two.Play();
        }

        yield return new WaitForSeconds(1.2f);
        if (three != null) {
            three.Play();
        }

        yield return new WaitForSeconds(1.9f);
        GoAudio.Play();
        BGM.Play();
        LapTimer.enabled = true;
        raceStarted = true;

    }

    public void loadP1() {
        DisableAllCharacters();
        characterList[PlayerPrefs.GetInt("P1")].SetActive(true);
    }

    private void DisableAllCharacters() {
        for (int i = 0; i < characterList.Count; i++) {
            characterList[i].SetActive(false);
        }
    }

    private void Update() {
        InputDevice inputDevice = InputManager.ActiveDevice;
        if (!raceFinished && (Input.GetKeyDown(KeyCode.Escape) || inputDevice.MenuWasPressed)) {
            if (paused) {
                paused = false;
                Time.timeScale = 1;
                //levelUI.SetActive(true);
                pauseUI.SetActive(false);

            } else {
                paused = true;
                Time.timeScale = 0;
                //levelUI.SetActive(false);
                pauseUI.SetActive(true);
            }

        }
    }

    public void FinishGame() {
        if (!recordWaypoints) {
            Kart[] winners = new Kart[6];
            LevelManager.Instance.KartList.CopyTo(winners);
            raceFinished = true;

            for (int i = 0; i < 6; i++) {
                string playerName;

                if (winners[5 - i].KartType == KartType.HUMAN) {
                    playerName = "Player 1";
                } else {
                    playerName = kartData.kartNames[winners[5 - i].KartModelId];
                }

                finishPlaces.places[i].text = playerName;
            }
            StartCoroutine(ActivateCameraEnding(winners[5].gameObject));
        }
    }

    public IEnumerator ActivateCameraEnding(GameObject winner) {
        cameras[0].gameObject.SetActive(false);
        cameras[1].gameObject.SetActive(true);

        // check if human
        Kart kartInfo = winner.GetComponent<Kart>();
        if (kartInfo.KartType == KartType.HUMAN) {
            KartMovement movement = winner.GetComponent<KartMovement>();
            movement.ChangeInput();
        }

        cameras[1].Follow = winner.transform;
        cameras[1].LookAt = winner.transform;

        yield return new WaitForSeconds(4f);
        cameras[1].gameObject.SetActive(false);
        cameras[2].gameObject.SetActive(true);

        finishUI.SetActive(true);
        levelUI.SetActive(false);
    }

}
