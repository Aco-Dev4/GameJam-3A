using UnityEngine;

public class Pickaxe : MonoBehaviour
{
    public int damage;

    private void OnTriggerEnter(Collider other)
    {
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
