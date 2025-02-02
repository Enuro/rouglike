using UnityEngine;

public class billboard : MonoBehaviour
{
    public Transform cam;
    
    void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}
