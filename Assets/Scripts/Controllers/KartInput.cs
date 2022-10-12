using UnityEngine;

public class KartInput : MonoBehaviour {
    protected float horizontalMovement;
    protected float driftingInput;
    protected Actions action;

    public float HorizontalMovement => horizontalMovement;
    public Actions Action => action;
    public float DriftingInput => driftingInput;

    public void RemoveBoost() {
        action = Actions.DECELERATING;
    }

    public void StartBoost() {
        action = Actions.BOOST;
    }

    public virtual void CancelDrifting() { }

}
