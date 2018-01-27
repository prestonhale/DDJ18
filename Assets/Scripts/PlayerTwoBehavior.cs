using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwoBehavior : MonoBehaviour
{

  private int minX = -40;
  private int maxX = 40;

  private int minZ = -19;

  private int maxZ = 19;

  private int failures = 0;

  private bool touchingPlayer = false;
  private bool touchingComputer = false;
  Vector3 GetMovement()
  {
    Vector3 pos = transform.position;

    float moveHorizontal = Input.GetAxis("Horizontal");
    float moveVertical = Input.GetAxis("Vertical");

    pos.x = Mathf.Clamp(transform.position.x + moveHorizontal, minX, maxX);
    pos.z = Mathf.Clamp(transform.position.z + moveVertical, minZ, maxZ);

    return pos;
  }

  void CheckForHuman()
  {
    if (touchingPlayer)
    {
      Debug.Log("You win!");
    }
    else if (touchingComputer)
    {
      Game.Instance.AddHunterFailure();
      Debug.Log("Oops you're wrong");
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

  // Use this for initialization
  void Start()
  {

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
