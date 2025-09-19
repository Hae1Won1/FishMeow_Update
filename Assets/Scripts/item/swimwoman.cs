using UnityEngine;

public class swimwoman : MonoBehaviour
{
    public GameObject effect;

    public Vector2 direction = Vector2.left;
    private Animator animator;
    bool effectOn = false;
    float time = 0f;
    void Update()
    {
        time += Time.deltaTime;
        // transform.Translate(direction * speed * Time.deltaTime);
        if (effectOn && time >= 0.3f)
        {

            effect.SetActive(false);
        }
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player"))
        {
            // 고양이가 위에 있으면 contactDirection.y > 0.5
            //Vector2 contactDirection = (other.transform.position - transform.position).normalized;

                Debug.Log("고양이가 위에서 밟음!");

            /*
                Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 knockback = direction.normalized * 700f + Vector2.up * 200f;
                    rb.AddForce(knockback);
                }
           */
                if (animator != null)
                {
                    animator.SetTrigger("iscat");
                    effect.SetActive(true);
                    effectOn = true;
            }

        }
    }
}
