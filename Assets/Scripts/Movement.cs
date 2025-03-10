using UnityEngine;

public class Movement : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float dashSpeed = 50f;
    public float dashTime = 0.5f;
    public float dashCooldown = 1f;
    public float dashStaminaCost = 25f;

    private Vector3 _moveDirection;
    private CharacterController _controller;
    public StaminaBar staminaBar;

    private bool isDashing = false;
    private float dashTimer = 0f;
    private float lastDashTime = -999f;
    private float staminaRecoveryTimer = 0f;

    public Transform walkingModel; 

    private Plane groundPlane;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        groundPlane = new Plane(Vector3.up, transform.position);
    }

    void Update()
    {
        if (isDashing) return;

        MovementLogic();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartDash();
        }

        RotateModelTowardsMouse();
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            DashLogic();
        }
    }

    private void MovementLogic()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        _moveDirection = new Vector3(moveHorizontal, 0, moveVertical).normalized;

        float currentSpeed = walkSpeed;
        bool isRunning = Input.GetKey(KeyCode.LeftShift) && staminaBar.stamina > 0 && staminaRecoveryTimer <= 0;

        if (isRunning)
        {
            currentSpeed = runSpeed;
        }

        HandleStamina(isRunning);

        _controller.Move(_moveDirection * currentSpeed * Time.deltaTime);
    }

    private void HandleStamina(bool isRunning)
    {
        if (isRunning)
        {
            staminaBar.stamina -= 20f * Time.deltaTime;
            if (staminaBar.stamina <= 0)
            {
                staminaRecoveryTimer = 2f; 
            }
        }
        else if (staminaRecoveryTimer <= 0)
        {
            staminaBar.stamina += 10f * Time.deltaTime;
        }

        if (staminaRecoveryTimer > 0)
        {
            staminaRecoveryTimer -= Time.deltaTime;
        }

        staminaBar.stamina = Mathf.Clamp(staminaBar.stamina, 0, staminaBar.maxStamina);
    }

    private void StartDash()
    {
        if (Time.time - lastDashTime < dashCooldown || staminaBar.stamina < dashStaminaCost) return;

        staminaBar.stamina -= dashStaminaCost;
        lastDashTime = Time.time;
        isDashing = true;
        dashTimer = dashTime;
    }

    private void DashLogic()
    {
        if (dashTimer > 0)
        {
            _controller.Move(_moveDirection * dashSpeed * Time.fixedDeltaTime);
            dashTimer -= Time.fixedDeltaTime;
        }
        else
        {
            isDashing = false;
        }
    }

    private void RotateModelTowardsMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float enter = 0f;

        if (groundPlane.Raycast(ray, out enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);

            Debug.DrawLine(ray.origin, hitPoint, Color.red); 
            Debug.DrawLine(hitPoint, hitPoint + Vector3.up * 5f, Color.green);

            Vector3 direction = (hitPoint - walkingModel.position).normalized;

            direction.y = 0;

            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                walkingModel.rotation = Quaternion.Slerp(walkingModel.rotation, lookRotation, Time.deltaTime * 10f);
            }
        }
    }
}