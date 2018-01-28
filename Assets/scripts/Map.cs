using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Map: MonoBehaviour {
    public float minX;
    public float maxX;

    public float minZ;
    public float maxZ;

    public void Awake(){
        minX = transform.GetChild(0).transform.position.x;
        maxX = transform.GetChild(1).transform.position.x;
        minZ = transform.GetChild(2).transform.position.z;
        maxZ = transform.GetChild(3).transform.position.z;
    }

    public bool InBounds(Vector3 checkPos){
        if (checkPos.x > maxX || checkPos.x < minX || checkPos.z > maxZ || checkPos.z < minZ){
                return false;
            }
        return true;
    }
  
    public Vector3 GetRandSpawnPos(){
        var xLoc = Mathf.Round(Random.Range(minX, maxX));
        var zLoc = Mathf.Round(Random.Range(minZ, maxZ));
        return new Vector3(xLoc, 0, zLoc);
    }

}