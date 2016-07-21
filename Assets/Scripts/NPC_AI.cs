using UnityEngine;
using System.Collections;

public class NPC_AI : MonoBehaviour {

    public float speed = 1;

    Rigidbody2D rbody;
    Animator anim;

    private float x = 0;
    private float y = 1;

	// Use this for initialization
	void Start () {
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (x == 0 && y == 1)
        {
            y = -1;
        }
        else if(x == 0 && y == -1)
        {
            x = -1;
            y = 0;
        }else if (x == -1 && y == 0)
        {
            x = 1;
        }else if (x == 1 && y == 0)
        {
            x = 0;
            y = 1;
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
	}
}
