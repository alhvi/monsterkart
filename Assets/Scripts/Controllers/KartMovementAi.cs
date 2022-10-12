using UnityEngine;

public class KartMovementAi : MonoBehaviour {
    [SerializeField] private float speed;
    private Rigidbody rb;
    private Kart kartData;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        kartData = GetComponent<Kart>();
    }

    // Update is called once per frame
    private void Update() {
        Vector3 targetDir = kartData.CurrentWaypoint.Position - transform.position;
        transform.position = Vector3.MoveTowards(transform.position, kartData.CurrentWaypoint.Position, speed * Time.deltaTime);
        transform.forward = targetDir.normalized;
    }
}
