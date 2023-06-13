using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelAsync : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(960, 600, false);
        Invoke("Load", 2);
    }

    private void Load()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
