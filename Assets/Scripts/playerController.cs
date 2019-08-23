using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class playerController : MonoBehaviour
{
    public float speed;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    private Animator playerAnimator;
    public TextMeshProUGUI test;


    void Start()
    {
        
        playerAnimator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator.SetFloat("moveX", 0);
        playerAnimator.SetFloat("moveY", -1);
        test = FindObjectOfType<TextMeshProUGUI>();
    }

    void Update()
    {
        change = Vector3.zero;
        change.x = SimpleInput.GetAxisRaw("Horizontal");
        change.y = SimpleInput.GetAxisRaw("Vertical");
        UpdateAnimationAndMove();
        if (SimpleInput.GetButtonDown("A button"))
        {
            test.text = "it's working A";
        }
        else if(SimpleInput.GetButtonDown("B button"))
        {

            test.text = "it's working B";
        }

    }

    void UpdateAnimationAndMove()
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
        change.Normalize();
        myRigidbody.MovePosition(transform.position + change * speed * Time.deltaTime);
    }
}
