using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapCharacters : MonoBehaviour
{
    // referenses to controlled game objects
	public GameObject avatar1, avatar2;

    [SerializeField]
    private AnimatorOverrideController avatar1Animations;
    [SerializeField]
    private AnimatorOverrideController avatar2Animations;

    private Animator animator;

    private GameObject currentAvatar;
    private GameObject timer;

    // variable contains which avatar is on and active
	int whichAvatarIsOn = 1;

    public void Awake(){
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        // enable first avatar and disable another one
		avatar1.gameObject.SetActive (true);
        animator.runtimeAnimatorController = avatar1Animations as RuntimeAnimatorController;
        currentAvatar = avatar1.gameObject;

		avatar2.gameObject.SetActive (false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            this.SwapCharacter();
            
        }
    }

    public Animator GetAnimator(){
        return currentAvatar.GetComponent<Animator>();
    }
    public void SwapCharacter (){
        Timer slider = GetComponent<Timer>();
        slider.resetTimer();

        
        // processing whichAvatarIsOn variable
		    switch (whichAvatarIsOn) {

		        // if the first avatar is on
		        case 1:

			        // then the second avatar is on now
			        whichAvatarIsOn = 2;
                    currentAvatar = avatar2.gameObject;
                    animator.runtimeAnimatorController = avatar2Animations as RuntimeAnimatorController;
			        // disable the first one and anable the second one
			        avatar1.gameObject.SetActive (false);
			        avatar2.gameObject.SetActive (true);
			        break;

		        // if the second avatar is on
		        case 2:

			        // then the first avatar is on now
			        whichAvatarIsOn = 1;
                    currentAvatar = avatar1.gameObject;
                    animator.runtimeAnimatorController = avatar1Animations as RuntimeAnimatorController;
			        // disable the second one and anable the first one
			        avatar1.gameObject.SetActive (true);
			        avatar2.gameObject.SetActive (false);
			        break;
		    }
    }

    public int getCurrentCharacter(){
        return whichAvatarIsOn;
    }
}
