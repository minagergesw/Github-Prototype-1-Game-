using UnityEngine;

public class CameraSwipeLook : MonoBehaviour
{ [Header("References")]
    public Transform player; // اللاعب اللى الكاميرا بتلف حواليه
    public Transform cameraTransform; // الكاميرا نفسها

    [Header("Settings")]
    public float sensitivityX = 0.2f;
    public float sensitivityY = 0.2f;
    public float minY = -30f;
    public float maxY = 60f;

    private Vector2 lastTouchPos;
    private float rotationX;
    private float rotationY;
    private bool isDragging = false;

    void Start()
    {
        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;

        Vector3 angles = player.eulerAngles;
        rotationX = angles.y;
        rotationY = cameraTransform.localEulerAngles.x;
    }

    void Update()
    {
        if (Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            lastTouchPos = touch.position;
            isDragging = true;
        }
        else if (touch.phase == TouchPhase.Moved && isDragging)
        {
            Vector2 delta = touch.position - lastTouchPos;
            lastTouchPos = touch.position;

            rotationX += delta.x * sensitivityX;
            rotationY -= delta.y * sensitivityY;
            rotationY = Mathf.Clamp(rotationY, minY, maxY);

            player.rotation = Quaternion.Euler(0, rotationX, 0);
            cameraTransform.localRotation = Quaternion.Euler(rotationY, 0, 0);
        }
        else if (touch.phase == TouchPhase.Ended)
        {
            isDragging = false;
        }
    }

}
