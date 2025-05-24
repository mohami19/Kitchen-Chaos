using UnityEngine;

public class PlayerSound : MonoBehaviour
{

    private Player player;
    private float footstepTimer;
    private float footstepTimerMax = 0.5f;

    private void Awake()
    {
        player = GetComponent<Player>();
        if (player == null)
        {
            Debug.LogError("PlayerSound requires a Player component.");
        }
    }

    private void Update()
    {
        footstepTimer -= Time.deltaTime;
        if (footstepTimer <= 0f)
        {
            footstepTimer = footstepTimerMax;
            if (player.IsWalking())
            {
                float volume = 1f;
                SoundManager.Instance.PlayFootstepsSound(player.transform.position, volume);
            }
        }
    }
}
