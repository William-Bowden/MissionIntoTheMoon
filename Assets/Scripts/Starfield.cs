using System.Collections.Generic;
using UnityEngine;

public class Starfield : MonoBehaviour {
    [SerializeField]
    Transform player;

    SpriteRenderer[] spriteRenderers;

    // Start is called before the first frame update
    void Start() {
        spriteRenderers = transform.GetComponentsInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {
        if( player.position.y > Globals.SPACE_Y )
            SetAlpha( 1.0f );
        else if( player.position.y < Globals.EARTH_Y )
            SetAlpha( 0.0f );
        else {
            float distAboveEarthAtmosphere = ( player.position.y - Globals.EARTH_Y );
            float earthToSpaceTransitionSpace = ( Globals.SPACE_Y - Globals.EARTH_Y );

            float spaceRatio = distAboveEarthAtmosphere / earthToSpaceTransitionSpace;

            SetAlpha( spaceRatio );
        }
    }

    public void SetAlpha( float value ) {
        foreach( SpriteRenderer sprite in spriteRenderers ) {
            Color color = sprite.color;
            color.a = value;
            sprite.color = color;
        }
    }
}
