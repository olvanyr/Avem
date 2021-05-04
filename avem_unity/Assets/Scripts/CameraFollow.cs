
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


    private int ScreenSizeX = 0;
    private int ScreenSizeY = 0;



    private void RescaleCamera()
    {

        if (Screen.width == ScreenSizeX && Screen.height == ScreenSizeY) return;

        float targetaspect = 16.0f / 9.0f;
        float windowaspect = (float)Screen.width / (float)Screen.height;
        float scaleheight = windowaspect / targetaspect;
        Camera camera = GetComponent<Camera>();

        if (scaleheight < 1.0f)
        {
            Rect rect = camera.rect;

            rect.width = 1.0f;
            rect.height = scaleheight;
            rect.x = 0;
            rect.y = (1.0f - scaleheight) / 2.0f;

            camera.rect = rect;
        }
        else // add pillarbox
        {
            float scalewidth = 1.0f / scaleheight;

            Rect rect = camera.rect;

            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;

            camera.rect = rect;
        }

        ScreenSizeX = Screen.width;
        ScreenSizeY = Screen.height;
    }


    void OnPreCull()
    {
        if (Application.isEditor) return;
        Rect wp = Camera.main.rect;
        Rect nr = new Rect(0, 0, 1, 1);

        Camera.main.rect = nr;
        GL.Clear(true, true, Color.black);

        Camera.main.rect = wp;

    }


    private void Start()
    {
        transform.position = new Vector3(startingPosition.initialValue.x, startingPosition.initialValue.y, transform.position.z);


        //color = gradient.Evaluate(0.5f);

        //CamerColorUpdate(color);
        RescaleCamera();
    }

    void Update()
    {
        RescaleCamera();
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
