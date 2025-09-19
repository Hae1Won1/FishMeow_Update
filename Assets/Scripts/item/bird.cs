
using UnityEngine;

public class bird : MonoBehaviour
{
    public GameObject effect;
    //public float speed = 3f;
    public Vector2 direction = Vector2.left; // 오른쪽에서 날아오면 -1
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Debug.Log("bird");
                time = 0;
                effect.SetActive(true);
                effectOn = true;
                // 플레이어를 뒤로 밀어냄
                rb.AddForce(new Vector2(-direction.x * 500f, 500f));
            }


        }
    }

}
