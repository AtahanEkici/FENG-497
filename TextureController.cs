using UnityEngine;
public class TextureController : MonoBehaviour
{
    public Material texture1;
    public Material texture2;
    public GameObject Player;

    private Rigidbody2D r2d2;
    private bool state = true;
    private float velocity;
    private MeshRenderer[] mr;

    private void Awake()
    {
        r2d2 = Player.GetComponent<Rigidbody2D>();
        mr = Player.GetComponentsInChildren<MeshRenderer>();
    }
    private void Update()
    {
        velocity = r2d2.velocity.y;
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
            mr[0].material = texture2;
            state = false;
        }
        else
        {
            mr[0].material = texture1;
            state = true;
        }
    }
}
