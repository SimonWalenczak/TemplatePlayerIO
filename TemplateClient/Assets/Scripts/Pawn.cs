using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private Vector2Int _coord;

    public Sprite CurrentSprite { get; private set; }
    public ChessColor PawnColor { get; private set; }
    public Vector2Int[] PossibleMovement { get; private set; }

    private void Start()
    {
    }

    public void InitBoard(Sprite newSprite)
    {
        _spriteRenderer.sprite = newSprite;
        GetComponent<BoxCollider2D>().enabled = false;
    }

    public void InitChess(Sprite newSprite, ChessColor chessColor, Vector2Int[] possibleMov, Vector2Int coord)
    {
        PawnColor = chessColor;
        PossibleMovement = possibleMov;
        CurrentSprite = newSprite;
        _spriteRenderer.sprite = newSprite;
        _spriteRenderer.enabled = true;
        
        if(coord.x >= 0)
            _coord = coord;
        
        _spriteRenderer.sortingOrder = 2;
    }

    public void InitNoChess(Vector2Int coord)
    {
        PawnColor = ChessColor.Nothing;
        _spriteRenderer.enabled = false;
        _coord = coord;
        _spriteRenderer.sortingOrder = 2;
    }

    private void OnMouseDown()
    {
        if (GridManager.Instance.SelectedPawn != new Vector2Int(-1, -1)
            && GridManager.Instance.GetIfClickOnHover(_coord))
        {
            Debug.Log("Hé ! Grid Manager ! J'ai bougé !");
            GridManager.Instance.SendMovePawn(_coord);
            ResetMovement();
            return;
        }
        
        if(PawnColor != ChessColor.Nothing)
            DrawMovement();
        else
            ResetMovement();
    }

    private void DrawMovement()
    {
        ResetMovement();
        // print("down");
        GridManager.Instance.DrawPossibleMovement(PossibleMovement, _coord);
    }

    private void ResetMovement()
    {
        // print("reset");
        GridManager.Instance.ResetPossibleMovement();
    }

    public void Deactivate()
    {
        PawnColor = ChessColor.Nothing;
        _spriteRenderer.enabled = false;
    }
}
