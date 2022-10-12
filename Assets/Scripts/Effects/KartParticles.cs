using System.Collections.Generic;
using UnityEngine;

public class KartParticles : MonoBehaviour {
    [SerializeField] private GameObject smokeParticles;
    [SerializeField] private GameObject driftParticlesL;
    [SerializeField] private GameObject driftParticlesR;
    [SerializeField] private GameObject boostParticles;
    [SerializeField] private GameObject SpeedParticles;
    [SerializeField] private List<TrailRenderer> skidMarks;
    private Actions pastAction;
    private bool skidMarksEnabled;

    private KartInput input;
    private Rigidbody rb;

    // Start is called before the first frame update
    private void Start() {
        input = GetComponentInParent<KartInput>();
        //rb = GetComponentInParent<Rigidbody>();
        pastAction = Actions.NONE;
        skidMarksEnabled = false;
        EnableSmoke();
        foreach (TrailRenderer item in skidMarks) {
            item.emitting = false;
        }
    }

    // Update is called once per frame
    private void Update() {

        if (input.Action != pastAction) {
            switch (input.Action) {
                case Actions.BOOST:
                    EnableBoost();
                    break;
                case Actions.DRIFTING:
                    EnableDrift();
                    break;
                default:
                    EnableSmoke();
                    break;

            }
        }

        pastAction = input.Action;

        if (input.Action == Actions.DRIFTING || input.Action == Actions.BOOST) {
            bool paintSkidMarks = false;
            if (Physics.Raycast(transform.position, transform.up * -1, out RaycastHit hit, 0.1f)) {
                paintSkidMarks = true;
            }

            foreach (TrailRenderer item in skidMarks) {
                item.emitting = paintSkidMarks;
            }
        } else {
            foreach (TrailRenderer item in skidMarks) {
                item.emitting = false;
            }
        }
    }

    private void EnableSmoke() {
        smokeParticles.SetActive(true);
        driftParticlesR.SetActive(false);
        driftParticlesL.SetActive(false);
        boostParticles.SetActive(false);
        SpeedParticles.SetActive(false);
    }

    private void EnableDrift() {
        driftParticlesR.SetActive(true);
        driftParticlesL.SetActive(true);
        smokeParticles.SetActive(false);
        boostParticles.SetActive(false);
        SpeedParticles.SetActive(false);
    }

    private void EnableBoost() {
        boostParticles.SetActive(true);
        SpeedParticles.SetActive(true);
        driftParticlesR.SetActive(false);
        driftParticlesL.SetActive(false);
        smokeParticles.SetActive(false);
    }
}
