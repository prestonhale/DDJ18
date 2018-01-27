using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class NPCManager: MonoBehaviour {
    public int npcCount;
    public GameObject npc;
    public bool spawnPlayer = false;
    public GameObject player;
    public List<NPC> NPCs = new List<NPC>();
    public Vector3 spawnPosition = Vector3.zero;

    public void Start(){
        Music.Instance.beatDetector.OnBeat += OnBeatDetected;
        SpawnNPCs();
        SpawnPlayer();
    }

    public void SpawnNPCs(){
        for (int i=0; i < npcCount; i++){
            NPCs.Add(SpawnNPC());
        }
    }
    
    public void SpawnPlayer(){
        NPCs.Add(Instantiate(player, spawnPosition, Quaternion.identity).GetComponent<NPC>());
    }

    public NPC SpawnNPC(){
        return Instantiate(npc, spawnPosition, Quaternion.identity).GetComponent<NPC>();
    }

    public void OnBeatDetected(){
        for (int i=0; i < NPCs.Count; i++){
            var npc = NPCs[i];
            npc.dancedThisBeat = false;
            if(npc.isPlayer){
                npc.nextDanceTime = GetPlayerDanceTime();
            } else {
                npc.nextDanceTime = GetRandomDanceTime();
            }
        }
    }

    public float GetRandomDanceTime(){
        var nextTime = Music.Instance.beatLength * Random.Range(0.2f, 0.8f);
        return Time.time + nextTime;
    }

    public float GetPlayerDanceTime(){
        var nextTime = Music.Instance.beatLength;
        return Time.time + nextTime;
    }
}