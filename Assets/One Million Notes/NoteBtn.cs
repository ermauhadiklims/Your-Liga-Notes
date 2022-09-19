using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteBtn : MonoBehaviour
{
    private bool dragged;
    private Vector2 center, left, pos;
    public GameObject header;
    public Vector3 point;
    private Vector3 fingerStart, fingerEnd;
   




    void Start(){
        center = new Vector2(-120, 0);
        left = new Vector2(-336,0);
        pos = center;
    }

    void Update(){
        if(!dragged){
            header.transform.localPosition = Vector2.MoveTowards(header.transform.localPosition, pos, Time.deltaTime*500);
        }else{
            point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            header.transform.position = Vector2.MoveTowards(header.transform.position, new Vector2(point.x, header.transform.position.y), 1);
            if(header.transform.localPosition.x < -336){
                header.transform.localPosition = new Vector2(-336, 0);
            }else if(header.transform.localPosition.x > -120){
                header.transform.localPosition = new Vector2(-120, 0);
            }
        }
    }

    public void Grab(){
        Debug.Log("Grabbed");
        dragged = true;
        fingerStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        gameObject.GetComponent<NotesListElement>().ToCenterAll();

    }

    public void SetToCenter(){
        pos = center;
    }

    public void Leave(){
        fingerEnd = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log("Leaved");
        dragged = false;
        if(header.transform.localPosition.x > -216){
            pos = center;
        }else{
            pos = left;
        }
        
    }

    public void NoteClick(){
        if(fingerStart == fingerEnd){
            gameObject.GetComponent<NotesListElement>().OpenNote();
        }
    }
    
}
