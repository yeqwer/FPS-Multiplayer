using Unity.VisualScripting;
using UnityEngine;

public class ArmsShakeController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponentInParent<Rigidbody>();
    }

    private void Update()
    {
        if (rb) 
        {
            animator.speed = rb.velocity.magnitude * 0.1f;
        }
    }
}
