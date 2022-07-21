using UnityEngine;

public class WheelTurnAnimationScript : MonoBehaviour
{
    [SerializeField] private float turnSpeed = 1F;

    private void Update()
    {
        transform.Rotate(0F, turnSpeed * Time.deltaTime, 0F);
    }
}
