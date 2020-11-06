using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour {

    [SerializeField]
    Manager manager;

    Rotator rotator;
    OctahedronSphereCreator osc;

    LandingSite landingSite;

    private void Awake() {
        rotator = GetComponent<Rotator>();
        osc = GetComponentInChildren<OctahedronSphereCreator>();
        landingSite = GetComponentInChildren<LandingSite>();

        Reset();
    }

    public void Reset() {
        rotator.StartRandRotation();
        landingSite.Reset( osc.radius );
    }

    public void PingLandingSite(Transform triggerer) {
        landingSite.Report( triggerer );
        //manager.MoonTargetGroup();
    }

    private void OnTriggerEnter( Collider other ) {
        PingLandingSite( other.transform );
    }

}
