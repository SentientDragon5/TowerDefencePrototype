using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    float zoom = 5;

    void Update()
    {
        transform.position =
            transform.position +
            Quaternion.AngleAxis(-45, Vector3.up) * Vector3.right * Input.GetAxis("Horizontal") * 10 * Time.deltaTime +
            Quaternion.AngleAxis(-45, Vector3.up) * Vector3.forward * Input.GetAxis("Vertical") * 10 * Time.deltaTime;
        zoom += Input.GetAxis("Mouse ScrollWheel");
        GetComponent<Camera>().orthographicSize = Mathf.Clamp(zoom, 1, 35);
    }
}

