using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Map: MonoBehaviour {
    public int minX = -40;
    public int maxX = 40;

    public int minZ = -19;
    public int maxZ = 19;

    public bool InBounds(Vector3 checkPos){
        if (checkPos.x > maxX || checkPos.x < minX || checkPos.z > maxZ || checkPos.z < minZ){
                return false;
            }
        return true;
    }
}