using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class UIController : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnStartClick()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void OnExitClick()
    {
        Application.Quit();
    }
}
