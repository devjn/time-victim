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
        mycam.orthographicSize = (Screen.height / zoom) * 0.25f;

    }
    private Vector2 pos;
    private Vector3 depth = new Vector3(0, 0, -10f);
    private float height = 6;
    private float width = 8;

    public int levelPixelHeight;
    public int levelPixelWidth;
    // Update is called once per frame
    void Update()
    {

        if (target)
        {
            pos = Vector2.Lerp(transform.position, target.position, m_speed);
            //Prevent camera from moving too far
            pos.y = (float) Mathf.Min(Mathf.Max(target.position.y - 0.5f, height * 0.5f -1f), levelPixelHeight - (height * 0.5f));
            pos.x = (float) Mathf.Min(Mathf.Max(target.position.x + 1f, width * 0.5f), levelPixelWidth - (width * 0.5f));
            depth.Set(pos.x, pos.y, depth.z);
                
            transform.position = depth;
        }

        /*if (Input.GetKeyDown("enter"))
        {
            target = false;
        }
        */
    }
}