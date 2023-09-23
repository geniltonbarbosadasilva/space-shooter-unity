using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuScript : MonoBehaviour
{
    public TextMeshProUGUI recordText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(recordText != null) recordText.text = "Record: " + PlayerPrefs.GetInt("score", 0).ToString();
    }

    public void Sair()
    {
        Time.timeScale = 1;
        StartCoroutine(WaitClose());
    }

    IEnumerator WaitClose()
    {
        yield return new WaitForSeconds(.5f);
        Application.Quit();
    }

    public void Jogar()
    {
        Time.timeScale = 1;
        StartCoroutine(WaitPlay());
    }

    IEnumerator WaitPlay()
    {
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene("Level");
    }

    public void VoltarMenu()
    {
        Time.timeScale = 1;
        StartCoroutine(BackMenu());
    }

    IEnumerator BackMenu()
    {
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene("Menu");
    }
}
