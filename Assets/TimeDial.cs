using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeDial : MonoBehaviour
{
    [SerializeField]
    private Transform dialTransform;
    [SerializeField]
    private float dialRange = 360f;

    [SerializeField]
    private float greenZoneSize = 0.15f;
    [SerializeField]
    private float yellowZoneSize = 0.4f;
    [SerializeField]
    private float redZoneSize = 0.45f;


    private Player player;
    private SwapCharacters swap;

    public void Start()
    {
        player = GameManager.Instance.player.GetComponent<Player>();
        swap = GameManager.Instance.player.GetComponent<SwapCharacters>();
    }
    public void Update()
    {
        float time1 = swap.GetAvatar1Time();
        float time2 = swap.GetAvatar2Time();
        float difference = Mathf.Abs(time1 - time2);
        float normalizedDiff = difference / swap.maxTime;

        float greenZoneEnd = greenZoneSize;
        float yellowZoneEnd = greenZoneEnd + yellowZoneSize;
        float redZoneEnd = yellowZoneEnd + redZoneSize;

        float xpChangeRate = 0f;
        if (normalizedDiff <= greenZoneEnd)
        {
            // Hand is in green zone
            xpChangeRate = 1;
            float t = Mathf.InverseLerp(0, greenZoneEnd, normalizedDiff);
            xpChangeRate *= Mathf.Lerp(0f, 1f, t);
            //GetComponent<Image>().color = Color.green;
        }
        else if (normalizedDiff <= yellowZoneEnd)
        {
            // Hand is in yellow zone
            xpChangeRate = 0f;
            //GetComponent<Image>().color = Color.yellow;
        }
        else
        {
            // Hand is in red zone
            xpChangeRate = -1;
            float t = Mathf.InverseLerp(yellowZoneEnd, redZoneEnd, normalizedDiff);
            xpChangeRate *= Mathf.Lerp(0f, 1f, t);
            //GetComponent<Image>().color = Color.red;
        }

        // Apply XP changes
        float xpChange = xpChangeRate * Time.deltaTime;
        player.ChangeXP(xpChange);
    }
    // Update the dial rotation based on the distribution of time between the two personalities
    public void UpdateDial(float time1, float time2,float maxDiff)
    {
        float timeDiff = time2 - time1;
        float normalizedDiff = timeDiff / maxDiff;
        float angle = 180 * normalizedDiff - 180;

        // set the rotation of the dial
        dialTransform.localRotation = Quaternion.Euler(0f, 0f, angle);

    }


}

