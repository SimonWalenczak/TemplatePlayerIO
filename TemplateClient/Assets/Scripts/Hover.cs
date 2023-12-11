using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Hover : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    
    public Vector2Int Coord { get; set; }


    public void InitHover(Sprite newSprite, Vector2Int coord)
    {
        _spriteRenderer.sortingOrder = 2;
        _spriteRenderer.sprite = newSprite;
        _spriteRenderer.color = Color.magenta;
        _spriteRenderer.enabled = false;
        Coord = coord;
    }

    public SpriteRenderer GetSpriteRend()
    {
        return _spriteRenderer;
    }
}