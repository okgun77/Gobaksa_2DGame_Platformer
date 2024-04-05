using System.Collections;
using UnityEngine;

public abstract class ItemBase : MonoBehaviour
{
    [SerializeField]
    private Vector2     spawnForce = new Vector2(1, 7);
    [SerializeField]
    private float       aliveTimeAfterSpawn = 5;

    private bool        allowCollect = true;

    public void Setup()
    {
        StartCoroutine(nameof(SpawnItemProcess));
    }

    private IEnumerator SpawnItemProcess()
    {
        allowCollect = false;

        var rigid2D             = gameObject.AddComponent<Rigidbody2D>();
        rigid2D.freezeRotation  = true;
        rigid2D.velocity        = new Vector2(Random.Range(-spawnForce.x, spawnForce.x), spawnForce.y);

        while (rigid2D.velocity.y > 0)
        {
            yield return null;
        }

        allowCollect = true;
        GetComponent<Collider2D>().isTrigger = false;

        yield return new WaitForSeconds(aliveTimeAfterSpawn);

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        if (allowCollect && _collision.CompareTag("Player"))
        {
            UpdateCollision(_collision.transform);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D _collision)
    {
        if (allowCollect && _collision.transform.CompareTag("Player"))
        {
            UpdateCollision(_collision.transform);
            Destroy(gameObject);
        }
    }

    public abstract void UpdateCollision(Transform _target);
    
}
