using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelSystemUI : MonoBehaviour
{

    [SerializeField]
    private TMP_Text levelText;
    [SerializeField]
    private Slider trustScoreSlider;
    private LevelSystem levelSystem;
    // Start is called before the first frame updat

    private void SetTrustBar(float trustNormalized)
    {
        trustScoreSlider.value = trustNormalized;
    }

    private void SetLevel(int level)
    {
        levelText.text = (level+1).ToString();
    }
    public void SetLevelSystem(LevelSystem levelSystem)
    {
        this.levelSystem = levelSystem;
        SetTrustBar(levelSystem.GetTrustNormalized());
        SetLevel(levelSystem.GetLevel());

        levelSystem.OnLevelChanged += LevelSystem_OnLevelChanged;
        levelSystem.OnTrustChanged += LevelSystem_OnTrustChanged;

    }

    private void LevelSystem_OnTrustChanged(object sender, System.EventArgs e)
    {
        SetTrustBar(levelSystem.GetTrustNormalized());
    }
    private void LevelSystem_OnLevelChanged(object sender, System.EventArgs e)
    {
        SetLevel(levelSystem.GetLevel());
    }
}
