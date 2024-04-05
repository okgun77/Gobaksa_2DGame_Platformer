using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    private EnemyBase enemyBase;

    private void Awake()
    {
        enemyBase = GetComponentInParent<EnemyBase>();
    }

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        if (enemyBase.IsDie == true) return;
             
        if (_collision.CompareTag("Player"))
        {
            _collision.GetComponent<PlayerHP>().DecreaseHP();
        }
        else if (_collision.CompareTag("PlayerProjectile"))
        {
            enemyBase.OnDie();
            Destroy(_collision.gameObject);
        }
    }
}
