using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 6f;
    public float jumpHeight = 2f;
    public float gravity = -10f;
    public float airControlPercent = 0.5f;
    public float sprintMultiplier = 1.5f;

    [Header("Mouse Look Settings")]
    public float mouseSensitivity = 1000f;
    public Transform cameraTransform;
    private float xRotation = 10f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;


    private CharacterController controller;
    private PlayerGunHandler gunHandler;
    private Vector3 velocity;
    [SerializeField]
    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        gunHandler = GetComponent<PlayerGunHandler>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMouseLook();
        HandleMovement();
        if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0))
        {
            gunHandler.FireGun();
        }
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotate the player left/right
        transform.Rotate(Vector3.up * mouseX);

        // Rotate camera up/down (pitch)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }


    void HandleMovement()
    {
        // Ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        // Input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Move relative to player’s facing direction
        Vector3 move = transform.right * x + transform.forward * z;
        
        if(!isGrounded)
            move *= airControlPercent;
        else if (Input.GetButton("Sprint"))
            move *= sprintMultiplier;

        controller.Move(move * moveSpeed * Time.deltaTime);

        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        // Gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }


}
