using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float rotate_Speed = 90f;
    [SerializeField] private float zoom_Speed = 20f;

    // LIMIT: the area the camera is allowed to be in
    [SerializeField] private Vector2 min_XZ = new Vector2(-10f, -10f);
    [SerializeField] private Vector2 max_XZ = new Vector2(30f, 30f);
    [SerializeField] private float min_Height = 3f;
    [SerializeField] private float max_Height = 25f;

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
        if (Input.GetKey(KeyCode.Q)) transform.Rotate(0, -rotate_Speed * Time.deltaTime, 0, Space.World);
        if (Input.GetKey(KeyCode.E)) transform.Rotate(0, rotate_Speed * Time.deltaTime, 0, Space.World);



        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, min_XZ.x, max_XZ.x);
        pos.z = Mathf.Clamp(pos.z, min_XZ.y, max_XZ.y);
        pos.y = Mathf.Clamp(pos.y, min_Height, max_Height);
        transform.position = pos;
    }
}