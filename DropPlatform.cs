using UnityEngine;

public class DropPlatform : MonoBehaviour
{
	Rigidbody2D rb2d;

	private void Awake()
	{
		rb2d = GetComponent<Rigidbody2D>();
	}

	private void Droplatform()
	{
		rb2d.isKinematic = false;
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Player" && GameObject.Find("Player").GetComponent<Rigidbody2D>().velocity.y == 0)
		{
			Invoke("Droplatform", 0.5f);
		}
	}
}
