using UnityEngine;

public class Player : Character
{
    private Camera mainCamera;
    private Transform mousePositionTransform;

    protected override void Start()
    {
        base.Start();
        mainCamera = Camera.main;

        mousePositionTransform = new GameObject("Mouse Position Target").transform;
    }

    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootProjectile();
        }
        base.Update();
    }

    protected override void FixedUpdate()
    {
        FaceMouse();
        MovePlayer();
        base.FixedUpdate();
    }

    private void FaceMouse()
    {
        if (mainCamera != null)
        {
            Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePositionTransform.position = mouseWorldPosition;

            FaceTarget(mousePositionTransform);
        }
    }

    private void MovePlayer()
    {
        // Get horizontal and vertical input axes (WASD or arrow keys)
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Create direction vector from input
        Vector2 direction = new Vector2(horizontalInput, verticalInput);

        // Only move if there's input to prevent unnecessary physics calculations
        if (direction != Vector2.zero)
        {
            moving = true;
            MoveInDirection(direction);
        }
        else
        {
            moving = false;
        }
    }
}
