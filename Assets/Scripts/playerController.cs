using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//required for using ui stuff duh.
using TMPro; //required for using textmeshpro.
public class playerController : MonoBehaviour
{
    public float speed;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    private Animator playerAnimator;
    private TextMeshProUGUI test; //just for testing purposes will remove

    void Start()
    {
        
        playerAnimator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator.SetFloat("moveX", 0);
        playerAnimator.SetFloat("moveY", -1);//sets it to -1 so it doesnt't assume any states
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
            test.text = "it's working A";//test button log
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
