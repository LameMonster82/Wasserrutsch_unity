using UnityEngine;

public class SlideDirectionScript : MonoBehaviour
{
    [SerializeField] private GameObject sliderPlayer;
    [Space]
    public GameObject bezierObject;

    void Update()
    {
        transform.position = sliderPlayer.transform.position;

        Vector3 lookDirection = bezierObject.transform.position - transform.position;
        transform.rotation = bezierObject.activeSelf ? Quaternion.LookRotation(lookDirection, Vector3.up) : transform.rotation;
        
        //DELETE AFTER TESTING
        if (Input.GetKeyDown(KeyCode.O))
        {
            Time.timeScale = 1F;
        }
        //DELETE AFTER TESTING
        if (Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale += 0.1F;
        }
        //DELETE AFTER TESTING
        if (Input.GetKeyDown(KeyCode.I) && Time.timeScale > 0.1F)
        {
            Time.timeScale -= 0.1F;
        }
    }
}
