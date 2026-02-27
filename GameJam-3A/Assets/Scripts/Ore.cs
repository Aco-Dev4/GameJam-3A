using System;
using UnityEngine;

public class Ore : MonoBehaviour
{
    public int value;
    public int health;

    public void HitOre(int damage)
    {
        health -= damage;
        transform.localScale *= 0.9f;
        if (health <= 0)
            Mined();
    }

    public void Mined()
    {
        GameObject.Destroy(gameObject);
        // Play sound
        // Add to inventory
    }
}
