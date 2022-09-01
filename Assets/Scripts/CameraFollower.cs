using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Vector3 followOffset;
    [SerializeField] private float followSpeed;
    private GameObject target;
    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }
    private void FixedUpdate()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, target.transform.position + followOffset, followSpeed);
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x + Input.GetAxis("Mouse Y") * 2, transform.localEulerAngles.y + Input.GetAxis("Mouse X") * 2, 0);
    }
}
