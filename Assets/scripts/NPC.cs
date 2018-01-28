using Enum = System.Enum;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC: MonoBehaviour
{
    public bool colored = true;
    public float movementScale = 1;
    public bool infected;


    public bool dancedThisBeat = false;
    public float nextDanceTime;
    public NPCManager manager;

    public Material material;

    public static Color[] colors = new Color[] { Color.red, Color.green, Color.yellow };

    // If we recalc their next dance time every dance, they appear drunk
    // If we don't recalc, they're consistent but still terrible

    // Use this for initialization
    void Start()
    {   
        material = transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material;
        material.color = GetRandColor();
    }

    void Update(){
        if (Time.time > nextDanceTime){
            if(!dancedThisBeat){
                Dance();
            }
        }
    }

    void SetRandColor(){
        transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material.color = GetRandColor();
    }

    public Color GetRandColor(){
        var color = colors[Random.Range(0, colors.Length)];
        return color;
    }

    public void Dance(){
        dancedThisBeat = true;
        var newPos = GetNextPosition();
        for (int i=0; i<11; i++){
            if (Game.Instance.map.InBounds(newPos)){
                transform.position = newPos;
                break;
            }
        }
    }

    Vector3 GetNextPosition(){
        var possibleDirection = GetRandVector();
        var possibleLocation = transform.position + possibleDirection;
        return possibleLocation;
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

    public void WasTransmittedTo(){
        if (!infected){
            material.color = Color.cyan;
            manager.NotifyInfected();
            infected = true;
        }
    }
}
