using System.Collections;
using System.Collections.Generic;
using GG.Infrastructure.Utils.Swipe;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class Ball_Movement : MonoBehaviour
{
    [SerializeField] private SwipeListener swipeListener;
    [SerializeField] private Level_Manager level_Manager;

    [SerializeField] private float stepDuration = 0.1f;
    [SerializeField] private LayerMask wallsAndRoadsLayer;
    private const float MAX_RAY_DISTANCE = 30f;

    private Vector3 moveDirection;
    public bool canMove = true;

    void Start()
    {
        transform.position = level_Manager.defaultBall_RoadTile.position;

        swipeListener.OnSwipe.AddListener(swipe =>
        {
            switch (swipe)
            {
                case "Right":
                    moveDirection = Vector3.right;
                    break;

                case "Left":
                    moveDirection = Vector3.left;
                    break;

                case "Up":
                    moveDirection = Vector3.forward;
                    break;

                case "Down":
                    moveDirection = Vector3.back;
                    break;
            }

            MoveBall();
        });
    }

    void MoveBall()
    {
        if (canMove)
        {
            canMove = false;

            //Add raycast in swipe direction (from the ball)
            RaycastHit[] hits = Physics.RaycastAll(transform.position, moveDirection, MAX_RAY_DISTANCE, wallsAndRoadsLayer.value).OrderBy(h => h.distance).ToArray();

            Vector3 targetPosition = transform.position;

            int steps = 0;

            for(int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.isTrigger)
                {
                    //Road
                }
                else
                {
                    //Wall
                    if(i == 0) //Wall is near the ball
                    {
                        canMove = true;
                        return;
                    }
                    //else:
                    steps = i;

                    targetPosition = hits[i - 1].transform.position;
                    break;
                }
            }

            //Move the ball to targetPostion
            float moveDuration = stepDuration * steps;

            transform
                .DOMove(targetPosition, moveDuration)
                .SetEase(Ease.OutExpo)
                .OnComplete(() => canMove = true);
        }
    }
}
