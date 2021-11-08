using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    Rigidbody2D rigidBody2D;

    public float runSpeed = 5f;
    public float jumpSpeed = 200f;

    public TextMeshProUGUI countText;

    public GameObject TicTac;
    public GameObject Lava;

    private int count;

    public SpriteRenderer spriteRenderer;
    public Animator animator;
    

    
    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        count = 0;
        SetCountText();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            int levelMask = LayerMask.GetMask("Level");

            if (Physics2D.BoxCast(transform.position, new Vector2(1, 0.1f), 0f, Vector2.down, 0.01f, levelMask))
            {
                Jump();
            }
        }
    }

    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector2 direction = new Vector2(horizontalInput * runSpeed * Time.deltaTime, 0);

        rigidBody2D.AddForce(direction);

        if( rigidBody2D.velocity.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }

        if ( Mathf.Abs(horizontalInput) > 0f)
        {
            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }
    }

    void Jump()
    {
        rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, jumpSpeed);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("TicTac"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }

        if (other.gameObject.CompareTag("Lava"))
        {
            SceneManager.LoadScene (0);
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if (count == 35)
        {
            countText.text = "YOU WON! You collected: " + count.ToString() + " Tic Tacs!";
        }
    }
}