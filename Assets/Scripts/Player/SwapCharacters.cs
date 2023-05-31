using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwapCharacters : MonoBehaviour
{
    // referenses to controlled game objects
	public GameObject avatar1, avatar2;
    private float maxTime = 0;
    [SerializeField]
    private TimeDial clock;
    [SerializeField]
    private float trustLostOnForceSwitch = 0f;
    private Animator animator;

    private PersonalityController2 currentAvatar;
    private GameObject timer;
    private PlayerController playerController;


    private float timeSpentAvatar1 = 1f;
    private float timeSpentAvatar2 = 1f;
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
        //currentAvatar = GetComponent<Personality1Controller>();

        playerController = GetComponent<PlayerController>();
        
		avatar2.gameObject.SetActive (false);
        maxTime = GetComponent<Timer>().swapTime;
    }
    public void OnSwitchCharacter(InputAction.CallbackContext ctx)
    {
        print("switch");
        if (ctx.started){
            this.SwapCharacter();
        }
        
    }

    public void ModifyMaxTime(float percentage)
    {
        maxTime *= percentage;
    }
    public void Update()
    {
        switch (whichAvatarIsOn)
        {
            // if the first avatar is on
            case 1:

                timeSpentAvatar1 += Time.deltaTime;
                break;
            // if the second avatar is on
            case 2:
                timeSpentAvatar2 += Time.deltaTime;
                break;
        }
        float timeDiff = Mathf.Abs(timeSpentAvatar1 - timeSpentAvatar2);
        if(timeDiff > maxTime)
        {
            //Forceful Switch
            SwapCharacter();
            GetComponent<Player>().ChangeTrust(-trustLostOnForceSwitch);
        }
        clock.UpdateDial(timeSpentAvatar1, timeSpentAvatar2, maxTime);
    }
    public void SwapCharacter (){
        Timer slider = GetComponent<Timer>();
        slider.resetTimer();
        playerController.SwitchAbilities();

        // processing whichAvatarIsOn variable
        switch (whichAvatarIsOn) {

		        // if the first avatar is on
		        case 1:
			        whichAvatarIsOn = 2;
			        break;
		        // if the second avatar is on
		        case 2:
			        // then the first avatar is on now
			        whichAvatarIsOn = 1;
			        break;
		    }
        animator.SetBool("isFox", whichAvatarIsOn == 2);
    }

    public PersonalityController2 GetCurrentPersonality()
    {
        return currentAvatar;
    }
    public float GetAvatar1Time()
    {
        return timeSpentAvatar1;
    }
    public float GetAvatar2Time()
    {
        return timeSpentAvatar2;
    }

    public int getCurrentCharacter(){
        return whichAvatarIsOn;
    }
}
