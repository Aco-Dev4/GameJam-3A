using System;
using UnityEngine;

public class Ore : MonoBehaviour
{
    public int value;
    public int health;
    float downscale = 0.95f;
    public void HitOre(int damage)
    {
        health -= damage;
        transform.localScale *= downscale;
        if (health <= 0)
            Mined();
    }

    public void Mined()
    {
        GameManager.manager.AddToQuota(value);
        GameObject.Destroy(gameObject);
        // Play sound
    }
}
