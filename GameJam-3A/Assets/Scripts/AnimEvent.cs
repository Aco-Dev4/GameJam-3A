using UnityEngine;

public class AnimEvent : MonoBehaviour
{
    private Pickaxe pickaxe;

    private void Awake()
    {
        pickaxe = GetComponentInChildren<Pickaxe>();
    }

    public void AllowToMine()
    {
        pickaxe.canMine = true;
    }

    public void DisallowToMine()
    {
        pickaxe.canMine = false;
    }
}
