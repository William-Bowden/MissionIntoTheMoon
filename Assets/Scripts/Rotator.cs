using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Rotator : MonoBehaviour {
    Tween rotationTween;

    [SerializeField]
    float xRot = 0.0f;
    [SerializeField]
    float yRot = 0.0f;
    [SerializeField]
    float zRot = 0.0f;

    [SerializeField]
    float rotationSpeedMin = 5.0f;
    [SerializeField]
    float rotationSpeedMax = 10.0f;

    public void StartRandRotation() {
        ResetRotation();
        rotationTween = transform.DORotate( new Vector3( xRot, yRot, zRot ), Random.Range( rotationSpeedMin, rotationSpeedMax ), RotateMode.LocalAxisAdd ).SetEase( Ease.Linear ).SetLoops( -1 );
    }

    void ResetRotation() {
        rotationTween.Kill();
        transform.rotation = Quaternion.identity;
    }

}
