using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FolderBtn : MonoBehaviour
{
    public Text folderName;
    public Button delFolder, renameFolder;
    public GameObject inputArea;
    public GameObject border;

    private FoldersControl fc;
    public void SetName(string name, FoldersControl fc, bool isLast){
        folderName.text = name;
        this.fc = fc;
       if(inputArea != null) inputArea.SetActive(false);
        GetComponent<Button>().onClick.AddListener(OpenFolder);
        delFolder.onClick.AddListener(DeleteFolder);
        renameFolder.onClick.AddListener(ShowRenaming);
        if(isLast) border.SetActive(false);

    }

    private void OpenFolder(){
        fc.OpenFolder(folderName.text);
    }

    private void DeleteFolder(){
        fc.DeleteFolder(folderName.text); 
    }

    private void ShowRenaming(){
        if(inputArea.activeSelf){
            FinishRename();
        }else{
            folderName.gameObject.SetActive(false);
            inputArea.GetComponent<InputField>().text = folderName.text;
            inputArea.GetComponent<InputField>().caretPosition = inputArea.GetComponent<InputField>().text.Length;
            inputArea.GetComponent<InputField>().ForceLabelUpdate();
            inputArea.SetActive(true);
            delFolder.interactable = false;
        }
    }

    public void FinishRename(){
        inputArea.SetActive(false);
        delFolder.interactable = true;
        CheckName(inputArea.GetComponent<InputField>().text);
    }


    private void CheckName(string name){
        DataManager.RemoveFolder(folderName.text);
        if(DataManager.GetFolders().Contains(name)){
            int index = 1;
            while(true){
                    if(DataManager.GetFolders().Contains(name +"("+ index + ")")){
                        index++;
                        continue;
                    }else{
                        DataManager.AddFolder(name +"("+ index + ")");
                        break;
                    }
            }
        }else{
            DataManager.AddFolder(name);
        }
        ChangeFolderInNotes(folderName.text, name);
        fc.ShowFolders();
    }

    private void ChangeFolderInNotes(string oldname, string newname){
        List<long> notesForChange = new List<long>();
        foreach(KeyValuePair<long, Note> note in DataManager.GetNotes()){
            if(note.Value.folder == oldname){
                notesForChange.Add(note.Key);  
            }
        }
        foreach(long timestamp in notesForChange){
            DataManager.ChangeNotesFolder(timestamp, newname);
        }
    }



}
