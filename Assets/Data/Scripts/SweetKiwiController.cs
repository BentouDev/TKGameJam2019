using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class SweetKiwiController : MonoBehaviour//, IBeatHandler
{
    public enum KiwiType
    {
        Standing = 0,
        Jumping = 1,
        Hovering = 2,
        Roaming = 3,
    }
    
    public enum RoamingDirection
    {
        Left = -1,
        Right = 1,
    }

    public KiwiType kiwiType = KiwiType.Standing;

    public EdgeCollider2D groundDetector;
    public LayerMask groundLayers;
    public float minGroundDistance = .1f;

    public float jumpForce = 2;
    public float oscilationValue = 3;
    public RoamingDirection roamingDirection = RoamingDirection.Left;
    

    private Rigidbody2D _rigidbody2D;
    private Vector3 initialPosition;

    // Start is called before the first frame update
    void Start( )
    {
        _rigidbody2D = GetComponent<Rigidbody2D>( );
        initialPosition = transform.position;

        switch ( kiwiType )
        {
            case KiwiType.Hovering:
                _rigidbody2D.simulated = false;
                break;
            case KiwiType.Standing:
                break;
            case KiwiType.Jumping:
                break;
            case KiwiType.Roaming:
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
            case KiwiType.Roaming:
                roamingKiwiHandler( );
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
        // Debug.Log(initialPosition.x + Mathf.Sin(Time.time) * oscilationValue  );
        transform.position.Set( 
            transform.position.x,
            initialPosition.x + Mathf.Sin(Time.time) * oscilationValue, 
            transform.position.z 
        );
    }

    void roamingKiwiHandler( )
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