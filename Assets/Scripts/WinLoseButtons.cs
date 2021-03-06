﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class WinLoseButtons : MonoBehaviour 
{
    SoundManager sm;

    void Awake() 
    {
        sm = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void PlayAgainButton()
    {
        if(EventSystem.current.currentSelectedGameObject != null)
            EventSystem.current.SetSelectedGameObject(null);

        sm.PlaySound(sm.sounds[0]);

        SceneManager.LoadScene("_GAME_");
    }

    public void BackToTitlescreenButton()
    {
        if(EventSystem.current.currentSelectedGameObject != null)
            EventSystem.current.SetSelectedGameObject(null);

        sm.PlaySound(sm.sounds[0]);

        SceneManager.LoadScene("_TITLESCREEN_");
    }
}
