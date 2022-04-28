/**** 
 * Created by: Bob Baloney
 * Date Created: April 20, 2022
 * 
 * Last Edited by: JP
 * Last Edited: 4/28
 * 
 * Description: Controls the ball and sets up the intial game behaviors. 
****/

/*** Using Namespaces ***/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    [Header("General Settings")]
    public int numberOfBalls = 3; //lives
    private int score = 0;
    public Paddle paddle;
    public Text ballTxt;
    public Text scoreTxt;
    private Rigidbody rb;
    private AudioSource audioSource;

    [Header("Ball Settings")]
    public float initialForce = 1f;
    public float speed = 1f;
    private bool isInPlay;




    //Awake is called when the game loads (before Start).  Awake only once during the lifetime of the script instance.
    void Awake()
    {
        rb = this.GetComponent<Rigidbody>(); //get rigidbody
        audioSource = this.GetComponent<AudioSource>(); //get audio source
    }//end Awake()


        // Start is called before the first frame update
    void Start()
    {
        SetStartingPos(); //set the starting position
    }//end Start()


    // Update is called once per frame
    void Update()
    {
        //update ui;
        ballTxt.text = "Balls: " + numberOfBalls;
        scoreTxt.text = "Score: " + score;

        //if ball is not in play then attach it to paddle
        if(!isInPlay)
        {
            SetStartingPos();
        }

        //if space is pressed start the move
        if(Input.GetKeyDown("space") && !isInPlay)
        {
            isInPlay = true;
            Move();
        }

    }//end Update()


    private void LateUpdate()
    {
        //if ball is in play keep the velocity constant
        if(isInPlay)
        {
            rb.velocity = speed * rb.velocity.normalized;
        }
    }//end LateUpdate()


    void SetStartingPos()
    {
        isInPlay = false;//ball is not in play
        rb.velocity = Vector3.zero;//set velocity to keep ball stationary

        Vector3 pos = new Vector3();
        pos.x = paddle.transform.position.x; //x position of paddel
        pos.y = paddle.transform.position.y + paddle.transform.localScale.y; //Y position of paddle plus it's height

        transform.position = pos;//set starting position of the ball 
    }//end SetStartingPos()

    void Move()
    {
        rb.AddForce(new Vector3(0,initialForce,0)); //add force to start move
    }

    private void OnCollisionEnter(Collision collision)
    {
        audioSource.Play(); //play bounce audio
        GameObject otherGo = collision.gameObject;
        //if ball colides with a brick, increase score and destroy the brick
        if(otherGo.tag == "Brick")
        {
            score += 100;
            Destroy(otherGo);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject otherGo = other.gameObject;
        //if ball goes out of bounds decrease lives
        if (otherGo.tag == "OutBounds")
        {
            numberOfBalls -= 1;
            if (numberOfBalls > 0)
            {
                Invoke("SetStartingPos", 2.0f);
            }
        }
    }
}
