using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    public int diamondValue = 1;
    private Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            player.AddGems(diamondValue);
            Destroy(gameObject);
        }
    }

}
