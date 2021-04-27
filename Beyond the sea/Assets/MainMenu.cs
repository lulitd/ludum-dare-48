using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private CanvasGroup mainCanvasGroup;
    [SerializeField] private CanvasGroup infoCanvasGroup;
    [SerializeField] private CanvasGroup settingsCanvasGroup;
    [SerializeField]private Slider audioSlider; 
    public AudioMixer Mixer;

    public GameObject QuitMenu; 
    private void Awake()
    {
        var vol =  PlayerPrefs.GetFloat("VOL", 0.75f);
        
        AdjustVolume(vol);
        // ensures ui is insync...
        if (audioSlider)
        {
            // setting the audio slider up 
            audioSlider.minValue = 0.0001f;
            audioSlider.maxValue = 1f; 
            audioSlider.SetValueWithoutNotify(vol);
        }
    }

#if !UNITY_WEBGL
    void OnPaused(InputValue value)
    {
        QuitMenu.SetActive(!QuitMenu.activeSelf);
    }
#endif
    public void CloseGame()
    {
        Application.Quit();
    }

  
    
    public void AdjustVolume(float vol)
    {
        Mixer.SetFloat("MainVolume", Mathf.Log10(vol) * 20);
        PlayerPrefs.SetFloat("VOL",vol);
    }
    
    
    public void GoPlay()
    {
        SceneManager.LoadScene(1);
    }
    
     void hideMenus()
     {
         mainCanvasGroup.alpha = 0;
         infoCanvasGroup.alpha = 0;
         settingsCanvasGroup.alpha = 0;
         mainCanvasGroup.interactable = false;
         mainCanvasGroup.blocksRaycasts = false;
         infoCanvasGroup.interactable = false;
         infoCanvasGroup.blocksRaycasts = false;
         settingsCanvasGroup.interactable = false;
         settingsCanvasGroup.blocksRaycasts = false;

     }

     public void showMenuMain()
     {
         hideMenus();
         mainCanvasGroup.alpha = 1f;
         mainCanvasGroup.interactable = true;
         mainCanvasGroup.blocksRaycasts = true;
     }
     
     public void showInfo()
     {
         hideMenus();
         infoCanvasGroup.alpha = 1f;
         infoCanvasGroup.interactable = true;
         infoCanvasGroup.blocksRaycasts = true;
     }
     public void showSettings()
     {
         hideMenus();
         settingsCanvasGroup.alpha = 1f;
         settingsCanvasGroup.interactable = true;
         settingsCanvasGroup.blocksRaycasts = true;
     }

}

public enum MenuPanel
{
    MAIN,
    INFO,
    SETTING
}
