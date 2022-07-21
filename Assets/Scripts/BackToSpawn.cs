using UnityEngine;
using UnityEngine.InputSystem;

public class BackToSpawn : MonoBehaviour
{
    [SerializeField]
    Transform spawn;
    
    public void BackToSpawnPoint(InputAction.CallbackContext context)
    {
        transform.position = spawn.position;
        //transform.rotation = Quaternion.Euler(spawn.forward);
        if(TryGetComponent(out Rigidbody rigidbody))
            rigidbody.velocity = Vector3.zero;
    }
}
