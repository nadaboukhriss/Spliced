using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterDungeon : MonoBehaviour
{
    //Index found in main menu: File > Build Settings > Scenes in Build
    public int switchToSceneWithBuildIndex;

    [SerializeField]
    private Transform teleportTo;
    private bool isPlayerInCollider = false;
    private Collider2D playerCollider;

    private void OnTriggerEnter2D(Collider2D other)
    {
       if (other.tag == "Player")
        {
            isPlayerInCollider = true;
            playerCollider = other;
        } 
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isPlayerInCollider = false;
    }   

    private void Update()
    {
        if(Input.GetButtonDown("Fire2") && isPlayerInCollider)
        {
            playerCollider.transform.position = teleportTo.position;
        }
    }
}