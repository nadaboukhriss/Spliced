using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDial : MonoBehaviour
{
    [SerializeField]
    private Transform dialTransform;
    [SerializeField]
    private float dialRange = 360f;

    // Update the dial rotation based on the distribution of time between the two personalities
    public void UpdateDial(float time1, float time2,float totalTime)
    {
        float maxDifference = 5;
        float timeDiff = time1 - time2;
        float normalizedDiff = timeDiff / maxDifference;
        float angle = 180 * normalizedDiff;

        // set the rotation of the dial
        dialTransform.localRotation = Quaternion.Euler(0f, 0f, angle);

    }


}

