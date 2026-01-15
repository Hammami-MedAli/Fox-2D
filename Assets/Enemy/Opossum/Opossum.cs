using Unity.VisualScripting;
using UnityEngine;

public class Opossum : MonoBehaviour
{
    [SerializeField] private float movingDistance;
    [SerializeField] private float movementSpeed;
    private Animator anim;
    private float startPosition;
    Rigidbody2D body;
    private bool isDead;
    [SerializeField] private float stompJumpPower = 20f;
    [SerializeField] private AudioClip hurt;
    [SerializeField] private AudioClip playerHurt;
    private float direction;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        startPosition = body.position.x;
        anim = GetComponent<Animator>();
    }
    // Update is called once per frame

    void Update()
    {
        if (isDead)
            return;
        body.linearVelocity = new Vector2(movementSpeed, body.linearVelocity.y);
        if (body.position.x > startPosition + movingDistance)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), 8, 8);
            movementSpeed = -Mathf.Abs(movementSpeed);
        }
        else if (body.position.x < startPosition)
        {  
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x),8, 8);
            movementSpeed = Mathf.Abs(movementSpeed);
        
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
        
        if (stomped) {
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
            Debug.Log(direction);
            SoundManager.instance.PlaySound(playerHurt);
            PlayerMovement.Instance.Die(direction);
        }
    }
}
