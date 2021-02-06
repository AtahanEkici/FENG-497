using UnityEngine;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour
{
	public GameManager gameManager;
	public Text textSpeed;
	public float speedMultiple = 1f;
	public float startLimmit = 5f;
	public float maxDistanceBeforeLose = 5f;
	public Transform target;

	private Vector3 targetPosition;
	private float distance;
	private float speed;
	private float timer;
	private bool isDone = false;
	private void Start()
	{
		timer = 0;
	}
	private void LateUpdate()
	{
		distance = target.position.y - transform.position.y;
		if (target.position.y < startLimmit)
			return;

		if (distance < -maxDistanceBeforeLose && isDone == false)
		{
			isDone = true;
			gameManager.GameOver();
			speed = 0f;
		}
		else if (distance > 1)
		{
			targetPosition = new Vector3(0, target.position.y, transform.position.z);
			transform.position = Vector3.Lerp(transform.position, targetPosition, distance * Time.deltaTime);
		}
		else
		{
			targetPosition = new Vector3(0, transform.position.y + speed, transform.position.z);
			transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime);
		}
		timer += Time.deltaTime;
		textSpeed.text = "x " + (int)(1 + (Time.time) / 60);
		if (isDone == false)
		{
			speed = (1 + (timer) / 60) * speedMultiple;
		}
	}
}