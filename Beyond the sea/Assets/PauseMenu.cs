using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    private bool isMenuActive;
    private CanvasGroup _canvasGroup;

    [SerializeField]private Slider audioSlider; 
    public AudioMixer Mixer;
    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        var vol =  PlayerPrefs.GetFloat("VOL", 0.75f);
        
        AdjustVolume(vol);
        // ensures ui is insync...
        if (audioSlider)
        {    // setting the audio slider up 
            audioSlider.minValue = 0.0001f;
            audioSlider.maxValue = 1f; 
            audioSlider.SetValueWithoutNotify(vol);
        }
    }


    public void ShowPause()
    {
        showMenu(!isMenuActive);
    }
    void OnPaused(InputValue inputValue)
    {
        Debug.Log("yo");
         showMenu(!isMenuActive);
    }

    void OnRightClick(InputValue value)
    {
        Debug.Log("test");
    }
    void showMenu(bool show)
    {
        isMenuActive = show;

        if (_canvasGroup)
        {
            _canvasGroup.alpha = isMenuActive ? 1 : 0;
            _canvasGroup.interactable = isMenuActive;
            _canvasGroup.blocksRaycasts = isMenuActive; 
        }
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void AdjustVolume(float vol)
    {
        Mixer.SetFloat("MainVolume", Mathf.Log10(vol) * 20);
        PlayerPrefs.SetFloat("VOL",vol);
    }
    
    
    
   
}
