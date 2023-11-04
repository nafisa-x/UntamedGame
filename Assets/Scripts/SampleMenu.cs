using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SampleMenu : MonoBehaviour
{

    public GameObject titleScreen;
    public GameObject menuScreen;
    // Start is called before the first frame update
    void Start()
    {
        titleScreen.SetActive(true);
        menuScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            if (titleScreen.activeInHierarchy == true)
            {
                titleScreen.SetActive(false);
                menuScreen.SetActive(true);
            }
            else if (menuScreen.activeInHierarchy == true)
            {
                SceneManager.LoadSceneAsync("NS-Test");
                menuScreen.SetActive(false);
            }
        }

    }
}
