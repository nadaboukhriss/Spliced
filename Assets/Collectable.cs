using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            SFXManager.sfxinstance.Audio.PlayOneShot(SFXManager.sfxinstance.PickupSound);
            Destroy(gameObject);
        }
    }
}
