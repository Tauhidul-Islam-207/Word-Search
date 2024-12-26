using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class MainManager : MonoBehaviour
{
    private List<string> targetWords = new List<string>(); // The information comes from Level Data
    private int level; // The information comes from Level Data
    private int wordToBeFound; // The information comes from Level Data
    private string formedWord = ""; 
    private bool isDragging = false; 
    private List<TextMeshPro> connectedWords = new List<TextMeshPro>();
    private List<TextMeshPro> greenWords = new List<TextMeshPro>();
    public List<TextMeshPro> wordMatrix;
    public List<TextMeshPro> dispWords; 
    public Button nextLvlButton;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI remainWordText;
    public List<LevelData> levelData; // This contains all the Level Data
    private int startIndex = 0; 
    private List<string> reserveWords = new List<string>(); 
    private List<string> foundWords = new List<string>(); 
    private int foundWordIndex = 0;
    private int reserveIndex = 0;
    public LineRenderer line;
    private List<Vector3> drawPositions = new List<Vector3>();
    private List<LineRenderer> permanentLines = new List<LineRenderer>();
    private Color customGreen = new Color(0.10196f, 1.0f, 0.0f, 0.88235f);
    private Color correctGreen = new Color(0.0f, 0.27451f, 0.0f, 1.0f);
    

    
    void Start()
    {
        ResetLevel();
        startIndex = 0;
        targetWords = levelData[startIndex].targetWords;
        wordToBeFound = levelData[startIndex].wordToBeFound;
        level = levelData[startIndex].level;
        UpdateLevelText();
        UpdateRemainingWordsText();
        GenerateRandomMatrix(); 
        PlaceAnswers();
        FillDispWords();
    }

    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0)) 
        {
            isDragging = true;
            line.gameObject.SetActive(true);
        }
        else if (Input.GetMouseButtonUp(0)) 
        {
            isDragging = false;
            CheckTextColor();
            connectedWords.Clear();
            DeActivateDrawing();
            formedWord = "";
        }

        if (isDragging)
        {
            Collider2D[] hitColliders = Physics2D.OverlapPointAll(mousePosition);
            foreach (var collider in hitColliders)
            {
                TextMeshPro textComponent = collider.GetComponent<TextMeshPro>();
                if (textComponent != null && !connectedWords.Contains(textComponent))
                {
                    formedWord += textComponent.text;
                    Debug.Log("Text: " + textComponent.text);
                    Debug.Log("Word is: " + formedWord);
                    connectedWords.Add(textComponent);

                    if(targetWords.Contains(formedWord) && !foundWords.Contains(formedWord))
                    {
                        Debug.Log("Succcess!");
                        wordToBeFound--;
                        foreach(TextMeshPro txt in connectedWords)
                        {
                            greenWords.Add(txt);
                            //txt.color = Color.green;
                            txt.color = correctGreen;
                            //txt.GetComponentInChildren<SpriteRenderer>().color = Color.black;
                        }
                        
                        LineRenderer newLine = Instantiate(line, line.transform.parent);
                        newLine.startColor = customGreen;
                        newLine.endColor = customGreen;
                        newLine.positionCount = drawPositions.Count;
                        newLine.SetPositions(drawPositions.ToArray());
                        permanentLines.Add(newLine);

                        
                        ChangeDispWordColor(formedWord);
                        foundWords.Add(formedWord);
                    }
                    else
                    {
                        foreach(TextMeshPro txt in connectedWords)
                        {
                            txt.color = Color.blue;
                            //txt.GetComponentInChildren<SpriteRenderer>().color = Color.yellow;
                        }
                    }
                }
            }

            DrawLine();
            CheckLevelComplete();
            UpdateLevelText();
            UpdateRemainingWordsText();
        }
    }

    public string GenerateRandomAlphabet()
    {
        char randomAlphabet = (char)('A' + Random.Range(0, 26));
        return randomAlphabet.ToString();
    }

    public void GenerateRandomMatrix()
    {
        foreach(TextMeshPro let in wordMatrix)
        {
            if (let != null)
            {
                let.text = GenerateRandomAlphabet();
            }
        }
    }

    public void PlaceWordVertical(int count, int index)
    {
        foreach(char i in targetWords[index])
        {
           wordMatrix[count].text =  i.ToString();
           count+=7;
        }
    }

    public void PlaceWordVerticalInvert(int count, int index)
    {
        foreach(char i in targetWords[index])
        {
           wordMatrix[count].text =  i.ToString();
           count-=7;
        }
    }

    public void PlaceWordHorizontal(int count, int index)
    {
        foreach(char i in targetWords[index])
        {
           wordMatrix[count].text =  i.ToString();
           count+=1;
        }
    }

    public void PlaceWordHorizontalInvert(int count, int index)
    {
        foreach(char i in targetWords[index])
        {
           wordMatrix[count].text =  i.ToString();
           count-=1;
        }
    }

    public void PlaceWordDiagonal1(int count, int index)
    {
        foreach(char i in targetWords[index])
        {
           wordMatrix[count].text =  i.ToString();
           count+=8;
        }
    }

    public void PlaceWordDiagonal1Invert(int count, int index)
    {
        foreach(char i in targetWords[index])
        {
           wordMatrix[count].text =  i.ToString();
           count-=8;
        }
    }

    public void PlaceWordDiagonal2(int count, int index)
    {
        foreach(char i in targetWords[index])
        {
           wordMatrix[count].text =  i.ToString();
           count+=6;
        }
    }

    public void PlaceWordDiagonal2Invert(int count, int index)
    {
        foreach(char i in targetWords[index])
        {
           wordMatrix[count].text =  i.ToString();
           count-=6;
        }
    }

    public void CheckTextColor()
    {
        foreach(TextMeshPro txt in connectedWords)
        {
            if(txt.color == Color.blue)
            {
                if(greenWords.Contains(txt))
                {
                    //txt.color = Color.green;
                    txt.color = correctGreen;
                    //txt.GetComponentInChildren<SpriteRenderer>().color = Color.black;
                }
                else
                {
                    txt.color = Color.black;
                    //txt.GetComponentInChildren<SpriteRenderer>().color = Color.white;
                }    
            }
        }        
    }

    public void ChangeDispWordColor(string word)
    {       
        if(targetWords.Count > dispWords.Count)
        {
            for(int i = 0; i < dispWords.Count; i++)
            {
                if(dispWords[i].text == word)
                {
                    dispWords[i].color = Color.green;
                    foundWordIndex = i;
                }
            }

            if(reserveIndex < reserveWords.Count)
            {
                dispWords[foundWordIndex].text = reserveWords[reserveIndex];
                dispWords[foundWordIndex].color = Color.black;
                reserveIndex++;
            }
        }
        else
        {
            foreach(TextMeshPro txt in dispWords)
            {
                if(txt.text == word)
                {
                    txt.color = Color.green;
                }
            }
        }
    }

    public void CheckLevelComplete()
    {
        if(wordToBeFound == 0)
        {
            nextLvlButton.gameObject.SetActive(true);
        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void GoToNextLevel()
    {
        startIndex++;

        if(startIndex < levelData.Count)
        {
            ResetLevel();
            targetWords = levelData[startIndex].targetWords;
            wordToBeFound = levelData[startIndex].wordToBeFound;
            level = levelData[startIndex].level;
            UpdateLevelText();
            UpdateRemainingWordsText();
            GenerateRandomMatrix();
            PlaceAnswers();
            FillDispWords();
        }
        else
        {
            Debug.Log("Game Completed");
            startIndex = 0;
            SceneManager.LoadScene("Menu");
        }
    }

    public void UpdateLevelText()
    {
        levelText.text = "Level " + level.ToString();
    }

    public void UpdateRemainingWordsText()
    {
        remainWordText.text = "Words Remaining " + wordToBeFound.ToString();
    }

    public void FillDispWords()
    {
        for(int i = 0; i < dispWords.Count && i < targetWords.Count; i++)
        {
            dispWords[i].text = targetWords[i];
            dispWords[i].transform.GetChild(0).gameObject.SetActive(true);
        }
            
        if(targetWords.Count > dispWords.Count)
        {
            for(int i = dispWords.Count; i < targetWords.Count; i++)
            {
                reserveWords.Add(targetWords[i]);
            }
        }
    }

    public void ResetLevel()
    {
        foreach(TextMeshPro word in wordMatrix)
        {
            word.text = "";
            word.color = Color.black;
            //word.GetComponentInChildren<SpriteRenderer>().color = Color.white;
        }

        foreach(TextMeshPro disptext in dispWords)
        {
            disptext.text = "";
            disptext.transform.GetChild(0).gameObject.SetActive(false);
            disptext.color = Color.black;
        }

        formedWord = "";
        isDragging = false; 
        connectedWords.Clear();
        greenWords.Clear();
        //targetWords.Clear();
        reserveWords.Clear();
        foundWords.Clear();
        drawPositions.Clear();
        RemoveGreenLines();
        permanentLines.Clear();
        reserveIndex = 0;
        foundWordIndex = 0;
        nextLvlButton.gameObject.SetActive(false);
    }

    public void PlaceAnswers()
    {
        if(levelData[startIndex].isVertical)
        {
            foreach (Vector2Int placement in levelData[startIndex].verticalPlacements)
            {
                PlaceWordVertical(placement.x, placement.y);
            }
        }

        if(levelData[startIndex].isVerticalInvert)
        {
            foreach (Vector2Int placement in levelData[startIndex].verticalInvertPlacements)
            {
                PlaceWordVerticalInvert(placement.x, placement.y);
            }
        }
        
        if(levelData[startIndex].isHorizontal)
        {
            foreach (Vector2Int placement in levelData[startIndex].horizontalPlacements)
            {
                PlaceWordHorizontal(placement.x, placement.y);
            }
        }

        if(levelData[startIndex].isHorizontalInvert)
        {
            foreach (Vector2Int placement in levelData[startIndex].horizontalInvertPlacements)
            {
                PlaceWordHorizontalInvert(placement.x, placement.y);
            }
        }
        
        if(levelData[startIndex].isDiagonal1)
        {
            foreach (Vector2Int placement in levelData[startIndex].diagonal1Placements)
            {
                PlaceWordDiagonal1(placement.x, placement.y);
            }
        }

        if(levelData[startIndex].isDiagonal1Invert)
        {
            foreach (Vector2Int placement in levelData[startIndex].diagonal1InvertPlacements)
            {
                PlaceWordDiagonal1Invert(placement.x, placement.y);
            }
        }

        if(levelData[startIndex].isDiagonal2)
        {
            foreach (Vector2Int placement in levelData[startIndex].diagonal2Placements)
            {
                PlaceWordDiagonal2(placement.x, placement.y);
            }
        }

        if(levelData[startIndex].isDiagonal2Invert)
        {
            foreach (Vector2Int placement in levelData[startIndex].diagonal2InvertPlacements)
            {
                PlaceWordDiagonal2Invert(placement.x, placement.y);
            }
        }
    }

    public void DrawLine()
    {
        drawPositions.Clear();

        foreach (var targetObject in connectedWords)
        {
            drawPositions.Add(targetObject.transform.position);  
        }

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        drawPositions.Add(mousePosition);

        line.positionCount = drawPositions.Count;
        line.SetPositions(drawPositions.ToArray());
    }

    public void DeActivateDrawing()
    {
        line.positionCount = 0;
        drawPositions.Clear();
        line.gameObject.SetActive(false);
    }

    public void RemoveGreenLines()
    {
        foreach(LineRenderer line in permanentLines)
        {
            line.positionCount = 0;
        }
    }
}
