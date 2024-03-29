using UnityEngine;

public class MovementRigidbody2D : MonoBehaviour
{
    [Header("[Layermask]")]
    [SerializeField] private LayerMask groundCheckLayer;            // 바닥 체크를 위한 충돌 레이어
    [SerializeField] private LayerMask aboveCollisionLayer;         // 머리 충돌 체크를 위한 레이어
    [SerializeField] private LayerMask belowCollisionLayer;         // 발 충돌 체크를 위한 레이어

    [Header("[Move]")]
    [SerializeField] private float      walkSpeed = 5;              // 걷는 속도
    [SerializeField] private float      runSpeed = 8;               // 뛰는 속도

    [Header("[Jump]")]
    [SerializeField] private float      jumpForce = 13;             // 점프 힘
    [SerializeField] private float      lowGravityScale = 2;        // 점프키를 오래 누르고 있을 때 적용되는 중력(높은 점프)
    [SerializeField] private float      highGravityScale = 3.5f;    // 일반적으로 적용되는 중력(낮은 점프)

    private float       moveSpeed;                                      // 이동 속도

    // 바닥에 착지 직전 조금 빨리 점프 키를 눌렀을 때 바닥에 착지하면 바로 점프가 되도록
    private float       jumpBufferTime = 0.1f;
    private float       jumpBufferCounter;

    // 낭떠러지에서 떨어질 때 아주 잠시 동안 점프가 가능하도록 설정하기 위한 변수
    private float       hangTime = 0.2f;
    private float       hangCounter;

    private Vector2     collisionSize;                              // 머리, 발 위치에 생성하는 충돌 박스 크기
    private Vector2     footPosition;                               // 발 위치
    private Vector2     headPosition;                               // 머리 위치

    private Rigidbody2D rigid2D;
    private Collider2D  collider2D;

    public bool         IsLongJump      { set; get; } = false;                  // 낮은 점프, 높은 점프 체크
    public bool         IsGrounded      { private set; get; } = false;          // 바닥 체크 (바닥에 닿아있을 때 true)
    public Collider2D   HitAboveObject  { private set; get; }                   // 머리에 충돌한 오브젝트 정보
    public Collider2D   HitBelowObject  { private set; get; }                   // 발에 충돌한 오브젝트 정보


    public Vector2 Velocity => rigid2D.velocity;


    private void Awake()
    {
        moveSpeed   = walkSpeed;

        rigid2D     = GetComponent<Rigidbody2D>();
        collider2D  = GetComponent<Collider2D>();
    }

    private void Update()
    {
        UpdateCollision();
        JumpHeight();
        JumpAdditive();
    }

    // x축 속력(velocity) 설정, 외부 클래스에서 호출
    public void MoveTo(float _x)
    {
        // _x의 절대값이 0.5이면 걷기(walkSPeed), 1이면 뛰기(runSpeed)
        moveSpeed = Mathf.Abs(_x) != 1 ? walkSpeed : runSpeed;

        // _x가 -0.5, 0.5의 값을 가질 때 _x를 -1, 1로 변경
        if (_x != 0) _x = Mathf.Sign(_x);

        // _x축 방향 속력을 _x * moveSpeed로 설정
        rigid2D.velocity = new Vector2(_x * moveSpeed, rigid2D.velocity.y);
    }

    // 충돌 체크
    private void UpdateCollision()
    {
        // 플레이어 오브젝트의 Collider2D min, center, max 위치 정보
        Bounds bounds   = collider2D.bounds;

        // 플레이어의 발에 생성하는 충돌 범위
        collisionSize   = new Vector2((bounds.max.x - bounds.min.x) * 0.5f, 0.1f);

        // 플레이어의 머리/발 위치
        headPosition    = new Vector2(bounds.center.x, bounds.max.y);
        footPosition    = new Vector2(bounds.center.x, bounds.min.y);

        // 플레이어가 바닥을 밟고 있는지 체크하는 충돌 박스
        IsGrounded      = Physics2D.OverlapBox(footPosition, collisionSize, 0, groundCheckLayer);

        // 플레이어의 머리/발에 충돌한 오브젝트 정보를 저장하는 충돌 박스
        HitAboveObject = Physics2D.OverlapBox(headPosition, collisionSize, 0, aboveCollisionLayer);
        HitBelowObject = Physics2D.OverlapBox(footPosition, collisionSize, 0, belowCollisionLayer);
    }

    // 다른 클래스에서 호출하는 점프 매서드, y축 점프
    public void Jump()
    {
        jumpBufferCounter = jumpBufferTime;
    }

    private void JumpHeight()
    {
        // 낮은 점프, 높은 점프 구현을 위한 중력 계수(gravityScale) 조절 (Jump Up일 때만 적용된다.)
        // 중력 계수가 낮은 if 문은 높은 점프가 되고, 중력 계수가 높은 else 문은 낮은 점프가 된다.
        if ( IsLongJump && rigid2D.velocity.y > 0 )
        {
            rigid2D.gravityScale = lowGravityScale;
        }
        else
        {
            rigid2D.gravityScale = highGravityScale;
        }

    }

    private void JumpAdditive()
    {
        // 낭떠러지에서 떨어질 때 아주 잠시동안 점프가 가능하도록 설정
        if (IsGrounded) hangCounter = hangTime;
        else            hangCounter -= Time.deltaTime;

        // 바닥 착지 직전 조금 빨리 점프 키를 눌렀을 때 바닥에 착지하면 바로 점프하도록 설정
        if ( jumpBufferCounter > 0 ) jumpBufferCounter   -= Time.deltaTime;

        if ( jumpBufferCounter > 0 && hangCounter > 0 )
        {
            // 점프 힘(jumpForce)만큼 y축 방향 속력으로 설정
            rigid2D.velocity    = new Vector2(rigid2D.velocity.x, jumpForce);
            jumpBufferCounter   = 0;
            hangCounter         = 0;    
        }
    }

    public void ResetVelocityY()
    {
        rigid2D.velocity = new Vector2(rigid2D.velocity.x, 0);
    }
}
