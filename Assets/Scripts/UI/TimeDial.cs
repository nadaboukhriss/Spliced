using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeDial : MonoBehaviour
{
    [SerializeField]
    private Transform dialTransform;
    [SerializeField]
    private float flashAngle = 30f;
    [SerializeField]
    private float dialRange = 360f;


    private Player player;
    private PlayerController playerController;
    private SwapCharacters swap;
    bool flashing = false;

    private Coroutine coroutine = null;
    public void Start()
    {
        player = GameManager.Instance.player.GetComponent<Player>();
        swap = GameManager.Instance.player.GetComponent<SwapCharacters>();
        playerController = GameManager.Instance.player.GetComponent<PlayerController>();
    }
    // Update the dial rotation based on the distribution of time between the two personalities
    public void UpdateDial(float time1, float time2,float maxDiff)
    {
        float timeDiff = time2 - time1;
        float normalizedDiff = timeDiff / maxDiff;
        float angle = 180 * normalizedDiff + 180;


        if(angle > 360) angle = 360;
        if(angle < 0) angle = 0;

        
        if(((angle > 360-flashAngle && !playerController.isHuman()) || (angle < flashAngle && playerController.isHuman()))){
            // flash the dial red
            if(!flashing){
                flashing = true;
                coroutine = StartCoroutine(FlashDial());
            }
                
        }
        else if(flashing){
            // stop the flashing
            StopCoroutine(coroutine);
            flashing = false;
            GetComponent<Image>().color = Color.white;
        }
        // set the rotation of the dial
        dialTransform.localRotation = Quaternion.Euler(0f, 0f, angle);

    }

    //IEnumerator that flash the dial red when the player is out of time, i.e angle is close to -180 or 180
    public IEnumerator FlashDial(){

        while(true){
            GetComponent<Image>().color = Color.red;
            yield return new WaitForSeconds(0.5f);
            GetComponent<Image>().color = Color.white;
            yield return new WaitForSeconds(0.5f);
        }
    }


}

