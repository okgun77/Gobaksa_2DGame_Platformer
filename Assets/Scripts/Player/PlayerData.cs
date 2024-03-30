using UnityEngine;

public class PlayerData : MonoBehaviour
{
    private int     coin        = 0;
    private int     projectile  = 0;
    private bool[]  stars       = new bool[3] { false, false, false };

    public int Coin
    {
        set => coin = Mathf.Clamp(value, 0, 9999);
        get => coin;
    }

    // get만 가능한 property이기 때문에 readonly/const와 같이 수정 불가능
    public int MaxProjectile { get; } = 10;

    public int CurrentProjectile
    {
        set => projectile = Mathf.Clamp(value, 0, MaxProjectile);
        get => projectile;
    }

    public void GetStar(int _index)
    {
        stars[_index] = true;
    }

}
