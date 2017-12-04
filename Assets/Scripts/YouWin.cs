using UnityEngine;

public class YouWin : MonoBehaviour 
{

    SoundManager sm;

    void Awake() 
    {
        sm = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    void Start()
    {
        sm.PlaySound(sm.sounds[6]);
    }
}
