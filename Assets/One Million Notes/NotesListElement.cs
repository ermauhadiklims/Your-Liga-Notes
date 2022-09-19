using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class NotesListElement : MonoBehaviour
{
    
    public TextMeshProUGUI header, date;
    private NotesControl nc;
    private long timestamp;
    private Note note;
    public Sprite pinned, unpinned, locked, unlocked;
    public Image pinBtn, lockBtn;
    private bool shorted;
   
            


    public void SetNote(Note note, long date, NotesControl nc){
        SetHeader(note);
        this.nc = nc;
        this.timestamp = date;
        this.note = note;
        this.date.text ="<i>" +  new DateTime(date).ToString() + "</i>";
        if(note.pinned){
            pinBtn.sprite = pinned;
        }else{
            pinBtn.sprite = unpinned;
        }
        if(note.locked){
            lockBtn.sprite = locked;
        }else{
            lockBtn.sprite = unlocked;
        }
    }

    private void SetHeader(Note note){
        if(String.IsNullOrWhiteSpace(note.header)){
            int headerMax= 0;
            if(note.text.Length > 20){
                headerMax = 20;
                shorted = true;
            }else{
                shorted = false;
                headerMax = note.text.Length;
            }
            if(shorted){

            header.text = note.text.Substring(0,headerMax) + "...";
            }else{
               header.text = note.text.Substring(0,headerMax); 
            }
        }else{
            header.text = note.header;
        }
    }


    public void DeleteNote(){
        DataManager.RemoveNote(timestamp);
        if(HasNoLocked()){
            PlayerPrefs.DeleteKey("password");
            PlayerPrefs.Save();
        }
        nc.ShowNotes();
    }

    public void LockNote(){
        
       if(note.locked){
        Debug.Log("tryUnlock");
            nc.UnlockNote(timestamp);
       }else{
            Debug.Log("trylock");
            nc.LockNote(timestamp);
       }
    }

    

    public void PinNote(){
        DataManager.PinNote(timestamp);
        nc.ShowNotes();
    }

    public void ChangeFolder(){
        nc.StartChangeFolder(note.folder, timestamp);
    }

    public void OpenNote(){
        if(note.locked){
            nc.OpenLockedNote(timestamp);
        }else{
            DataManager.currentNote = DataManager.GetNotes()[timestamp];
            SceneManager.LoadScene("Note");
        }
    }


    public void ToCenterAll(){
        nc.ToCenterAll();
    }

    public bool HasNoLocked(){
          foreach(KeyValuePair<long, Note> note in DataManager.GetNotes()){
               if(note.Value.locked) return false;
          }
          return true;
     }
}
