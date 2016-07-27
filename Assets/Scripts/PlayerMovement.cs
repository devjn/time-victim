using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

    public static PlayerMovement instance = null;

    public int pointsPerFood = 10;
    public int pointsPerSoda = 20;

    public float speed = 1;
    public float restartLevelDelay = 1f;
    public Text foodText;

    Rigidbody2D rbody;
    Animator anim;

    private int food;

    // Use this for initialization
    void Start () {
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        food = GameManager.instance.playerFoodPoints;
    }

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
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
            food += pointsPerFood;
            updateLivesText();
            //SoundManager.instance.RandomizeSfx(eatSound1, eatSound2);
            other.gameObject.SetActive(false);
        }
        else if (other.tag == "Enemy")
        {
            LoseFood(20);
        }
    }

    private void updateLivesText()
    {
        foodText.text = "HP: " + food;
    } 

    private void Restart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void LoseFood(int loss)
    {
        anim.SetBool("isBlood", true);
        //animator.SetTrigger("playerHit");
        food -= loss;
        print("lives= " + food);
        foodText.text = "HP: " + food;
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
