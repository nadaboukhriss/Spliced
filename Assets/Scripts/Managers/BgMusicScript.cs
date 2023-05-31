using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgMusicScript : MonoBehaviour
{
    public static BgMusicScript BgMusicInstance; // singelton - so that only one background music object exists

    void Awake()
    {
        if (BgMusicInstance != null && BgMusicInstance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        BgMusicInstance = this;
        DontDestroyOnLoad(this);

    }
}
