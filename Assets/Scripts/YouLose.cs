using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YouLose : MonoBehaviour 
{

    SoundManager sm;

    void Awake() 
    {
        sm = GameObject.Find("SoundManager").GetComponent<SoundManager>();

    }

    void Start()
    {
        sm.PlaySound(sm.sounds[7]);
    }
}
