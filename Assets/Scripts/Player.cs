using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float movespeed = 6f;
    [SerializeField] private float rotation = 10f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask counterLayerMask;

    private bool isWalking;
    private Vector3 lastInterctionDir;
    private void Update()
    {
        HandleMovement();
        HandleInteraction();
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    private void HandleInteraction()
    {

        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero)
        {
            lastInterctionDir = moveDir;
        }

        float interactionDistance = 2f;
        if (Physics.Raycast(transform.position, lastInterctionDir, out RaycastHit hit,
                    interactionDistance, counterLayerMask))
        {
            if (hit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                clearCounter.Ineract();
            }
        }
    }

    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new(inputVector.x, 0f, inputVector.y);

        float moveDistance = movespeed * Time.deltaTime;
        float playerRadius = 0.7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,
                        playerRadius, moveDir, moveDistance);

        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0f).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,
                        playerRadius, moveDirX, moveDistance);

            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0f, 0f, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,
                            playerRadius, moveDirZ, moveDistance);

                if (canMove)
                {
                    moveDir = moveDirZ;
                }
            }
        }

        if (canMove)
        {
            transform.position += moveDistance * moveDir;
        }

        isWalking = moveDir != Vector3.zero;

        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotation);
    }
}
