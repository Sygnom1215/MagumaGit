using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    [SerializeField]
    private MovementDataSO movementDataSO;
    [SerializeField]
    private GameObject firePrefab = null;
    [SerializeField]
    private float teleportDistance = 0f;
    private GameObject fireObject = null;
    private bool isFire = false;

    private void Start()
    {
        fireObject = Instantiate(firePrefab, Vector3.zero, Quaternion.identity);
        fireObject.SetActive(false);
    }

    void Update()
    {
        CheckDistance();
    
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (!isFire && movementDataSO._movementData.IsGrounded)
            {
                Fire();
            }
            else if (isFire)
            {
                Teleport();
            }
        }
    }

    private void Teleport()
    {
        transform.position = fireObject.transform.position;
        isFire = false;
        fireObject.SetActive(false);
    }

    private void Fire()
    {
        fireObject.transform.position = transform.position;
        isFire = true;
        fireObject.SetActive(true);
    }

    private void CheckDistance()
    {
        float distance = Vector3.Distance(transform.position, fireObject.transform.position);
        if (distance > teleportDistance)
        {
            isFire = false;
            fireObject.SetActive(false);
        }
    }
}