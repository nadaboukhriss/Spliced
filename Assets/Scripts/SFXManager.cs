using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public AudioSource Audio;

    public AudioClip PickupSound;
    public AudioClip FireballSound; // Plays when Ash shoots her fireball
    public AudioClip HitSound; // Can be played whenever something is hit (with sword/fireball)
    public AudioClip NoFaceAttackSound; // When the floating enemy that looks like No Face from Sprited Away shoots its projectiles
    public AudioClip NoFaceDeathSound; // self-explanatory XD
    public AudioClip NoFaceHitSound; // when it's hit
    public AudioClip PlayerSwordSwingSound; // self-explanatory
    public AudioClip SkellyDeathSound; // skeleton dies
    public AudioClip SkellyStepSound; // when the skeletons walk
    public AudioClip WalkOnGrassSound;
    public AudioClip WalkOnStoneSound;

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
