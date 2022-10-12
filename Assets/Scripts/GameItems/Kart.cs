using System;
using UnityEngine;

public enum KartType { HUMAN, BOT }

public class Kart : MonoBehaviour, IComparable<Kart> {
    [SerializeField] private int kartId;
    [SerializeField] private int lane;
    [SerializeField] private int currentWaypointId;
    [SerializeField] private KartType kartType = KartType.BOT;
    [SerializeField] private int position;
    [SerializeField] private int kartModelId;

    private int modelId;
    private int lap;
    private Waypoint currentWaypoint;

    private int pastLane;
    private int pastWaypointId;

    public Waypoint CurrentWaypoint => currentWaypoint;
    public int Lap { get => lap; set => lap = value; }
    public int Position { get => position; set => position = value; }
    public KartType KartType { get => kartType; set => kartType = value; }
    public int KartModelId { get => kartModelId; set => kartModelId = value; }

    private KartInputHuman humanInput;
    private KartInputAi aiInput;

    private void Start() {
        currentWaypoint = LevelManager.Instance.GetCurrentWaypoint(lane, currentWaypointId);
        lap = 0;
        pastLane = lane;
        pastWaypointId = currentWaypointId;

        if (kartType == KartType.HUMAN) {
            kartModelId = PlayerPrefs.GetInt("P1");

            humanInput = GetComponent<KartInputHuman>();
            aiInput = GetComponent<KartInputAi>();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (GameManager.Instance.raceStarted && !GameManager.Instance.raceFinished) {
            if (other.CompareTag("Waypoint")) {

                if (kartType == KartType.BOT) {
                    if (other.gameObject == currentWaypoint.gameObject) {
                        pastLane = lane;
                        pastWaypointId = currentWaypointId;
                        currentWaypointId = LevelManager.Instance.GetNextWaypointId(lane, currentWaypointId);
                        currentWaypoint = LevelManager.Instance.GetCurrentWaypoint(lane, currentWaypointId);
                    }
                } else {
                    Waypoint waypoint = other.gameObject.GetComponent<Waypoint>();
                    lane = waypoint.Lane;
                    currentWaypointId = waypoint.WaypointId;

                    pastLane = lane;
                    pastWaypointId = currentWaypointId;

                    currentWaypointId = LevelManager.Instance.GetNextWaypointId(lane, currentWaypointId);
                    currentWaypoint = LevelManager.Instance.GetCurrentWaypoint(lane, currentWaypointId);

                    LevelManager.Instance.SortKarts();
                }
            } else if (other.CompareTag("LapTrigger")) {
                if (kartType == KartType.BOT) {
                    lap++;
                    if (lap == 4) {
                        GameManager.Instance.FinishGame();
                    }
                }
            }
        }
    }

    public int CompareTo(Kart obj) {
        int result = 0;

        if (lap > obj.lap) {
            result = 1;
        } else if (lap < obj.lap) {
            result = -1;
        } else {

            int myWaypoint = currentWaypointId == 0 ? LevelManager.Instance.GetWaypointsInLane(lane) : currentWaypointId;
            int theirWaypoint = obj.currentWaypointId == 0 ? LevelManager.Instance.GetWaypointsInLane(obj.lane) : obj.currentWaypointId;

            float waypointProgressMine = (lap + 1) * 100 + ((float)myWaypoint / (LevelManager.Instance.GetWaypointsInLane(lane) + lane));
            float waypointProgressTheirs = (obj.lap + 1) * 100 + ((float)theirWaypoint / (LevelManager.Instance.GetWaypointsInLane(obj.lane) + obj.lane));

            result = waypointProgressMine.CompareTo(waypointProgressTheirs);

            if (result == 0) {
                float distanceMine = Vector3.Distance(transform.position, currentWaypoint.transform.position);
                float distanceTheirs = Vector3.Distance(obj.transform.position, obj.currentWaypoint.transform.position);

                result = distanceMine.CompareTo(distanceTheirs) * -1;
            }
        }

        return result;
    }

}
