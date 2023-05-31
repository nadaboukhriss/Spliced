using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScreen : MonoBehaviour
{

    private Animator animator;

    private Vector3 moveTo;

    private bool active = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void StartFade(Vector3 position){
        if (active) return;

        active = true;
        moveTo = position;
        
        //Hide the player while screen is fading
        GameManager.Instance.player.GetComponent<PlayerController>().LockMovement();
        //GameManager.Instance.player.SetActive(false);
        
        animator.SetTrigger("FadeIn");
    }
    public void MovePlayer(){
        //GameManager.Instance.player.SetActive(true);
        GameManager.Instance.player.transform.position = moveTo; // Second line
        animator.SetTrigger("FadeOut");
    }
    public void FadingDone(){
        active = false;
        GameManager.Instance.player.GetComponent<PlayerController>().UnlockMovement();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
