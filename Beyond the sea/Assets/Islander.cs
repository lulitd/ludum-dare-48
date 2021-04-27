using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Islander : MonoBehaviour
{
  // TODO: 
  // AI LOGIC,Roam, Pause
  // DIALOG
  // QUEST
  [SerializeField] public IslanderData _islanderData;
  [SerializeField] private Quest _quest;
  private int lastTalked =-1; 
  private NavMeshAgent _navMeshAgent;
  private float _moveSmoothing = 0.3f;
  [SerializeField] private MeshRenderer shirtMesh;

  private bool isTarget = false;
  private RadioTower _tower; 
  public void SetShirtColor(Color color)
  {
    if (shirtMesh)
    {
     shirtMesh.material.color = color; 
    }
    
  }


  void Start()
  {
    _navMeshAgent = GetComponent<NavMeshAgent>();
    _navMeshAgent.updatePosition = true;
    if (DialogPanel.instance != null)
    {
    
      DialogPanel.instance.OnAccept.AddListener(OnAccept);
      DialogPanel.instance.OnDecline.AddListener(OnDeny);
    }

    _tower = FindObjectOfType<RadioTower>();
  }
  
  private void OnDisable()
  {
    if (DialogPanel.instance != null)
    {
      DialogPanel.instance.OnAccept.RemoveListener(OnAccept);
      DialogPanel.instance.OnDecline.RemoveListener(OnDeny);
    }
  }


  private void OnTriggerEnter(Collider other)
  {

    if (other.CompareTag("Player"))
    {
      other.GetComponent<UnderwaterPlayerController>().OnAbleToTalk(true,this,this.transform);
      isTarget = true; 
    }
  }

  public void talk()
  {
   

    if (_quest.hasItems() && lastTalked!=-1)
    {
      DialogPanel.instance.ShowDialog(true, _islanderData.personalityType.askMat,true);
      lastTalked = -1; 
      return;
      
    }


    var length = _tower.questComplete
      ? _islanderData.personalityType.smallTalk.Length + 1
      : _islanderData.personalityType.smallTalk.Length;
    
    var convo = Random.Range(0,length);
    
    while (lastTalked == convo)
    {convo = Random.Range(0, length);
    }

    var dialog = convo == _islanderData.personalityType.smallTalk.Length
      ? _islanderData.personalityType.talkLeave
      : _islanderData.personalityType.smallTalk[convo];
    
    DialogPanel.instance.ShowDialog(true, dialog  );
    lastTalked = convo; 
  }

  public void OnAccept()
  {
    if (isTarget)
    {
      DialogPanel.instance.ShowDialog(true, _islanderData.personalityType.getMat, false);


      var items = _quest.trade;
      
      var r = _quest.GetRewards();

      foreach (var trade in items)
      {
        PlayerInventory.instance.RemoveFromInventory(trade.item,trade.Amount);
      }
      foreach (var reward in r)
      {
        PlayerInventory.instance.AddToInventory(reward.item,reward.Amount);
      }
   
    }
  }

  public void OnDeny()
  {
    if (isTarget)
    {
      DialogPanel.instance.ShowDialog(true, _islanderData.personalityType.denyMat, false);
    }
  }

  private void OnTriggerExit(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      other.GetComponent<UnderwaterPlayerController>().OnAbleToTalk(false);
      DialogPanel.instance.ShowDialog(false  );
      isTarget = false; 
    }
  }
}




