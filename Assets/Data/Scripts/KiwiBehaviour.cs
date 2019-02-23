using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiwiBehaviour : MonoBehaviour
{
    public new Rigidbody2D rigidbody2D;
    public Transform Model;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>( );
    }

    // Update is called once per frame
    void Update()
    {
        if (!Model)
            return;

        var dir = rigidbody2D.velocity;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Model.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
