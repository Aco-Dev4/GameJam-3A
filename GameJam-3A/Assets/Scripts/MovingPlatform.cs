using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float moveHeight = 3f;
    public float moveSpeed = 2f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * moveSpeed) * moveHeight;
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }
}