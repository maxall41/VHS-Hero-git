using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0.125f;

    public bool stripX;

    public bool stripY;

    public bool stripZ;

    public Vector3 correctionOffset;

    private void LateUpdate()
    {
        Vector3 desiredPosition = target.position + correctionOffset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        if (stripX == true)
        {
            smoothedPosition.x = transform.position.x;
        }

        if (stripY == true)
        {
            smoothedPosition.y = transform.position.y;
        }

        if (stripZ == true)
        {
            smoothedPosition.z = transform.position.z;
        }

        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
    }
}
