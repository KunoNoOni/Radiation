using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class TitlescreenButtons : MonoBehaviour 
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

    public void PlayGameButton()
    {
        if(EventSystem.current.currentSelectedGameObject != null)
            EventSystem.current.SetSelectedGameObject(null);

        sm.PlaySound(sm.sounds[0]);
        SceneManager.LoadScene("_GAME_");
    }

    public void InstructionsButton()
    {
        if(EventSystem.current.currentSelectedGameObject != null)
            EventSystem.current.SetSelectedGameObject(null);

        sm.PlaySound(sm.sounds[0]);
        SceneManager.LoadScene("_INSTRUCTIONS_");
    }

    public void CreditsButton()
    {
        if(EventSystem.current.currentSelectedGameObject != null)
            EventSystem.current.SetSelectedGameObject(null);

        sm.PlaySound(sm.sounds[0]);
        SceneManager.LoadScene("_CREDITS_");
    }
}
