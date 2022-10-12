using System.IO;
using UnityEditor;
using UnityEngine;

public class MenuItemExamplesScreenshots : MonoBehaviour {
    [MenuItem("My Tools/Screenshots/Take screenshot 1x %#&1")]
    public static void TakeScreenShot1x() {
        ScreenCapture.CaptureScreenshot(Path.Combine(Application.persistentDataPath, "Screenshot_" + System.DateTime.Now.ToString().Replace('/', '_').Replace(":", "_").Replace(" ", "_") + ".png"));
        Debug.Log("Screenshot 1x saved to " + Application.persistentDataPath);
    }

    [MenuItem("My Tools/Screenshots/Take screenshot 2x %#&2")]
    public static void TakeScreenShot2x() {
        ScreenCapture.CaptureScreenshot(Path.Combine(Application.persistentDataPath, "Screenshot_" + System.DateTime.Now.ToString().Replace('/', '_').Replace(":", "_").Replace(" ", "_") + ".png"), 2);
        Debug.Log("Screenshot 2x saved to " + Application.persistentDataPath);
    }
}
