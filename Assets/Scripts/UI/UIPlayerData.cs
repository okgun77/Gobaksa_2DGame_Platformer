using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIPlayerData : MonoBehaviour
{
    [Header("HP")]
    [SerializeField]
    private Image[]         hpImages;

    [Header("COIN")]
    [SerializeField]
    private TextMeshProUGUI textCoin;

    [Header("PROJECTILE")]
    [SerializeField]
    private TextMeshProUGUI textProjectile;

    [Header("STAR")]
    [SerializeField]
    private GameObject[]    starObjects;


    public void SetupHP( int _index, bool _isActive)
    {
        hpImages[_index].color = _isActive == true ? Color.white : Color.black;
    }

    public void SetCoin(int _coinCount)
    {
        textCoin.text = $"x {_coinCount}";
    }

    public void SetProjectile(int _current, int _max)
    {
        textProjectile.text = $"{_current}/{_max}";

        if (((float)_current / _max) <= 0.3f)   textProjectile.color = Color.red;
        else                                    textProjectile.color = Color.white;
    }

    public void SetStar(int _index)
    {
        starObjects[_index].SetActive(true);
    }
}
