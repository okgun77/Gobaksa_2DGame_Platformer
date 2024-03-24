using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private KeyCode jumpKeyCode = KeyCode.C;

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
    }

    private void UpdateMove(float _x)
    {
        // 플레이어의 물리적 이동(좌/우)
        movement.MoveTo(_x);
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


}
