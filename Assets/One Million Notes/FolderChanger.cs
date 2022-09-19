using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FolderChanger : MonoBehaviour
{

    public TMP_Dropdown dd;
    public NotesControl nc;

    public void ChangeFolder(string currentFolder){
        dd.ClearOptions();
        List<string> options = new List<string>();
        options.Add(currentFolder);
        foreach(string folder in DataManager.GetFolders()){
            if(folder != currentFolder){
                options.Add(folder);
            }
        }
        dd.AddOptions(options);
        dd.RefreshShownValue();
        dd.Show();
        gameObject.SetActive(true);
    }

    public void Change(){
        Debug.Log(dd.captionText.text);
        nc.ChangeFolderEnd(dd.captionText.text);
        gameObject.SetActive(false);
    }

    public void Cancel(){
        gameObject.SetActive(false);
    }


}
