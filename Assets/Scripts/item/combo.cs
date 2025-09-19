using UnityEngine;

public class combo : MonoBehaviour
{
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        if (audioSource != null)
        {
            Debug.Log("»ç¿îµå");
            audioSource.Play();
        }
    }
    void Start()
    {
        //audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
