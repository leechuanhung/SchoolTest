using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{

    public Transform cameraTransform;  // ī�޶��� Transform

    void Update()
    {
        // ����� ī�޶��� ��ġ�� �̵���Ű��
        transform.position = new Vector3(cameraTransform.position.x, cameraTransform.position.y, transform.position.z);
    }
}
