using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerConroller res;
    private void Awake()
    {
        res = GetComponentInParent<PlayerConroller>();
    }
    // Update is called once per frame
   
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
