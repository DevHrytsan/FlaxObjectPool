using FlaxEngine;

public class DragFreeCamera : Script
{
    [Limit(0, 100), Tooltip("Camera movement speed factor")]
    public float MoveSpeed { get; set; } = 4;

    [Tooltip("Camera rotation smoothing factor")]
    public float CameraSmoothing { get; set; } = 20.0f;

    private float pitch;
    private float yaw;

    public override void OnStart()
    {
        var initialEulerAngles = Actor.Orientation.EulerAngles;
        pitch = initialEulerAngles.X;
        yaw = initialEulerAngles.Y;
    }

    public override void OnUpdate()
    {
        var mouseDelta = new Float2();

        if (Input.GetMouseButton(MouseButton.Right))
        {
            mouseDelta.X = Input.GetAxis("Mouse X");
            mouseDelta.Y = Input.GetAxis("Mouse Y");

            Screen.CursorVisible = false;
            Screen.CursorLock = CursorLockMode.Locked;
        }
        else
        {
            Screen.CursorVisible = true;
            Screen.CursorLock = CursorLockMode.None;
        }

        pitch = Mathf.Clamp(pitch + mouseDelta.Y, -88, 88);
        yaw += mouseDelta.X;
    }

    public override void OnFixedUpdate()
    {
        var camTrans = Actor.Transform;
        var camFactor = Mathf.Saturate(CameraSmoothing * Time.DeltaTime);

        camTrans.Orientation = Quaternion.Lerp(camTrans.Orientation, Quaternion.Euler(pitch, yaw, 0), camFactor);

        var inputH = Input.GetAxis("Horizontal");
        var inputV = Input.GetAxis("Vertical");
        var move = new Vector3(inputH, 0.0f, inputV);
        move.Normalize();
        move = camTrans.TransformDirection(move);

        camTrans.Translation += move * MoveSpeed;

        Actor.Transform = camTrans;
    }
}
