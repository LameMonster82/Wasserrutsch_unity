using UnityEngine;
using PathCreation;

public class FollowPathObject : MonoBehaviour
{
    public PathCreator creator;
    [SerializeField] private float speed = 0F;
    [SerializeField] private float distanceTravelled;
    [SerializeField] private float currentLength;
    [SerializeField] private GameObject directionGameobject;

    [SerializeField] private PlayerSlideScript playerSlideScript;

    private void FixedUpdate()
    {
        currentLength = distanceTravelled;
        speed = playerSlideScript.playerVelocity.magnitude;

        if (gameObject.activeSelf == true)
        {
            distanceTravelled += speed * Time.deltaTime;
            transform.position = creator.path.GetPointAtDistance(distanceTravelled);

            //transform.rotation = creator.path.GetRotationAtDistance(distanceTravelled);
            //directionGameobject.transform.rotation = transform.rotation;
        }
        else return;

        //if (distanceTravelled + 2 >= creator.path.length)
        //{
        //    gameObject.SetActive(false);
        //}

        gameObject.SetActive(distanceTravelled + 2 <= creator.path.length);

    }
}
