using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	public ScoreManager scoreManager;
	public GameObject Player;
	public float speed = 0.25f;
	public float jumpForce = 1000f;
	public float HorizontalJumpFactor = 100f;
	public ParticleSystem forceJumpEffect;
	public ParticleSystem moveParticle;

	private Slider slide;
	private Rigidbody2D rb2D;
	private Animation anim;
	private float velocity;
	private float mp;
	private float screen_width;
	private const int slider_value = 10;
	private void Awake()
	{
		rb2D = GetComponent<Rigidbody2D>();
		slide = GameObject.FindGameObjectWithTag("Slider").GetComponent<Slider>();
		anim = transform.GetChild(1).GetComponent<Animation>();
	}
	private void Start()
	{
		InitializeSlider();
		anim.wrapMode = WrapMode.Once;
		screen_width = Screen.width;
	}
	private void Update()
	{
		velocity = rb2D.velocity.y;
		mp = Input.mousePosition.x;
	}
	private void FixedUpdate()
	{
		JumpControl();
		Move_With_Mouse();
	}
	private void InitializeSlider()
	{
		slide.wholeNumbers = true;
		slide.minValue = 0;
		slide.maxValue = 100;
		slide.value = 0;
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
	private void Move_With_Mouse()
	{
		if (Input.GetMouseButton(0))
		{
			if (mp < (screen_width / 2))
			{
				Player.transform.Translate(-speed, 0, 0);
			}
			else if (mp > (screen_width / 2))
			{
				Player.transform.Translate(speed, 0, 0);
			}
		}
	}
	private void Move_With_Keyboard()
	{
		if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
		{
			Player.transform.Translate(-speed, 0, 0);
		}
		else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
		{
			Player.transform.Translate(speed, 0, 0);
		}
	}
	private void Escape()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
	}
	private void JumpControl()
	{
		if (velocity == 0)
		{
			anim.Play();
			Jump();
		}
		else if (velocity < 0)
		{
			forceJumpEffect.Stop();
			moveParticle.Stop();
		}
	}
	private void Jump()
	{
		if (slide.value == 100)
		{
			rb2D.AddForce(Vector2.up * ((jumpForce * 3) + Mathf.Abs(rb2D.velocity.y) * HorizontalJumpFactor));
			SliderDefault();
			moveParticle.Stop();
			forceJumpEffect.Play();
		}
		else
		{
			rb2D.AddForce(Vector2.up * (jumpForce + Mathf.Abs(rb2D.velocity.y) * HorizontalJumpFactor));
			SliderUpdate();
			forceJumpEffect.Stop();
			moveParticle.Play();
		}
	}
	private void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.CompareTag("Platform"))
		{
			scoreManager.UpdateScore((int)transform.position.y);
		}
	}
	private void SliderDefault()
	{
		slide.value = 0;
	}
	private void SliderUpdate()
	{
		if (slide.value < 100)
		{
			slide.value += slider_value;
		}
	}
}