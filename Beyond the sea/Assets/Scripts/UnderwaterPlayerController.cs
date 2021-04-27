using System;
using System.Collections;
using Cinemachine;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UnderwaterPlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    

    private float playerInputHorizontal;
    private float playerInputVertical;
    [SerializeField] private float SwimStrength;
    [SerializeField] private float rotationStrength;
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth;
    [SerializeField] private float oxygenDamage;
    [SerializeField] private bool isDiving;
    [SerializeField] private ParticleSystem ps; 
    public UnityEvent<float> OnHealthUpdate;
    [SerializeField]private Transform respawnLocation;
    bool isAbleToSwim;
    private bool isAbleToTalk; 
    [SerializeField] private Image statusIcon;
    private NavMeshAgent walkerAgent;
    private NavMeshNavigator _navigator;
    private Transform swimLocation;
    [SerializeField] private PlayerAction talk; 
    [SerializeField] private PlayerAction swim;

    private Islander talker;
    private Transform talkerPosition;
    [SerializeField] private GameObject playerCam;
    [SerializeField] private GameObject groupCam;
    [SerializeField] private CinemachineTargetGroup groupshot;
    private bool isTalking = false;

    private bool isResurfacing = false; 
    private PauseMenu _pauseMenu;
    private void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody>();
        walkerAgent = GetComponent<NavMeshAgent>();
        _navigator = GetComponent<NavMeshNavigator>();
        _pauseMenu = FindObjectOfType<PauseMenu>(); // HACK: need to deal with unity input sytem properly. 
    }

    private void OnMove(InputValue movementData)
    {
        var move = movementData.Get<Vector2>();
        playerInputHorizontal = move.x;
        playerInputVertical = move.y;
    }

    private void OnFire(InputValue fireData)
    {
        if (isAbleToSwim)
        {
            prepareToSwim(swimLocation);
        }
    }

    private void OnInteract(InputValue fireData)
    {
        if (isAbleToTalk)
        {
            prepareToTalk(talker,talkerPosition);
        }
    }

    private void prepareToTalk(Islander islander, Transform talkerPosition1)
    {
        if (islander == null) return;
       
        if (!isTalking)
        {
            _navigator.canWalk = false;
            groupshot.m_Targets[1].target = talkerPosition1;
            groupCam.SetActive(true);
            playerCam.SetActive(false);
            statusIcon.enabled = false;
            // showdialog
            islander.talk();
            
          
        }
        if (isTalking)
        {
            _navigator.canWalk = true;
            groupshot.m_Targets[1].target = talkerPosition1;
            groupCam.SetActive(false);
            playerCam.SetActive(true);
            statusIcon.enabled = false;
         
            //DialogPanel.instance.ShowDialog(false);
        
        }

        isTalking = !isTalking; 
    }

    void OnPaused(InputValue inputValue)
    {
      
        Time.timeScale = Time.timeScale == 0 ? 1 : 0; 
        _navigator.canWalk =  Time.timeScale == 0 ? false : true ;
        _pauseMenu.ShowPause();
    }

    void OnResurface(InputValue inputValue)
    {
        if (!isDiving) return;
        ;
        if (!isResurfacing)
        StartCoroutine(Resurface(SwimStrength));
    }

    private void OnDestroy()
    {
        Time.timeScale = 1;
    }

    private void prepareToSwim(Transform swimLocation1)
    {
      
         walkerAgent.enabled = false;
         _navigator.enabled = false;
        
        transform.position = swimLocation1.position; 
        _rigidbody.isKinematic = false;
    }

    public void OnAbleToSwim(bool isAble, Transform spot =null)
    {
        isAbleToSwim = isAble;
        statusIcon.enabled = isAbleToSwim;
        statusIcon.sprite = swim.icon;
        statusIcon.color = swim.spriteColor; 
        swimLocation = spot; 
    }
    
    public void OnAbleToTalk(bool isAble, Islander talker = null, Transform talkerTransform = null)
    {
        isAbleToTalk = isAble;
        statusIcon.enabled = isAbleToTalk;
        statusIcon.sprite = talk.icon;
        statusIcon.color = talk.spriteColor;
        this.talker = talker;
        this.talkerPosition = talkerTransform;

    }
    

    public void goDiving(bool go)
    {
        isDiving = go;
        currentHealth = maxHealth;
        OnHealthUpdate.Invoke(currentHealth/maxHealth);
        _rigidbody.isKinematic = !go;
    

        if (go)
        {
            ps.Play();
        }
        else
        {
            ps.Stop();
        }
    }


    private void Update()
    {
        if (isDiving)
        {
            currentHealth -= oxygenDamage * Time.deltaTime;

            OnHealthUpdate.Invoke(currentHealth / maxHealth);
            if (currentHealth < 0f)
            {
                isDiving = false;
                StartCoroutine(Resurface(5));
            }
        }
        
    }

    IEnumerator Resurface(float speed)
    {
        isResurfacing = true;
        Vector3 startPosition = transform.position;
        Quaternion startRotation = transform.rotation;
        float time = 0f;

        while(transform.position != respawnLocation.position)
        {
            transform.position = Vector3.Lerp(startPosition, respawnLocation.position, (time/Vector3.Distance(startPosition, respawnLocation.position))*speed);
            transform.rotation = Quaternion.Lerp(startRotation, quaternion.identity,
                (time / Quaternion.Dot(startRotation, quaternion.identity)) * speed);
            time += Time.deltaTime;
            yield return null;
        }
        
        walkerAgent.enabled = true;
        _navigator.enabled = true;
        isResurfacing = false;
    }

    private void FixedUpdate()
    {
        if(!isDiving) return;
        
        var force = new Vector3(playerInputHorizontal, playerInputVertical, 0f);
        var lookVector = new Vector3(playerInputHorizontal, 0f, playerInputVertical);
        _rigidbody.AddForce(force * SwimStrength);
        var rot = _rigidbody.rotation;
        var lerpRot = Quaternion.Lerp(rot, Quaternion.LookRotation(lookVector), rotationStrength * Time.fixedDeltaTime);
        _rigidbody.MoveRotation(lerpRot);
    }
}