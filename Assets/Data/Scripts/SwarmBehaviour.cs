using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmBehaviour : MonoBehaviour
{
    public GameObject Kiwi;
    public int kiwiCount = 125;
    public Rigidbody2D swarmRigidbody2D;
    public List<GameObject> kiwis;
    public List<Rigidbody2D> rigidKiwis;
    public float swarmingSpeed = 100;
    public float spawnScatterRadius = 5;

    // Start is called before the first frame update
    void Start( )
    {
//        swarmRigidbody2D = 
        
        for ( int i = 0; i < kiwiCount; i++ )
        {
            GameObject kiwi = Instantiate( Kiwi, this.transform, true );
            Vector2 vxy = Random.insideUnitCircle * spawnScatterRadius;
            kiwi.transform.position = transform.position - new Vector3( vxy.x, vxy.y, 0 );
            kiwis.Add( kiwi );
            rigidKiwis.Add( kiwi.GetComponent<Rigidbody2D>( ) );
            
        }
    }

    // Update is called once per frame
    void FixedUpdate( )
    {
        
        
        for ( int i = 0; i < kiwis.Count; i++ )
        {
            var kiwiPosition = kiwis[i].transform.position;
            Vector2 directionToSwarmCenter = ( new Vector2( kiwiPosition.x, kiwiPosition.y ) -
                                               new Vector2( transform.position.x, transform.position.y ) ).normalized;

            // forth quarter
            if ( kiwis[i].transform.localPosition.x > 0 && kiwis[i].transform.localPosition.y < 0 )
            {
                rigidKiwis[i].AddForce( directionToSwarmCenter + ( new Vector2( -1, 0 ) * swarmingSpeed ) );
            }

            // third quarter
            if ( kiwis[i].transform.localPosition.x < 0 && kiwis[i].transform.localPosition.y < 0 )
            {
                rigidKiwis[i].AddForce( directionToSwarmCenter + ( new Vector2( 0, 1 ) * swarmingSpeed ) );
            }
        }
    }

    private void OnDrawGizmos( )
    {
        Gizmos.DrawWireSphere( transform.position, spawnScatterRadius );
        throw new System.NotImplementedException( );
    }
}