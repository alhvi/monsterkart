using System.Collections;
using UnityEngine;

public enum Actions { NONE, ACCELERATING, DECELERATING, BRAKING, DRIFTING, BOOST }

public class KartMovement : MonoBehaviour {

    [SerializeField] private float maxSpeedRegular = 50f;
    [SerializeField] private float maxSpeedBoost = 70f;
    [SerializeField] private float acceleration = 50f;
    [SerializeField] private float deceleration = 20f;
    [SerializeField] private float backwardsAcceleration = 30f;
    [SerializeField] private float minRotationSpeed = 20f;
    [SerializeField] private float maxRotationSpeed = 100f;
    [SerializeField] private float totalTurnTime = 1f;

    private float boostPadTime = 2.5f;

    private float currentMaxSpeed;
    private Rigidbody rb;
    private Kart kartInfo;
    private KartInput kartInput;
    private KartInput kartInput2;
    private float horizontalMovement;
    private float driftingInput;

    private Actions action;

    private bool turning;
    private float turningTime;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        kartInfo = GetComponent<Kart>();
        action = Actions.NONE;
        turning = false;
        turningTime = 0;
        currentMaxSpeed = maxSpeedRegular;

        if (kartInfo.KartType == KartType.HUMAN) {
            kartInput = GetComponent<KartInputHuman>();
            kartInput2 = GetComponent<KartInputAi>();
        } else {
            kartInput = GetComponent<KartInputAi>();

            //Randomizar valores
            maxSpeedRegular = Random.Range(maxSpeedRegular - 15, maxSpeedRegular + 15);
            maxSpeedBoost = Random.Range(maxSpeedBoost - 10, maxSpeedBoost + 10);
            acceleration = Random.Range(acceleration - 10, acceleration + 10);
        }
    }

    public void ChangeInput() {
        kartInput = kartInput2;
    }

    private void Update() {

        horizontalMovement = kartInput.HorizontalMovement;
        driftingInput = kartInput.DriftingInput;
        action = kartInput.Action;

        if (horizontalMovement == 0) {
            turning = false;
        }

    }

    private void FixedUpdate() {
        Vector3 velocity = Utils.RemoveYFromVector(rb.velocity);
        Vector3 forward = Utils.RemoveYFromVector(transform.forward);

        if (Mathf.Abs(horizontalMovement) > 0.1f) {

            if (!turning) {
                turning = true;
                turningTime = 0;
            }

            float distance = Vector3.Distance(forward.normalized, velocity.normalized);
            float direction = distance < 1.2 || velocity.magnitude < 0.05 ? 1 : -1;
            float factor = Mathf.Lerp(minRotationSpeed, maxRotationSpeed, 1 - velocity.magnitude / currentMaxSpeed);
            float factor2 = Mathf.Lerp(0.3f, 1f, Mathf.Clamp(turningTime / totalTurnTime, 0, totalTurnTime));

            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x,
                transform.localEulerAngles.y + (horizontalMovement * factor * factor2 * direction * Time.fixedDeltaTime),
                transform.localEulerAngles.z);

            turningTime += Time.fixedDeltaTime;

            if (/*driftingInput == 0*/ action != Actions.DRIFTING || direction == -1) {
                forward = Utils.RemoveYFromVector(transform.forward);
                Vector3 newVelocity = forward.normalized * velocity.magnitude * direction;
                rb.velocity = new Vector3(newVelocity.x, rb.velocity.y, newVelocity.z);

            } else {

                Vector3 driftingVector = Vector3.Slerp(forward.normalized, velocity.normalized, 0.5f);
                driftingVector = driftingVector.normalized;

                Vector3 newVelocity = driftingVector.normalized * velocity.magnitude * direction;
                rb.velocity = new Vector3(newVelocity.x, rb.velocity.y, newVelocity.z);
            }

            //Updating vectors
            velocity = Utils.RemoveYFromVector(rb.velocity);
            forward = Utils.RemoveYFromVector(transform.forward);

        }

        switch (action) {
            case Actions.BOOST:
            case Actions.DRIFTING:
            case Actions.ACCELERATING:
                if (velocity.magnitude < currentMaxSpeed) {
                    rb.velocity += acceleration * Time.fixedDeltaTime * forward;
                }
                break;
            case Actions.DECELERATING:
                if (velocity.magnitude > 0f) {
                    Vector3 newVelocity = velocity.normalized * Mathf.Clamp(velocity.magnitude - deceleration * Time.fixedDeltaTime, 0, currentMaxSpeed);

                    rb.velocity = new Vector3(newVelocity.x, rb.velocity.y, newVelocity.z);
                }
                break;
            case Actions.BRAKING:
                //if (velocity.magnitude < currentMaxSpeed) {
                rb.velocity -= backwardsAcceleration * Time.fixedDeltaTime * forward;
                //}
                break;
            default:
                break;

        }

    }

    private void FixCarRotation() {
        int trackLayer = ~0;
        float raycastDistance = 1f;
        if (Physics.Raycast(transform.position, transform.up * -1, out RaycastHit hit, raycastDistance, trackLayer)) {
            float angle = Vector3.SignedAngle(transform.up, hit.normal, Vector3.right);
            if (Mathf.Abs(angle) > 10 && Mathf.Abs(angle) < 160) {
                transform.Rotate(Vector3.right, angle * -1);
            }
            //Debug.DrawRay(position, forward * raycastDistanceFront, Color.green);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("BoostPad")) {
            StartBoost(boostPadTime);
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (!collision.gameObject.CompareTag("Track")) {

            if (action == Actions.BOOST) {
                RemoveBoost();
            } else if (action == Actions.DRIFTING) {
                kartInput.CancelDrifting();
            }

            if (kartInfo.KartType == KartType.HUMAN) {

                ContactPoint point = collision.GetContact(0);
                Vector3 newDirection = Utils.RemoveYFromVector(Vector3.Cross(point.normal, Vector3.up)).normalized;
                newDirection *= Mathf.Sign(Vector3.Dot(transform.forward, newDirection));
                transform.forward = new Vector3(newDirection.x, transform.forward.y, newDirection.z);
            }

            Vector3 velocity = Utils.RemoveYFromVector(rb.velocity);
            Vector3 forward = Utils.RemoveYFromVector(transform.forward);
            Vector3 newVelocity = forward.normalized * velocity.magnitude / 2;
            rb.velocity = new Vector3(newVelocity.x, rb.velocity.y, newVelocity.z);
        }
    }

    public void StartBoost(float time) {
        StopCoroutine("BoostCoroutine");
        currentMaxSpeed = maxSpeedBoost;
        Vector3 velocity = Utils.RemoveYFromVector(rb.velocity);
        Vector3 newVelocity = velocity.normalized * maxSpeedBoost;
        rb.velocity = new Vector3(newVelocity.x, rb.velocity.y, newVelocity.z);
        StartCoroutine(BoostCoroutine(time));
        kartInput.StartBoost();
    }

    public void RemoveBoost() {
        currentMaxSpeed = maxSpeedRegular;
        Vector3 velocity = Utils.RemoveYFromVector(rb.velocity);
        Vector3 newVelocity = velocity.normalized * maxSpeedRegular;
        rb.velocity = new Vector3(newVelocity.x, rb.velocity.y, newVelocity.z);
        kartInput.RemoveBoost();
    }

    private IEnumerator BoostCoroutine(float time) {
        yield return new WaitForSeconds(time);
        RemoveBoost();
    }
}
