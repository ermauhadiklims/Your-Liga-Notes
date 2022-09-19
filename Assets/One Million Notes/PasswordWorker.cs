using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PasswordWorker : MonoBehaviour
{
   public TMP_InputField input;
   public NotesControl nc;
   private bool createNewPassword;
   public Animator animator;
   public string operation;
   private long timestamp;
   

   void Start(){
//     gameObject.SetActive(false);
   }

     public void Cancel(){
          Debug.Log("cancel");
          gameObject.SetActive(false);
          operation = "";
          timestamp = 0;
     }



   public void CreatePassword(long timestamp){
          this.timestamp = timestamp;
          gameObject.SetActive(true);
          operation = "create";
          input.text = "";
   }

   public void CheckPassword(string op){
     Debug.Log("checking");
          operation = op;
           Debug.Log("window active - " + gameObject.activeSelf);
          gameObject.SetActive(true);
          gameObject.SetActive(true);

          input.text = "";
          Debug.Log("window active - " + gameObject.activeSelf);
     Debug.Log("start check for " + op);
   }

   public void Confirm(){
     Debug.Log("confirm");
     if(String.IsNullOrWhiteSpace(input.text)){
          animator.SetTrigger("wrong");
     }else{
          switch (operation){
               case "create":
                    PlayerPrefs.SetString("password", Crypto.Encrypt(input.text));
                    PlayerPrefs.Save();
                    nc.LockNote(timestamp);
                    timestamp = 0;
                    operation = "";
                    gameObject.SetActive(false);
                    break;
               case "unlock":
                    if(input.text != Crypto.Decrypt(PlayerPrefs.GetString("password"))){
                         animator.SetTrigger("wrong");
                    }else{
                         nc.SuccesUnlock();
                         if(HasNoLocked()){
                              PlayerPrefs.DeleteKey("password");
                              PlayerPrefs.Save();
                         }
                         operation = "";
                         gameObject.SetActive(false);
                    }
                    break;
               case "open":
                    if(input.text != Crypto.Decrypt(PlayerPrefs.GetString("password"))){
                         animator.SetTrigger("wrong");
                    }else{
                         nc.SuccesOpen();
                         operation = "";
                         gameObject.SetActive(false);
                    }
                    break;
          }
          
     }
   }


     public bool HasNoLocked(){
          foreach(KeyValuePair<long, Note> note in DataManager.GetNotes()){
               if(note.Value.locked) return false;
          }
          return true;
     }


  
}
