using UnityEngine;
using UnityEngine.UI;

public class LapComplete : MonoBehaviour {

    public GameObject LapCompleteTrig;
    public GameObject HalfLapTrig;

    public GameObject MinuteDisplay;
    public GameObject SecondDisplay;
    public GameObject MilliDisplay;

    public GameObject LapTimeBox;

    public GameObject lapCounter;
    public int lapsDone;
    public int laps;

    public GameObject finishUI;
    public GameObject levelUI;
    public GameObject levelvcam;
    public GameObject finishvcam;

    private void OnTriggerEnter(Collider player) {

        if (player.tag == "Player") {
            lapsDone += 1;
            Kart kartInfo = player.GetComponent<Kart>();
            kartInfo.Lap = lapsDone;

            LevelManager.Instance.SortKarts();

            if (lapsDone > laps) {
                GameManager.Instance.FinishGame();
            }

            if (LapTimeManager.SecondCount <= 9) {
                SecondDisplay.GetComponent<Text>().text = "0" + LapTimeManager.SecondCount + ".";
            } else {
                SecondDisplay.GetComponent<Text>().text = "" + LapTimeManager.SecondCount + ".";
            }

            if (LapTimeManager.MinuteCount <= 9) {
                MinuteDisplay.GetComponent<Text>().text = "0" + LapTimeManager.MinuteCount + ".";
            } else {
                MinuteDisplay.GetComponent<Text>().text = "" + LapTimeManager.MinuteCount + ".";
            }

            MilliDisplay.GetComponent<Text>().text = "" + LapTimeManager.MilliCount;

            LapTimeManager.MinuteCount = 0;
            LapTimeManager.SecondCount = 0;
            LapTimeManager.MilliCount = 0;
            lapCounter.GetComponent<Text>().text = "" + lapsDone;

            HalfLapTrig.SetActive(true);
            LapCompleteTrig.SetActive(false);
        }

    }
}