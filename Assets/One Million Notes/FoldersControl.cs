using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
public class FoldersControl : MonoBehaviour
{
    public GameObject folderPref;
    public Transform content;


    void Start(){
        Screen.orientation = ScreenOrientation.Portrait;
        ShowFolders();
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            //
        }
    }


    public void ShowFolders(){
        foreach(Transform folder in content){
            if(folder.gameObject.name != "SAVE"){
                Destroy(folder.gameObject);
            }else{
                folder.gameObject.GetComponent<FolderBtn>().SetName("Заметки", this, DataManager.GetFolders().Count ==1);
            }
        }
        foreach(string folder in DataManager.GetFolders()){
            bool isLast = DataManager.GetFolders().IndexOf(folder) == DataManager.GetFolders().Count -1;
            if(folder == "Заметки") continue;
            GameObject newFolder = Instantiate(folderPref);
            newFolder.transform.SetParent(content);
            newFolder.transform.localScale = new Vector2(1,1);
            newFolder.GetComponent<FolderBtn>().SetName(folder, this, isLast);
        }
    }

    public void AddFolder(){
       int index = 1;
       while(true){
            if(DataManager.GetFolders().Contains("Заметки("+ index + ")")){
                index++;
                continue;
            }else{
                DataManager.AddFolder("Заметки("+ index + ")");
                ShowFolders();
                return;
            }
       }
    }



    public void OpenFolder(string folderName){
        DataManager.CurrentFolder = folderName;
        SceneManager.LoadScene("NotesList");

    }


    public void DeleteFolder(string FolderName){
        DataManager.RemoveFolder(FolderName);
        List<long> notesForDelete = new List<long>();
        foreach(KeyValuePair<long, Note> note in DataManager.GetNotes()){
            if(note.Value.folder == FolderName){
                notesForDelete.Add(note.Key);
            }
        }
        foreach(long notefordelete in notesForDelete){
            DataManager.RemoveNote(notefordelete);
        }
        ShowFolders();
    }

    public void NewNote(){
        DataManager.GetNotes();
        DataManager.CurrentFolder = "Заметки";
        DataManager.currentNote = null;
        SceneManager.LoadScene("Note");
    }
}
