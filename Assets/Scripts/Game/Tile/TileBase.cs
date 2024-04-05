using System.Collections;
using UnityEngine;

public class TileBase : MonoBehaviour
{
    [SerializeField]
    private bool    canBounce = false;     // Bounce 가능 여부
    private float   startpositionY;       // 타일의 최초 y 위치

    // 타일과 플레이어가 충돌했는지 체크 (일정시간동안 다시 충돌체크를 하지 않도록)
    public bool     IsHit { private set; get; } = false;

    private void Awake()
    {
        startpositionY = transform.position.y;
    }

    public virtual void UpdateCollision()
    {
        // Debug.Log($"{gameObject.name} 타일 충돌");

        if (canBounce == true)
        {
            IsHit = true;

            StartCoroutine(nameof(OnBounce));
        }
    }

    private IEnumerator OnBounce()
    {
        float maxBounceAmount = 0.35f;  // 타일이 충돌해 올라가는 최대 높이

        yield return StartCoroutine(MoveToY(startpositionY, startpositionY + maxBounceAmount));

        yield return StartCoroutine(MoveToY(startpositionY + maxBounceAmount, startpositionY));

        IsHit = false;
    }

    private IEnumerator MoveToY(float _start, float _end)
    {
        float percent = 0;
        float bounceTime = 0.2f;

        while ( percent < 1 )
        {
            percent += Time.deltaTime / bounceTime;

            Vector3 position = transform.position;
            position.y = Mathf.Lerp(_start, _end, percent);
            transform.position = position;

            yield return null;
        }
    }
}
