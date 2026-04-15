using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    int lifePoints = 50;
    InputAction moveAction;

    [SerializeField]
    float movementSpeed = 2f;

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
    }

    void Update()
    {
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        transform.position += (Vector3)moveValue * movementSpeed * Time.deltaTime;
    }
}
