using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    [SerializeField] private int numLanes;
    [SerializeField] private List<Lane> laneList;
    [SerializeField] private List<Kart> kartList;
    [SerializeField] private LapComplete playerLapInfo;
    [SerializeField] private PositionNumbers playerPosition;
    private static LevelManager instance;

    public static LevelManager Instance => instance;
    public int PlayerLap => playerLapInfo.lapsDone;

    public List<Kart> KartList => kartList;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        playerPosition.SetPosition(2);
    }

    public Waypoint GetCurrentWaypoint(int lane, int waypointId) {
        Waypoint result = null;
        if (lane < laneList.Count && waypointId < laneList[lane].waypoints.Count) {
            result = laneList[lane].waypoints[waypointId];
        }

        return result;
    }

    public int GetNextWaypointId(int lane, int waypointId) {
        int nextWaypoint = 0;
        if (waypointId < laneList[lane].waypoints.Count - 1) {
            nextWaypoint = waypointId + 1;
        } else {
            nextWaypoint = 0;
        }

        return nextWaypoint;
    }

    public int GetWaypointsInLane(int lane) {
        return laneList[lane].waypoints.Count;
    }

    private void Update() {
        if (GameManager.Instance.raceStarted) {
            //SortKarts();
        }
    }

    public void SortKarts() {
        if (!GameManager.Instance.recordWaypoints) {
            kartList.Sort();
            for (int i = 0; i < kartList.Count; i++) {
                kartList[i].Position = kartList.Count - i;
                if (kartList[i].KartType == KartType.HUMAN) {
                    playerPosition.SetPosition(kartList[i].Position);
                }
            }
        }

    }

}
