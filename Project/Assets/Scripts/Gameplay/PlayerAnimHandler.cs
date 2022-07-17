using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Idle,
    Moving,
    Throwing
}
public class PlayerAnimHandler : MonoBehaviour
{
    public AnimatedSprite animatedSprite;
    private MovementController movementController;
    public GrabHand grabHand;
    public PlayerState playerState;
    private float timeSinceStateChange = 0;
    private bool throwFlipValue;

    void Start()
    {
        movementController = GetComponent<MovementController>();
        grabHand.throwDelegate += (Vector3 direction) => {
            playerState = PlayerState.Throwing;
            animatedSprite.SelectAnim("THROW");
            timeSinceStateChange = 0;
            throwFlipValue = direction.x > 0;
        };
    }

    void Update()
    {
        timeSinceStateChange += Time.deltaTime;
        switch(playerState)
        {
            case PlayerState.Idle:
                
                
                timeSinceStateChange = 0;
                if(grabHand.grabbedElement != null)
                    animatedSprite.SelectAnim("IDLE_CARRY");
                else
                    animatedSprite.SelectAnim("IDLE");
                if(movementController.inputDirection.sqrMagnitude != 0)
                {
                    playerState = PlayerState.Moving;
                }
                
                if(movementController.inputDirection.x != 0)
                {
                    animatedSprite.flipX = movementController.inputDirection.x > 0;
                }
                break;
            case PlayerState.Moving:
                
                timeSinceStateChange = 0;
                if(grabHand.grabbedElement != null)
                    animatedSprite.SelectAnim("RUN_CARRY");
                else
                    animatedSprite.SelectAnim("RUN");
                if(movementController.inputDirection.sqrMagnitude == 0)
                {
                    playerState = PlayerState.Idle;
                }
                
                if(movementController.inputDirection.x != 0)
                {
                    animatedSprite.flipX = movementController.inputDirection.x > 0;
                }
                break;
            case PlayerState.Throwing:
                animatedSprite.flipX = throwFlipValue;
                if(animatedSprite.isAnimationFinished)
                    playerState = PlayerState.Idle;
                break;
        }
    }
}
