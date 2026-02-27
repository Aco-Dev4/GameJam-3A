using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    float speed;
    public int offset;
    bool moving;
    public bool goesUp;
    Vector3 newpos;
    void Start()
    {
        speed = 0;
        moving = false;
        newpos = transform.position;
        if (goesUp)
            newpos.y += offset;
        else
            newpos.y -= offset;
    }
    void Update()
    {
        if (moving)
        {
            speed += 0.0005f;
            transform.position = Vector3.Lerp(transform.position, newpos, Time.deltaTime * speed);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        print("Collided with " + other.name);
        if (other.CompareTag("Player"))
        {
            moving = true;
            Invoke("Transition", 3f);
        }
    }
    void Transition()
    {
        // SceneManager.LoadScene(0);
        // Tu potom pridajte kod pre prechod do novej sceny + animaciu prechodovej obrazovky
    }
}
