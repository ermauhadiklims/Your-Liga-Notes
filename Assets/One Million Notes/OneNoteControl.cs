using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class OneNoteControl : MonoBehaviour
{
    public TMP_InputField header, text;
    private Note openedNote;
    private bool isNewNote;

    void Start(){
        Screen.orientation = ScreenOrientation.Portrait;
        if(DataManager.currentNote == null){
            isNewNote = true;
            openedNote = new Note("", "", DataManager.CurrentFolder, 1337);
        }else{
            openedNote = DataManager.currentNote;
            Debug.Log(DataManager.currentNote.time);
            Debug.Log(openedNote.time);
        }
        if(!String.IsNullOrWhiteSpace(openedNote.header)){
            header.text = openedNote.header;
        }
        if(!String.IsNullOrWhiteSpace(openedNote.text)){
            text.text = openedNote.text;
        }
    }

    private void OnDestroy() {
     
        SaveData();

    }
    
     void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
           Back();
        }
    }

    public void Back(){
        SceneManager.LoadScene("NotesList");
    }

    private void SaveData(){
          if(!String.IsNullOrWhiteSpace(header.text) || !String.IsNullOrWhiteSpace(text.text)){
       
            if(header.text != openedNote.header || text.text != openedNote.text){
    
                long time = DateTime.Now.Ticks;
                if(String.IsNullOrWhiteSpace(header.text)){
                    header.text = "";
                }else if(String.IsNullOrWhiteSpace(text.text)){
                    text.text = "";
                }
                Note newNote = new Note(header.text, text.text, DataManager.CurrentFolder, time);
 
                if(!isNewNote){
      
                    DataManager.RemoveNote(DataManager.currentNote.time);
                }

                DataManager.AddNote(time, newNote);
            }    
        }else{
            Debug.Log("everething empty, skip");
        }
    }

    

}
