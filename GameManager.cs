using UnityEngine;
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

	private void Awake()
	{
		Game_Text = gameScore.GetComponent<Text>();
		Menu_Text = menuScore.GetComponent<Text>();
		tc1 = Texture_Changer.GetComponent<TextureController>();
		pc1 = Player.GetComponent<PlayerController>();
		ScoreTracker = scoremanager.GetComponent<ScoreManager>();
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
		if (ScoreTracker.max_score >= 60 && ScoreTracker.current_score <= 0)
		{
			GameOver();
		}
	}
    private void PauseGame()
	{
		Time.timeScale = 0;
	}
	private void ResumeGame()
	{
		Time.timeScale = 1;
	}
	public void Quit_game()
	{
		Application.Quit();
	}
	public void PlayAgain()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene(0);
	}
}