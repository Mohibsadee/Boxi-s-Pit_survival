using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 10f;
    private float jumpingPower = 50f;

    private bool isFaceingRight = true;
    private bool isCursed = false;
    private float curseStartTime = 0f;

    private SpawnScript isDangerCheck;
    private float isDangerCheckTime= 0f;
    public GameObject warningText;
    private GameObject currentWarningText;

    public ParticleSystem moshParticleSystem;
    private bool isTextOn = false;
    public static bool isPoisonus = false;

    private float poisonDuration = 5f; // Adjust as needed
    private float poisonEndTime = 0f;



    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;







    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        isDangerCheck = FindObjectOfType<SpawnScript>();

        //superCurse

    if (isPoisonus)
    {   
        if (!isCursed)
        {
            isCursed = true;
            curseStartTime = Time.time;
        }

        if (Time.time - curseStartTime >= poisonDuration)
        {
            isCursed = false;
            Debug.Log("Not poisonous anymore");
       }
    }

// Reset the isPoisonus flag after 3 seconds
    if (isCursed && Time.time - curseStartTime >= 3f)
    {
        isPoisonus = false;
        Debug.Log("Not poisonous anymore");
    }

       


        /////jump/////

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {

            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }
        if (Input.GetButtonDown("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
        
        // Show warning text 


        if (!isTextOn && isDangerCheck.IsDanger())
        {
            Debug.Log("isDanger is true");
            isDangerCheckTime = Time.time;
            isTextOn = true;
            ShowWarningText();

        }

        if(isTextOn && Time.time - isDangerCheckTime >1f){
            DestroyWarningText();
            isTextOn = false;
        }

        Flip();
        
    }
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.5f, groundLayer);
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

    }
    private void Flip()
    {
        if (isFaceingRight && horizontal < 0f || !isFaceingRight && horizontal > 0f)
        {
            isFaceingRight = !isFaceingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }


    private void ShowWarningText()
    {

        Vector3 offset = new Vector3(-1.5f, 1.5f, 0f); // Define the offset you want
        Vector3 textSpawnPosition = transform.position + offset;
        currentWarningText = Instantiate(warningText, textSpawnPosition, Quaternion.identity, transform);

    }


    private void DestroyWarningText()
    {
        if (currentWarningText != null)
        {
            Destroy(currentWarningText);
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isPoisonus && isCursed && collision.gameObject.CompareTag("Cube"))
        {
            Instantiate(moshParticleSystem, transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (isPoisonus && isCursed && collision.gameObject.CompareTag("Cube"))
        {
            Instantiate(moshParticleSystem, transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
        }
    }


}
