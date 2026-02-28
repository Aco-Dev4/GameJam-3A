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

    [Header("Time")]
    public int currentTime;

    [Header("Stats")]
    public int money;
    public int quotaTarget;
    public int quotaAmount;
    // UI Toolkit môže sledovať zmeny cez delegáty alebo jednoduchý refresh
    public System.Action OnDataChanged;

    public void UpdateValues(int m, int qt, int qa)
    {
        money = m;
        quotaTarget = qt;
        quotaAmount = qa;
        OnDataChanged?.Invoke();
    }
}
