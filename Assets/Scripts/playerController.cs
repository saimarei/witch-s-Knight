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
    public float dashTime;
    private float startdashTime=1f;
    private int taps = 0;
    public Button top, bottom, left, right;
    private float timer=0,maxWaitTime=0.5f;
    private bool dashing = false;
    private int direction;

    void Start()
    {
        
        playerAnimator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator.SetFloat("moveX", 0);
        playerAnimator.SetFloat("moveY", -1);//sets it to -1 so it doesnt't assume any states
        test = FindObjectOfType<TextMeshProUGUI>();
        dashTime = startdashTime;
        top.onClick.AddListener(CheckButtonClick);
        bottom.onClick.AddListener(CheckButtonClick);
        left.onClick.AddListener(CheckButtonClick);
        right.onClick.AddListener(CheckButtonClick);

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
        }
        else if (SimpleInput.GetButtonDown("B button") || Input.GetKeyDown(KeyCode.X))
        {
            test.text = "it's working B";
        }
        if (taps > 0)
        {
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
            playerAnimator.SetBool("moving", true);
        }
        else
        {
            playerAnimator.SetBool("moving", false);
        }
    }

    void MoveCharacter()
    {
        if (!dashing)
        {
            change.Normalize();
            myRigidbody.MovePosition(transform.position + change * speed * Time.deltaTime);
        }
    }

    void Dash()
    {
        try
        {
            dashTime -= Time.deltaTime;
            if (dashing)
            {
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
                dashTime = startdashTime;
                dashSpeed = 0f;
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e, this);
        }
    }
    
}
