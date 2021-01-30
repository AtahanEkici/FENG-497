using UnityEngine;
public class TextureController : MonoBehaviour
{
    public Material texture1;
    public Material texture2;

    private Rigidbody2D Player;
    private bool state = true;
    private float velocity;
    private MeshRenderer mr1;
    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        mr1 = GetComponent<MeshRenderer>();
    }
    private void Update()
    {
        velocity = Player.velocity.y;
    }
    void FixedUpdate()
    {
        if(velocity == 0)
        {
            ChangeFace();
        }
    }
    public void ChangeFace()
    {
        if (state == true)
        {
            mr1.material = texture2;
            state = false;
        }
        else
        {
            mr1.material = texture1;
            state = true;
        }
    }
}
