using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            GetComponent<DialogueTrigger>().TriggerDialogue();
            //Destroy(gameObject);
        }
    }
}
