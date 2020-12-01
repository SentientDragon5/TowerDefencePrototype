using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class SceneActions : MonoBehaviour
{
    public enum SceneIndex { MainMenu, CustomGame, Loading};

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GotoScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
