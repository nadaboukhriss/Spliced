using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class LevelSystem: MonoBehaviour
{
    [SerializeField]
    private LevelSystemUI levelSystemUI;

    public event EventHandler OnTrustChanged;
    public event EventHandler OnLevelChanged;
    private int level;
    private float trust;
    private float trustToNextLevel;

    public void Start()
    {
        level = 0;
        trust = 0f;
        trustToNextLevel = 100f;

        levelSystemUI.SetLevelSystem(this);
    }

    public void ChangeTrust(float amount)
    {
        trust += amount;
        if (trust <= 0)
        {
            trust = 0;
        }
        while (trust >= trustToNextLevel)
        {
            LevelUp();
            trust -= trustToNextLevel;
            trustToNextLevel = (int)(trustToNextLevel * 1.5f);
            if (OnLevelChanged != null) OnLevelChanged(this, EventArgs.Empty);
        }
        if (OnTrustChanged != null) OnTrustChanged(this, EventArgs.Empty);
    }
    public void LevelUp()
    {
        GetComponent<SwapCharacters>().ModifyMaxTime(1.5f);
        SkillTree.Instance.ChangeSkillPoints(1);
        level++;
    }

    public int GetLevel()
    {
        return level;
    }

    public float GetTrustNormalized()
    {
        return trust / trustToNextLevel;
    }
}
