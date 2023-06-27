using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement details")]
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float gravity = 15f;

    private Vector3 moveDirection;

    [Header("Camera details")]
    [SerializeField] private float sensetivity = 1f;

    private Transform playerCamera;
    private CharacterController controller;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        playerCamera = GetComponentInChildren<Camera>().transform;


        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        transform.Rotate(0, Input.GetAxis("Mouse X") * sensetivity, 0);

        playerCamera.Rotate(-Input.GetAxis("Mouse Y") * sensetivity, 0, 0);

        if (playerCamera.localRotation.eulerAngles.y != 0)
        {
            playerCamera.Rotate(Input.GetAxis("Mouse Y") * sensetivity, 0, 0);
        }



        moveDirection = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, moveDirection.y, Input.GetAxis("Vertical") * moveSpeed);
        moveDirection = transform.TransformDirection(moveDirection);


        if (controller.isGrounded)
        {
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpForce;
            }
            else
            {
                moveDirection.y = 0;
            }

        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }


    public void PauseController() => StartCoroutine(PauseControllerCoroutine());

    private IEnumerator PauseControllerCoroutine()
    {
        controller.enabled = false;
        yield return new WaitForSeconds(0.05f);
        controller.enabled = true;
    }
}
