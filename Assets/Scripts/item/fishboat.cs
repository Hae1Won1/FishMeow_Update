using UnityEngine;

public class fishboat : MonoBehaviour
{
    public float requiredStayTime = 0.05f;
    float stayTimer = 0f;
    bool rewarded = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (rewarded) return;
        if (!collision.CompareTag("Player")) return;

        stayTimer += Time.deltaTime;

        if (stayTimer >= requiredStayTime)
        {
            rewarded = true;

            GameManager.instance.AddScore(1);
            GameManager.instance.ComboScore();

            Debug.Log("FISH GET! +1");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            stayTimer = 0f;
        }
    }



}
