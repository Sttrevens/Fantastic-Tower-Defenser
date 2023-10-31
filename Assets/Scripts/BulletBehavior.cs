using FSM;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float lifetime = 1.0f; // The bullet will destroy itself after this time
    BaseStateMachine stateMachine;
    private void Start()
    {
        // Destroy the bullet after 'lifetime' seconds
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the bullet collided with an enemys
        if (other.gameObject.CompareTag("Enemy"))
        {
            stateMachine = other.gameObject.GetComponent<BaseStateMachine>();
            stateMachine.BloodMinus();
            if (gameObject != null) { Destroy(gameObject); }
            

        }
    }
}