using UnityEngine;

public class SlowlyFollowPlayer : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Transform playerTransform;
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, playerTransform.position, Time.deltaTime * speed);
    }
}
