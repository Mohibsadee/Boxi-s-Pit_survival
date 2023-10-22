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
    private bool isFullPowerJump = false;
    private float fullPowerJumpStartTime = 0f;

    private SpawnScript isDangerCheck;
    private float isDangerCheckTime= 0f;
    public GameObject warningText;
    private GameObject currentWarningText;

    public ParticleSystem moshParticleSystem;
    private bool isTextOn = false;





    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;





    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        isDangerCheck = FindObjectOfType<SpawnScript>();

        //superpower


        if (Input.GetKeyDown(KeyCode.F))
        {

            Debug.Log("F key pressed");
            isFullPowerJump = true;
            fullPowerJumpStartTime = Time.time;

        }



        if (isFullPowerJump && Time.time - fullPowerJumpStartTime >= 3f)
        {
            isFullPowerJump = false;
        }

        /////jump

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
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
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
        if (collision.gameObject.CompareTag("Cube") && isFullPowerJump)
        {
            Instantiate(moshParticleSystem, transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Cube") && isFullPowerJump)
        {
            Instantiate(moshParticleSystem, transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
        }
    }


}
