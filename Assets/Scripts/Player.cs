using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float movespeed = 6f;
    [SerializeField] private float rotation = 10f;
    [SerializeField] private GameInput gameInput;
    private bool isWalking;

    private void Update()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new(inputVector.x, 0f, inputVector.y);
        transform.position += movespeed * Time.deltaTime * moveDir;

        isWalking = moveDir != Vector3.zero;

        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotation);
    }

    public bool IsWalking()
    {
        return isWalking;
    }
}
