using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    int lifePoints = 50;
    InputAction moveAction;

    [SerializeField]
    float movementSpeed = 2f;

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        moveAction.performed += Move;
    }

    void Move(InputAction.CallbackContext ctx)
    {
        Vector2 moveDirection = ctx.ReadValue<Vector2>();
        Debug.Log(moveDirection);
        if (CanMove(moveDirection))
        {
            Debug.Log("Can move");
            transform.position += (Vector3)moveDirection;
        }

    } 

    bool CanMove(Vector2 movingDirection)
    {
        Vector3Int gridPosition = GameManager.Instance.GroundTilemap.WorldToCell(transform.position + (Vector3)movingDirection);
        Debug.Log(gridPosition);
        if(GameManager.Instance.GroundTilemap.HasTile(gridPosition) && !GameManager.Instance.ObstacleTilemap.HasTile(gridPosition))
        {
            return true;
        }

        Debug.Log("can't move to " + gridPosition + ". Ground: "+ GameManager.Instance.GroundTilemap.HasTile(gridPosition) + ". Obstacles: " + GameManager.Instance.ObstacleTilemap.HasTile(gridPosition));
        return false;
    }
}
