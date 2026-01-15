using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;
    float horizontalMovement = 0f;
    public float jumpPower = 13f;
    public float runSpeed = 5f;
    Rigidbody2D body;
    private Animator anim;
    private bool Grounded;
    [SerializeField]private LayerMask groundLayer;
    private BoxCollider2D boxCollider;
    public CoinsManager cm;
    [SerializeField] private AudioClip jump;
    public Vector2 startPosition;
    [SerializeField] private GameObject WinMenu;
    [SerializeField] private GameObject LostMenu;
    [SerializeField] private AudioClip winGame;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMovement = Input.GetAxis("Horizontal");
        Walk();
        anim.SetBool("run", horizontalMovement != 0f);
        Jump();
        anim.SetBool("grounded", isGrounded());
        if (IsCrouched())
        {
            body.linearVelocityX = 0f;
        }
        anim.SetBool("Crouch", IsCrouched());

    }
    private void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && isGrounded())
        {
            SoundManager.instance.PlaySound(jump);
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpPower);
            anim.SetTrigger("grounded");
        }
    }

    private void Walk()
    {
        if (horizontalMovement > 0.1f)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            body.linearVelocity = new Vector2(horizontalMovement * runSpeed, body.linearVelocity.y);
        }
        else if (horizontalMovement < -0.1f)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            body.linearVelocity = new Vector2(horizontalMovement * runSpeed , body.linearVelocity.y);
        }
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool IsCrouched()
    {
        return Input.GetKey(KeyCode.LeftShift);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            cm.CoinsCounter = cm.CoinsCounter + 1;  
        }
        if (collision.gameObject.CompareTag("Win"))
        {
            SoundManager.instance.PlaySound(winGame);
            StartCoroutine(WinGame());
        }
    }
    public IEnumerator Respawn()
    {
        yield return new WaitForSeconds(1f);
        transform.position = startPosition;
        anim.Play("Idle");
    }
    public void Die(float direct)
    { 
        anim.SetTrigger("death");
        body.linearVelocity = Vector2.zero;
        body.AddForce(new Vector2(direct * 20f,0f), ForceMode2D.Impulse);
        StartCoroutine(Menu());
        
    }
    
    private IEnumerator Menu()
    {
        yield return new WaitForSeconds(1.7f);
        LostMenu.SetActive(true);
        Time.timeScale = 0;
    }
    private IEnumerator WinGame()
    {
        yield return new WaitForSeconds(1.7f);
        WinMenu.SetActive(true);
        Time.timeScale = 0;
    }
}
