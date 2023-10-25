using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 5.0f;  // The speed at which the enemy moves to the left

    void Update()
    {
        // Move the enemy to the left
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }
}