using UnityEngine;
using UnityEngine.SceneManagement;

public class TestingScript : MonoBehaviour
{
    public Ore ore1;
    public Ore ore2;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            ore1.HitOre(25);
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            ore2.HitOre(25);
        }
        if(Input.GetKeyDown(KeyCode.T))
        {
            SceneManager.LoadScene("Shop");
        }
    }
}
