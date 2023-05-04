using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public AudioSource Audio;

    public AudioClip PickupSound;

    public static SFXManager sfxinstance; // singelton - so that only one manager object exists

    void Awake()
    {
        if (sfxinstance != null && sfxinstance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        sfxinstance = this;
        DontDestroyOnLoad(this);

    }
}
