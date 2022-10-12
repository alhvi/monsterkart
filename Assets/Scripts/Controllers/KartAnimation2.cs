using UnityEngine;

public class KartAnimation2 : MonoBehaviour {
    private Animator anim;
    private KartInput kartInput;
    private Actions pastAction;

    public void Start() {
        kartInput = GetComponentInParent<KartInput>();
        anim = GetComponent<Animator>();
    }

    private void Update() {
        float horizontalMovement = kartInput.HorizontalMovement;

        //Forward animation
        if (kartInput.Action == Actions.ACCELERATING) {
            if (anim.GetInteger("Direction") == 0) {
                anim.SetInteger("Acceleration", 60);
            }
        } else {
            anim.SetInteger("Acceleration", 0);
        }

        //Turn animation
        if (horizontalMovement < 0) {
            anim.SetInteger("Direction", -30);
        } else if (horizontalMovement > 0) {
            anim.SetInteger("Direction", 30);
        } else {
            anim.SetInteger("Direction", 0);
        }

        //Drift animation
   //     if(kartInput.Action == Actions.DRIFTING)
    //    {
    //        anim.SetBool("Drift", true);
   //     }
  //      else
    //    {
    //        anim.SetBool("Drift", false);
   //     }

    }
}
