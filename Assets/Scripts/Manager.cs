using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class Manager : MonoBehaviour {

    [SerializeField]
    PlayerController playerController;

    [SerializeField]
    Moon moon;

    [SerializeField]
    CinemachineTargetGroup targetGroup;

    // Update is called once per frame
    void Update() {
        if( Input.GetKeyDown( KeyCode.R ) )
            ResetAll();
    }

    [ContextMenu( "Reset All" )]
    void ResetAll() {
        playerController.Reset();
        moon.Reset();

        //ResetTargetGroup();
    }

    void SetCamWeightPlayer( float newWeight ) {
        targetGroup.m_Targets[ 0 ].weight = newWeight;
    }

    void SetCamRadiusMoon( float newRadius ) {
        targetGroup.m_Targets[ 1 ].radius = newRadius;
    }

    void ResetTargetGroup() {
        Sequence focusSequence = DOTween.Sequence();
        focusSequence.Append( DOVirtual.Float( targetGroup.m_Targets[ 0 ].weight, 1.0f, 1.0f, SetCamWeightPlayer ) );
        focusSequence.Join( DOVirtual.Float( targetGroup.m_Targets[ 1 ].radius, 0.0f, 1.0f, SetCamRadiusMoon ) );
    }

    public void MoonTargetGroup() {
        Sequence focusSequence = DOTween.Sequence();
        focusSequence.Append( DOVirtual.Float( targetGroup.m_Targets[ 0 ].weight, 0.0f, 1.0f, SetCamWeightPlayer ) );
        focusSequence.Join( DOVirtual.Float( targetGroup.m_Targets[ 1 ].radius, 10.0f, 1.0f, SetCamRadiusMoon ) );
    }
}
