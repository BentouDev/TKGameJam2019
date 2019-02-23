using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmBehaviour : MonoBehaviour
{
    public GameObject Kiwi;
    public int kiwiCount = 50;
    public List<GameObject> kiwis;
    public List<Rigidbody2D> rigidKiwis;
    public float swarmingSpeed = 100;
    
    // Start is called before the first frame update
    void Start()
    {
        for ( int i = 0; i < kiwiCount; i++ )
        {
            GameObject kiwi = Instantiate( Kiwi, this.transform, true );
            kiwi.transform.position = transform.position;
            kiwis.Add( kiwi );
            rigidKiwis.Add( kiwi.GetComponent<Rigidbody2D>(  ) );
            //todo place kiwi somewhere inside the shape
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for ( int i = 0; i < kiwis.Count; i++ )
        {
            // forth quarter
            if ( kiwis[ i ].transform.localPosition.x > 0 && kiwis[ i ].transform.localPosition.y < 0 )
            {
                rigidKiwis[ i ].AddForce( new Vector2( -1, 0 ) * swarmingSpeed );
            }
            // third quarter
            if ( kiwis[ i ].transform.localPosition.x < 0 && kiwis[ i ].transform.localPosition.y < 0 )
            {
                rigidKiwis[ i ].AddForce( new Vector2( 0, 1 ) * swarmingSpeed );
            }
        }
    }
}
