using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour
{
    [SerializeField]
    public bool CanInteractWithAelia = true;
    [SerializeField]
    public bool CanInteractWithAsh = true;

    private Rigidbody2D rigidbody2d;
    private PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.Instance.player.GetComponent<PlayerController>();
        rigidbody2d = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (player.IsPlayingAelia() && !CanInteractWithAelia) rigidbody2d.bodyType = RigidbodyType2D.Static;
        else if (player.IsPlayingAsh() && !CanInteractWithAsh) rigidbody2d.bodyType = RigidbodyType2D.Static;
        else rigidbody2d.bodyType = RigidbodyType2D.Dynamic;
    }
}
