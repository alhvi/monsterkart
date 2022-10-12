using InControl;
using UnityEngine;

public class CameraSwitch : MonoBehaviour {
    //Funciona como retrovisor :) 

    public GameObject[] cameraList;
    private int currentCamera;

    private void Start() {
        currentCamera = 0;
        for (int i = 0; i < cameraList.Length; i++) {
        }

        if (cameraList.Length > 0) {
            cameraList[0].gameObject.SetActive(true);
        }
    }

    private void Update() {
        InputDevice inputDevice = InputManager.ActiveDevice;
        if (Input.GetKeyDown(KeyCode.LeftShift) || inputDevice.Action3.WasPressed) {
            currentCamera++;
            if (currentCamera < cameraList.Length) {
                cameraList[currentCamera - 1].gameObject.SetActive(false);
                cameraList[currentCamera].gameObject.SetActive(true);
            } else {
                //cameraList[currentCamera - 1].gameObject.SetActive(false);
                currentCamera = 0;
                cameraList[currentCamera].gameObject.SetActive(true);
            }
        } else if (Input.GetKeyUp(KeyCode.LeftShift) || inputDevice.Action3.WasReleased) {
            currentCamera++;
            if (currentCamera < cameraList.Length) {
                cameraList[currentCamera - 1].gameObject.SetActive(false);
                cameraList[currentCamera].gameObject.SetActive(true);
            } else {
                currentCamera = 0;
                cameraList[currentCamera].gameObject.SetActive(true);
            }
        }
    }
}