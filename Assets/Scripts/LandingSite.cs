using UnityEngine;

public class LandingSite : MonoBehaviour {

    Transform moonTransform;

    private void Awake() {
        moonTransform = transform.parent;
    }

    [ContextMenu( "Reset" )]
    public void Reset( float radius ) {
        transform.localPosition = Random.onUnitSphere * radius;

        Vector3 parentToTransform = transform.position - transform.parent.position;
        transform.rotation = Quaternion.LookRotation( parentToTransform, Vector3.up );
    }

    public void Report( Transform player ) {
        int layerMask = 1 << 9;
        RaycastHit hit;

        float distance = 0.0f;

        // Does the ray intersect any objects excluding the player layer
        if( Physics.Raycast( player.position, moonTransform.position - player.position, out hit, Mathf.Infinity, layerMask ) ) {
            distance = Mathf.Abs( ( hit.point - transform.position ).sqrMagnitude );
            Debug.Log( $"distance: {distance}" );
            Debug.DrawLine( hit.point, hit.point + ( transform.position - hit.point ), Color.yellow, 5.0f );
        }

        float angleBetween = Mathf.Abs( Vector3.Angle( player.up, -transform.forward ) );
        Debug.Log( $"angleBetween: {angleBetween}" );

        float distScore = 500.0f - distance * 3.0f;
        float angleScore = 500.0f - angleBetween * 3.0f;

        float score = Mathf.Max( 0.0f, distScore + angleScore );

        Debug.Log( $"Score: {score}" );
    }
}