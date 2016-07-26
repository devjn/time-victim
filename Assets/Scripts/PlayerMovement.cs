using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public float speed = 1;
    public float restartLevelDelay = 1f;

    Rigidbody2D rbody;
    Animator anim;

    private int food;

    // Use this for initialization
    void Start () {
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        food = GameManager.instance.playerFoodPoints;
    }
	
	// Update is called once per frame
	void Update () {
        if (!CameraFollow.followTarget) return;
        Vector2 movement_vector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        print("OnTriggerEnter2D: "+other);
        if (other.tag == "Exit")
        {
            print("OnTrigger: exit, Restarting");
            Invoke("Restart", restartLevelDelay);
            enabled = false;
        }
        else if (other.tag == "Food")
        {
            //food += pointsPerFood;
            //foodText.text = "+" + pointsPerFood + " Food: " + food;
            //SoundManager.instance.RandomizeSfx(eatSound1, eatSound2);
            other.gameObject.SetActive(false);
        }
        else if (other.tag == "Enemy")
        {
            LoseFood(20);
        }
    }

    private void Restart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void LoseFood(int loss)
    {
        //animator.SetTrigger("playerHit");
        food -= loss;
        print("lives= " + food);
        //foodText.text = "-" + loss + " Food: " + food;
        CheckIfGameOver();
    }

    private void CheckIfGameOver()
    {
        if (food <= 0)
        {
            //SoundManager.instance.PlaySingle(gameOverSound);
            //SoundManager.instance.musicSource.Stop();
            GameManager.instance.GameOver();
        }
    }


}
