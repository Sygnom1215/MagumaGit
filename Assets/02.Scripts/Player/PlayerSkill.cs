using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    [SerializeField]
    private MovementDataSO movementDataSO;

    [SerializeField]
    private GameObject glowObject;
    private Rigidbody2D rigid;
    public bool isGlow = false;
    private bool isDiveOnce = false;
    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        Glow();
    }
    private void FixedUpdate()
    {
        if (movementDataSO._movementData.IsDive && !isDiveOnce)
        {
            rigid.velocity += Vector2.down * 7f;

        }
        else if (movementDataSO._movementData.MoveInput != 0)
        {
            rigid.velocity = new Vector2(movementDataSO._movementData.MoveInput * movementDataSO._movementData.Speed, rigid.velocity.y);
        }
    }

    private void Glow()
    {
        glowObject.SetActive(isGlow);

    }
    public void Dive()
    {
        if (movementDataSO._movementData.IsCanDive && !movementDataSO._movementData.IsDive)
        {
            StartCoroutine(DiveIE());
        }
    }

    private IEnumerator DiveIE()
    {
        Debug.Log("doDash");
        float gravity = rigid.gravityScale;
        rigid.gravityScale = 0;
        movementDataSO._movementData.IsDive = true;
        yield return new WaitForSeconds(0.1f);

        rigid.velocity = Vector2.zero;
        movementDataSO._movementData.IsDive = false;
        rigid.gravityScale = gravity;
        yield return new WaitForSeconds(2f); //ÄðÅ¸ÀÓ
        isDiveOnce = false;
    }
}
