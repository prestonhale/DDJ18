using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum Controller{
    keys,
    joystick
}


public enum Controllers{

}

class Player: MonoBehaviour{
    
    public float transmitSuccessChance;
    public float transmitRadius;
    public Controller controller;

    public void Start(){
        Music.Instance.beatDetector.OnBeat += OnBeatDetected;
    }

    public void OnBeatDetected(){
        Move();
        Transmit();
    }
    
    public void Transmit(){
        var layermask = 1 << 8;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, transmitRadius, layermask);
        for (int i=0; i < hitColliders.Length; i++){
            var NPC = hitColliders[i].transform.parent.GetComponent<NPC>();
            if (NPC){
                if (Random.value <= transmitSuccessChance){
                    NPC.WasTransmittedTo();
                }
            }
        }
    }

    public void Move(){
        string hAxisName;
        string vAxisName;
        if (controller == Controller.joystick){
            hAxisName = "Dancer_Horizontal_Joystick";
            vAxisName = "Dancer_Verical_Joystick";
        } else {
            Debug.Log("No controller type");
            return;
        }
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