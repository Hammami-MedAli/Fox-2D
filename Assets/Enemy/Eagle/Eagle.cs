using System.Text;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Eagle : MonoBehaviour
{
    [SerializeField] private float flyingDistance;
    [SerializeField] private float flyingSpeed;
    Rigidbody2D body;
    private float startPosition;
    private Animator anim;
    private bool isDead;
    [SerializeField] private float stompJumpPower = 25f;
    [SerializeField] private AudioClip hurt;
    [SerializeField] private AudioClip playerHurt;
    private float direction;
    private void Awake()
    { 
        body = GetComponent<Rigidbody2D>();
        startPosition = body.position.y;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
            return;
        body.linearVelocity = new Vector2(body.linearVelocity.x, flyingSpeed);
        if (body.position.y > startPosition + flyingDistance) 
        {
            flyingSpeed = -Mathf.Abs(flyingSpeed);
        }
        else if (body.position.y < startPosition)
        {
            flyingSpeed = Mathf.Abs(flyingSpeed);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        bool stomped = false;
        if (!(collision.gameObject.CompareTag("Player")))
            return;
        Rigidbody2D player = collision.gameObject.GetComponent<Rigidbody2D>();
        Animator animPlayer = collision.gameObject.GetComponent<Animator>();
        foreach (ContactPoint2D contact in collision.contacts)
        {
            Vector2 normal = contact.normal;

            if (normal.y < -0.5f && player.linearVelocity.y < 0f)
            {
                stomped = true;
                break;
            }
        }

        if (stomped)
        {
            SoundManager.instance.PlaySound(hurt);
            isDead = true;
            Destroy(gameObject, 0.5f);
            body.linearVelocity = Vector2.zero;
            anim.SetTrigger("death");
            body.bodyType = RigidbodyType2D.Kinematic;
            GetComponent<Collider2D>().enabled = false;
            player.AddForce(Vector2.up * stompJumpPower, ForceMode2D.Impulse);
        }
        else
        {
            direction = player.transform.position.x - gameObject.transform.position.x;
            direction = Mathf.Sign(direction);
            SoundManager.instance.PlaySound(playerHurt);
            PlayerMovement.Instance.Die(direction);
        }
    }
}
