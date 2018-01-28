using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Canvas introCanvas;
    public Canvas instructionsCanvas;

    public Text promptText;

    bool instructionsShowing = false;

    void Start()
    {
        StartCoroutine("BlinkPrompt");
        introCanvas.gameObject.SetActive(true);
        instructionsCanvas.gameObject.SetActive(false);
        promptText.gameObject.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            if (instructionsShowing)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Warehouse - Preston");
            }
            else
            {
                introCanvas.gameObject.SetActive(false);
                instructionsCanvas.gameObject.SetActive(true);
                StopCoroutine("BlinkPrompt");
                promptText.gameObject.SetActive(false);
                instructionsShowing = true;
            }
        }
    }

    IEnumerator BlinkPrompt()
    {
        bool showing = false;
        promptText.gameObject.SetActive(false);

        while (true)
        {
            showing = !showing;
            promptText.enabled = showing;
            yield return new WaitForSeconds(0.8f);
        }

    }
}
