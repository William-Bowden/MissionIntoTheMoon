using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    Transform mainThruster;
    bool mainThrusterActive;
    ParticleSystem mainParticles;

    [SerializeField]
    Transform leftThruster;
    bool leftThrusterActive;
    ParticleSystem leftParticles;

    [SerializeField]
    Transform rightThruster;
    bool rightThrusterActive;
    ParticleSystem rightParticles;

    [SerializeField]
    GameObject crashParticles;
    [SerializeField]
    ParticleSystem smokingParticles;

    float thrusterForce = 10.0f;
    float mainScalar = 2.0f;

    Collider prevTrigger;

    bool canInput;
    bool hasCrashed;

    Vector3 startPos;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody>();
        mainParticles = mainThruster.GetComponentInChildren<ParticleSystem>();
        rightParticles = rightThruster.GetComponentInChildren<ParticleSystem>();
        leftParticles = leftThruster.GetComponentInChildren<ParticleSystem>();

        startPos = transform.position;

        Init();
    }

    // Update is called once per frame
    void Update() {
        if( Input.GetKeyDown( KeyCode.Space ) )
            StartMain();
        else if( Input.GetKeyUp( KeyCode.Space ) )
            StopMain();

        if( Input.GetKeyDown( KeyCode.A ) )
            StartLeft();
        else if( Input.GetKeyUp( KeyCode.A ) )
            StopLeft();

        if( Input.GetKeyDown( KeyCode.D ) )
            StartRight();
        else if( Input.GetKeyUp( KeyCode.D ) )
            StopRight();
    }

    private void FixedUpdate() {
        if( mainThrusterActive )
            rb.AddForceAtPosition( mainThruster.up * thrusterForce * mainScalar, mainThruster.position );

        if( leftThrusterActive )
            rb.AddForceAtPosition( leftThruster.up * thrusterForce /** leftScalar*/, leftThruster.position );

        if( rightThrusterActive )
            rb.AddForceAtPosition( rightThruster.up * thrusterForce /** rightScalar*/, rightThruster.position );
    }

    public void Init() {
        canInput = true;
    }

    #region Thrusters
    void StopAll() {
        StopMain();
        StopLeft();
        StopRight();
    }

    void StartMain() {
        if( canInput ) {
            mainThrusterActive = true;
            mainParticles.Play();
        }
    }

    void StopMain() {
        if( canInput ) {
            mainThrusterActive = false;
            mainParticles.Stop();
        }
    }

    void StartLeft() {
        if( canInput ) {
            leftThrusterActive = true;
            leftParticles.Play();
        }
    }

    void StopLeft() {
        if( canInput ) {
            leftThrusterActive = false;
            leftParticles.Stop();
        }
    }

    void StartRight() {
        if( canInput ) {
            rightThrusterActive = true;
            rightParticles.Play();
        }
    }

    void StopRight() {
        if( canInput ) {
            rightThrusterActive = false;
            rightParticles.Stop();
        }
    }
    #endregion

    void SetVelocity( float modifier ) {
        rb.velocity *= modifier;
        rb.angularVelocity *= modifier * 0.5f;
    }

    void Crash( Vector3 impactPos, Transform otherTransform ) {
        if( !hasCrashed ) {
            hasCrashed = true;

            transform.SetParent( otherTransform );
            Sequence powerDown = DOTween.Sequence();
            powerDown.Append( DOVirtual.Float( 1.0f, 0.0f, 0.5f, SetVelocity ).SetEase( Ease.OutQuad ).OnComplete( () => {
                StopAll();
                canInput = false;
                rb.constraints = RigidbodyConstraints.FreezePosition;
            } ) );

            smokingParticles.Play();

            GameObject crashParticlesGO = Instantiate( crashParticles, impactPos, Quaternion.LookRotation( Vector3.forward, impactPos - otherTransform.position ) );

            crashParticlesGO.transform.SetParent( otherTransform );
            Destroy( crashParticlesGO, 5.0f );
        }
    }

    public void Reset() {
        canInput = true;
        hasCrashed = false;
        prevTrigger = null;

        ResetPhysics();

        smokingParticles.Stop();
        transform.SetParent( null );
    }

    void ResetPhysics() {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX| RigidbodyConstraints.FreezeRotationY;

        transform.position = startPos;
        transform.rotation = Quaternion.identity;
    }

    private void OnTriggerEnter( Collider other ) {
        if( prevTrigger != other )
            Crash( other.ClosestPoint( transform.position ), other.transform );

        prevTrigger = other;
    }

    private void OnTriggerExit( Collider other ) {
        if( prevTrigger != other )
            Reset();
    }
}
