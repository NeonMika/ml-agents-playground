using UnityEngine;

public class MouseMove : MonoBehaviour
{
    // Smaller is faster
    private const float mouseSpeedToZoom = 10f;
    
    private const float mouseScrollSpeed = 10f;

    private Camera _camera;
    private Vector3 _targetPosition;
    private float _targetZoom;
    
    // Start is called before the first frame update
    void Awake()
    {
        _camera = GetComponent<Camera>();
        _targetPosition = transform.position;
        _targetZoom = _camera.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        float mouseScroll = Input.GetAxis("Mouse ScrollWheel");

        /*
        if (mouseX != 0 || mouseY != 0 || mouseScroll != 0)
        {
            Debug.Log($"Mouse: {mouseX} {mouseY} {mouseScroll}");
        }
        */

        if (Input.GetMouseButton(1))
        {
            float zoomRatio = (_camera.orthographicSize / mouseSpeedToZoom);
            _targetPosition += new Vector3(mouseX * zoomRatio, mouseY * zoomRatio, 0);
        }

        _targetZoom -= mouseScroll * mouseScrollSpeed;

        if (_targetZoom < 1f)
        {
            _targetZoom = 1f;
        }

        _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, _targetZoom, 0.98f * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, _targetPosition, 0.98f * Time.deltaTime);
    }
}
