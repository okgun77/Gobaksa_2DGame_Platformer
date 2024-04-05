using UnityEngine;

public class ItemInvincibility : ItemBase
{
    [SerializeField]
    private float invincibilityTime = 3;

    public override void UpdateCollision(Transform _target)
    {
        _target.GetComponent<PlayerHP>().OnInvincibility(invincibilityTime);
    }
}
