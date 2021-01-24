using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	private Slider slide;
	public ScoreManager scoreManager;
	public GameObject Player;

	public float speed = 0.28f;
	public float jumpForce = 1000f;
	public float HorizontalJumpFactor = 100f;

	private Rigidbody2D rb2D;
	private Animation anim;

	public ParticleSystem forceJumpEffect;
	public ParticleSystem moveParticle;

	private float velocity;
	private float mp;
	private float screen_width;

	private const int slider_value = 10;

	private void Awake()
	{
		InitializeSlider();
		rb2D = GetComponent<Rigidbody2D>();
		anim = transform.GetChild(1).GetComponent<Animation>();
		screen_width = Screen.width;
		anim.wrapMode = WrapMode.Once;
	}
    private void Update()
    {
		velocity = rb2D.velocity.y;
		mp = Input.mousePosition.x;
	}
    private void FixedUpdate()
	{
		Move_With_Touch();
		JumpControl();
	}
	private void InitializeSlider()
	{
		slide = GameObject.FindGameObjectWithTag("Slider").GetComponent<Slider>();
		slide.value = 0;
		slide.maxValue = 100;
		slide.minValue = 0;
		slide.wholeNumbers = true;
	}
	private void Move_With_Touch()
	{
		if (Input.GetMouseButton(0)) // If the screen is touched //
		{
			if (mp < (screen_width / 2)) // Move Left //
			{
				Player.transform.Translate(-speed,0,0);
			}
			else // Move Right //
			{
				Player.transform.Translate(speed, 0, 0);
			}
		}
	}

	private void JumpControl()
	{
		if (velocity == 0)
		{
			Jump();
			anim.Play();
		}
		else if (velocity < 0)
		{
			forceJumpEffect.Stop();
			moveParticle.Stop();
		}
	}

	private void Jump()
	{
		if(slide.value < 100)
		{
			rb2D.AddForce(Vector2.up * (jumpForce + Mathf.Abs(rb2D.velocity.y) * HorizontalJumpFactor));
			SliderUpdate();
			forceJumpEffect.Stop();
			moveParticle.Play();
		}

		else if(slide.value >= 100)
        {
			rb2D.AddForce(Vector2.up * ((jumpForce * 3) + Mathf.Abs(rb2D.velocity.y) * HorizontalJumpFactor));
			SliderDefault();
			moveParticle.Stop();
			forceJumpEffect.Play();
		}
	}

	private void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Platform")
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
		if(slide.value < 100)
        {
			slide.value += slider_value;
		}
    }
}