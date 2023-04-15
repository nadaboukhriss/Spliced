using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Slider timerSlider;
    public Text timerText;
    public float swapTime;
    public float swapSpeed;
    
    private SwapCharacters swap;
    private float timeSpent;
    private int it = 0;

    public void Awake(){
        swap = GetComponent<SwapCharacters>();
    }
    // Start is called before the first frame update
    void Start()
    {
        timerSlider.maxValue = swapTime;
        timerSlider.value = swapTime;
        timeSpent = 0;
        
    }

    void Update(){
        timeSpent += Time.deltaTime;
        
        if (timeSpent < swapTime){
            timerSlider.value = swapTime - timeSpent;
        }
        else{
            swap.SwapCharacter();
            //this.resetTimer();
        }
    }

    public void resetTimer(){
        timeSpent = 0;
        it+=1;
        timerSlider.value = swapTime;
    }
}
