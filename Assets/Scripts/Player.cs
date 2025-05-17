using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float movespeed = 6f;
    [SerializeField] private float rotation = 10f;
    private bool isWalking;
    private void Update()
    {
        Vector2 inputVector = new();
        if (Input.GetKey(KeyCode.W))
        {
            inputVector.y += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputVector.y -= 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x += 1;
        }

        inputVector = inputVector.normalized;

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
