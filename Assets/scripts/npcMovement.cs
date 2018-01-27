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

public class npcMovement : MonoBehaviour
{
    public bool colored = true;
    public float shittyDancingCoefficient;
    public float movementScale = 1;
    public float beatTime = 2; // 30 bpm
    public float maxShittyTime = 1f;
    float timeSinceLastBeat;
    bool dancedThisBeat = false;
    public float shittyTime;

    // If we recalc their next dance time every dance, they appear drunk
    // If we don't recalc, they're consistent but still terrible

    // Use this for initialization
    void Start()
    {
        shittyDancingCoefficient = Random.value;
        shittyTime = (maxShittyTime * shittyDancingCoefficient);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timeSinceLastBeat += Time.deltaTime;
        if (timeSinceLastBeat >= (beatTime - shittyTime)){
            Debug.Log("Dance");
            Dance();
        }
        if (timeSinceLastBeat > beatTime){
            Debug.Log("Beat");
            Beat();
        }
    }

    void Beat(){
        dancedThisBeat = false;
        timeSinceLastBeat = 0;
    }

    void Dance(){
        if (dancedThisBeat){
            return;
        }
        transform.Translate(GetRandVector());
        dancedThisBeat = true;
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
