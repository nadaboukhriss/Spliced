using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Object", menuName = "Inventory System/Items/Item")]
public class ItemObject : ScriptableObject
{
    public string itemName;
    public ItemType type;
    public Sprite icon;
    public bool hasDialogue;

    

    public bool isQuest = false;
    public Dialogue dialogue;
    [TextArea(15, 20)]
    public string description;

    public float trustGain = 0f;

    public void TriggerDialogue()
    {
        dialogue.TriggerDialogue(itemName, icon);
    }

    public void Collect(Player player)
    {
        SFXManager.sfxinstance.Audio.PlayOneShot(SFXManager.sfxinstance.PickupSound);
        player.ChangeTrust(trustGain);
        if (hasDialogue)
        {
            dialogue.TriggerDialogue(itemName, icon);
        }
        if (isQuest)
        {
            ProgressManager.Instance.CompleteQuest(type);
        }
        else
        {
            player.inventory.AddItem(this);
        }
    }
}

public enum ItemType
{
    Skull,
    KeyDungeon1,
    KeyDungeon2,
    BlueJewel,
    GoldenChain,
    SilverPlates,
    RedJewel,
    Drops,
    FinalAmulet
}
