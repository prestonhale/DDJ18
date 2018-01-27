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

public class npcMovement : BeatListener
{
    public bool colored = true;

    public float movementScale = 1;
    public bool isPlayer = false;
    public float offBeatTime;
    public BeatObserver observer;

    float maxOffBeatTime = 0.32f;
    public float timeSinceLastBeat = 0;
    public bool dancedThisBeat = false;

    // If we recalc their next dance time every dance, they appear drunk
    // If we don't recalc, they're consistent but still terrible

    // Use this for initialization
    void Start()
    {
        observer.register(this);
        if (isPlayer){
            offBeatTime = 0f;
        } else {
            float shittyDancingCoefficient = Random.value;
            offBeatTime = (maxOffBeatTime * shittyDancingCoefficient);
        }
    }

    void FixedUpdate()
    {
        timeSinceLastBeat += Time.deltaTime;

    }

    void Update(){
        // if (timeSinceLastBeat > offBeatTime){
            if(!dancedThisBeat){
                Debug.Log("Dance");
                Dance();
            }
        // }
    }

    public override void OnBeat(){
        Debug.Log("Beat");
        timeSinceLastBeat = 0;
        dancedThisBeat = false;
    }

    void SetRandColor(){
        
    }

    void Dance(){
        dancedThisBeat = true;
        transform.Translate(GetRandVector());
    }

    Vector3 GetRandVector(){
        Debug.Log("random vect");
        return GetRandDirection().ToVector3();
    }

    Direction GetRandDirection()
    {
        var values = Enum.GetValues(typeof(Direction));
        var randomDirection = (Direction)Random.Range(0, values.Length);
        return randomDirection;
    }
}
