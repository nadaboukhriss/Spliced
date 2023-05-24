using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterDungeon : MonoBehaviour
{
    //Index found in main menu: File > Build Settings > Scenes in Build
    public int switchToSceneWithBuildIndex;

    [SerializeField]
    private GameObject fadeInFadeOutElt;
    [SerializeField]
    private Transform teleportTo;
    private bool isPlayerInCollider = false;
    private Collider2D playerCollider;

    private void start(){
        // GameObject prefab = Resources.Load<GameObject>("FadeInFadeOut");
        // fadeInFadeOutElt = Instantiate(prefab);
    }
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
            // fadeInFadeOutElt.SetActive(true);
            // yield return new WaitForSeconds(1f); // Wait for 1 second
            // playerCollider.transform.position = teleportTo.position;
            // Animator animator = fadeInFadeOutElt.GetComponent<Animator>();
            // animator.SetTrigger("FadeOut");
            // gameObject.SetActive(false);
            StartCoroutine(ActivateAndTeleport());

            // fadeInFadeOutElt.SetActive(true); // First line
            // Animator animator = fadeInFadeOutElt.GetComponent<Animator>();
            // animator.SetTrigger("FadeIn");

            // // yield return new WaitForSeconds(1f); // Wait for 1 second

            // playerCollider.transform.position = teleportTo.position; // Second line

    
            // animator.SetTrigger("FadeOut"); // Third line

            // fadeInFadeOutElt.SetActive(false);
        }

    }

    IEnumerator ActivateAndTeleport()
    {
    // // GameObject prefab = Resources.Load<GameObject>("FadeInFadeOut");
    // // fadeInFadeOutElt = Instantiate(prefab);
        // fadeInFadeOutElt.SetActive(true); // First line

        Animator animator = fadeInFadeOutElt.GetComponent<Animator>();
        animator.SetTrigger("FadeIn");

        yield return new WaitForSeconds(0.8f); // Wait for 1 second

        playerCollider.transform.position = teleportTo.position; // Second line

        yield return new WaitForSeconds(0.25f); // Wait for 1 second
        animator.SetTrigger("FadeOut"); // Third line

        // fadeInFadeOutElt.SetActive(false);
    // // Destroy(fadeInFadeOutElt);
    }
}