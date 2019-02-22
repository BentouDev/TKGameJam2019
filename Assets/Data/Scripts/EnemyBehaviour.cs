using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class EnemyBehaviour : MonoBehaviour
{
    public Transform target;
    public Rigidbody2D rigidbody2D;
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
        Vector2 direction = new Vector2( target.position.x, target.position.y ) -
                            new Vector2( rigidbody2D.position.x, rigidbody2D.position.y );
        
        rigidbody2D.AddForce( direction.normalized * impulseValue, ForceMode2D.Impulse );

        if ( rigidbody2D.velocity.magnitude <= maxVelocity ) return;
        rigidbody2D.velocity.Normalize(  );
        rigidbody2D.velocity *= maxVelocity;
    }
}