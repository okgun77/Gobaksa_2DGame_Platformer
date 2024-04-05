using UnityEngine;

public class ItemProjectile : ItemBase
{
    public override void UpdateCollision(Transform _target)
    {
        _target.GetComponent<PlayerData>().CurrentProjectile++;
    }
}
