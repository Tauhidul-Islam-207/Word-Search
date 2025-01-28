using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Game/LevelData")]
public class LevelData : ScriptableObject
{
    public List<string> targetWords; // Words to find
    public int wordToBeFound;        // Number of words to find
    public int level;        // Number of words to find
    public bool isVertical = false;
    public bool isVerticalInvert = false;
    public bool isHorizontal = false;
    public bool isHorizontalInvert = false;
    public bool isDiagonal1 = false;
    public bool isDiagonal1Invert = false;
    public bool isDiagonal2 = false;
    public bool isDiagonal2Invert = false;
    public List<Vector2Int> verticalPlacements; // Positions for vertical words
    public List<Vector2Int> verticalInvertPlacements; // Positions for vertical words
    public List<Vector2Int> horizontalPlacements; // Positions for horizontal words
    public List<Vector2Int> horizontalInvertPlacements; // Positions for horizontal words
    public List<Vector2Int> diagonal1Placements; // Positions for diagonal words
    public List<Vector2Int> diagonal2Placements; // Positions for diagonal words
    public List<Vector2Int> diagonal1InvertPlacements; // Positions for diagonal words
    public List<Vector2Int> diagonal2InvertPlacements; // Positions for diagonal words
}




