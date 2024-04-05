using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D _collision)
    {
        Destroy(gameObject);
        if (_collision.CompareTag("Player"))
        {
            _collision.GetComponent<PlayerHP>().DecreaseHP();
        }
    }
}
