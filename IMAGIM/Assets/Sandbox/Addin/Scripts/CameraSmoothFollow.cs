using UnityEngine;

public class CameraSmoothFollow : MonoBehaviour
{
    [SerializeField] private float followSpeed = 6.5f;
    private Transform target;
    private float yCameraOffset;
    private void Awake() {
        target = GameObject.FindWithTag("Player").transform;
        yCameraOffset = transform.position.y - target.position.y;
    }
    private void Update()
    {
        Vector3 newPosition = target.position;
        newPosition.y += yCameraOffset;
        newPosition.z = -10;
        transform.position = Vector3.Slerp(transform.position, newPosition, followSpeed * Time.deltaTime);
    }

}
