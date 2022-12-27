using UnityEngine;
using UnityEngine.Events;

public class CameraFPSControl : MonoBehaviour
{
    public float sensibility = 100f;
    public playerController playerController;
    public Transform playerBody;

    private float xRotation = 0f;
    private float yRotation = 0f;

    private Quaternion? savedLocalRotation;
    private bool haveSaveRotation = false;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensibility * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensibility * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        yRotation += mouseX;
        yRotation = Mathf.Clamp(yRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}