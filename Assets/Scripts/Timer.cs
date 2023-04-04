using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public GameObject player;
    public Slider timerSlider;
    public Text timerText;
    public float swapTime;
    public float swapSpeed;

    private bool timerEnded;
    private int timeSpent;
    private int it = 0;
    // Start is called before the first frame update
    void Start()
    {

        timerEnded = false;
        timerSlider.maxValue = swapTime;
        timerSlider.value = swapTime;
        timeSpent = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        timeSpent += 1;
        
        float  score = swapTime - timeSpent / swapSpeed;
        print(score);
        
        
        if (score >= 0){
            timerSlider.value = score;
        }
        else{
            player = GameObject.FindGameObjectWithTag("Player");
            SwapCharacters swap = player.GetComponent<SwapCharacters>();
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
