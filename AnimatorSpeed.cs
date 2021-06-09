using UnityEngine;
public class AnimatorSpeed : MonoBehaviour
{
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        animator.speed = Random.Range(0.5f, 2f);
    }
}
