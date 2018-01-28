using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum Controller{
    joystick
}


public enum Controllers{

}

class Player: MonoBehaviour{
    
    public float transmitSuccessChance;
    public float transmitRadius;
    public Controller controller;
    public float beatLength = 0.75f;
    public float beatBuffer = 0.2f;
    public bool hitThisBeat = true;
    public Color color;

    public void Start(){
        Music.Instance.beatDetector.OnBeat += OnBeatDetected;
        SpawnPlayer();
    }

    public void OnBeatDetected(){
        Move();
        Transmit();
    }

    public void SpawnPlayer(){
        // spawn player set color
    }

    public void Update(){
        if (Input.GetKeyDown(KeyCode.Space)){
            HitBeat();
        }
        if (Music.Instance.timeSinceLastBeat > (beatLength - beatBuffer)){
            if (!hitThisBeat){
                BeatFail();
            }
            hitThisBeat = false;
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
        this.GetComponent<MeshRenderer>().material.color = Color.green;
        hitThisBeat = true;
        StartCoroutine(ReturnToColor());
    }

    public void BeatFail(){
        this.GetComponent<MeshRenderer>().material.color = Color.red;
        StartCoroutine(ReturnToColor());
    }

    IEnumerator ReturnToColor(){
        yield return new WaitForSeconds(0.1f);
        this.GetComponent<MeshRenderer>().material.color = Color.white;
    }

    public void Move(){
        string hAxisName;
        string vAxisName;
        hAxisName = "Dancer_Horizontal_Joystick";
        vAxisName = "Dancer_Vertical_Joystick";
        var horizontal = Input.GetAxis(hAxisName);
        var vertical = Input.GetAxis(vAxisName);
        Direction direction;
        if (horizontal != 0 || vertical != 0){
            if (controller == Controller.joystick){
                direction = InputAxisToDirection(horizontal, vertical);
            } else {
                Debug.Log("No controller type");
                return;
            }
            transform.Translate(direction.ToVector3());
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