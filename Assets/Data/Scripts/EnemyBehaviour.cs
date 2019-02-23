using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class EnemyBehaviour : MonoBehaviour
{
    public Transform target;
    public new Rigidbody2D rigidbody2D;
    public float impulseValue = 1;
    public float maxVelocity = 10;

    // Start is called before the first frame update
    void Start( )
    {
        rigidbody2D = GetComponent<Rigidbody2D>( );
    }

    // Update is called once per frame
    void FixedUpdate( )
    {
        var targetPosition = target.position;
        var position = rigidbody2D.position;
        Vector2 direction = new Vector2( targetPosition.x, targetPosition.y ) -
                            new Vector2( position.x, position.y );
        
        rigidbody2D.AddForce( direction.normalized * impulseValue, ForceMode2D.Impulse );

        if ( rigidbody2D.velocity.magnitude <= maxVelocity ) return;
        rigidbody2D.velocity = rigidbody2D.velocity.normalized * maxVelocity;
    }
}