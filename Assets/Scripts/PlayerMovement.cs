using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public Camera playerCamera;
    public Camera spectatorCamera;

    public float walkSpeed = 1f;
    public float runSpeed = 1.5f;
    public float jumpPower = 10f;
    public float gravity = 10f;
    public float lookSpeed = 2f;
    public float lookXLimit = 60f;
    public float defaultHeight = 2f;
    public float crouchHeight = 1f;
    public float crouchSpeed = 3f;
    public int jumpCount = 3;
    public float teleportDistance = 1f;
    public int redPillCount = 1;
    public int greenPillCount = 1;
    public int bluePillCount = 1;
    public GameObject gameOver;

    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private float spectatorRotationX = 0;
    private CharacterController characterController;
    private Camera Camera;

    private bool canMove = true;
    private bool isSpectator = false;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        spectatorCamera.gameObject.SetActive(false);
        playerCamera.gameObject.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            ToggleSpectatorMode();
        }

        if (!isSpectator)
        {
            PlayerControl();
        }
        else
        {
            SpectatorControl();
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        print(other.name);
        if (other.gameObject.tag == "BluePill")
        {
            print("bluepill");
            jumpCount++; //+skok
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "RedPill")
        {
            print("redpill");
            redPillCount++;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "GreenPill")
        {
            print("greenpill");
            greenPillCount++; //teleport
            Destroy(other.gameObject);
        }
        else if(other.gameObject.tag == "Enemy")
        {
            print("urDed");
            gameOver.SetActive(true);
            canMove = false;
            Cursor.visible = true;
        }
    }
    void PlayerControl()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;

        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetKeyUp(KeyCode.X))
        {
            Camera.enabled = !Camera.enabled;
        }

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded && jumpCount > 0)
        {
            moveDirection.y = jumpPower;
            jumpCount--;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
            moveDirection.x = 0;
            moveDirection.z = 0;
        }

        if (Input.GetKey(KeyCode.LeftControl) && canMove)
        {
            characterController.height = crouchHeight;
            walkSpeed = crouchSpeed;
            runSpeed = crouchSpeed;
        }
        else
        {
            characterController.height = defaultHeight;
            walkSpeed = 1f;
            runSpeed = 1.5f;
        }

        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        if (Input.GetKeyDown(KeyCode.E) && greenPillCount > 0)
        {
            Vector3 teleportDirection = playerCamera.transform.forward;
            Vector3 teleportTarget = transform.position + teleportDirection.normalized * teleportDistance;

            characterController.enabled = false;
            transform.position = teleportTarget;
            characterController.enabled = true;
            greenPillCount--;
        }
    }

    void SpectatorControl()
    {
        float moveSpeed = Input.GetKey(KeyCode.LeftShift) ? 10f : 5f;
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Jump") - Input.GetAxis("Fire1"), Input.GetAxis("Vertical"));

        spectatorCamera.transform.position += spectatorCamera.transform.TransformDirection(move) * moveSpeed * Time.deltaTime;

        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
        float mouseY = -Input.GetAxis("Mouse Y") * lookSpeed;

        // Omezíme rotaci nahoru/dolù
        spectatorRotationX += mouseY;
        spectatorRotationX = Mathf.Clamp(spectatorRotationX, -lookXLimit, lookXLimit);

        // Nastavíme rotaci, ale zabráníme rotaci kolem osy Z
        spectatorCamera.transform.rotation = Quaternion.Euler(spectatorRotationX, spectatorCamera.transform.eulerAngles.y + mouseX, 0);
    }

    void ToggleSpectatorMode()
    {
        isSpectator = !isSpectator;

        if (isSpectator)
        {
            spectatorCamera.gameObject.SetActive(true);
            playerCamera.gameObject.SetActive(false);
            characterController.enabled = false; 
            spectatorCamera.transform.position = playerCamera.transform.position;
            spectatorCamera.transform.rotation = playerCamera.transform.rotation;
        }
        else
        {
            spectatorCamera.gameObject.SetActive(false);
            playerCamera.gameObject.SetActive(true);
            characterController.enabled = true;
        }
    }
}