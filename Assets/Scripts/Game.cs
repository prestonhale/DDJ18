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


[System.Serializable]
public enum GameState
{
  RevealPlayer,
  ReadyToStart,
  Started
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

  public static Game Instance;

  public Map map;

  private bool gameOver = false;

  public UnityEngine.UI.Text winnerText;
  public UnityEngine.UI.Text playAgainText;

  public GameState gameState = GameState.RevealPlayer;
  public UnityEngine.UI.Text hunterFailureNumber;
  public UnityEngine.UI.Text remainingDancers;

  public Font dancerFont;
  public Font hunterFont;

  public GameObject GameOverUI;

  public Player player;
  public NPCManager npcManager;

  public bool testingRevealPlayer = false;

  void Start(){
    if (testingRevealPlayer){
      SetupLevel();
    } else {
      SetupLevel();
      HidePlayer();
      StartLevel();
    }
  }
  
  public void SetupLevel() {
    player = Instantiate(player, Vector3.zero, Quaternion.identity).GetComponent<Player>();
    player.SpawnPlayer();
    npcManager = Instantiate(npcManager, Vector3.zero, Quaternion.identity).GetComponent<NPCManager>();
    npcManager.SpawnNPCs();

    hunterFailureNumber.text = "X X X";
  }

  public void HidePlayer(){
    gameState = GameState.ReadyToStart;
    npcManager.RevealAllColors();
  }

  public void StartLevel(){
    gameState = GameState.Started;
    Music.Instance.Play();
  }

  public void DancerWin()
  {
    Debug.Log("Dancer win!");
    GameOver(1);
  }

  public void HunterWin()
  {
    Debug.Log("Hunter win!");
    GameOver(0);
  }

  public void AddHunterFailure()
  {
    hunterFailures++;

    if (hunterFailures == 2)
    {
      hunterFailureNumber.text = "X";
    }

    if (hunterFailures == 1)
    {
      hunterFailureNumber.text = "X X";
    }

    if (hunterFailures >= maxHunterFailures)
    {
      hunterFailureNumber.text = "";
      GameOver(1);
    }
  }

  public void GameOver(int winner)
  {
    string winnerName = winner == 1 ? "Dancer" : "Hunter";
    Font font = winner == 1 ? dancerFont : hunterFont;
    var color = winner == 1 ? new Color(0.0f / 255.0f, 78.0f / 255.0f, 206.0f / 255.0f) : new Color(236.0f / 255f, 7.0f / 255f, 7.0f / 255f);

    winnerText.font = font;
    winnerText.color = color;
    playAgainText.font = font;
    winnerText.text = "The " + winnerName + " Won!";
    playAgainText.text = "Press Spacebar to Restart";
    StartCoroutine(FadeTo(0.0f, 1f));
    gameOver = true;
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

  void Update(){
    if (testingRevealPlayer){
      if (gameState == GameState.RevealPlayer && Input.GetKeyDown(KeyCode.Space)){
        HidePlayer();
      }
      else if (gameState == GameState.ReadyToStart && Input.GetKeyDown(KeyCode.Space)){
        StartLevel();
      }
    }
    if (gameOver == true && Input.GetKeyDown("space"))
    {
      UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }

    remainingDancers.text = Game.Instance.npcManager.npcCount - Game.Instance.npcManager.infectedCount + "/" + Game.Instance.npcManager.npcCount + " Remaining";
  }

}
