using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    private MovementRigidbody2D movement;
    private float               originSpeed;

    public void Setup(int _direction)
    {
        movement = GetComponent<MovementRigidbody2D>();
        movement.MoveTo(_direction);

        originSpeed = Mathf.Abs(movement.Velocity.x);
    }

    private void Update()
    {
        if (movement.IsGrounded) movement.Jump();

        if (Mathf.Abs(movement.Velocity.x) < originSpeed)
        {
            Destroy(gameObject);
        }
    }
}
