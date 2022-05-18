using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalMove : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;

    private Rigidbody2D rigid;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        rigid.velocity += Vector2.right * speed * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if(col.tag == "AnimalZone")
        {
            transform.Rotate(new Vector3(0, 180, 0));
        }
    }
}
