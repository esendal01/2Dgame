using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuManagerInGame : MonoBehaviour
{
    public GameObject InGameScren, PauseScren;
    // Start is called before the first frame update

    private PlayerConroller res;

    private void Awake()
    {
        res = GetComponentInParent<PlayerConroller>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            InGameScren.SetActive(false);
            PauseScren.SetActive(true);
        }

    }
    

    public void PlayButton()
    {
        Time.timeScale = 1;
        InGameScren.SetActive(true);
        PauseScren.SetActive(false);

    }

    public void RePlayButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void HomeButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void exitButton()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
