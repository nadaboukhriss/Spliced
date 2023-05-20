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


    private Player player;
    private SwapCharacters swap;

    public void Start()
    {
        player = GameManager.Instance.player.GetComponent<Player>();
        swap = GameManager.Instance.player.GetComponent<SwapCharacters>();
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

