using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class FadeTile : MonoBehaviour
{
    [SerializeField]
    private float fadeSpeed = 10.0f;
    [SerializeField]
    private Tilemap tilemap;
    public Transform vec = null;
    public Collider2D collider2d;
    private Vector2 tilePosition = Vector2.zero;
    void Start()
    {

    }
    private void Update()
    {
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            tilePosition = vec.position - new Vector3(0f, 1f, 0f);
            Paint();
        }
    }
    [ContextMenu("Paint")]
    void Paint()
    {
        Color alpahColor = new Color(255f, 255f, 255f, 0f);
        Vector3Int tilePos = tilemap.WorldToCell(tilePosition);
        tilemap.SetTileFlags(tilePos, TileFlags.None);
        //tilemap.SetColor(tilePos, new Color(255f,255f,255f,Mathf.Lerp(1f,0f,fadeSpeed)));
        tilemap.SetColor(tilePos, alpahColor);

        Debug.Log($"{tilePos}, {tilePosition},{tilemap.color}");

    }
}