using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Object/Player")]
public class PlayerData : ScriptableObject
{
    [Header("Movement")]
    public float speed = 20.0f;
    public float gravity = -20.0f;
    public float jumpHeight = 1.2f;

    [Header("Mining")]
    public float miningSpeed = 1f;
    public float lightEmit = 1f;
    public float miningCooldown = 1.0f; // Ako dlho trvá jeden úder
}
