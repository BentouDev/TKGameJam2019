using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    public PlayerMovement Player;
    public Animator Anim;

    public string IsMoving = "IsMoving";
    public string AirVelocity = "AirVelocity";
    
    void Update()
    {
        if (!Anim)
            return;
        
        Anim.SetBool(IsMoving, Player.IsMoving());
        Anim.SetFloat(AirVelocity, Player.GetAirVelocity());
    }
}
