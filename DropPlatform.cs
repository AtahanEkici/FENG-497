using UnityEngine;

public class DropPlatform : MonoBehaviour
{
	private Rigidbody2D rb2d;
	private Rigidbody2D Player;
	private void Awake()
	{
		rb2d = GetComponent<Rigidbody2D>();
		Player = GameObject.Find("Player").GetComponent<Rigidbody2D>();
	}
	private void Droplatform()
	{
		rb2d.isKinematic = false;
	}
	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.CompareTag("Player") && Player.velocity.y == 0)
		{
			Invoke(nameof(Droplatform), 0.30f);
		}
	}
}
