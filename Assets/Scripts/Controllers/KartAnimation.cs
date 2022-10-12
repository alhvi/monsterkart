using UnityEngine;

public class KartAnimation : MonoBehaviour {
    public Animator anim;

    private void Update() {
        float horizontalMovement = Input.GetAxis("Horizontal");

        //Forward animation
        if (Input.GetButtonDown("Accelerate")) {
            if (anim.GetInteger("Direction") == 0) {
                anim.SetInteger("Acceleration", 60);
            }
        }
        //Idle animation
        if (Input.GetButtonUp("Accelerate")) {
            anim.SetInteger("Acceleration", 0);
        }

        if (Input.GetButtonDown("Brake")) {
            anim.SetInteger("Acceleration", 0);
        }

        if (Input.GetButtonUp("Brake")) {
            anim.SetInteger("Acceleration", 0);
        }

        //Turn animation
        //Left
        if (horizontalMovement < 0)
        {
            anim.SetInteger("Direction", -30);
        }
        if (horizontalMovement == 0)
        {
            anim.SetInteger("Direction", 0);
        }

        //Right
        if (horizontalMovement > 0)
        {
            anim.SetInteger("Direction", 30);
        }
        if (horizontalMovement == 0)
        {
            anim.SetInteger("Direction", 0);
        }
    }
}
