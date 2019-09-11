using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//required for using ui stuff duh.
using TMPro; //required for using textmeshpro.
using UnityEngine.EventSystems;
public class playerController : MonoBehaviour
{
    public float speed=5f;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    private Animator playerAnimator;
    private TextMeshProUGUI test; //just for testing purposes will remove
    public float dashSpeed=10f;
    //public float dashTime;
    //private float startdashTime=1f;
    private int taps = 0;
    public Button top, bottom, left, right;
    private float timer=0,maxWaitTime=0.5f;
    private bool dashing = false;
    private int direction;
    private bool idleTooLong=false;
    private float idleTimer;
    void Start()
    {
        
        playerAnimator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator.SetFloat("moveX", 0);
        playerAnimator.SetFloat("moveY", -1);//sets it to -1 so it doesnt't assume any states
        test = FindObjectOfType<TextMeshProUGUI>();
        top.onClick.AddListener(CheckButtonClick);
        bottom.onClick.AddListener(CheckButtonClick);
        left.onClick.AddListener(CheckButtonClick);
        right.onClick.AddListener(CheckButtonClick);
        idleTimer = 0f;
    }

    void Update()
    {
        change = Vector3.zero;
        change.x = SimpleInput.GetAxisRaw("Horizontal");
        change.y = SimpleInput.GetAxisRaw("Vertical");
        UpdateAnimationsAndMove();
        if (SimpleInput.GetButtonDown("A button") || Input.GetKeyDown(KeyCode.Z))
        {
            test.text = "it's working A";//test button log
            idleTooLong = false;
            playerAnimator.SetBool("idle2long", false);
        }
        else if (SimpleInput.GetButtonDown("B button") || Input.GetKeyDown(KeyCode.X))
        {
            test.text = "it's working B";
            idleTooLong = false;
            playerAnimator.SetBool("idle2long", false);
        }
        if (taps > 0)
        {
            idleTooLong = false;
            playerAnimator.SetBool("idle2long", false);
            timer += Time.deltaTime;
            if (taps == 1)
            {
                if (timer >= maxWaitTime)
                    taps = 0;
            }
            else if (taps > 1)
            {
                if (timer < maxWaitTime)
                {
                    dashing = true;
                    dashSpeed = 10f;
                }
                else
                {
                    taps = 0;
                    dashing = false;
                }
            }
        }
        else
            timer = 0f;
    }
    private void FixedUpdate()
    {
        Dash();
    }


    void CheckButtonClick()
    {
        ++taps;
    }

    void UpdateAnimationsAndMove()
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
            change.x = Mathf.Round(change.x);
            change.y = Mathf.Round(change.y);
            playerAnimator.SetFloat("moveX", change.x);
            playerAnimator.SetFloat("moveY", change.y);
        }
        else
        {
            playerAnimator.SetBool("moving", false);
            IsIdleTooLong();
        }
    }

    void MoveCharacter()
    {
        if (!dashing)
        {
            change.Normalize();
            myRigidbody.MovePosition(transform.position + change * speed * Time.deltaTime);
            playerAnimator.SetBool("moving", true);
            idleTimer = 0f;
            idleTooLong = false;
            playerAnimator.SetBool("idle2long", false);
        }
    }

    void Dash()
    {
        try
        {
            if (dashing)
            {
                idleTooLong = false;
                playerAnimator.SetBool("rolling",true);
                if (EventSystem.current.currentSelectedGameObject.name == "Top")
                {
                    myRigidbody.velocity = Vector2.up * dashSpeed; ;
                }
                else if (EventSystem.current.currentSelectedGameObject.name == "Right")
                {
                    myRigidbody.velocity = Vector2.right * dashSpeed;
                }
                else if (EventSystem.current.currentSelectedGameObject.name == "Bottom")
                {
                    myRigidbody.velocity = Vector2.down * dashSpeed;
                }
                else if (EventSystem.current.currentSelectedGameObject.name == "Left")
                    myRigidbody.velocity = Vector2.left * dashSpeed;
            }
            else if (!dashing)
            {
                playerAnimator.SetBool("rolling", false);
                dashSpeed = 0f;
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e, this);
        }
    }

    void IsIdleTooLong()
    {
        idleTimer += Time.deltaTime;
        if (idleTimer >= 8f)
        {
            idleTooLong = true;
            if (idleTooLong)
            {

                int blinks = 0;
                ++blinks;
                playerAnimator.SetBool("idle2long", true);
                if(idleTimer>9f)
                    playerAnimator.SetBool("idle2long", false);
                if (idleTimer >11f)
                {
                    playerAnimator.SetBool("idle2long", true);
                    ++blinks;
                }
                if (idleTimer > 12f)
                    playerAnimator.SetBool("idle2long", false);
                if (idleTimer > 17f)
                {
                    playerAnimator.SetBool("idle2long", true);
                    ++blinks;
                }
                if (idleTimer > 18f)
                    playerAnimator.SetBool("idle2long", false);
                if (blinks == 3)
                {
                    blinks = 0;
                    idleTimer = 0f;
                    idleTooLong = false;
                    playerAnimator.SetBool("idle2long", false);
                }
            }
        }

    }
    
}
