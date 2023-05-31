using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
    [SerializeField]
    private Sprite uncompleteSprite;
    [SerializeField]
    private Sprite completeSprite;

    [SerializeField]
    private float timeBeforeFadeIn = 0.1f;
    [SerializeField]
    private float timeBetweenFadeIn = 0.02f;

    public ItemType questItem;


    private Image image;
    private bool complete = false;
    private bool fadeInImage = false;
    // Start is called before the first frame update
    void Awake()
    {
        image = GetComponent<Image>();
        image.sprite = uncompleteSprite;
    }

    private void OnEnable()
    {
        image = GetComponent<Image>();
        if (!complete && ProgressManager.Instance.IsQuestCompleted(questItem)) {
            complete = true;

            fadeInImage = true;
            image.color = new Color(1, 1, 1, 0);
            image.sprite = completeSprite;
        }
        if(complete && fadeInImage)
        {
            fadeInImage = false;
            StartCoroutine(FadeIn());
            
        }
    }
    private void OnDisable()
    {
        if(complete && !fadeInImage)
        {
            image.color = new Color(1, 1, 1, 1);
        }
    }
    IEnumerator FadeIn()
    {
        yield return new WaitForSecondsRealtime(timeBeforeFadeIn);
        for (float i = 0; i <= 1; i += 0.1f)
        {
            Debug.Log("Fading In");
            // set color with i as alpha
            image.color = new Color(1, 1, 1, i);
            yield return new WaitForSecondsRealtime(timeBetweenFadeIn);
        }
        image.color = new Color(1, 1, 1, 1);
    }

    public void Complete()
    {
        complete = true;

        fadeInImage = true;
    }
}


