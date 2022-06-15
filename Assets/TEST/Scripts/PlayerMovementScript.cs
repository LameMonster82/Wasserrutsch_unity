using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovementScript : MonoBehaviour
{
    private Vector3 PlayerMovementInput;
    private Vector2 PlayerMouseInput;
    private float xRot;

    [SerializeField] private Transform PlayerCamera;
    [SerializeField] private Rigidbody Rb;
    [Space]
    [SerializeField] private float Speed;
    [SerializeField] private float Sensitivity;
    [SerializeField] private float Jumpforce;
    [Space]
    [Header("Jumping Requirements")]
    [SerializeField] private Transform GroundedTransform;
    [SerializeField] private float CheckRadius;
    [SerializeField] private LayerMask FloorMask;



    private void Update()
    {
        PlayerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        PlayerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        MovePlayer();
        MovePlayerCamera();
    }

    private void MovePlayer()
    {
        Vector3 MoveVector = transform.TransformDirection(PlayerMovementInput) * Speed;
        Rb.velocity = new Vector3(MoveVector.x, Rb.velocity.y, MoveVector.z);

        if (Physics.CheckSphere(GroundedTransform.position, CheckRadius, FloorMask))
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                Rb.AddForce(Vector3.up * Jumpforce, ForceMode.Impulse);
            }
        }
        
    }

    private void MovePlayerCamera()
    {
        xRot -= PlayerMouseInput.y * Sensitivity;

        transform.Rotate(0f, PlayerMouseInput.x * Sensitivity, 0f);
        PlayerCamera.transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
    }
}
