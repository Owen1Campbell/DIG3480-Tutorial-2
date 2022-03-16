using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;
    public float jumpForce = 3f;

    public Text score;
    public GameObject winMsg;

    // variables for ground checking are included, but unused to enable wall jumping
    public Transform groundCheck;
    private bool isGrounded;
    public float checkRadius;
    public LayerMask allGround;

    private int scoreValue = 0;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        winMsg.SetActive(false);
    }
    
    private void Update()
    {
        if (scoreValue == 4)
        {
            winMsg.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, allGround);
    }

    private void OnTriggerEnter2D(Collider2D collision) // using OnTrigger instead of OnCollision prevents coins from stopping the player's movement
    {
        if (collision.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse); //the 3 in this line of code is the player's "jumpforce," and you change that number to get different jump behaviors.  You can also create a public variable for it and then edit it in the inspector.
            }
        }
    }
}