using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapCharacters : MonoBehaviour
{
    // referenses to controlled game objects
	public GameObject avatar1, avatar2;
    private GameObject timer;

    // variable contains which avatar is on and active
	int whichAvatarIsOn = 1;

    // Start is called before the first frame update
    void Start()
    {
        // enable first avatar and disable another one
		avatar1.gameObject.SetActive (true);
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


    public void SwapCharacter (){
        timer = GameObject.FindGameObjectWithTag("Timer");
        Timer slider = timer.GetComponent<Timer>();
        slider.resetTimer();
        // processing whichAvatarIsOn variable
		    switch (whichAvatarIsOn) {

		        // if the first avatar is on
		        case 1:

			        // then the second avatar is on now
			        whichAvatarIsOn = 2;

			        // disable the first one and anable the second one
			        avatar1.gameObject.SetActive (false);
			        avatar2.gameObject.SetActive (true);
			        break;

		        // if the second avatar is on
		        case 2:

			        // then the first avatar is on now
			        whichAvatarIsOn = 1;

			        // disable the second one and anable the first one
			        avatar1.gameObject.SetActive (true);
			        avatar2.gameObject.SetActive (false);
			        break;
		    }
    }
}
