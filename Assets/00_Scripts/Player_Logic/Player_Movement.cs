using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float rotate_Speed = 90f;
    [SerializeField] private float zoom_Speed = 20f;

    private void Update()
    {
        // MOVE: forward/right flattened so the camera stays at the same height
        Vector3 forward = transform.forward;
        forward.y = 0;
        forward.Normalize();

        Vector3 right = transform.right;
        right.y = 0;
        right.Normalize();

        Vector3 move = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) move += forward;
        if (Input.GetKey(KeyCode.S)) move -= forward;
        if (Input.GetKey(KeyCode.D)) move += right;
        if (Input.GetKey(KeyCode.A)) move -= right;

        transform.position += move.normalized * speed * Time.deltaTime;

        // ZOOM: scroll moves the camera along the direction it faces
        float scroll = Input.mouseScrollDelta.y;
        transform.position += transform.forward * scroll * zoom_Speed * Time.deltaTime;

        // ROTATE: spin around the world's up axis
        if (Input.GetKey(KeyCode.LeftControl)) transform.Rotate(0, -rotate_Speed * Time.deltaTime, 0, Space.World);
        if (Input.GetKey(KeyCode.LeftAlt)) transform.Rotate(0, rotate_Speed * Time.deltaTime, 0, Space.World);
    }
}