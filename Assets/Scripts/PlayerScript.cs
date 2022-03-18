using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;
    public float jumpForce = 3f;

    public TextMeshProUGUI score;
    public TextMeshProUGUI lives;
    public GameObject winMsg;
    public Transform level2spawn;
    public SoundManager smgr;

    // variables for ground checking are included, but unused to enable wall jumping
    public Transform groundCheck;
    private bool isGrounded;
    public float checkRadius;
    public LayerMask allGround;

    private int scoreValue = 0;
    private int livesValue;


    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = "Score: " + scoreValue;
        winMsg.SetActive(false);
        livesValue = 3;
        lives.text = "Lives: " + livesValue.ToString();
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
            Destroy(collision.gameObject);
            UpdateScore();
        }

        if (collision.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            UpdateLives();
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

    void UpdateScore()
    {
        scoreValue += 1;
        score.text = "Score: " + scoreValue;

        if (scoreValue == 4)
        {
            transform.position = level2spawn.position;
            livesValue = 4;
            UpdateLives();
        }

        if (scoreValue == 9)
        {
            winMsg.SetActive(true);
            smgr.PlayWin();
        }
    }

    void UpdateLives()
    {
        livesValue -= 1;
        lives.text = "Lives: " + livesValue;
        if (livesValue == 0)
        {
            winMsg.GetComponent<TextMeshProUGUI>().text = "You Lose!";
            winMsg.SetActive(true);
            gameObject.SetActive(false); // setactive instead of destroy prevents errors in camera script which relies on player object
            smgr.PlayLose();
        }
    }
}