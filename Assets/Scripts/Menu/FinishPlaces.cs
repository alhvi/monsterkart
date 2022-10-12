using InControl;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishPlaces : MonoBehaviour {
    public List<Text> places;

    private void Update() {

        InputDevice inputDevice = InputManager.ActiveDevice;
        if (Input.anyKeyDown || inputDevice.AnyButton.HasChanged) {
            SceneManager.LoadScene("MainMenu");
        }
    }

}
