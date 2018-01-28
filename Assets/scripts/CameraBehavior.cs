
using UnityEngine;
using UnityEditor;
using System.Collections;


public class CameraBehavior : MonoBehaviour
{
    public float shakeAmount = .01f; 
    public float decreaseFactor = 1;
    public float shakeTime = 0.5f;
    public Vector3 startPosition;

    float shake;

    void Start(){
        startPosition = transform.localPosition;
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.R)){
            Shake();
        }
        if (shake > 0){
            transform.localPosition = startPosition + (Random.insideUnitSphere * shakeAmount);
            shake -= Time.deltaTime * decreaseFactor;
        } else {
            shake = 0.0f;
            transform.localPosition = startPosition;
        }
    }
    
    public void Shake(){
        shake = shakeTime;
    }
}