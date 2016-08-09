using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public GameObject player;
	public float levelStartDelay = 2f;
	public float turnDelay = .1f;
	public static GameManager instance = null;
	public BoardManager boardScript;
	public int playerFoodPoints = 100;
	[HideInInspector] public bool playersTurn = true;

	private Text levelText;
	private GameObject levelImage;
	private int level = 1;
	//private List<Enemy> enemies;
	private bool enemiesMoving;
	private bool doingSetup;

	// Use this for initialization
	void Awake () {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		DontDestroyOnLoad (gameObject);
		//enemies = new List<Enemy> ();
		boardScript = GetComponent<BoardManager> ();
		InitGame ();
	}

	void OnLevelWasLoaded (int index)
	{
		InitGame ();
	}

    public void setLevel(int num)
    {
        this.level = num;
    } 

    public void nextLevel()
    {
        this.level++;
    }

    bool hack = false;

	void InitGame()
	{
		doingSetup = true;

		levelImage = GameObject.Find ("LevelImage");
		levelText = GameObject.Find ("LevelText").GetComponent<Text> ();
		levelText.text = "Level " + level;
		levelImage.SetActive (true);
		Invoke ("HideLevelImage", levelStartDelay);

		//enemies.Clear ();
		boardScript.SetupScene (level);
	}

	private void HideLevelImage()
	{
		levelImage.SetActive (false);
		doingSetup = false;
	}

	public void GameOver()
	{
		levelText.text = "Your are dead at the level " + level + ".";
		levelImage.SetActive (true);
		enabled = false;
        pauseGame();
    }
	
	// Update is called once per frame
	void Update () {
		if (playersTurn || enemiesMoving || doingSetup)
			return;

		StartCoroutine (MoveEnemies ());
	}

	//public void AddEnemyToList(Enemy script)
	//{
	//	enemies.Add (script);
	//}

	IEnumerator MoveEnemies()
	{
		enemiesMoving = true;
		yield return new WaitForSeconds (turnDelay);
		//if (enemies.Count == 0) {
		//	yield return new WaitForSeconds (turnDelay);
		//}

		//for (int i = 0; i < enemies.Count; i++) {
		//	enemies [i].MoveEnemy ();
		//	yield return new WaitForSeconds (enemies [i].moveTime);
		//}

		playersTurn = true;
		enemiesMoving = false;
	}

    public static void pauseGame()
    {
        Time.timeScale = 0;
    }

    public static void resumeGame()
    {
        Time.timeScale = 1f;
    }

    public static bool isPaused()
    {
        return Time.timeScale == 0;
    }
}
