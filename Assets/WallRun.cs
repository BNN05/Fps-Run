using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WallRun : MonoBehaviour
{
    [SerializeField]
    private playerController playerController;

    public Transform orientation;
    private bool wallRunningTransformed = false;

    private void Update()
    {
        if (IsWallRunning())
        {
            if (GetWallPosition() == WallPosition.Left && !wallRunningTransformed)
            {
                playerController.isWallRunning = true;
                playerController.body.transform.Rotate(Vector3.fwd, (playerController.transform.rotation.x - 15f));
                wallRunningTransformed = true;
            }
            if (Input.GetButton("Jump") && !playerController.isJumping)
            {
                playerController.Jump();
            }
        }
        else if (wallRunningTransformed)
        {
            playerController.isWallRunning = false;
            //playerController.transform.rotation = new Quaternion(0, playerController.transform.rotation.y, 0, 0);
            playerController.body.transform.Rotate(Vector3.back, (playerController.transform.rotation.x - 15f));
            wallRunningTransformed = false;
        }
    }

    private WallPosition GetWallPosition()
    {
        bool right = Physics.Raycast(transform.position, transform.right, 1.5f, playerController.groundMask);
        bool left = Physics.Raycast(transform.position, -transform.right, 1.5f, playerController.groundMask);
        if (right)
            return WallPosition.Right;
        if (left)
            return WallPosition.Left;
        return WallPosition.Null;
    }

    private bool IsWallRunning()
    {
        var pos = GetWallPosition();
        //if (playerController.inertia < 1.5)
        //    return false;
        if (pos != WallPosition.Null)
            return true;
        return false;
    }
}

public enum WallPosition
{
    Left,
    Right,
    Null
}