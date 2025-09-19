using UnityEngine;

public class fishman : MonoBehaviour
{
    bool isAttacked = false;
    float stayTimer = 0;
    Animator animator;
    //public float speed = 3f;
    public Vector2 direction = Vector2.left; // �����ʿ��� ���ƿ��� -1
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("attack");
        if (!collision.CompareTag("Player")) return;

        stayTimer += Time.deltaTime;

        if (stayTimer >= 0.4f)
        {
            isAttacked = true;

            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                SpriteRenderer sr = GetComponentInChildren<SpriteRenderer>();
                if (sr != null)
                {
                    sr.flipX = true; // �� �̷��� �ؾ� �¿� ������
                }

                animator.SetTrigger("iscat");
                // �÷��̾ �ڷ� �о
                rb.AddForce(new Vector2(-direction.x * 700f, 200f));
            }

            
        }

    }
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }


    void Update()
    {
        //transform.Translate(direction * speed * Time.deltaTime);
    }
}
