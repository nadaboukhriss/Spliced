using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressManager : MonoBehaviour
{
    public static ProgressManager Instance;

    [SerializeField]
    private QuestUIGroup questUIGroup;
    private Dictionary<ItemType, bool> progress;

    private int curr = 0;
    public void Awake()
    {
        Instance = this;
    }
    public void Start()
    {
        progress = new Dictionary<ItemType, bool>();
        foreach (ItemType k in Enum.GetValues(typeof(ItemType)))
        {
            progress.Add(k, false);
        }
    }

    public void CompleteQuest(ItemType qi)
    {
        progress[qi] = true;
        //questUIGroup.CompleteQuest(qi);

    }

    public bool IsQuestCompleted(ItemType qi)
    {
        return progress[qi];
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            CompleteQuest((ItemType)curr);
            curr += 1;
        }
    }


}

public enum QuestItem
{
    BlueJewel,
    GoldenChain,
    SilverPlates,
    RedJewel,
    Drops,
    FinalAmulet,
}
