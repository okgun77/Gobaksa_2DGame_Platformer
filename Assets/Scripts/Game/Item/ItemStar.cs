using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStar : ItemBase
{
    [Tooltip("별 아이템은 한 맵에 항상 3개를 배치하고 0, 1, 2 순번 부여")]
    [SerializeField][Range(0, 2)]
    private int starIndex;

    public override void UpdateCollision(Transform _target)
    {
        _target.GetComponent<PlayerData>().GetStar(starIndex);
    }
}
