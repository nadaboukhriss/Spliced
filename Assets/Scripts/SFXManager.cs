using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public AudioSource Audio;

    public AudioSource PickupSound;
    public AudioSource FireballSound; // Plays when Ash shoots her fireball
    public AudioSource HitSound; // Can be played whenever something is hit (with sword/fireball)
    public AudioSource NoFaceAttackSound; // When the floating enemy that looks like No Face from Sprited Away shoots its projectiles
    // in Enemy scipt: public AudioClip NoFaceDeathSound; // self-explanatory XD
    // in Enemy scipt: public AudioClip NoFaceHitSound; // when it's hit
    public AudioSource PlayerSwordSwingSound; // self-explanatory
    // in Enemy scipt: public AudioClip SkellyDeathSound; // skeleton dies
    // not in use: public AudioClip SkellyStepSound; // when the skeletons walk
    // not in use: public AudioClip WalkOnGrassSound;
    // not in use: public AudioClip WalkOnStoneSound
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
