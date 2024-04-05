using UnityEngine;

public class EnemyMushRoomAnimator : MonoBehaviour
{
    [SerializeField]
    private GameObject parent;

    public void OnDieEvent()
    {
        Destroy(parent);
    }
}
