using UnityEngine;

public class ItemCoin : ItemBase
{
    public override void UpdateCollision(Transform _target)
    {
        _target.GetComponent<PlayerData>().Coin++;
    }
}
