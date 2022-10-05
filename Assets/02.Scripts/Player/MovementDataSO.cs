using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[CreateAssetMenu(menuName = "SO/Agent/MovementData")]
public class MovementDataSO : ScriptableObject
{
    public MovementData _movementData;
    public Vector2 startPos;

    private float defaultSpeed;
    private float gravity;
    private bool isDashOnce = false;
    [SerializeField]
    private LoadingDataSO loadingDataSO;
    public float DefaultSpeed { get => defaultSpeed; set { defaultSpeed = value; } }
    public float Gravity { get => gravity; set { gravity = value; } }
    public bool IsDashOnce { get => isDashOnce; set { isDashOnce = value; } }

    public void MoveReset(PlayerMove user)
    {
        _movementData.IsCanDash = true;
        _movementData.IsDash = false;
        user.Rigid.gravityScale = gravity;
        _movementData.Speed = defaultSpeed;
        _movementData.IsRunning = false;

        if (LoadContainer.IsFirstLoad)
        {
            Potal.SetDefaultPos(this, loadingDataSO, SceneManager.GetActiveScene().name);
            LoadContainer.IsFirstLoad = false;
        }
        user.transform.position = startPos;
    }
}