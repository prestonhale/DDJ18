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
        var newNpc = Instantiate(npc, spawnPosition, Quaternion.identity).GetComponent<NPC>();
        newNpc.manager = this;
        return newNpc;
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
}