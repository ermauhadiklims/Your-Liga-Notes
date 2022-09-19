using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class NotesControl : MonoBehaviour
{
    public GameObject notesPref;
    public Transform content;
    public TextMeshProUGUI FolderName;
    public FolderChanger fc;
    public PasswordWorker pw;
    private long savedNote;

    void Start(){
        Screen.orientation = ScreenOrientation.Portrait;
        FolderName.text = DataManager.CurrentFolder;
        ShowNotes();
        fc.gameObject.SetActive(false);

        
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
           Back();
        }
    }

    public void Back(){
        SceneManager.LoadScene("Folders");
    }

    public void NewNote(){
        DataManager.currentNote = null;
        SceneManager.LoadScene("Note");
    }

    public void ToCenterAll(){
        foreach(Transform oldNotes in content){
            oldNotes.gameObject.GetComponent<NoteBtn>().SetToCenter();
        }
    }

    public void ShowNotes(){
        Debug.Log("Notes count = " + DataManager.GetNotes().Count);
        foreach(Transform oldNotes in content){
            Destroy(oldNotes.gameObject);
        }
        List<KeyValuePair<long, Note>> pinned = new List<KeyValuePair<long, Note>>();
        foreach(KeyValuePair<long, Note> note in DataManager.GetNotes()){
            if(note.Value.pinned && note.Value.folder == DataManager.CurrentFolder){
                pinned.Add(note);
            }
        }
        foreach(KeyValuePair<long, Note> note in pinned){
            SpawnNote(note);
        }
        foreach(KeyValuePair<long, Note> note in DataManager.GetNotes()){
            if(note.Value.folder == DataManager.CurrentFolder && !note.Value.pinned){
                SpawnNote(note);
            }
        }

    }
    private void SpawnNote(KeyValuePair<long, Note> note){
        GameObject newNote = Instantiate(notesPref);
        newNote.transform.SetParent(content);
        newNote.transform.localScale = new Vector2(1,1);
        Debug.Log("Setting note: Header - " + note.Value.header + " text - " + note.Value.text + " time " + note.Key );
        newNote.GetComponent<NotesListElement>().SetNote(note.Value, note.Key, this);
    }

    public void StartChangeFolder(string currentFolder, long note){
        savedNote = note;
        fc.ChangeFolder(currentFolder);
    }

    public void ChangeFolderEnd(string newfolder){
        DataManager.ChangeNotesFolder(savedNote, newfolder);
        savedNote = 0;
        ShowNotes();
    }

    
    public void LockNote(long timestamp){
        if(PlayerPrefs.HasKey("password")){
            DataManager.LockNote(timestamp, true);
            ShowNotes();
        }else{
            savedNote = timestamp;
            pw.CreatePassword(timestamp);
        }
    }

    public void UnlockNote(long timestamp){
        savedNote = timestamp;
        pw.CheckPassword("unlock");
    }

    public void OpenLockedNote(long timestamp){
        savedNote = timestamp;
        pw.CheckPassword("open");
    }

    public void SuccesUnlock(){
        DataManager.LockNote(savedNote, false);
        savedNote = 0;
        ShowNotes();
    }

    public void SuccesOpen(){
        DataManager.currentNote = DataManager.GetNotes()[savedNote];
        SceneManager.LoadScene("Note");
        savedNote = 0;
    }


}
