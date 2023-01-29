using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rig;
    public float moveSpeed;
    public float jumpForce;

    private bool isGrounded;

    public int score;

    public TextMeshProUGUI scoreText;


    private void Start()
    {
        scoreText.text = "Score: " + score.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal") * moveSpeed;
        float z = Input.GetAxisRaw("Vertical") * moveSpeed;

        rig.velocity = new Vector3(x,rig.velocity.y,z);


        Vector3 vel = rig.velocity;
        vel.y= 0;
        if(vel.x != 0 || vel.z != 0 )
        {
            //set player forward direction to velocity direction
            transform.forward = vel;
        }
        

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rig.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        if(transform.position.y < -5)
        {
            GameOver();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.GetContact(0).normal == Vector3.up)
        {
            isGrounded = true;
        }
    }

    public void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void AddScore(int amount)
    {
        score += amount;
        //update score text UI tbd
        scoreText.text = "Score: " + score.ToString();
    }
}
