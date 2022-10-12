using UnityEngine;
using UnityEngine.UI;

public class LapTimeManager : MonoBehaviour
{

    public static int MinuteCount;
    public static int SecondCount;
    public static float MilliCount;
    public static string MilliDisplay;

    public GameObject MinBox;
    public GameObject SecBox;
    public GameObject MilBox;



    void Update()
    {
        MilliCount += Time.deltaTime * 10;
        MilliDisplay = MilliCount.ToString("F0");
        MilBox.GetComponent<Text>().text = "" + MilliDisplay;

        if (MilliCount >= 10) { MilliCount = 0; SecondCount += 1; }
        if (SecondCount <= 9) { SecBox.GetComponent<Text>().text = "0" + SecondCount + "."; }
        else { SecBox.GetComponent<Text>().text = "" + SecondCount + "."; }
        if (SecondCount >= 60) { SecondCount = 0; MinuteCount += 1; }
        if (MinuteCount <= 9) { MinBox.GetComponent<Text>().text = "0" + MinuteCount + ":"; }
        else { MinBox.GetComponent<Text>().text = "" + MinuteCount + ":"; }
    }
}
