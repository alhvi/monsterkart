using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    //Player selection variables
    public List<GameObject> characterList = new List<GameObject>();
    public int Plindex;
    public Text selectedName;

    //Stage selection variables
    public List<GameObject> stageList = new List<GameObject>();
    public int SIndex;

    //UI Variables
    public List<GameObject> Panels;
    public int pIndex;

    // Player and Stage to load
    public int SelectedPlayer1;
    public int SeletectedStage1;

    public void Start() {
        //Start with the tittle screen
        pIndex = 0;
        ActivatePanels();
    }

    public void SavePlayer() {
        PlayerPrefs.SetInt("P1", Plindex);
    }

    //Activate and deactivate windows
    public void toPSelect(int p) {
        pIndex = p;
        ActivatePanels();
        Button bt = GameObject.Find("C1").GetComponent<Button>();
        EventSystem es = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        es.SetSelectedGameObject(null);
        es.SetSelectedGameObject(bt.gameObject);
    }

    public void toMTittle(int p) {
        pIndex = p;
        ActivatePanels();
        Button bt = GameObject.Find("startBt").GetComponent<Button>();
        EventSystem es = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        es.SetSelectedGameObject(null);
        es.SetSelectedGameObject(bt.gameObject);
    }
    public void toSSelect(int p) {
        SavePlayer();
        pIndex = p;
        ActivatePanels();
        Button bt = GameObject.Find("t1").GetComponent<Button>();
        EventSystem es = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        es.SetSelectedGameObject(null);
        es.SetSelectedGameObject(bt.gameObject);
    }
    public void play() {
        SceneManager.LoadScene(1);
    }

    public void Track1() {
        SceneManager.LoadScene("Track1");
    }

    public void Track2() {
        SceneManager.LoadScene("Track2");
    }

    public void ActivatePanels() {
        foreach (GameObject p in Panels) {
            p.SetActive(false);
        }
        Panels[pIndex].SetActive(true);
    }
    //

    public void Awake() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }
    public void selectCharacter() {
        DisableAllCharacters();
        characterList[Plindex].SetActive(true);
    }

    private void DisableAllCharacters() {
        for (int i = 0; i < characterList.Count; i++) {
            characterList[i].SetActive(false);
        }
    }
}
