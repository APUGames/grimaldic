using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D playerCharacter;
    Animator playerAnimator;
    [SerializeField] float runSpeed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        playerCharacter = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
       Run(); 
       FlipSprite();
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

}


