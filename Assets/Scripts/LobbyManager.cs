using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    
    public AudioSource click;

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
}
