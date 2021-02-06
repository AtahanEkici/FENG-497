using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
	public Text textScore;
	public int current_score;
	public GameManager game_manager;
	public int max_score = 0;

    private void Awake()
    {
		game_manager = GetComponent<GameManager>();
	}
    private void Start() 
	{
		textScore.text = "Score: 0";
		current_score = 0;
	}
	public void UpdateScore (int score) 
	{
		current_score = (score / 3) * 10;

		if(current_score > max_score)
        {
			max_score = current_score;
			textScore.text = "Score: " + max_score;
		}
	}
}
