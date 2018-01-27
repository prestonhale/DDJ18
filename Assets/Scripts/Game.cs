using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{

  public int hunterFailures = 0;
  public int maxHunterFailures = 3;

  public int dancerLocations = 0;
  public int maxDancerLocations = 5;

  public static Game Instance;

  public Canvas canvas;

  public Map map;

  private CanvasGroup canvasGroup;
  private bool gameOver = false;

  public UnityEngine.UI.Text text;

  public void AddDancerLocation()
  {
    dancerLocations++;

    if (dancerLocations >= maxDancerLocations)
    {
      GameOver(0);
    }
  }

  public void AddHunterFailure()
  {
    hunterFailures++;

    if (hunterFailures >= maxHunterFailures)
    {
      GameOver(1);
    }
  }

  public void GameOver(int winner)
  {
    string winnerName = winner == 0 ? "Dancer" : "Hunter";

    canvasGroup.alpha = 1;
    text.text = "The " + winnerName + " Won!";
    gameOver = true;
  }
  void Awake()
  {
    if (Instance != null)
    {
      Destroy(gameObject);
      return;
    }
    Instance = this;

    canvasGroup = canvas.GetComponent<CanvasGroup>();
  }

  // Use this for initialization
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    if (gameOver == true && Input.GetKeyDown("joystick 1 button 0"))
    {
      UnityEngine.SceneManagement.SceneManager.LoadScene("patricScene");
    }
  }
}
