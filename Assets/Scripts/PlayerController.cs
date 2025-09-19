using UnityEngine;

// PlayerController는 플레이어 캐릭터로서 Player 게임 오브젝트를 제어한다.
public class PlayerController : MonoBehaviour {
    public AudioClip deathClip; // 사망시 재생할 오디오 클립
    public AudioClip defaultClip;
    public AudioClip hitClip; // 충돌시 재생할 오디오 클립
    public float jumpForce = 700f; // 점프 힘
    public float speed = 5;


   private int jumpCount = 0; // 누적 점프 횟수
   private bool isGrounded = false; // 바닥에 닿았는지 나타냄
   private bool isDead = false; // 사망 상태

   private Rigidbody2D playerRigidbody; // 사용할 리지드바디 컴포넌트
   private Animator animator; // 사용할 애니메이터 컴포넌트
   private AudioSource playerAudio; // 사용할 오디오 소스 컴포넌트

   private void Start() {
        // 초기화
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
   }

   private void Update() {
        // 사용자 입력을 감지하고 점프하는 처리
        if (isDead)
        {
            return;
        }

        //좌 우 조작이니 x만 있으면 됨
        //float xInput = Input.GetAxis("Horizontal");
        float xInput = Input.acceleration.x; // 자이로
        float xSpeed = xInput * speed;
        //Vector2 currentVelocity = playerRigidbody.linearVelocity;
        //Vector2 newVelocity = new Vector3(xSpeed, currentVelocity.y);
        //playerRigidbody.linearVelocity = newVelocity;

        playerRigidbody.linearVelocity = new Vector2(xSpeed, playerRigidbody.linearVelocity.y);


        // 점프 부분
        // 마우스 왼쪽 버튼, 최대 2번 점프
        if(Input.GetMouseButtonDown(0) && jumpCount < 2)
        {
            playerAudio.clip = defaultClip;
            jumpCount++;
            playerRigidbody.linearVelocity = Vector2.zero;
            playerRigidbody.AddForce(new Vector2(0, jumpForce));
            playerAudio.Play();
        }
        else if(Input.GetMouseButtonUp(0) && playerRigidbody.linearVelocity.y > 0)
        {
            // 점프 버튼을 오래 누르고 있으면 상대적으로 높이 점프한다
            // 즉, 마우스 버튼에서 손을 떼는 순간, 위로 향하는 힘을 1/2로 만듬
            playerRigidbody.linearVelocity = playerRigidbody.linearVelocity * 0.5f;
        }
        animator.SetBool("Grounded", isGrounded);

    }

    private void Die() {
        // 사망 처리
        Debug.Log("Die");
        animator.SetTrigger("Die");

        playerAudio.clip = deathClip;
        playerAudio.Play();

        playerRigidbody.linearVelocity = Vector2.zero;
        isDead = true;
        GameManager.instance.OnPlayerDead();
   }

   private void OnTriggerEnter2D(Collider2D other) {
       // 트리거 콜라이더를 가진 장애물과의 충돌을 감지
       if(other.tag == "Dead" && !isDead)
        {
            Debug.Log("Die");
            Die();
        }
        if (other.tag == "etc")
        {
            playerAudio.clip = hitClip;
            playerAudio.Play();
        }
    }

   private void OnCollisionEnter2D(Collision2D collision) {
        // 바닥에 닿았음을 감지하는 처리
        if (collision.contacts[0].normal.y > 0.7f)
        {
            isGrounded = true;
            jumpCount = 0;

        }
   }

   private void OnCollisionExit2D(Collision2D collision) {
        // 바닥에서 벗어났음을 감지하는 처리
        isGrounded = false;
    }
}