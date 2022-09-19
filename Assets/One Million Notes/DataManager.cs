using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[Serializable]
public class DescendingComparer<T> : IComparer<T> where T : IComparable<T> {
    public int Compare(T x, T y) {
        return y.CompareTo(x);
    }
}


public class DataManager 
{
    private static SortedList<long, Note> notes;
    private static List<string> foldersName;
    public static string CurrentFolder;
    public static Note currentNote; 

    public static void AddNote(long time, Note note){
        Debug.Log("Add note " + time + " \"" + note.header + "\" \"" + note.text + "\"");
        notes.Add(time, note);
        SaveNotes();
    }

    public static void PinNote(long time){
        bool pinnedStatus = false;
        pinnedStatus = notes[time].pinned;
        notes[time].pinned = !pinnedStatus;
        SaveNotes();
    }
    
    public static void LockNote(long time, bool lockStatus){
        notes[time].locked = lockStatus;
        SaveNotes();
    }

    public static void AddFolder(string folderName){
        foldersName.Add(folderName);
        SaveFolders();
    }

    public static List<String> GetFolders(){
        foldersName = LoadFolders();
        return foldersName;
    }

    public static void RemoveFolder(string folderName){
        foldersName.Remove(folderName);
        SaveFolders();
    }


    public static SortedList<long, Note> GetNotes(){
        notes = LoadNotes();
        return notes;
    }

    public static Note GetNote(long time){
        return notes[time];
    }

    public static void SaveAllNotes(){
        SaveNotes();
    }

    public static void RemoveNote(long time){
        Debug.Log("removing " + time);
        notes.Remove(time);
        SaveNotes();
    }

    public static void ChangeNotesFolder(long time, string newFolder){
        notes[time].folder = newFolder;
        SaveNotes();
    }
   

    private static void SaveNotes(){
        BinaryFormatter bf = new BinaryFormatter(); 
        FileStream file = File.Create(Application.persistentDataPath 
            + "/notes.dat"); 
        bf.Serialize(file, notes);
        file.Close();
    }

    private static SortedList<long, Note> LoadNotes(){
       SortedList<long, Note> loadedNotes;
        if (File.Exists(Application.persistentDataPath 
            + "/notes.dat")){
        
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath +
                "/notes.dat", FileMode.Open);
            loadedNotes = (SortedList<long, Note>)bf.Deserialize(file);
            file.Close();
        }else{
            loadedNotes = new SortedList<long, Note>(new DescendingComparer<long>());
        }
        return loadedNotes;
    }


    private static void SaveFolders(){
        BinaryFormatter bf = new BinaryFormatter(); 
        FileStream file = File.Create(Application.persistentDataPath 
            + "/folders.dat"); 
        bf.Serialize(file, foldersName);
        file.Close();
    }

    private static List<string> LoadFolders(){
       List<string> loadedFolders;
        if (File.Exists(Application.persistentDataPath 
            + "/folders.dat")){
        
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath +
                "/folders.dat", FileMode.Open);
            loadedFolders = (List <string>)bf.Deserialize(file);
            file.Close();
        }else{
            loadedFolders = new List<string> () {"Заметки"};
        }
        return loadedFolders;
    }
    
   
}







