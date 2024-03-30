using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private StageData           stageData;
    [SerializeField]
    private KeyCode             jumpKeyCode = KeyCode.C;

    private MovementRigidbody2D movement;
    private PlayerAnimator      playerAnimator;


    private void Awake()
    {
        movement        = GetComponent<MovementRigidbody2D>();
        playerAnimator  = GetComponentInChildren<PlayerAnimator>();
    }

    private void Update()
    {
        // 키 입력 (좌우/ 방향키, x키)
        float x = Input.GetAxisRaw("Horizontal");
        float offset = 0.5f + Input.GetAxisRaw("Sprint") * 0.5f;

        // 걷기 일 땐 값의 범위가 -0.5 ~ 0.5
        // 뛰기 일 땐 값의 범위가 -1 ~ 1로 설정
        x *= offset;

        // 플레이어의 이동 제어 (좌/우)
        UpdateMove(x);

        // 플레이어의 점프 제어
        UpdateJump();

        // 플레이어 애니메이션 재생
        playerAnimator.UpdateAnimation(x);

        // 머리/발에 충돌한 오브젝트 처리
        UpdateAboveCollision();
        UpdateBelowCollision();

    }

    private void UpdateMove(float _x)
    {
        // 플레이어의 물리적 이동(좌/우)
        movement.MoveTo(_x);

        // 플레이어의 x축 이동 한계치 설정 (PlayerLimitMinX ~ PlayerLimitMaxX)
        float xPosition = Mathf.Clamp(transform.position.x, stageData.PlayerLimitMinX, stageData.PlayerLimitMaxX);
        transform.position = new Vector2(xPosition, transform.position.y);
    }

    private void UpdateJump()
    {
        if ( Input.GetKeyDown(jumpKeyCode) )
        {
            movement.Jump();
        }
        if ( Input.GetKey(jumpKeyCode) )
        {
            movement.IsLongJump = true;
        }
        else if ( Input.GetKeyUp(jumpKeyCode) )
        {
            movement.IsLongJump = false;
        }
    }

    private void UpdateAboveCollision()
    {
        if (movement.Velocity.y >= 0 && movement.HitAboveObject != null)
        {
            // 플레이어의 머리와 오브젝트가 충돌했기 때문에 y축 속력을 0으로 설정
            movement.ResetVelocityY();

            // 플레이어의 머리와 충돌한 오브젝트가 Tile일 때 Tile의 속성에 따라 충돌 처리
            if (movement.HitAboveObject.TryGetComponent<TileBase>(out var _tile) && !_tile.IsHit)
            {
                _tile.UpdateCollision();
            }
        }
    }

    private void UpdateBelowCollision()
    {
        if (movement.HitBelowObject != null)
        {
            // Platform_03_OneWay

            if (Input.GetKeyDown(KeyCode.DownArrow) && movement.HitBelowObject.TryGetComponent<PlatformEffectorExtension>(out var _p))
            {
                _p.OnDownWay();
            }
            if (movement.HitBelowObject.TryGetComponent<PlatformBase>(out var _platform))
            {
                _platform.UpdateCollision(gameObject);
            }
        }
    }
}
