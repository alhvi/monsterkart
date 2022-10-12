using UnityEngine;

public class Waypoint : MonoBehaviour {

    [SerializeField] private int waypointId;
    [SerializeField] private int lane;

    public int WaypointId { get => waypointId; set => waypointId = value; }
    public int Lane { get => lane; set => lane = value; }
    public Vector3 Position => transform.position;
    public Vector3 Forware => transform.forward;

    private void Start() {

    }

    private void Update() {

    }
}
