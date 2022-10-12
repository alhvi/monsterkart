using InControl;
using UnityEngine;

public class KartInputHuman : KartInput {

    private float previouslyDrifting;
    private float driftingTime;
    private KartMovement kartMovement;
    private float maxDriftTime = 2.5f;

    private void Start() {
        previouslyDrifting = 0;
        driftingTime = 0;
        kartMovement = GetComponent<KartMovement>();
    }

    private void Update() {

        if (GameManager.Instance.raceStarted) {
            InputDevice inputDevice = InputManager.ActiveDevice;
            horizontalMovement = inputDevice.LeftStickX.Value;
            driftingInput = inputDevice.RightTrigger.Value;

            if (action != Actions.DRIFTING && action != Actions.BOOST) {
                if (inputDevice.Action1.WasPressed || inputDevice.Action1.IsPressed) {
                    action = Actions.ACCELERATING;
                }

                if (inputDevice.Action1.WasReleased) {
                    action = Actions.DECELERATING;
                }

                if (inputDevice.Action2.WasPressed) {
                    action = Actions.BRAKING;
                }

                if (inputDevice.Action2.WasReleased) {
                    action = Actions.DECELERATING;
                }
            }

            if (action != Actions.BOOST) {
                if (previouslyDrifting == 0 && driftingInput > 0 && horizontalMovement != 0) {
                    driftingTime = 0;
                    action = Actions.DRIFTING;
                    previouslyDrifting = driftingInput;
                } else if (previouslyDrifting > 0 && (driftingInput == 0 || horizontalMovement == 0)) {
                    action = Actions.BOOST;
                    float boostTime = Mathf.Lerp(0, 1, driftingTime / maxDriftTime);
                    action = Actions.BOOST;
                    kartMovement.StartBoost(boostTime);
                    previouslyDrifting = 0;
                } else if (driftingInput > 0 && horizontalMovement != 0) {
                    driftingTime += Time.deltaTime;
                    action = Actions.DRIFTING;
                    previouslyDrifting = driftingInput;
                }
            }
        }
    }

    public override void CancelDrifting() {
        action = Actions.DECELERATING;
        previouslyDrifting = 0;
        driftingTime = 0;
    }

}
