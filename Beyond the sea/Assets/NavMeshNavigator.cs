using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class NavMeshNavigator : MonoBehaviour
{
   
    private NavMeshAgent _navMeshAgent;
    private float _moveSmoothing = 0.3f;
    private float playerInputHorizontal;
    private float playerInputVertical;

    [SerializeField] private Animator _animator;
    [SerializeField] private string animSpeedControl = "Speed";
    private int animationSpeedHash;
    Vector2 smoothDeltaPosition = Vector2.zero;
    Vector2 velocity = Vector2.zero;

    public bool canWalk = false; 
    
    private void OnMove(InputValue movementData)
    {
        var move = movementData.Get<Vector2>();
        playerInputHorizontal = move.x;
        playerInputVertical = move.y;
    }
    
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.updatePosition = false;
        animationSpeedHash = Animator.StringToHash(animSpeedControl);
        _animator = GetComponent<Animator>();


    }

    private void Update()
    {
        // FROM UNITY  https://docs.unity3d.com/Manual/nav-CouplingAnimationAndNavigation.html
        Vector3 worldDeltaPosition = _navMeshAgent.nextPosition - transform.position;

        // Map 'worldDeltaPosition' to local space
        float dx = Vector3.Dot (transform.right, worldDeltaPosition);
        float dy = Vector3.Dot (transform.forward, worldDeltaPosition);
        Vector2 deltaPosition = new Vector2 (dx, dy);

        // Low-pass filter the deltaMove
        float smooth = Mathf.Min(1.0f, Time.deltaTime/0.15f);
        smoothDeltaPosition = Vector2.Lerp (smoothDeltaPosition, deltaPosition, smooth);

        // Update velocity if time advances
        if (Time.deltaTime > 1e-5f)
            velocity = smoothDeltaPosition / Time.deltaTime;

     
        _animator.SetFloat (animationSpeedHash, velocity.magnitude);

    }

    void LateUpdate()
    {
        if (!canWalk) return; 
 
        Vector3 movement = new Vector3(playerInputHorizontal, 0, playerInputVertical);

        _navMeshAgent.Move(movement * (Time.deltaTime * _navMeshAgent.speed));
        this.transform.position = Vector3.Lerp(this.transform.position, _navMeshAgent.nextPosition, _moveSmoothing);
        var rot = this.transform.rotation;
       // var lerpRot = Quaternion.Lerp(rot, Quaternion.LookRotation(movement), 10 * Time.deltaTime);
       // transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(movement), _navMeshAgent.angularSpeed * Time.deltaTime);

    }
}


