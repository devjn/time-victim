using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{

    public Transform target;
    public float m_speed = 0.1f;

    public float zoom = 10f;

    Camera mycam;

    // Use this for initialization
    void Start()
    {
        mycam = GetComponent<Camera>();

    }

    // Update is called once per frame
    void Update()
    {
        mycam.orthographicSize = (Screen.height / zoom) / 4f;

        if (target)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, m_speed) + new Vector3(0, 0, -10);
        }

        /*if (Input.GetKeyDown("enter"))
        {
            target = false;
        }
        */
    }
}