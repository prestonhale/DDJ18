using Enum = System.Enum;
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
    NW
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
        new Vector3 (-1, 0, 1)
    };

    public static Vector3 ToVector3(this Direction direction)
    {
        return vectors[(int)direction];
    }
}

public class NPC: MonoBehaviour
{
    public bool colored = true;
    public float movementScale = 1;
    public bool isPlayer;


    public bool dancedThisBeat = false;
    public float nextDanceTime;

    // If we recalc their next dance time every dance, they appear drunk
    // If we don't recalc, they're consistent but still terrible

    // Use this for initialization
    void Start()
    {
    }

    void Update(){
        if (Time.time > nextDanceTime){
            if(!dancedThisBeat){
                Dance();
            }
        }
    }

    void SetRandColor(){
    }

    void Dance(){
        dancedThisBeat = true;
        transform.Translate(GetRandVector());
    }

    Vector3 GetRandVector(){
        return GetRandDirection().ToVector3();
    }

    Direction GetRandDirection()
    {
        var values = Enum.GetValues(typeof(Direction));
        var randomDirection = (Direction)Random.Range(0, values.Length);
        return randomDirection;
    }
}
