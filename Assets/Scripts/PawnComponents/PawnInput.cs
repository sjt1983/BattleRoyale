using UnityEngine;
using UnityEngine.InputSystem;

using FishNet.Object;

public sealed class PawnInput : NetworkBehaviour
{
    //Reference to the Unity Input System
    private PlayerInputSystem playerInputSystem;
    private InputAction movementInput;
 
    //2D Movement Directions
    public float horizontalDirection;
    public float verticalDirection;
    private Vector2 movementDirection;

    /** Camera/Mouse Variables. **/

    //Used to capture raw mouse input.
    private Vector2 rawMouseInput;

    //Values of the mouse delta from the last fram, adjusted for sensitivity.
    public float AdjustedMouseX;
    public float AdjustedMouseY;
    private float sensitivity = 15;

    /** Actions **/

    public bool Interacting;

    public bool jump;
    public bool fire;

    private void Awake()
    {        
        //Lets setup the controller
        playerInputSystem = new PlayerInputSystem();
        movementInput = playerInputSystem.PlayerControls.Move;

        //X-Axis movement 
        playerInputSystem.PlayerControls.MouseX.performed += ctx => rawMouseInput.x = ctx.ReadValue<float>();
        playerInputSystem.PlayerControls.MouseX.Enable();

        //Y-Axis Movement
        playerInputSystem.PlayerControls.MouseY.performed += ctx => rawMouseInput.y = ctx.ReadValue<float>();        
        playerInputSystem.PlayerControls.MouseY.Enable();

        //Interaction
        playerInputSystem.PlayerControls.Interact.Enable();

        //Enable the controller.
        movementInput.Enable();

        //Lock the cursor to the window
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;

    }

    private void Update()
    {
        if (!IsOwner)
            return;

        //Adjust mouse delta for sensitivity.
        AdjustedMouseX = rawMouseInput.x * sensitivity * Time.deltaTime;
        AdjustedMouseY = rawMouseInput.y * sensitivity * Time.deltaTime;

        //Move the player.
        //movementInput.ReadValue returns a Vector2 to see which movement buttons are pressed.
        //We assigned those to variables to indicate certain directions are "on" (Forward/backward/strafe left/strafe right)
        //PawnMovement script reads these values and determines what to do.
        movementDirection = movementInput.ReadValue<Vector2>();
        horizontalDirection = movementDirection.x;
        verticalDirection = movementDirection.y;
        

        //Handle Interaction
        if (playerInputSystem.PlayerControls.Interact.WasPressedThisFrame())
        {
            Interacting = true;
        }

        if (playerInputSystem.PlayerControls.Interact.WasReleasedThisFrame())
        {
            Interacting = false;
        }

        // jump = Input.GetButton("Jump");
        // fire = Input.GetButton("Fire1");
    }

   
}
