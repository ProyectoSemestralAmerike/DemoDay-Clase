using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    int lifePoints = 50;
    InputAction moveAction;

    InputAction takePillAction;

    [SerializeField]
    float movementSpeed = 2f;

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        moveAction.performed += Move;

        takePillAction = InputSystem.actions.FindAction("Attack");
        takePillAction.performed += TakePill;
    }

    void Move(InputAction.CallbackContext ctx)
    {
        Vector2 moveDirection = ctx.ReadValue<Vector2>();

        if (CanMove(moveDirection))
        {
            transform.position += (Vector3)moveDirection;
        }

    } 

    bool CanMove(Vector2 movingDirection)
    {
        Vector3Int gridPosition = GameManager.Instance.GroundTilemap.WorldToCell(transform.position + (Vector3)movingDirection);

        if(GameManager.Instance.GroundTilemap.HasTile(gridPosition) && !GameManager.Instance.ObstacleTilemap.HasTile(gridPosition))
        {
            return true;
        }

        return false;
    }

    void TakePill(InputAction.CallbackContext ctx)
    {
        GameManager.Instance.PillWasTaken();
    }
}
