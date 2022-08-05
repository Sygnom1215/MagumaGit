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
    [SerializeField]
    private GameObject fireObject = null;
    private bool isFire = false;

    private void OnEnable()
    {
        if (GameObject.Find("TeleportFire(Clone)") != null || fireObject != null)
        {
            Destroy(fireObject);
            Destroy(GameObject.Find("TeleportFire(Clone)"));
        }

        fireObject = Instantiate(firePrefab, Vector3.zero, Quaternion.identity);
        fireObject.SetActive(false);
    }

    void Update()
    {
        CheckDistance();
    }

    public void CheckFireOrTeleport()
    {
        if (!isFire && movementDataSO._movementData.IsGrounded)
        {
            SetFire();
        }
        else if (isFire)
        {
            Teleport();
        }
    }

    private void Teleport()
    {
        transform.position = fireObject.transform.position;
        isFire = false;
        fireObject.SetActive(false);
    }

    private void SetFire()
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