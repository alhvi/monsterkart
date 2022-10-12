using UnityEngine;

public class KartInputAi : KartInput {

    [SerializeField] private float raycastDistanceFront = 6f;
    [SerializeField] private LayerMask layerMask;
    private Kart kartInfo;

    private float waitingTime = 0.2f;

    private void Start() {
        kartInfo = GetComponent<Kart>();

    }

    private void FixedUpdate() {

        if (GameManager.Instance.raceStarted) {
            if (action != Actions.BOOST || action != Actions.DRIFTING) {
                action = Actions.ACCELERATING;
            }

            driftingInput = 0;

            //int layerMask = 1 << botId + 10;
            //layerMask = ~layerMask;
            Vector3 targetDir = kartInfo.CurrentWaypoint.Position - transform.position;

            float signedAngle = Vector3.SignedAngle(targetDir, transform.forward, Vector3.up);

            if (signedAngle > 7f) {
                horizontalMovement = -1;
            } else if (signedAngle < -7f) {
                horizontalMovement = 1;
            } else {
                horizontalMovement = 0;
            }

            /*if (!Physics.Raycast(transform.position, forward, out RaycastHit hitForward, raycastDistanceFront, layerMask)) {
                Debug.DrawRay(position, forward * raycastDistanceFront, Color.green);
            } else {
                Debug.DrawRay(position, forward * raycastDistanceFront, Color.red);
            }*/
        }
    }

}
