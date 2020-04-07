//it loads the sceans based on string name of the scean
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class load_scenes : MonoBehaviour
{
    public string scene;

    public void loadScene()
    {
        SceneManager.LoadScene(scene);
    }
}
