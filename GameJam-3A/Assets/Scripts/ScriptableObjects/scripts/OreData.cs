using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Ore", menuName = "Object/Ore")]
public class OreData : ScriptableObject
{
    public int value;

    public float maxHealth;
}
