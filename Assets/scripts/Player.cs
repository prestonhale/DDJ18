using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum Controller{
    joystick
}


public enum Controllers{

}

public class Player: MonoBehaviour{
    
    public float transmitSuccessChance;
    public float transmitRadius;
    public Controller controller;
    public float beatLength = 0.75f;
    public float beatBuffer = 0.2f;
    public bool hitThisBeat = true;
    public Color color;
    public Color[] colors = new Color[] { Color.red, Color.yellow, Color.green };
    public bool checkingBeat = false;

    public void Start(){
        Music.Instance.beatDetector.OnBeat += OnBeatDetected;
    }

    public void OnBeatDetected(){
        Move();
    }

    public void SpawnPlayer(){
        this.transform.position = Game.Instance.map.GetRandSpawnPos();
        this.color = GetRandColor();
        RevealColor();
    }
    
    public Color GetRandColor(){
        var color = colors[Random.Range(0, colors.Length)];
        return color;
    }

    public void RevealColor(){
        this.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = color;
    }

    public void Update(){
        if (Input.GetKeyDown(KeyCode.Space)){
            HitBeat();
        }
        for (int i = 0; i < 20; i++)
        {
            if (Input.GetKey("joystick 1 button " + i)){
                Transmit();
            }
        }
        if (checkingBeat){
            if (Music.Instance.timeSinceLastBeat > (beatLength - beatBuffer)){
                if (!hitThisBeat){
                    BeatFail();
                }
                hitThisBeat = false;
            }
        }
    }
    
    public void Transmit(){
        var layermask = 1 << 8;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, transmitRadius, layermask);
        for (int i=0; i < hitColliders.Length; i++){
            var NPC = hitColliders[i].transform.parent.GetComponent<NPC>();
            if (NPC){
                if (Random.value <= transmitSuccessChance){
                    NPC.WasTransmittedTo(this.color);
                }
            }
        }
    }

    public void HitBeat(){
        bool valid = false;
        if(Music.Instance.timeSinceLastBeat <= beatBuffer){
            valid=true;
        }
        if(Music.Instance.timeSinceLastBeat > (beatLength - beatBuffer)){
            valid=true;
        }
        if (valid){
            BeatSuccess();
        }
        else {
            BeatFail();
        }
    }

    public void BeatSuccess(){
        MeshRenderer mRenderer = this.GetComponent<MeshRenderer>();
        if (mRenderer != null) {
            mRenderer.material.color = Color.green;
        }
        hitThisBeat = true;
        StartCoroutine(ReturnToColor());
    }

    public void BeatFail(){
        MeshRenderer mRenderer = this.GetComponent<MeshRenderer>();
        if (mRenderer != null) {
            mRenderer.material.color = Color.red;
        }
        StartCoroutine(ReturnToColor());
    }

    IEnumerator ReturnToColor(){
        yield return new WaitForSeconds(0.1f);
        MeshRenderer mRenderer = this.GetComponent<MeshRenderer>();
        if (mRenderer != null) {
            mRenderer.material.color = Color.white;
        }
    }

    public void Move(){
        string hAxisName;
        string vAxisName;
        hAxisName = "Dancer Horizontal Joystick";
        vAxisName = "Dancer Vertical Joystick";
        var horizontal = Input.GetAxis(hAxisName);
        var vertical = Input.GetAxis(vAxisName);
        Direction direction;
        if (horizontal != 0 || vertical != 0){
            if (controller == Controller.joystick){
                direction = InputAxisToDirection(horizontal, vertical);
            } else {
                return;
            }
            transform.Translate(direction.ToVector3(), Space.World);
            transform.LookAt(transform.position + direction.ToVector3());
        }
    }

    public Direction InputAxisToDirection(float horizontal, float vertical){
        float rHorizontal = Mathf.Floor(horizontal / 0.25f + 0.5f ) * 0.25f;
        float rVertical = Mathf.Floor(vertical / 0.25f + 0.5f ) * 0.25f;
        if (rVertical < 0){
            if(rHorizontal >= 0.75f){
                return Direction.E;
            } else if(rHorizontal >= 0.25f){
                return Direction.SE;
            } else if(rHorizontal >= -0.25f){
                return Direction.S;
            } else if(rHorizontal >= -0.75f){
                return Direction.SW;
            } else {
                return Direction.W;
            }
        } else if (rVertical >= 0){
            if(rHorizontal >= 0.75f){
                return Direction.E;
            } else if(rHorizontal >= 0.25f){
                return Direction.NE;
            } else if(rHorizontal >= -0.25f){
                return Direction.N;
            } else if(rHorizontal >= -0.75f){
                return Direction.NW;
            } else {
                return Direction.W;
            }
        }
        return Direction.None;
    }

}