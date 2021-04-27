using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogPanel : MonoBehaviour
{
    [SerializeField] public  TextMeshProUGUI mainText;
    [SerializeField] private Button postiveButton;
    [SerializeField] private Button negativeButton;

    [SerializeField] private GameObject buttonPanel;
    public static DialogPanel instance = null; 

    public UnityEvent OnAccept;
    public UnityEvent OnDecline; 
    
    // Start is called before the first frame update
    void Awake()
    {
        if (instance!=null && instance!=this)Destroy(this);
        else if (instance == null) instance = this; 

        this.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        instance = null; 
    }

    private void OnEnable()
    {
        postiveButton.onClick.AddListener(OnPostiveButtonClick);
        negativeButton.onClick.AddListener(OnNegativeButtonClick);
    }

    private void OnDisable()
    {
        postiveButton.onClick.RemoveListener(OnPostiveButtonClick);
        negativeButton.onClick.RemoveListener(OnNegativeButtonClick);
    }


    public void ShowDialog(bool on, string text = "",bool showbuttons=false)
    {
        mainText.text = text; 
        gameObject.SetActive(on);
        buttonPanel.SetActive(showbuttons);
    }

    void OnPostiveButtonClick()
    {
        Debug.Log("+++?");
        
     OnAccept.Invoke();   
    }

    void OnNegativeButtonClick()
    {
        OnDecline.Invoke();
    }
}
