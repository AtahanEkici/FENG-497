using System;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
	public ScoreManager scoreManager;
	public GameObject Player;
	public ParticleSystem forceJumpEffect;
	public ParticleSystem moveParticle;

	private readonly Vector3 jump = new Vector3(0.0f, 2.0f, 0.0f);
	private Slider slide;
	private Rigidbody2D rb2D;
	private Animation anim;
	private float velocity;
	private float mp;
	private float screen_width;
	private readonly int slider_value = 10;
	private readonly float jump_Force = 10f;
	private readonly float power_jump_Force = 30f;
	private readonly float speed = 13f;
	private float targetTime = 0.75f;
	private bool isReady = true;

	private void Awake()
	{
		slide = GameObject.FindGameObjectWithTag("Slider").GetComponent<Slider>();
		anim = transform.GetChild(1).GetComponent<Animation>();
		rb2D = GetComponent<Rigidbody2D>();
	}
	private void Start()
	{
		anim.wrapMode = WrapMode.Once;
		InitializeSlider();
		screen_width = Screen.width;
	}
	private void Update()
	{
		velocity = rb2D.velocity.y;
		mp = Input.mousePosition.x;
		Movement_Control();
		TimerControl();
	}
	private void FixedUpdate()
	{
		JumpControl();
	}
	private void InitializeSlider()
	{
		slide.value = 0;
		slide.wholeNumbers = true;
		slide.minValue = 0;
		slide.maxValue = 100;
	}
	private void Movement_Control()
    {
		Move_With_Mouse();
	}
	
	private void Move_With_Mouse()
	{
		if (Input.GetMouseButton(0)) // Ekranı 2'ye ayırıp basılan tarafa göre karakteri hareket ettirme fonksiyonu //
		{
			if (mp < (screen_width / 2))
			{
				Player.transform.Translate(-speed * Time.deltaTime, 0, 0);
			}
			else if (mp > (screen_width / 2))
			{
				Player.transform.Translate(speed * Time.deltaTime, 0, 0);
			}
		}
	}
	private void Move_With_Keyboard()
	{
		if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
		{
			Player.transform.Translate(-speed * Time.deltaTime, 0, 0);
		}
		else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
		{
			Player.transform.Translate(speed * Time.deltaTime, 0, 0);
		}
	}

	private void TimerControl()
    {
		targetTime -= Time.deltaTime;

		if (targetTime <= 0.0f)
		{
			isReady = true;
		}
	}

	private void JumpControl()
	{
		if (velocity == 0 && isReady == true)
		{
			Velocity_Is_Zero(); 
		}
		else if (velocity < 0)
		{
			Velocity_Is_Minus();
		}
	}
	private void Jump()
	{
		isReady = false;

		if (slide.value < 100)
		{
			Normal_Jump();
			After_Normal_Jump();
		}
		else if(slide.value == 100)
		{
			Power_Jump();
			After_Power_Jump();
		}	
	}

	private void Velocity_Is_Minus() 
    {
		forceJumpEffect.Stop();
		moveParticle.Stop();
    }

	private void Velocity_Is_Zero()
	{
		anim.Play();
		Jump();
	}

	private void Normal_Jump()
    {
		rb2D.AddForce((jump * jump_Force), ForceMode2D.Impulse);
	}

    private void Power_Jump()
    {
		rb2D.AddForce((jump * power_jump_Force), ForceMode2D.Impulse);
	}

	private void After_Normal_Jump()
    {
		SliderUpdate();
		forceJumpEffect.Stop();
		moveParticle.Play();
	}

	private void After_Power_Jump()
    {
		SliderDefault();
		moveParticle.Stop();
		forceJumpEffect.Play();
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