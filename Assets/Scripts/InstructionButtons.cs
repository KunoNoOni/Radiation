using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class InstructionButtons : MonoBehaviour 
{
    public GameObject page1;
    public GameObject page2;
    public GameObject page3;
    public GameObject nextPageButton;
    public GameObject previousPageButton;
    public GameObject playGameButton;

    int currentPage = 1;

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

    void ActivateGameobject(GameObject go)
    {
        go.SetActive(true);
    }

    void DeactivateGameobject(GameObject go)
    {
        go.SetActive(false);
    }

    public void NextPageButton()
    {
        if(EventSystem.current.currentSelectedGameObject != null)
            EventSystem.current.SetSelectedGameObject(null);

        sm.PlaySound(sm.sounds[0]);

        if(currentPage == 1)
        {
            ActivateGameobject(page2);
            ActivateGameobject(previousPageButton);
            DeactivateGameobject(page1);
            currentPage++;
        }
        else if(currentPage == 2)
        {
            ActivateGameobject(page3);
            ActivateGameobject(playGameButton);
            DeactivateGameobject(page2);
            DeactivateGameobject(nextPageButton);
            currentPage++;
        }
    }

    public void PreviousPageButton()
    {
        if(EventSystem.current.currentSelectedGameObject != null)
            EventSystem.current.SetSelectedGameObject(null);

        sm.PlaySound(sm.sounds[0]);

        if(currentPage == 3)
        {
            ActivateGameobject(page2);
            ActivateGameobject(nextPageButton);
            DeactivateGameobject(page3);
            DeactivateGameobject(playGameButton);
            currentPage--;
        }
        else if(currentPage == 2)
        {
            ActivateGameobject(page1);
            DeactivateGameobject(page2);
            DeactivateGameobject(previousPageButton);
            currentPage--;
        }
    }

    public void PlayGameButton()
    {
        if(EventSystem.current.currentSelectedGameObject != null)
            EventSystem.current.SetSelectedGameObject(null);

        sm.PlaySound(sm.sounds[0]);

        SceneManager.LoadScene("_GAME_");
    }
}
