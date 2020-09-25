using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;

    private float speed = 12f;
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.2f;
    [SerializeField] private LayerMask groundMask;
    private bool isGrounded;


    private float velocityY;
    [SerializeField] private Transform playerCamera;
    [SerializeField] private float mouseSensitivity = 3.5f;
    [SerializeField] private bool lockCursor = true;
    private float cameraPitch = 0.0f;
    [SerializeField] private float maxAngle = 90f;
    [SerializeField] private float walkSpeed = 6.0f;
    [SerializeField] [Range(0.0f, 0.5f)] private float moveSmoothTime = 0.3f;
    [SerializeField] [Range(0.0f, 0.5f)] private float mouseSmoothTime = 0.03f;

    Vector2 currentDir = Vector2.zero;
    Vector2 currentDirVelocity = Vector2.zero;

    Vector2 currentMouseDelta = Vector2.zero;
    Vector2 currentMouseDeltaVelocity = Vector2.zero;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

    }

    void UpdateMovement()
    {
        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDir.Normalize();

        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);


        if (controller.isGrounded)
        {
            velocityY = -2f;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocityY = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }


        velocityY += gravity * Time.deltaTime;

        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * walkSpeed + Vector3.up * velocityY;
        controller.Move(velocity * Time.deltaTime);
    }

    void UpdateMouseLook()
    {
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        targetMouseDelta =
            Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        cameraPitch -= targetMouseDelta.y * mouseSensitivity;
        cameraPitch = Mathf.Clamp(cameraPitch, -maxAngle, maxAngle);

        playerCamera.localEulerAngles =Vector3.right * cameraPitch;
        transform.Rotate(Vector3.up * targetMouseDelta.x * mouseSensitivity);

    }

    void Update()
    {
        UpdateMouseLook();
        UpdateMovement();

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position,groundDistance);
    }
}
