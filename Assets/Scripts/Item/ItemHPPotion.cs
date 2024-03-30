using UnityEngine;

public class ItemHPPotion : ItemBase
{
    public override void UpdateCollision(Transform _target)
    {
        _target.GetComponent<PlayerHP>().IncreaseHP();
    }
}
