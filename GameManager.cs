using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public Animator panelGameOverAnim;
	public GameObject gameScore;
	public GameObject menuScore;
	public GameObject scoremanager;
	public GameObject Texture_Changer;
	public GameObject Player;

	private Text Game_Text;
	private Text Menu_Text;
	private TextureController tc1;
	private PlayerController pc1;
	private ScoreManager ScoreTracker;
	private AsyncOperation asyncLoadLevel;

	private void Awake()
	{
		Game_Text = gameScore.GetComponent<Text>();
		Menu_Text = menuScore.GetComponent<Text>();
		tc1 = Texture_Changer.GetComponent<TextureController>();
		pc1 = Player.GetComponent<PlayerController>();
		ScoreTracker = scoremanager.GetComponent<ScoreManager>();
	}

    private void Start()
    {
		QualitySettings.vSyncCount = 1;
	}
    IEnumerator Restart()
	{
		asyncLoadLevel = SceneManager.LoadSceneAsync("0", LoadSceneMode.Single);

		while (!asyncLoadLevel.isDone)
		{
			yield return null;
		}
	}
	public void GameOver()
	{
		Game_Text.text = Menu_Text.text;
		panelGameOverAnim.SetTrigger("Open");
		scoremanager.SetActive(false);
		menuScore.SetActive(false);
		tc1.enabled = false;
		pc1.enabled = false;
	}
    private void Update()
    {
		ResumeORPause();

		if (ScoreTracker.max_score >= 60 && ScoreTracker.current_score <= 0)
		{
			GameOver();
		}
	}
	public void Quit_game()
	{
		Application.Quit();
	}
	public void PlayAgain()
	{
        StartCoroutine(Restart());
    }
	private void Quit_yes_Or_No()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Quit_game();
		}
	}
	private void ResumeORPause()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (Time.timeScale == 1)
			{
				Time.timeScale = 0;
			}
			else
			{
				Time.timeScale = 1;
			}
		}
	}
}