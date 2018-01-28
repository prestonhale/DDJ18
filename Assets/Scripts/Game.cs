using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum Direction
{
  N,
  NE,
  E,
  SE,
  S,
  SW,
  W,
  NW,
  None
}


public static class Directions
{
  private static Vector3[] vectors = {
        new Vector3 (0, 0, 1),
        new Vector3 (1, 0, 1),
        new Vector3 (1, 0, 0),
        new Vector3 (1, 0, -1),
        new Vector3 (0, 0, -1),
        new Vector3 (-1, 0, -1),
        new Vector3 (-1, 0, 0),
        new Vector3 (-1, 0, 1),
        new Vector3 (0, 0, 0)
    };

  public static Vector3 ToVector3(this Direction direction)
  {
    return vectors[(int)direction];
  }
}

public class Game : MonoBehaviour
{

  public int hunterFailures = 0;
  public int maxHunterFailures = 3;

  public int dancerLocations = 0;
  public int maxDancerLocations = 5;

  public static Game Instance;

  public Map map;

  private bool gameOver = false;

  public UnityEngine.UI.Text winnerText;
  public UnityEngine.UI.Text playAgainText;


  public Font dancerFont;
  public Font hunterFont;

  public GameObject GameOverUI;

  public Player player;
  public NPCManager npcManager;

  void Start(){
    StartLevel();
  }
  
  public void StartLevel() {
    player = Instantiate(player, Vector3.zero, Quaternion.identity).GetComponent<Player>();
    player.SpawnPlayer();
    npcManager = Instantiate(npcManager, Vector3.zero, Quaternion.identity).GetComponent<NPCManager>();
    npcManager.SpawnNPCs();
  }

  public void DancerWin()
  {
    Debug.Log("Dancer win!");
    GameOver(1);
  }

  public void HunterWin(){
    Debug.Log("Hunter win!");
    GameOver(0);
  }
  
  public void AddHunterFailure()
  {
    hunterFailures++;

    if (hunterFailures >= maxHunterFailures)
    {
      GameOver(0);
    }
  }

  public void GameOver(int winner)
  {
    string winnerName = winner == 0 ? "Dancer" : "Hunter";
    Debug.Log(winnerName);
    // Font font = winner == 0 ? dancerFont : hunterFont;
    // var color = winner == 0 ? new Color(0.0f / 255.0f, 78.0f / 255.0f, 206.0f / 255.0f) : new Color(236.0f / 255f, 7.0f / 255f, 7.0f / 255f);

    // winnerText.font = font;
    // winnerText.color = color;
    // playAgainText.font = font;
    // winnerText.text = "The " + winnerName + " Won!";
    // playAgainText.text = "Press Square to Play Again.";
    // StartCoroutine(FadeTo(0.0f, 1f));
    // gameOver = true;
  }


  IEnumerator FadeTo(float aValue, float aTime)
  {
    for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
    {
      GameOverUI.GetComponent<CanvasGroup>().alpha = 0.75f * t;
      yield return null;
    }
  }
    
  void Awake()
  {
    if (Instance != null)
    {
      Destroy(gameObject);
      return;
    }
    Instance = this;

  }

  // Update is called once per frame
  void Update()
  {
    if (gameOver == true && Input.GetKeyDown("joystick 1 button 0"))
    {
      UnityEngine.SceneManagement.SceneManager.LoadScene("Warehouse - Patric");
    }
  }

}
