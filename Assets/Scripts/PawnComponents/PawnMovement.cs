using FishNet.Object;
using UnityEngine;

public sealed class PawnMovement : NetworkBehaviour
{

    /** References to other Components **/
    private PawnInput pawnInput;
    private CharacterController pawnCharacterController;

    /** Movement related "physics" values. **/
    private float speed = 5;
    private float jumpSpeed = 1;
    private float gravityScale = 1;

    /** Movement Vectors **/

    //Used to determine X/Z Movement in 3d space based on input.
    private Vector3 inputCalculatedVelocity;
    //Actual calculated velocity after applying physics/gravity.
    private Vector3 movementVelocity;

    public override void OnStartNetwork()
    {
        base.OnStartNetwork();
        pawnInput = GetComponent<PawnInput>();
        pawnCharacterController = GetComponent<CharacterController>();
    }

    public void Update()
    {
        if (!IsOwner)
            return;

        //Calculate the velocity based on input.
        inputCalculatedVelocity = Vector3.ClampMagnitude(((transform.forward * pawnInput.verticalDirection) + (transform.right * pawnInput.horizontalDirection)) * speed, speed);

        //Take X/Z from the calculated velocity and store in the vector to move the player.
        movementVelocity.x = inputCalculatedVelocity.x;
        movementVelocity.z = inputCalculatedVelocity.z;

        //If the controller is on the ground already, cancel gravity
        if (pawnCharacterController.isGrounded)
        {
            movementVelocity.y = 0.0f;

            //Only allow jumping if grounded.
            if (pawnInput.jump)
            {
                movementVelocity.y = jumpSpeed;
            }
        }
        else //Apply gravity
        {
            movementVelocity.y += Physics.gravity.y * gravityScale * Time.deltaTime;
        }

        //Finally, move the controller.
        pawnCharacterController.Move(movementVelocity * Time.deltaTime);
    }
}
