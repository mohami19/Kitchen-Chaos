using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    private const string IS_WALKING = "IsWalking";
    [SerializeField] private Player player;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found on the player object.");
        }

        animator.SetBool(IS_WALKING, player.IsWalking());
    }

    private void Update()
    {
        if (animator != null)
        {
            animator.SetBool(IS_WALKING, player.IsWalking());
        }
    }
}
