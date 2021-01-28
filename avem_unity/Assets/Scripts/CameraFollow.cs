
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float timeOffset;
    public Vector3 positionOffset;

    public Vector2 maxPosition;
    public Vector2 minPosition;

    private Vector3 velocity;

    public Camera cam;

    public Color color;
    public Gradient gradient;

    

    //Scene transition coordinate
    public VectorValue startingPosition;

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

    private void Start()
    {
        transform.position = new Vector3(startingPosition.initialValue.x, startingPosition.initialValue.y, transform.position.z);


        //color = gradient.Evaluate(0.5f);

        CamerColorUpdate(color);
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

    public void CamerColorUpdate(Color color)
    {
        cam.backgroundColor = color;
    }
}
