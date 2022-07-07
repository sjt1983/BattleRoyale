using FishNet.Object;
using UnityEngine;

public sealed class PawnMovement : NetworkBehaviour
{
    /** References to other Components **/
    [SerializeField]
    private PawnInput pawnInput;

    [SerializeField]
    private Pawn pawn;

    [SerializeField]
    private CharacterController pawnCharacterController;

    /** Player Movement Variables **/
    private const float CROUCH_SPEED = 3;
    private const float WALK_SPEED = 5;
    private const float SPRINT_SPEED = 10;
    private const float JUMP_SPEED = 6;
    private const float MAX_STAMINA = 100;
    private const float STAMINA_BURN = 12;
    private const float STAMINA_REGEN = 5;

    private float currentSpeed = WALK_SPEED;
   
    /** Movement Vectors **/

    //Used to determine X/Z Movement in 3d space based on input.
    private Vector3 inputCalculatedVelocity;
    //Actual calculated velocity after applying physics/gravity.
    private Vector3 movementVelocity;

    public override void OnStartNetwork()
    {
        base.OnStartNetwork();
    }

    public void Update()
    {
        if (!IsOwner)
            return;

        //Stamina
        //If sprinting, set sprint speed and drain stamina.
        //If we completley blow stamina, you cant sprint again until it regens.
        if (!pawnInput.Crouching && pawnInput.Sprinting && !pawn.SprintLock)
        {
            currentSpeed = SPRINT_SPEED;
            pawn.Stamina -= STAMINA_BURN * Time.deltaTime;
            if (pawn.Stamina <= 0f)
            {
                pawn.SprintLock = true;
                pawn.Stamina = 0f;                
            }
        }
        else
        {
            currentSpeed = pawnInput.Crouching ? CROUCH_SPEED : WALK_SPEED;
            pawn.Stamina += STAMINA_REGEN * Time.deltaTime;
            if (pawn.Stamina > MAX_STAMINA)
            {
                pawn.SprintLock = false;
                pawn.Stamina = MAX_STAMINA;
            }
        }

        //Calculate the velocity based on input.
        inputCalculatedVelocity = Vector3.ClampMagnitude(((transform.forward * pawnInput.verticalDirection) + (transform.right * pawnInput.horizontalDirection)) * currentSpeed, currentSpeed);

        //Take X/Z from the calculated velocity and store in the vector to move the player.
        movementVelocity.x = inputCalculatedVelocity.x;
        movementVelocity.z = inputCalculatedVelocity.z;

        //If the controller is on the ground already, cancel gravity
        if (pawnCharacterController.isGrounded)
        {
            movementVelocity.y = 0.0f;

            //Only allow jumping if grounded.
            if (pawnInput.Jumping)
            {
                movementVelocity.y = JUMP_SPEED;
            }
        }
        else //Apply gravity
        {
            movementVelocity.y += Physics.gravity.y * 1.5f * Time.deltaTime;
        }

        //Finally, move the controller.
        pawnCharacterController.Move(movementVelocity * Time.deltaTime);
    }
}
