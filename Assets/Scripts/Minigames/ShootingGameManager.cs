using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class ShootingGameManager : MonoBehaviour
{
    [SerializeField]
    GameObject crosshair;
    [SerializeField]
    List<ShootingGameTargetObject> targets;

    [SerializeField]
    Transform crosshairStartPosition;

    Vector2 prevCrosshairPosition, nextCrosshairPosition;

    [SerializeField]
    float crosshairSpeed = 1.0f;

    InputAction shootAction;
    [SerializeField]
    float shootDelay = 1f;

    int currentScore = 0;
    int winningScore = 5;

    [SerializeField]
    float timeLimit = 30f;

    [SerializeField]
    Vector2 minRange;
    [SerializeField]
    Vector2 maxRange;

    [SerializeField]
    float maxDistanceFromTargets = 1.5f;


    private void Start()
    {
        shootAction = InputSystem.actions.FindAction("Shoot");
        shootAction.performed += HandleShootAction;

        SetShootingArea();
        crosshair.transform.position = crosshairStartPosition.position;
        prevCrosshairPosition = crosshairStartPosition.position;
        nextCrosshairPosition = GetNextCrossHairPositionRandom();
    }

    void SetShootingArea()
    {
        float minX = targets[0].transform.position.x, maxX = targets[0].transform.position.x;
        float minY = targets[0].transform.position.y, maxY = targets[0].transform.position.y;

        foreach (ShootingGameTargetObject target in targets)
        {
            if (target.transform.position.x < minX)
            {
                minX = target.transform.position.x;
            }
            else if (target.transform.position.x > maxX)
            {
                maxX = target.transform.position.x;
            }

            if (target.transform.position.y < minY)
            {
                minY = target.transform.position.y;
            }
            else if (target.transform.position.y > maxY)
            {
                maxY = target.transform.position.y;
            }
        }

        minRange = new Vector2(minX - maxDistanceFromTargets, minY - maxDistanceFromTargets);
        maxRange = new Vector2(maxX + maxDistanceFromTargets, maxY + maxDistanceFromTargets);
    }

    void Update()
    {
        //Mover el crosshair desde prevPosition a nextPosition
        //Investigar e implementar con Lerp
    }

    Vector2 GetNextCrossHairPositionRandom()
    {
        return new Vector2(Random.Range(minRange.x, maxRange.x), Random.Range(minRange.y, maxRange.y));
    }

    void HandleShootAction(InputAction.CallbackContext context)
    {

    }
}
