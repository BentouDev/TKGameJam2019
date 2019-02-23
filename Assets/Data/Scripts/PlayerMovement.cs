using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    public bool DrawDebug;

    [Header("Physics")] public Rigidbody2D Body;
    public float MaxSpeed;
    public LayerMask GroundLayers;

    private float CurrentVelocity;
    private float AirVelocity;

    [FormerlySerializedAs("JumpHeight")] 
    public float JumpPower = 2;
    public float MinJumpTime = 1;
    public float MaxJumpTime = 1;

    private float LastJumpTime;
    private float LastJumpButtonTime;

    public EdgeCollider2D GroundDetector;

    public float MinJumpDistance = 0.2f;
    public float MinGroundDistance = 0.1f;

    public float Gravity = Physics.gravity.y;

    private float MaxGravity
    {
        get { return Gravity * 10; }
    }

    [Header("Input")] 
    public string Horizontal = "Horizontal";
    public string Jump = "Jump";
    public string Skill = "Fire1";

    private bool _isGrounded;
    private bool _canJump;

    // Rythm
    private RythmController Rythm;

    private string RythmMsg;

    private bool RythmPassed;
    private bool RythmOverdid;

    private int RythmCombo;

    public bool IsJumping
    {
        get { return Time.time - LastJumpTime < MinJumpTime; }
    }

    public float JumpSpeed
    {
        get { return JumpPower; }
    }

    public float GetSpeed()
    {
        return MaxSpeed;
    }

    void Start()
    {
        LastJumpTime = 0;
        
        if (!Body)
            Body = GetComponentInChildren<Rigidbody2D>();

        Rythm = FindObjectOfType<RythmController>();
        Rythm.OnAfterBeat += OnAfterBeat;
        Rythm.OnAfterPause += OnAfterPause;
    }

    void Update()
    {
        ProcessInput();

        if (IsGrounded())
            AirVelocity = 0;
        else
        {
            if (IsJumping)
            {
                AirVelocity = JumpSpeed;
            }
            else
            {
                AirVelocity += Gravity;
            }
        }
    }

    private void FixedUpdate()
    {
        _canJump = AirVelocity < 0 && Physics2D.Raycast(transform.position, Vector2.down, MinJumpDistance, GroundLayers);
        _isGrounded = GroundDetector.IsTouchingLayers(GroundLayers) || Physics2D.Raycast(transform.position, Vector2.down, MinGroundDistance, GroundLayers);
        Body.velocity = new Vector2(CurrentVelocity, AirVelocity) * Time.fixedDeltaTime;
    }

    void ProcessInput()
    {
        if (Input.GetButton(Jump))
            DoJump();

        if (Input.GetButtonDown(Skill))
            DoSkill();

        DoInput(Input.GetAxis(Horizontal));
    }

    void DoInput(float directional)
    {
        CurrentVelocity = GetSpeed() * directional;
        CurrentVelocity = Mathf.Clamp(CurrentVelocity,-MaxSpeed, MaxSpeed);
    }

    void DoJump()
    {
        if (IsJumping && Time.time - LastJumpButtonTime < MaxJumpTime)
        {
            LastJumpTime = Time.time;
            AirVelocity = JumpSpeed;
        }
        else if (_canJump || IsGrounded())
        {
            _isGrounded = false;
            
            LastJumpTime = Time.time;
            LastJumpButtonTime = Time.time;
            AirVelocity = JumpSpeed;
        }
    }

    void DoSkill()
    {
        if (RythmPassed)
            RythmOverdid = true;
        
        if (Rythm.IsInSkillFrame())
        {
            RythmPassed = true;
        }
        else
        {
            RythmOverdid = true;
        }
    }

    void OnAfterBeat()
    {
        if (RythmPassed && !RythmOverdid)
        {
            RythmCombo++;
            RythmMsg = "GOOD " + RythmCombo;
            Rythm.Hit();
        }
        else
        {
            RythmCombo = 0;
            Rythm.Miss();
            
            if (!RythmPassed)
                RythmMsg = "MISS!";
            else if (RythmOverdid && RythmPassed)
                RythmMsg = "TOO MUCH!";
            else if (RythmOverdid)
            {
                RythmMsg = "EARLY!";
            }
            else
            {
                RythmMsg = string.Empty;
            }
        }

        RythmOverdid = false;
        RythmPassed = false;
    }

    private void OnAfterPause()
    {
        if (RythmOverdid)
        {
            RythmCombo = 0;
            RythmMsg = "BAAD";
        }

        RythmOverdid = false;
    }

    bool IsGrounded()
    {
        return _isGrounded;
    }
    
    void OnGUI()
    {
        GUI.Label(new Rect(Screen.width * 0.5f - 100, Screen.height * 0.5f - 10, 200, 20), RythmMsg);
        
        if (!DrawDebug)
            return;
        
        GUI.Label(new Rect(10,10,200,30), "isGrounded: " + IsGrounded());
        GUI.Label(new Rect(10,30,200,30), "canJump: " + (_canJump || IsGrounded()));
        GUI.Label(new Rect(10,50,200,30), "air vel: " + AirVelocity);
        GUI.Label(new Rect(10,70,200,30), "move vel: " + CurrentVelocity);
    }
}
