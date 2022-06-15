using UnityEngine;

public class JumpCheckboxScript : MonoBehaviour
{
    [SerializeField] GameObject nextCheckboxObject;
    [Space]
    [SerializeField] GameObject playerObjectHolder;
    private Rigidbody rb;
    [Space]
    [SerializeField] GameObject directionObject;

    [SerializeField] float _playerSpeed = 30F;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out PlayerSlideScript playerSlide))
        {
            playerSlide.ChangeSpeed(_playerSpeed);
        }

        if (other.CompareTag("Player"))
        {
            playerObjectHolder = other.gameObject;
            EnableBetterCollision();
            NextCheckbox();
        }
    }

    private void NextCheckbox()
    {
        directionObject.GetComponent<SlideDirectionScript>().bezierObject = nextCheckboxObject;
    }

    private void EnableBetterCollision()
    {
        rb = playerObjectHolder.GetComponent<Rigidbody>();
        // ACHTUNG!!! SPAETER AUSSCHALTEN VIA SCRIPT !!!
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    //Pascals idee
    //private PlayerSlideScript playerSlider;

    //private void Awake()
    //{
    //    DOTween.defaultEaseType = Ease.Linear;
    //}

    //private void Start()
    //{
    //    playerSlider = playerObjectHolder.GetComponent<PlayerSlideScript>();
    //    float x = 0f;
    //    x.TweenFloat(10f, 10f, Test);
    //}

    //void Test(float value)
    //{
    //    Debug.Log(value);
    //}

    //Testing
    //void Method(out int answer, out string message, string stillNull)
    //{
    //    answer = 44;
    //    message = "I've been returned";
    //    stillNull = null;
    //}

    //private void Update()
    //{
    //    int argNumber;
    //    string argMessage, argDefault, stillNull = null;
        
    //    Method(out argNumber, out argMessage, stillNull);
    //    Debug.Log("ARGNUMBER: " + argNumber);
    //    Debug.Log("ARGMESSAGE: " + argMessage);
    //    Debug.Log(stillNull == null);
    //}
}
