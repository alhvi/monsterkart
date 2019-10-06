using UnityEngine;

public enum Actions { NONE, ACCELERATING, DECELERATING, BRAKING }

public class KartMovement : MonoBehaviour {

    [SerializeField] private float maxSpeed = 50f;
    [SerializeField] private float acceleration = 50f;
    [SerializeField] private float deceleration = 20f;
    [SerializeField] private float backwardsAcceleration = 30f;
    [SerializeField] private float minRotationSpeed = 20f;
    [SerializeField] private float maxRotationSpeed = 100f;
    private Rigidbody rb;
    private float horizontalMovement;
    private Actions action;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        action = Actions.NONE;
    }

    private void Update() {

        horizontalMovement = Input.GetAxis("Horizontal");


        if (Input.GetButtonDown("Accelerate")) {
            action = Actions.ACCELERATING;
        }

        if (Input.GetButtonUp("Accelerate")) {
            action = Actions.DECELERATING;
        }

        if (Input.GetButtonDown("Brake")) {
            action = Actions.BRAKING;
        }

        if (Input.GetButtonUp("Brake")) {
            action = Actions.DECELERATING;
        }

    }

    private void FixedUpdate() {
        Vector3 velocity = Utils.RemoveYFromVector(rb.velocity);
        Vector3 forward = Utils.RemoveYFromVector(transform.forward);



        if (Mathf.Abs(horizontalMovement) > 0.1f) {

            float distance = Vector3.Distance(forward.normalized, velocity.normalized);
            float direction = distance < 1.2 || velocity.magnitude < 0.05 ? 1 : -1;
            float factor = Mathf.Lerp(minRotationSpeed, maxRotationSpeed, 1 - velocity.magnitude / maxSpeed);

            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x,
                transform.localEulerAngles.y + (horizontalMovement * factor * direction * Time.fixedDeltaTime),
                transform.localEulerAngles.z);



            forward = Utils.RemoveYFromVector(transform.forward);
            Vector3 newVelocity = forward.normalized * velocity.magnitude * direction;
            rb.velocity = new Vector3(newVelocity.x, rb.velocity.y, newVelocity.z);


            //Updating vectors
            velocity = Utils.RemoveYFromVector(rb.velocity);
            forward = Utils.RemoveYFromVector(transform.forward);

        }


        switch (action) {
            case Actions.ACCELERATING:
                if (velocity.magnitude < maxSpeed) {
                    rb.velocity += acceleration * Time.fixedDeltaTime * forward;
                }
                break;
            case Actions.DECELERATING:
                if (velocity.magnitude > 0f) {
                    Vector3 newVelocity = velocity.normalized * Mathf.Clamp(velocity.magnitude - deceleration * Time.fixedDeltaTime, 0, maxSpeed);

                    rb.velocity = new Vector3(newVelocity.x, rb.velocity.y, newVelocity.z);
                }
                break;
            case Actions.BRAKING:
                if (velocity.magnitude < maxSpeed) {
                    rb.velocity -= backwardsAcceleration * Time.fixedDeltaTime * forward;
                }
                break;
            default:
                break;

        }

    }
}
