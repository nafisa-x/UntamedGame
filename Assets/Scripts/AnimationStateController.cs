using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //if player presses w, isWalking is true
        if (Input.GetKey("w") || Input.GetKey("s") || Input.GetKey("a") || Input.GetKey("d")){
            animator.SetBool("isWalking", true);
        } else{
            animator.SetBool("isWalking", false);
        }

        if (Input.GetKey("space")){
            animator.SetBool("isJumping", true);
        } else{
            animator.SetBool("isJumping", false);
        }
    }
}
