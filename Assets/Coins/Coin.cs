using Unity.Properties;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private AudioClip claim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SoundManager.instance.PlaySound(claim);
            anim.SetTrigger("claim");
            GetComponent<Collider2D>().enabled = false;
            Destroy(gameObject);
        }
    }
}
