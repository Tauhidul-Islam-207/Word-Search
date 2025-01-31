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
    public GameObject settingPanel;
    public List<Button> starButton; 
    private Color defaultWhite = new Color(1.0f, 1.0f, 1.0f, 0.1176f);

    
    
    //This method is used to load the main game scene.
    public void WordSearch()
    {
        SceneManager.LoadScene("Main");
    }

    
    //This method is used to initiate the lobby to game transition.
    public void PlayWordSearch()
    {
        click.Play();
        Invoke("WordSearch", 0.5f);
    }

    
    //This method is used to initiate the exit transition.
    public void ExitApp()
    {
        click.Play();
        Invoke("ExitApplication", 0.5f);
    }

    
    //This method is used to exit the application.
    public void ExitApplication()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    
    //This message is used to display a message if other games are clicked.
    public void OtherGameClick()
    {
        click.Play();
        messageScreen.SetActive(true);
        messagePanel.SetActive(true);
    }

    
    //This method is used to close a message window.
    public void CloseMessage()
    {
        click.Play();
        messageScreen.SetActive(false);
        messagePanel.SetActive(false);
        ratingPanel.SetActive(false);
        settingPanel.SetActive(false);
    }

    
    //This method is used to display the "Rate the Game" window.
    public void RateGameClick()
    {
        click.Play();
        messageScreen.SetActive(true);
        ratingPanel.SetActive(true);
    }
    
    
    //This method is used to rate the game according to the stars clicked.
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


    //This method is used to open the settings window.
    public void Settings()
    {
        click.Play();
        messageScreen.SetActive(true);
        settingPanel.SetActive(true);
    }
}





