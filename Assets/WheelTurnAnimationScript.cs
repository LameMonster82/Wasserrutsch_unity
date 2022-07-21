using UnityEngine;

public class WheelTurnAnimationScript : MonoBehaviour
{
    [SerializeField] private float turnSpeed = 1F;

    private void FixedUpdate()
    {
        transform.Rotate(0F, turnSpeed, 0F);
    }
}
