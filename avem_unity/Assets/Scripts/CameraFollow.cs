
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float timeOffset;
    public Vector3 positionOffset;

    public Vector2 maxPosition;
    public Vector2 minPosition;

    private Vector3 velocity;

    public static CameraFollow instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("il y a plus d'une instance de CameraFollow dans la scéne");
            return;
        }

        instance = this;
    }

    void Update()
    {
        if (target == null)
        {
            return;
        }


        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z) + positionOffset;

        targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, timeOffset);

    }
}
