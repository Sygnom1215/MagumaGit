using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ObjectMoving : MonoBehaviour
{
    public enum MovingState
    {
        Up,
        Down,
        None
    }

    public float movingDistance = 0.5f; //중심을 기준으로 움직일 범위 
    public float waitTime = 0.2f;
    private Vector2 maxPosition = Vector2.zero;
    private Vector2 minPosition = Vector2.zero;
    private Vector2 midPosition = Vector2.zero;

    [SerializeField]
    private MovingState state = MovingState.None;

    private void Start()
    {
        midPosition = gameObject.transform.position;
        maxPosition = midPosition + Vector2.up * movingDistance;
        minPosition = midPosition - Vector2.up * movingDistance;
        state = MovingState.Up;
        StartCoroutine(Moving());
    }

    private IEnumerator Moving()
    {
        gameObject.transform.DOMoveY(maxPosition.y, 0.5f);
        yield return new WaitForSeconds(0.5f);
        state = MovingState.Down;
        yield return new WaitForSeconds(waitTime);

        while (true)
        {
            switch (state)
            {
                case MovingState.Up:
                    gameObject.transform.DOMoveY(maxPosition.y, 1f);
                    yield return new WaitForSeconds(1f);
                    state = MovingState.Down;
                    break;
                case MovingState.Down:
                    gameObject.transform.DOMoveY(minPosition.y, 1f);
                    yield return new WaitForSeconds(1f);
                    state = MovingState.Up;
                    break;
            }
            yield return new WaitForSeconds(waitTime);
        }
    }
}
