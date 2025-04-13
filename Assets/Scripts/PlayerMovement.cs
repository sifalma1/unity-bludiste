using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public Camera playerCamera;
    public Camera spectatorCamera;
    public AudioSource munchSound;
    public GameObject playerModel;

    public float walkSpeed = 1f;
    public float runSpeed = 1.5f;
    public float jumpPower = 10f;
    public float gravity = 10f;
    public float lookSpeed = 2f;
    public float lookXLimit = 60f;
    public float defaultHeight = 2f;
    public float crouchHeight = 1f;
    public float crouchSpeed = 3f;
    public int bluePillCount = 10;
    public float teleportDistance = 0.6f;
    public int redPillCount = 10;
    public int greenPillCount = 10;
    public float invisibilityDuration = 5f;
    private bool isInvisible = false;
    public GameObject gameOver;
    public DoorSpawner doorSpawner;


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
            bluePillCount++; //+skok
            munchSound.Play();
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "RedPill")
        {
            print("redpill");
            redPillCount++;
            munchSound.Play();
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "GreenPill")
        {
            print("greenpill");
            greenPillCount++; //teleport
            munchSound.Play();
            Destroy(other.gameObject);
        }
        else if(other.gameObject.tag == "Enemy" && isInvisible == false)
        {
            print("urDed");
            gameOver.SetActive(true);
            canMove = false;
            Cursor.visible = true;
        }
        else if (other.gameObject.tag == "UnderTheMap")
        {
            print("urDed");
            gameOver.SetActive(true);
            canMove = false;
            Cursor.visible = true;
        }
        else if(other.gameObject.tag == "Key")
        {
            print("Key Picked up");
            munchSound.Play();
            doorSpawner.SpawnDoors();
            Destroy(other.gameObject);
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

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded && bluePillCount > 0)
        {
            moveDirection.y = jumpPower;
            bluePillCount--;
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

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (GameObject enemy in enemies)
            {
                RandomMovement ai = enemy.GetComponent<RandomMovement>();
                if (ai != null && ai.centrePoint != null)
                {
                    ai.centrePoint.localPosition = new Vector3(0, 0.5f, 0);
                    ai.range = 3f;
                }
            }
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (GameObject enemy in enemies)
            {
                RandomMovement ai = enemy.GetComponent<RandomMovement>();
                if (ai != null && ai.centrePoint != null)
                {
                    ai.centrePoint.localPosition = playerModel.transform.position;
                    ai.range = 0.1f;
                }
            }
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

        if (Input.GetKeyDown(KeyCode.Q) && redPillCount > 0 && !isInvisible)
        {
            StartCoroutine(BecomeInvisible());
            redPillCount--;
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
    IEnumerator BecomeInvisible()
    {
        isInvisible = true;
        playerModel.SetActive(false);

        // Mùžeš sem pøidat i zvuk nebo efekt
        yield return new WaitForSeconds(invisibilityDuration);

        playerModel.SetActive(true);
        isInvisible = false;
    }

}