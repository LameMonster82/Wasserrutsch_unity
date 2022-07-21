using UnityEngine;

public class TeleportTo : MonoBehaviour
{
    [SerializeField] Transform teleportPoint;
    [SerializeField] float distanceForward;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = teleportPoint.position + teleportPoint.TransformDirection(Vector3.left * distanceForward);
            other.transform.rotation = Quaternion.Euler(teleportPoint.forward * 180);
            if (other.TryGetComponent(out Rigidbody thingRb))
                thingRb.velocity = Vector3.zero;
        }
    }
}
