using NUnit.Framework;
using UnityEngine;

public class OreNode : MonoBehaviour
{
    public GameObject[] ores;

    private void Start()
    {
        int type = Mathf.RoundToInt(Random.Range(0, ores.Length));
        GameObject ore = Instantiate(ores[type], transform.position, Quaternion.identity);
    }
}
