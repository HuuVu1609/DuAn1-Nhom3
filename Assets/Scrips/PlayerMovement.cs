using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float Player_speed = 5f; // toc do player

    [Header("Jump")]
    [SerializeField] private float Player_jumpForce = 15f;  // luc tac dong khi nhay
    [SerializeField] private float jumpHoldForce = 10f; // luc cong them khi giu phim space
    [SerializeField] private float maxJumpHoldTime = 0.3f; // thoi gian khi giu phim space

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck; // diem check mat dat
    [SerializeField] private LayerMask groundLayer;
    
    [Header("Camera")]
    [SerializeField] private CameraShake cameraShake;

    private float horizontal;
    private bool _isGrounded;

    private Rigidbody2D rb;
    private Animator animator;
    private string currentAnimation;

    private bool isJumping = false;
    private float jumpHoldTimer = 0f;
    private AudioManager audioManager;
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // khoi tao animation
    }

    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal"); // Khoi tao nut di chuyen trai phai cho Player
        
        if (horizontal != 0)
        {
            transform.localScale = new Vector2(Mathf.Sign(horizontal), 1);
            ChangeAnimation("Run");
        }
        else
        {
            ChangeAnimation("Idle");
        }

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            isJumping = true;
            jumpHoldTimer = 0f;
            Jump();
        }
        
        if (Input.GetKey(KeyCode.Space) && isJumping)
        {
            if (jumpHoldTimer < maxJumpHoldTime && rb.linearVelocity.y > 0)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y + jumpHoldForce * Time.deltaTime);
                jumpHoldTimer += Time.deltaTime;
            }
        }
        
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }
        
        // Khi hoan thanh aniamtion Jump thi se chuyen sang Run hoac Idle
        if (_isGrounded && currentAnimation == "Jump")
        {
            ChangeAnimation(horizontal != 0 ? "Run" : "Idle");
        }
        if (!_isGrounded && rb.linearVelocity.y < 0)
        {
            ChangeAnimation("Down");
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * Player_speed, rb.linearVelocity.y);
        
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    // Quan li Animation cho character
    private void ChangeAnimation(string animation, float crossfade = 0.3f)
    {
        if (currentAnimation != animation)
        {
            currentAnimation = animation;
            animator.CrossFade(animation, crossfade);
        }
    }
    private void Jump()
    {
        ChangeAnimation("Jump");
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, Player_jumpForce);
         audioManager.PlayJumpAudio();
    }
    
    private IEnumerator DieAnimation(bool DieCheck = false) // trang thai khi player chet
    {
        cameraShake.Shake(0.2f, 0.2f); // Camera rung trong 2s khi palyer chet
        yield return new WaitForSeconds(0.5f);
        if (DieCheck)
        {
            for (int i = 0; i < 2; i++) 
            {
                transform.Rotate(0f, 0f, 90f); 
                yield return new WaitForSeconds(0.5f);
            }
        }
        yield return new WaitForSeconds(0.5f);
        UIManager.instance.ShowResults(); // Hien thi man hinh ket qua
    }

   

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Xu li va cham giua character voi trap
        if (other.gameObject.CompareTag("trap"))
        {
            Vector2 pushDirection = new Vector2(-3, 0);
            rb.AddForce(pushDirection * 200f, ForceMode2D.Impulse);
            StartCoroutine(DieAnimation(true));
            ChangeAnimation("Player_Die");
            
            BoxCollider2D collider = gameObject.GetComponent<BoxCollider2D>();
            collider.isTrigger = true;
            
            AudioManager.instance.PlayDieAudio();
        }

        if (other.gameObject.CompareTag("oneWayGround"))
        {
            CompositeCollider2D ground = other.gameObject.GetComponent<CompositeCollider2D>();
            OnWayGround.instance.GroundOneWay(ground);
        }
    }

    private void OnTriggerEnter2D(Collider2D tr)
    {
        // Xu li va cham giua Character voi Carrot
        if (tr.gameObject.CompareTag("Carrot"))
        {
            UIManager.instance.AddCarrot();
            Destroy(tr.gameObject);
            
            AudioManager.instance.PlayCarrotAudio();
        }
      
    }
}