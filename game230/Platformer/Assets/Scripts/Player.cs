using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D playerCharacter;
    Animator playerAnimator;
    Collider2D playerCollider;

    [SerializeField] float runSpeed = 5.0f;
    [SerializeField] float jumpSpeed = 5.0f;
    [SerializeField] float climbSpeed = 5.0f;
    float gravityScaleAtStart;
       
       
    // Start is called before the first frame update
    void Start()
    {
        playerCharacter = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerCollider = GetComponent<Collider2D>();

        gravityScaleAtStart = playerCharacter.gravityScale;
    }
      
    // Update is called once per frame
    void Update()
    { 
       


       Run(); 
       FlipSprite();
       Jump();
       Climb();
       
    }

    private void Run()
   {
   // horizontal movement value between -1 and 1 
      float hMovement = Input.GetAxis("Horizontal");  
      Vector2 runVelocity = new Vector2(hMovement * runSpeed,playerCharacter.velocity.y); 
      playerCharacter.velocity = runVelocity;
      //turn on the Animator's run parameter 
      playerAnimator.SetBool("run",true);
      // Turn off the Animator's run Parameter
        bool hSpeed = Mathf.Abs(playerCharacter.velocity.x) > Mathf.Epsilon;
        playerAnimator.SetBool("run", hSpeed);

        //print(runVelocity);
   }
   private void FlipSprite() 
   {
      //if player is moving  
      bool hMovement = Mathf.Abs(playerCharacter.velocity.x) > Mathf.Epsilon;
      if (hMovement)
      {
          //Reverse the current scaling of the xaxis 
          transform.localScale = new Vector2(Mathf.Sign(playerCharacter.velocity.x), 1f);
      }
   }

   private void Jump()


   {
      if(!playerCollider.IsTouchingLayers(LayerMask.GetMask("Foreground")))
      {
         //will stop this function if false
         return;
      }


      if(Input.GetButtonDown("Jump"))
      {
         //Get new Y velocity based on a controllable variable 
         Vector2 jumpVelocity = new Vector2(0.0f, jumpSpeed);
         playerCharacter.velocity += jumpVelocity;
      }
   }
   private void Climb()
   {
      if(!playerCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
      {
         playerAnimator.SetBool("climb", false);
         playerCharacter.gravityScale = gravityScaleAtStart;
         return;
      }
      
      //"Vertical from Input axises
      float vMovement = Input.GetAxis("Vertical");
      // x needs to remain the same as we chamge y
      Vector2 climbVelocity = new Vector2(playerCharacter.velocity.x, vMovement * climbSpeed);
      playerCharacter.velocity = climbVelocity;
   
      playerCharacter.gravityScale = 0.0f; 
      
      bool vSpeed = Mathf.Abs(playerCharacter.velocity.y) > Mathf.Epsilon;
      playerAnimator.SetBool("climb", vSpeed);

   }
}

 
   

