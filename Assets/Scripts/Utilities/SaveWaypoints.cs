using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveWaypoints : MonoBehaviour {
    [SerializeField] private int numPoints = 40;
    [SerializeField] private float savingTime = 2f;
    [SerializeField] private int numLaps = 10;
    [SerializeField] private int carril = 1;
    private bool saving = false;

    private float currentTime = 5;
    private float laps = 0;
    private Vector3[] pointArray;
    private Vector3[] pointRotation;
    private int index = 0;

    private void Start() {
        pointArray = new Vector3[numPoints];
        pointRotation = new Vector3[numPoints];
        currentTime = savingTime * 2;

        for (int i = 0; i < numPoints; i++) {
            pointArray[i] = new Vector3(0, 0, 0);
            pointRotation[i] = new Vector3(0, 0, 0);
        }
    }

    // Update is called once per frame
    private void Update() {
        if (saving) {
            if (currentTime >= savingTime) {
                if (index < numPoints) {
                    pointArray[index] += transform.position;
                    pointRotation[index] += transform.eulerAngles;
                    index++;
                    currentTime = 0;
                }
            } else {
                currentTime += Time.deltaTime;
            }

        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("LapTrigger")) {
            if (laps < numLaps) {
                laps++;
                index = 0;
                //Hack para que guarde la posición en la meta
                currentTime = savingTime * 2;
                saving = true;
                Debug.Log("Saving lap " + laps);
            } else {
                saving = false;
                for (int i = 0; i < numPoints; i++) {
                    pointArray[i] /= laps;
                    pointRotation[i] /= laps;
                }
                SaveToDisk();
            }

        }
    }

    private void SaveToDisk() {
        string fileName = SceneManager.GetActiveScene().name + "_" + carril + ".txt";
        fileName = Application.persistentDataPath + "/" + fileName;
        StreamWriter fileHandler = File.CreateText(fileName);

        for (int i = 0; i < numPoints; i++) {
            fileHandler.WriteLine(pointArray[i].x + "," + pointArray[i].y + ", " + pointArray[i].z + " ; " + pointRotation[i].x + ", " + pointRotation[i].y + ", " + pointRotation[i].z);
        }

        fileHandler.Close();
        Debug.Log("File Saved");
    }
}
