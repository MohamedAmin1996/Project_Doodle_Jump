using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerBehaviour : MonoBehaviour
{
    private Rigidbody2D rb;
    private float moveInput;
    public float speed = 10.0f;
    
    public int negativeOffsetX = -3;
    public int positiveOffsetX = 3;
    
    private int deadLayer = 8;

    public Text scoreText;
    private float topScore = 0.0f;

    public string sceneName;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    { 
        flip();
        relocate();
        highScore();
    }

    void FixedUpdate()
    {
        move();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.layer == deadLayer)
        {
            die();
        }
    }

    void move()
    {
        moveInput = Input.GetAxis("Horizontal") * speed;
        Vector2 velocity = rb.velocity;
        velocity.x = moveInput;
        rb.velocity = velocity;
    }

    void flip()
    {
        if (moveInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (moveInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    void relocate()
    {
        if (transform.position.x > positiveOffsetX)
        {
            Vector3 newPos = new Vector3(negativeOffsetX, transform.position.y, transform.position.z);
            transform.position = newPos;
        }
        else if(transform.position.x < negativeOffsetX)
        {
            Vector3 newPos = new Vector3(positiveOffsetX, transform.position.y, transform.position.z);
            transform.position = newPos;
        }
    }
    void die()
    {
        SceneManager.LoadScene(sceneName);
    }

    void highScore()
    {
        if (rb.velocity.y > 0 && transform.position.y > topScore)
        {
            topScore = transform.position.y;
        }
        scoreText.text = "Score: " + Mathf.Round(topScore).ToString();
    }
}
