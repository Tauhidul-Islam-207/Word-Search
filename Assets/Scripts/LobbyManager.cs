using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    
    public AudioSource click;
    public GameObject messageScreen;
    public GameObject messagePanel;
    public GameObject ratingPanel;
    public List<Button> starButton; 
    private Color defaultWhite = new Color(1.0f, 1.0f, 1.0f, 0.1176f);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WordSearch()
    {
        SceneManager.LoadScene("Main");
    }

    public void PlayWordSearch()
    {
        click.Play();
        Invoke("WordSearch", 0.5f);
    }

    
    public void ExitApp()
    {
        click.Play();
        Invoke("ExitApplication", 0.5f);
    }

    public void ExitApplication()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void OtherGameClick()
    {
        click.Play();
        messageScreen.SetActive(true);
        messagePanel.SetActive(true);
    }

    public void CloseMessage()
    {
        click.Play();
        messageScreen.SetActive(false);
        messagePanel.SetActive(false);
        ratingPanel.SetActive(false);
    }

    public void RateGameClick()
    {
        click.Play();
        messageScreen.SetActive(true);
        ratingPanel.SetActive(true);
    }
    
    public void RatingClick(Button clickedButton)
    {
        for(int i=0; i<starButton.Count; i++)
        {
            if(clickedButton == starButton[i])
            {
                for(int j=0; j<=i; j++)
                {
                    starButton[j].image.color = Color.yellow;
                }

                for(int k=i+1; k<starButton.Count; k++)
                {
                    starButton[k].image.color = defaultWhite;
                }
            }
        }

        ratingPanel.transform.GetChild(1).gameObject.SetActive(true);
    }
}
