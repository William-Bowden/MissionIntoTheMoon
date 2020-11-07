using UnityEngine;

public class SkyboxColorizer : MonoBehaviour {

    Camera cam;

    [SerializeField]
    Transform player;

    [SerializeField]
    Color earthColor;

    [SerializeField]
    Color spaceColor;

    private void Start() {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update() {
        if( player.position.y > Globals.SPACE_Y )
            cam.backgroundColor = spaceColor;
        else if( player.position.y < Globals.EARTH_Y )
            cam.backgroundColor = earthColor;
        else {
            float distAboveEarthAtmosphere = ( player.position.y - Globals.EARTH_Y );
            float earthToSpaceTransitionSpace = ( Globals.SPACE_Y - Globals.EARTH_Y );

            float spaceRatio = distAboveEarthAtmosphere / earthToSpaceTransitionSpace;
            float earthRatio = 1 - spaceRatio;
            cam.backgroundColor = earthColor * earthRatio + spaceColor * spaceRatio;
        }
    }
}
