using UnityEngine;
using System.Collections;

public class NPC_AI : MonoBehaviour {

    public float speed = 10;

    Rigidbody2D rbody;
    Animator anim;

    private float x = 0;
    private float y = 1;
    private float c = 0;
    public float radius = 50;

    public Transform target;
    private float minDistance = 0f;
    private float range;

    // Use this for initialization
    void Start () {
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Vector2.Distance(rbody.position, target.position) >= 6)
        {
            anim.SetBool("iswalking", false);
            if (x == 0 && y == 1 && c > 50)
            {
                y = 0;
                x = -1;
                c = 0;
            }
            else if (x == -1 && y == 0 && c > 50)
            {
                x = 0;
                y = -1;
                c = 0;
            }
            else if (x == 0 && y == -1 && c > 50)
            {
                x = 1;
                y = 0;
                c = 0;
            }
            else if (x == 1 && y == 0 && c > 50)
            {
                x = 0;
                y = 1;
                c = 0;
            }
            Vector2 movement_vector = new Vector2(x, y);
            if (movement_vector != Vector2.zero)
            {
                anim.SetBool("iswalking", true);
                anim.SetFloat("input_x", movement_vector.x);
                anim.SetFloat("input_y", movement_vector.y);
            }
            else
            {
                anim.SetBool("iswalking", false);
            }

            rbody.MovePosition(rbody.position + movement_vector * Time.deltaTime * speed);
            c++;
            return;
        }

        range = Vector2.Distance(transform.position, target.position);

        if (range > minDistance)
        {
            Vector2 movement_vector = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime) - rbody.position;

            if(movement_vector != Vector2.zero)
            {
                anim.SetBool("iswalking", true);
                anim.SetFloat("input_x", movement_vector.x);
                anim.SetFloat("input_y", movement_vector.y);
            }
            else
            {
                anim.SetBool("iswalking", false);
            }

            rbody.MovePosition(Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime));
        }
    }
}
