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
    public Color[] colors = new Color[] { Color.red, Color.yellow, Color.green };

    public void Start(){
        Music.Instance.beatDetector.OnBeat += OnBeatDetected;
    }

    public void SpawnNPCs(){
        for (int i=0; i < npcCount; i++){
            NPCs.Add(SpawnNPC());
        }
    }
    
    public NPC SpawnNPC(){
        var newSpawnPosition = Game.Instance.map.GetRandSpawnPos();
        var newNpc = Instantiate(npc, newSpawnPosition, Quaternion.identity).GetComponent<NPC>();
        newNpc.manager = this;
        newNpc.SetColor(GetRandColor());
        return newNpc;
    }

    public void RevealAllColors(){
        for(int i=0; i < NPCs.Count; i++){
            NPCs[i].RevealColor();
        }
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
        var nextTime = Music.Instance.beatLength * Random.Range(0.1f, 0.9f);
        return Time.time + nextTime;
    }

}