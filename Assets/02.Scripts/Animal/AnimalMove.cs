using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalMove : MonoBehaviour
{
    [SerializeField]
    private float speed = 3f;
    private void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
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
