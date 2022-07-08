using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacter : MonoBehaviour {
    [SerializeField] private float     movementSpeed;
    [SerializeField] private float     cameraSpeed;
    [SerializeField] private float     jumpForce;
    [SerializeField] private float     boostForce;
    [SerializeField] private float     boostTimeout;
    [SerializeField] private float     airMovementMultiplier;
    [SerializeField] private float     maxMovementVelocity;
    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private OnGround  groundChecker;

    private Vector3 cameraRotation;
    private Vector3 moveDirection;
    float boostTimeKeeper;


    private void Awake() {
        //Cursor.lockState = CursorLockMode.Locked;
        cameraRotation = cameraTransform.localEulerAngles;
    }

    private void FixedUpdate() {
        var velocity              = playerRigidbody.velocity;
        var end_magnitude = Mathf.Clamp(maxMovementVelocity - velocity.magnitude, 0, float.MaxValue);

        playerRigidbody.AddRelativeForce(Vector3.ClampMagnitude(moveDirection, end_magnitude), ForceMode.Impulse);
        
    }

    private void LateUpdate() {
        cameraTransform.localEulerAngles = cameraRotation;
        if (boostTimeKeeper > 0)
        {
            boostTimeKeeper -= Time.deltaTime;
        }
    }

    public void OnMouseAim(InputAction.CallbackContext context) {
        if(Cursor.lockState == CursorLockMode.Locked) {
            Vector2 mousePosition = context.ReadValue<Vector2>() * cameraSpeed;
            
            cameraRotation = new Vector3(Mathf.Clamp(cameraRotation.x - mousePosition.y, -80f, 80f), 0, 0);

            Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, mousePosition.x, 0));
            transform.rotation *= deltaRotation;
            //print(cameraRotation);
            
        }
    }

    public void OnPlayerMove(InputAction.CallbackContext context) {
        if(Cursor.lockState == CursorLockMode.Locked) {
            var newMoveDirection  = context.ReadValue<Vector2>() * movementSpeed;

            moveDirection = new Vector3(newMoveDirection.x, 0, newMoveDirection.y);
        }
    }

    public void OnPlayerJump(InputAction.CallbackContext context) {
        if(Cursor.lockState == CursorLockMode.Locked && groundChecker.isOnGround) {
            var jump = context.ReadValue<float>() * jumpForce;
            
            playerRigidbody.AddRelativeForce(new Vector3(0, jump, 0), ForceMode.Acceleration);
        }
    }
    
    public void OnMouseExit(InputAction.CallbackContext context) {
        Cursor.lockState = CursorLockMode.None;
    }
    
    public void OnMouseEnter(InputAction.CallbackContext context) {
        //print("o");
        if (Cursor.lockState == CursorLockMode.None)
            Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnPlayerBoost(InputAction.CallbackContext context)
    {
        if (boostTimeKeeper <= 0)
        {
            playerRigidbody.AddRelativeForce(Vector3.forward * boostForce, ForceMode.Acceleration);
            boostTimeKeeper = boostTimeout;
        }
    }
}
