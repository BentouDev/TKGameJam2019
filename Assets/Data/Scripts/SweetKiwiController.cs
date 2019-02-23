using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweetKiwiController : MonoBehaviour
{
    public enum KiwiType
    {
        Standing = 0,
        Jumping = 1,
        Hovering = 2,
    }

    public KiwiType kiwiType = KiwiType.Standing;
    
    public EdgeCollider2D groundDetector;
    public LayerMask groundLayers;
    public float minGroundDistance = .1f;

    public float jumpForce = 2;

    private Rigidbody2D _rigidbody2D;
    
    // Start is called before the first frame update
    void Start( )
    {
        _rigidbody2D = GetComponent<Rigidbody2D>( );

        switch ( kiwiType )
        {
            case KiwiType.Hovering:
                _rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
                break;
            case KiwiType.Standing:
                break;
            case KiwiType.Jumping:
                break;
            default:
                throw new ArgumentOutOfRangeException( );
        }
    }

    // Update is called once per frame
    void FixedUpdate( )
    {
        switch ( kiwiType )
        {
            case KiwiType.Standing:
                standingKiwiHandler( );
                break;
            case KiwiType.Jumping:
                jumpingKiwiHandler( );
                break;
            case KiwiType.Hovering:
                hoveringKiwiHandler( );
                break;
            default:
                throw new ArgumentOutOfRangeException( );
        }
    }

    void standingKiwiHandler( )
    {
    }

    void jumpingKiwiHandler( )
    {
        
    }

    void hoveringKiwiHandler( )
    {
        
    }

    void doJump( )
    {
        _rigidbody2D.AddForce( Vector2.up * jumpForce, ForceMode2D.Impulse );
    }

    bool isTouchingGround( )
    {
        return groundDetector.IsTouchingLayers( groundLayers ) ||
            Physics2D.Raycast( transform.position, Vector2.down, minGroundDistance, groundLayers );
    }
}