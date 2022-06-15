using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerSlideScript : MonoBehaviour
{
    [SerializeField] private Transform direction;
    [Space]
    [SerializeField] private float pushForce = 0F;
    [SerializeField] private float sidePushForce = 0F;
    [SerializeField] private Rigidbody rb;
    [Space]
    [SerializeField] private float gravityMultiplier;
    [Space]
    public float maxSpeed;
    public bool capMaxSpeed = true;

    [HideInInspector]
    public Vector3 playerVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //Bewegung
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(direction.transform.forward * pushForce * Time.deltaTime, ForceMode.VelocityChange);
        }

        float horizontal = Input.GetAxis("Horizontal");
        rb.AddForce(direction.transform.right * sidePushForce * Time.deltaTime * horizontal, ForceMode.VelocityChange);

        //Gravit?tseinfluss
        rb.AddForce(Vector3.down * gravityMultiplier, ForceMode.Acceleration);
        //Debug.DrawRay(transform.position, -transform.up);

        //Speed ?bergabe an Follower durch variable
        playerVelocity = rb.velocity;

        //niedrigster Speedwert der erlaubt ist
        if (capMaxSpeed && rb.velocity.magnitude < maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    public void ChangeSpeed(float speed)
    {
        capMaxSpeed = false;
        rb.velocity = rb.velocity.normalized * speed;
    }
}
