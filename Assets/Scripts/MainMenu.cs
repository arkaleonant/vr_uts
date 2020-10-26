using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void PindahHalaman(string halaman)
    {
        SceneManager.LoadScene(halaman);
    }

    // Update is called once per frame
    public void KeluarAplikasi()
    {
        Application.Quit();
    }
}
