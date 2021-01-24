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

    public void GameOver()
	{
		PauseGame();
		panelGameOverAnim.SetTrigger("Open");
		scoremanager.SetActive(false);
		menuScore.SetActive(false);
		Texture_Changer.GetComponent<TextureController>().enabled = false;
		gameScore.GetComponent<Text>().text = menuScore.GetComponent<Text>().text;
		Player.GetComponent<PlayerController>().enabled = false;
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
		ResumeGame();
	}
}
