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

    float moveHorizontal = Input.GetAxis("Hunter Horizontal Joystick") * sensitivity;
    float moveVertical = Input.GetAxis("Hunter Vertical Joystick") * sensitivity;

    pos.x = Mathf.Clamp(transform.position.x + moveHorizontal, minX, maxX);
    pos.z = Mathf.Clamp(transform.position.z + moveVertical, minZ, maxZ);

    return pos;
  }

  void CheckForHuman()
  {
    if (touchingPlayer)
    {
      Game.Instance.HunterWin();
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
  void Update()
  {
    transform.position = GetMovement();

    if (Input.GetKeyDown("joystick 2 button 16"))
    {
      CheckForHuman();
    }
  }
}
