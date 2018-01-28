using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwoBehavior : MonoBehaviour
{
  public float sensitivity;
  public float minX;
  public float maxX;

  public float minZ;
  public float maxZ;

  private bool touchingPlayer = false;
  private bool touchingComputer = false;

  public void Start()
  {
    maxX = Game.Instance.map.maxX;
    minX = Game.Instance.map.minX;
    maxZ = Game.Instance.map.maxZ;
    minZ = Game.Instance.map.minZ;
  }

  Vector3 GetMovement()
  {
    Vector3 pos = transform.position;

    float moveHorizontal = Input.GetAxis("Horizontal") * sensitivity;
    float moveVertical = Input.GetAxis("Vertical") * sensitivity;

    pos.x = Mathf.Clamp(transform.position.x + moveHorizontal, minX, maxX);
    pos.z = Mathf.Clamp(transform.position.z + moveVertical, minZ, maxZ);

    return pos;
  }

  void CheckForHuman()
  {
    if (touchingPlayer)
    {
      Debug.Log("YOU WIN!");
      // Game.Instance.GameOver(1);
    }
    else if (touchingComputer)
    {
      Game.Instance.AddHunterFailure();
    }
  }


  void OnTriggerEnter(Collider other)
  {
    if (other.tag == "Player")
    {
      touchingPlayer = true;
    }

    if (other.tag == "Computer")
    {
      touchingComputer = true;
    }
  }

  void OnTriggerExit(Collider other)
  {
    if (other.tag == "Player")
    {
      touchingPlayer = false;
    }

    if (other.tag == "Computer")
    {
      touchingComputer = false;
    }
  }

  // Update is called once per frame
  void FixedUpdate()
  {
    transform.position = GetMovement();

    if (Input.GetKeyDown("joystick 1 button 1"))
    {
      CheckForHuman();
    }
  }
}
