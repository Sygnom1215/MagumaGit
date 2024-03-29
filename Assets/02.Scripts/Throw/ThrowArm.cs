using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowArm : MonoBehaviour
{
    private Vector2 dir;
    [SerializeField]
    private float force;

    [SerializeField]
    private GameObject pointPrefab;
    [SerializeField]
    private Transform fireTransform;
    [SerializeField]
    private Transform PointsParentTransform;
    [SerializeField]
    private int numberOfPoints;
    private GameObject[] points;
    private void Start()
    {
        points = new GameObject[numberOfPoints];
        float alpa = 1f;
        for (int i = 0; i < numberOfPoints; i++)
        {
            points[i] = Instantiate(pointPrefab, fireTransform.position, fireTransform.rotation);
            points[i].transform.SetParent(PointsParentTransform);

            SpriteRenderer renderer = points[i].GetComponent<SpriteRenderer>();
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, alpa);
            if (alpa > 0)
                alpa -= 0.08f;
        }
        PointsParentTransform.gameObject.SetActive(false);
    }
    private void Update()
    {
        Vector2 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 bowPos = transform.position;

        dir = MousePos - bowPos; //방향 계산
        FaceMouse();

        for (int i = 0; i < points.Length; i++)
        {
            points[i].transform.position = PointPos(i * 0.05f);
        }
    }
    private void FaceMouse()
    {
        transform.right = dir;
    }
    private Vector2 PointPos(float t)
    {
        //P = p1 +vel*t+at^2/2
        //궤적 계산 공식
        Vector2 currentPoints = (Vector2)fireTransform.position + dir.normalized * (force * t) + 0.5f * Physics2D.gravity * (t * t);
        return currentPoints;
    }
}
