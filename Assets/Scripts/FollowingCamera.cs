using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//カメラの追従、回転スクリプト
[ExecuteInEditMode,DisallowMultipleComponent]
public class FollowingCamera : MonoBehaviour
{
    public GameObject target;
    public Vector3 offset;

    [SerializeField] private float distance = 4.0f;
    [SerializeField] private float polarAngle = 45.0f;
    [SerializeField] private float azimuthalAngle = 45.0f;

    [SerializeField] private float minDistance = 1.0f;
    [SerializeField] private float maxDistance = 7.0f;
    [SerializeField] private float minPolerAngle = 5.0f;
    [SerializeField] private float maxPolerAngle = 75.0f;
    [SerializeField] private float mouseXSensitivity = 5.0f;
    [SerializeField] private float mouseYSensitivity = 5.0f;
    [SerializeField] private float scrollSensitivity = 5.0f;
    // Start is called before the first frame update
    void LateUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            updateAngle(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        }
        updateDistance(Input.GetAxis("Mouse ScrollWheel"));

        var lookAtPos = target.transform.position + offset;
        updatePosition(lookAtPos);
        transform.LookAt(lookAtPos);
    }

    // Update is called once per frame
    void updateAngle(float x,float y)
    {
        x = azimuthalAngle - x * mouseXSensitivity;
        azimuthalAngle = Mathf.Repeat(x, 360);

        y = polarAngle + y * mouseYSensitivity;
        polarAngle = Mathf.Clamp(y, minPolerAngle, maxPolerAngle);
    }

    void updateDistance(float scroll)
    {
        scroll = distance - scroll * scrollSensitivity;
        distance = Mathf.Clamp(scroll, minDistance, maxDistance);
    }

    void updatePosition(Vector3 lookAtPos)
    {
        var da = azimuthalAngle * Mathf.Deg2Rad;
        var dp = polarAngle * Mathf.Deg2Rad;
        transform.position = new Vector3(
            lookAtPos.x + distance * Mathf.Sin(dp) * Mathf.Cos(da),
            lookAtPos.y + distance * Mathf.Cos(dp),
            lookAtPos.z + distance * Mathf.Sin(dp) * Mathf.Sin(da));

    }
}
