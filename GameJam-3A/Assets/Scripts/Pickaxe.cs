using UnityEngine;

public class Pickaxe : MonoBehaviour
{
    public bool canMine = false;
    public int damage;

    private void OnTriggerEnter(Collider other)
    {
        if (!canMine) return;

        if (other.gameObject.CompareTag("Ore"))
        {
            OreHandler oreHandler = other.gameObject.GetComponent<OreHandler>();

            if (oreHandler != null)
            {
                oreHandler.HitOre(damage);
            }
        }

    }
}
