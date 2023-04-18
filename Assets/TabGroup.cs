using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{

    [SerializeField]
    private int defaultTab;

    [SerializeField]
    private List<TabButton> tabButtons = new List<TabButton>();

    [SerializeField]
    private List<GameObject> pagesToSwap = new List<GameObject>();

    [SerializeField]
    private Sprite idleSprite;
    [SerializeField]
    private Sprite hoverSprite;
    [SerializeField]
    private Sprite activeSprite;


    private int currentTab;

    private void Awake()
    {
        currentTab = defaultTab;
    }
    private void Start()
    {
        OnTabSelected(tabButtons[defaultTab]);
    }
    public void OnTabEnter(TabButton button)
    {
        ResetTabs();
        int index = button.transform.GetSiblingIndex();
        if (index != currentTab) {
            button.background.sprite = hoverSprite;
        }
    }
    public void OnTabExit(TabButton button)
    {
        ResetTabs();
    }
    public void NextTab()
    {
        int newIndex = (currentTab + 1) % tabButtons.Count;
        OnTabSelected(tabButtons[newIndex]);
    }

    public void PrevTab()
    {
        int newIndex = ((currentTab - 1) + tabButtons.Count) % tabButtons.Count;
        OnTabSelected(tabButtons[newIndex]);
    }
    public void OnTabSelected(TabButton button)
    {
        if(tabButtons[currentTab] != null)
        {
            tabButtons[currentTab].Deselect();
        }
        int index = button.transform.GetSiblingIndex();
        currentTab = index;
        button.Select();
        ResetTabs();
        button.background.sprite = activeSprite;

        
        for(int i = 0; i < pagesToSwap.Count; i++)
        {
            if(i == currentTab)
            {
                pagesToSwap[i].SetActive(true);
            }
            else
            {
                pagesToSwap[i].SetActive(false);
            }
        }
    }

    public void ResetTabs()
    {
        foreach (TabButton button in tabButtons)
        {
            if (tabButtons[currentTab] != null && button == tabButtons[currentTab]) { continue; }
            button.GetComponent<Image>().sprite = idleSprite;
        }
    }

}
