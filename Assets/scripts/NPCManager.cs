using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager: MonoBehaviour {
    public int npcCount;
    public int infectedCount = 0;
    public GameObject npc;
    public bool spawnPlayer = false;
    public GameObject player;
    public float NPCMoveChance = 0.5f;
    public List<NPC> NPCs = new List<NPC>();
    public Vector3 spawnPosition;
    public Color[] colors = new Color[] { Color.red, Color.yellow, Color.green };

    public void Start(){
        spawnPosition = transform.position;
        Music.Instance.beatDetector.OnBeat += OnBeatDetected;
        SpawnNPCs();
    }

    public void SpawnNPCs(){
        for (int i=0; i < npcCount; i++){
            NPCs.Add(SpawnNPC());
        }
    }
    
    public NPC SpawnNPC(){
        var newSpawnPosition = GetRandSpawnPos();
        var newNpc = Instantiate(npc, newSpawnPosition, Quaternion.identity).GetComponent<NPC>();
        newNpc.manager = this;
        newNpc.SetColor(GetRandColor());
        return newNpc;
    }
    
    public Color GetRandColor(){
        var color = colors[Random.Range(0, colors.Length)];
        return color;
    }

    public void OnBeatDetected(){
        for (int i=0; i < NPCs.Count; i++){
            var npc = NPCs[i];
            npc.dancedThisBeat = false;
            if (Random.value <= NPCMoveChance){
                npc.nextDanceTime = GetRandomDanceTime();
            } else {
                npc.nextDanceTime = Mathf.Infinity;
            }
        }
    }

    public void NotifyInfected(){
        infectedCount +=1;
        if (infectedCount >= npcCount){
            Game.Instance.DancerWin();
        }
    }

    public float GetRandomDanceTime(){
        var nextTime = Music.Instance.beatLength * Random.Range(0.2f, 0.8f);
        return Time.time + nextTime;
    }

    public Vector3 GetRandSpawnPos(){
        var cleanMinX = Mathf.Round(Game.Instance.map.minX);
        var cleanMaxX = Mathf.Round(Game.Instance.map.maxX);
        var cleanMinZ = Mathf.Round(Game.Instance.map.minZ);
        var cleanMaxZ = Mathf.Round(Game.Instance.map.maxZ);
        var xLoc = Random.Range(cleanMinX, cleanMaxX);
        var zLoc = Random.Range(cleanMinZ, cleanMaxZ);
        return new Vector3(xLoc, 0, zLoc);
    }
}