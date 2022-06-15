using UnityEngine;

public class CamSliderFollowScript : MonoBehaviour
{
    //public GameObject sliderPlayer;
    //public GameObject directionGameobject;
    //[Space]
    //[SerializeField] private float offSetY = 0F;
    //[SerializeField] private float offSetZ = 0F;
    //[Space]
    //[SerializeField] private Vector2 sensitivity;
    //private Vector2 rotation;
    //[SerializeField] private float maxAngleDown;
    //[SerializeField] private float maxAngleUp;
    //[SerializeField] private float maxAngleLeft;
    //[SerializeField] private float maxAngleRight;

    //void Update()
    //{
    //    //Vector3 rotation = new Vector3(directionGameobject.transform.eulerAngles.x, directionGameobject.transform.eulerAngles.y, directionGameobject.transform.eulerAngles.z);
    //    //transform.eulerAngles = rotation;


    //    transform.position = Position(transform.position);
    //    MouseInput();
    //    FixedCameraRotation();
    //}

    //private Vector3 Position(Vector3 position)
    //{
    //    return position = new Vector3(sliderPlayer.transform.position.x, sliderPlayer.transform.position.y + offSetY, sliderPlayer.transform.position.z + offSetZ);
    //}

    //private void MouseInput()
    //{
    //    //Rotation der Maus
    //    Vector2 wantedVelocity = GetInput() * sensitivity;
    //    rotation += wantedVelocity * Time.deltaTime;
    //    transform.localEulerAngles = new Vector3(rotation.y, rotation.x, 0);
    //}

    //private Vector2 GetInput()
    //{
    //    Vector2 input = new Vector2(Input.GetAxis("Mouse X"),Input.GetAxis("Mouse Y"));
    //    return input;
    //}

    //private void FixedCameraRotation()
    //{
    //    //Festsetzung der maximalen horizontalen und vertikalen Kamerawinkel
    //    rotation.y = ClampVerticalAngle(rotation.y);


    //    rotation.x = ClampHorizontalAngle(rotation.x);
    //    Debug.Log(ClampHorizontalAngle(rotation.x));
    //}

    //private float ClampVerticalAngle(float angle)
    //{
    //    return Mathf.Clamp(angle, -maxAngleUp, maxAngleDown);
    //}
    //private float ClampHorizontalAngle(float angle)
    //{
    //    //Debug.Log(angle);
    //    return Mathf.Clamp(angle, directionGameobject.transform.eulerAngles.y - maxAngleLeft, directionGameobject.transform.eulerAngles.y + maxAngleRight);
    //}



    [SerializeField] float maxAngleDown;
    [SerializeField] float maxAngleUp;
    [SerializeField] float maxAngleLeft;
    [SerializeField] float maxAngleRight;
    [Space]
    [SerializeField] Transform directionObject;
    Vector2 currentRotationOffset = Vector2.zero;
    [Space]
    [SerializeField] Vector2 mouseSensitivity;
    [Space]
    [SerializeField] private Vector3 cameraOffset;

    private void Update()
    {
        HandleInput();
        ApplyRotation();
        ApplyPosition();
    }


    void HandleInput()
    {
        currentRotationOffset.x += Input.GetAxis("Mouse X") * mouseSensitivity.x * Time.deltaTime;
        currentRotationOffset.y += Input.GetAxis("Mouse Y") * mouseSensitivity.y * Time.deltaTime;
        currentRotationOffset.x = Mathf.Clamp(currentRotationOffset.x, maxAngleLeft, maxAngleRight);
        currentRotationOffset.y = Mathf.Clamp(currentRotationOffset.y, maxAngleDown, maxAngleUp);
    }

    void ApplyRotation()
    {
        transform.rotation = directionObject.rotation;
        transform.Rotate((-currentRotationOffset.y), currentRotationOffset.x, 0F);
        //Vector3 up = transform.up;
        //transform.Rotate(up * currentRotationOffset.x, Space.World);
        //Vector3 right = transform.right;
        //transform.Rotate(right * currentRotationOffset.y, Space.World);
    }

    void ApplyPosition()
    {
        Vector3 position = directionObject.position;
        position += transform.forward * cameraOffset.z;
        position += transform.up * cameraOffset.y;
        transform.position = position;
    }
}
