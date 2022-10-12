using InControl;
using UnityEngine;

public class KeyboardInputProfile : UnityInputDeviceProfile {
    public KeyboardInputProfile() {
        Name = "Keyboard";
        Meta = "A keyboard and mouse combination for our Kart game";

        // This profile only works on desktops.
        SupportedPlatforms = new[]
        {
            "Windows",
            "Mac",
            "Linux"
        };

        Sensitivity = 1.0f;
        LowerDeadZone = 0.0f;
        UpperDeadZone = 1.0f;

        ButtonMappings = new[]
        {
            new InputControlMapping
            {
                Handle = "Accelerate",
                Target = InputControlType.Action1,
                Source = KeyCodeButton( KeyCode.W)
            },
            new InputControlMapping
            {
                Handle = "Brake",
                Target = InputControlType.Action2,
                Source = KeyCodeButton(KeyCode.S)
            },
            new InputControlMapping
            {
                Handle = "Change Camera",
                Target = InputControlType.Action3,
                Source = KeyCodeButton(KeyCode.LeftShift)
            },
            new InputControlMapping
            {
                Handle = "Pause",
                Target = InputControlType.Pause,
                Source = KeyCodeButton(KeyCode.Escape)
            }

            };

        AnalogMappings = new[]
        {
            new InputControlMapping
            {
                Handle = "Move X",
                Target = InputControlType.LeftStickX,
				// KeyCodeAxis splits the two KeyCodes over an axis. The first is negative, the second positive.
				Source = KeyCodeAxis( KeyCode.A, KeyCode.D )
            },
            new InputControlMapping
            {
                Handle = "Drift",
                Target = InputControlType.RightTrigger,
                Source = KeyCodeButton(KeyCode.Space)
            },
            new InputControlMapping
            {
                Handle = "Shoot",
                Target = InputControlType.LeftTrigger,
                Source = KeyCodeButton(KeyCode.E)
            }
        };
    }
}
