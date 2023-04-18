using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwapCharacters : MonoBehaviour
{
    // referenses to controlled game objects
	public GameObject avatar1, avatar2;

    [SerializeField]
    private TimeDial clock;
    private Animator animator;

    private GameObject currentAvatar;
    private GameObject timer;


    private float timeSpentAvatar1 = 1f;
    private float timeSpentAvatar2 = 1f;
    private float timeSpentTotal = 2f;
    // variable contains which avatar is on and active
    int whichAvatarIsOn = 1;

    public void Awake(){
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        // enable first avatar and disable another one
        animator.SetBool("isFox", false);
		avatar1.gameObject.SetActive (true);
        // animator.runtimeAnimatorController = avatar1Animations as RuntimeAnimatorController;
        currentAvatar = avatar1.gameObject;

		avatar2.gameObject.SetActive (false);

    }
    public void OnSwitchCharacter(InputAction.CallbackContext ctx)
    {
        print("switch");
        if (ctx.started){
            this.SwapCharacter();
        }
        
    }

    public void FixedUpdate()
    {
        timeSpentTotal += Time.fixedDeltaTime;
        switch (whichAvatarIsOn)
        {
            // if the first avatar is on
            case 1:

                timeSpentAvatar1 += Time.fixedDeltaTime;
                break;
            // if the second avatar is on
            case 2:
                timeSpentAvatar2 += Time.fixedDeltaTime;
                break;
        }
        clock.UpdateDial(timeSpentAvatar1, timeSpentAvatar2, timeSpentTotal);
    }
    public Animator GetAnimator(){
        return currentAvatar.GetComponent<Animator>();
    }
    public void SwapCharacter (){
        Timer slider = GetComponent<Timer>();
        slider.resetTimer();
        animator.SetBool("isFox", whichAvatarIsOn == 2);

        // processing whichAvatarIsOn variable
        switch (whichAvatarIsOn) {

		        // if the first avatar is on
		        case 1:

			        // then the second avatar is on now
			        whichAvatarIsOn = 2;
                    currentAvatar = avatar2.gameObject;
                    //animator.runtimeAnimatorController = avatar2Animations as RuntimeAnimatorController;
			        // disable the first one and anable the second one
			        avatar1.gameObject.SetActive (false);
			        avatar2.gameObject.SetActive (true);
			        break;

		        // if the second avatar is on
		        case 2:

			        // then the first avatar is on now
			        whichAvatarIsOn = 1;
                    currentAvatar = avatar1.gameObject;
                    //animator.runtimeAnimatorController = avatar1Animations as RuntimeAnimatorController;
			        // disable the second one and anable the first one
			        avatar1.gameObject.SetActive (true);
			        avatar2.gameObject.SetActive (false);
			        break;
		    }
    }
}
