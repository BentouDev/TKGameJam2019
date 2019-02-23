using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmBehaviour : MonoBehaviour
{
    public GameObject Kiwi;
    public int kiwiCount;
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject kiwi = Instantiate( Kiwi );
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
}
