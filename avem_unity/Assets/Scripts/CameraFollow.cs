
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

    //screenshake
    private float shakeTimeRemaining, shakeEmplitude, shakeFadeTime, shakeRotation, rotMultiplier, rotateFadeTime;

    

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


    //screenshacke
    private void LateUpdate()
    {
        if (shakeTimeRemaining > 0)
        {
            shakeTimeRemaining -= Time.deltaTime;

            float xAmount = Random.Range(-1f, 1) * shakeEmplitude;
            float yAmount = Random.Range(-1f, 1) * shakeEmplitude;
            float rotAmount = Random.Range(-1f, 1) * rotMultiplier;

            transform.position += new Vector3(xAmount, yAmount, 0);
            transform.rotation = Quaternion.Euler(0,0,rotAmount);

            shakeEmplitude = Mathf.MoveTowards(shakeEmplitude, 0f, shakeFadeTime * Time.deltaTime);
            shakeRotation = Mathf.MoveTowards(shakeRotation, 0f, rotateFadeTime * Time.deltaTime);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            shakeTimeRemaining = 0;
            shakeEmplitude = 0;
            rotMultiplier = 0;
            shakeFadeTime = 0;
            rotateFadeTime = 0;
        }
    }

    public void StartScreenShake(float length, float emplitude, float rotEmplitude)
    {
        if ( shakeEmplitude < emplitude || rotMultiplier < rotEmplitude)
        {
            shakeTimeRemaining = length;
            shakeEmplitude = emplitude;
            rotMultiplier = rotEmplitude;

            shakeFadeTime = emplitude / length;
            rotateFadeTime = rotEmplitude / length;
        }
    }
}
