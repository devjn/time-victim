using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{

    public Transform target;
    public float m_speed = 0.1f;

    public float zoom = 10f;

    Camera mycam;

    private float correctionX;
    private float correctionY;

    public static bool followTarget = true;

    // Use this for initialization

        

    void Start()
    {
        mycam = GetComponent<Camera>();
        mycam.orthographicSize = (Screen.height / zoom) * 0.25f;

        levelPixelHeight = GameManager.instance.boardScript.rows;
        levelPixelWidth = GameManager.instance.boardScript.columns;

        height = Mathf.RoundToInt(Screen.height / zoom) * 0.25f ;
        width = Mathf.RoundToInt(Screen.width / zoom) * 0.25f ;

        correctionX = 2.5f * Mathf.RoundToInt(Screen.width / zoom) / 32;
        correctionY = 2.5f * Mathf.RoundToInt(Screen.height / zoom) / 32;

        print("correctionX= "+ correctionX);
        print("correctionY= " + correctionY);

        levelPixelHeight -= Mathf.RoundToInt(correctionY + 1f);
        levelPixelWidth -= Mathf.RoundToInt(correctionX + 0.5f);

        //StartCoroutine(LateStart());

    }


    IEnumerator LateStart()
    {
        yield return new WaitForFixedUpdate();
        yield return new WaitForSeconds(2);
        levelPixelHeight = GameManager.instance.boardScript.columns;
        levelPixelWidth = GameManager.instance.boardScript.rows;
    }



    private Vector2 pos;
    private Vector3 depth = new Vector3(0, 0, -10f);
    private float height = 6;
    private float width = 8;
    public int FreeCameraSpeed = 15;

    private int levelPixelHeight;
    private int levelPixelWidth;
    // Update is called once per frame
    void Update()
    {

        if (followTarget)
        {
            pos = Vector2.Lerp(transform.position, target.position, m_speed);
            //Prevent camera from moving too far
            pos.y = (float)Mathf.Min(Mathf.Max(target.position.y, height * 0.5f + correctionY), 
                levelPixelHeight - (height * 0.5f));
            pos.x = (float)Mathf.Min(Mathf.Max(target.position.x, width * 0.5f + correctionX), 
                levelPixelWidth - (width * 0.5f));
            depth.Set(pos.x, pos.y, depth.z);
                
            transform.position = depth;
        } else
        {
            Vector2 movement_vector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            Vector2 position = transform.position;

            Vector2 targetPos = position + movement_vector * Time.deltaTime * FreeCameraSpeed;

            print(movement_vector);

            pos = Vector2.Lerp(transform.position, targetPos, m_speed);
            //Prevent camera from moving too far
            pos.y = (float)Mathf.Min(Mathf.Max(targetPos.y, height * 0.5f + correctionY),
                levelPixelHeight - (height * 0.5f));
            pos.x = (float)Mathf.Min(Mathf.Max(targetPos.x, width * 0.5f + correctionX),
                levelPixelWidth - (width * 0.5f));
            depth.Set(pos.x, pos.y, depth.z);

            transform.position = depth;
        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            print("KeypadEnter");
            if (followTarget)
            {
                print("target set to null");
                exTarget = target;
                followTarget = false;
            } else
            {
                print("target reseted");
                followTarget = true;
            }

        }

        if (Input.GetKeyDown(KeyCode.Comma))
        {
            zoom -= 1f;
            mycam.orthographicSize = (Screen.height / zoom) * 0.25f;
        } else if (Input.GetKeyDown(KeyCode.Period))
        {
            zoom += 1f;
            mycam.orthographicSize = (Screen.height / zoom) * 0.25f;
        }


    }

    private Transform exTarget;

}