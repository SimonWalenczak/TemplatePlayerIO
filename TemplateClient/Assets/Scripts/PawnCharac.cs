using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Pawn", menuName = "Pawn")]
public class PawnCharac : ScriptableObject
{
    public char Letter;
    public Sprite[] Sprites;
    public Vector2Int[] PossibleMovements;
}
