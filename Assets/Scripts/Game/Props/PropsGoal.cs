using UnityEngine;

public class PropsGoal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D _collision)
    {
        if (_collision.CompareTag("Player"))
        {
            // Debug.Log("Game Clear");
            _collision.GetComponent<PlayerController>().LevelComplete();
        }
    }
}
