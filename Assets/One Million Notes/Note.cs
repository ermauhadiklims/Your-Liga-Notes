using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Note
{
    public string header;
    public string text;
    public string folder;
    public bool secure;
    public bool locked;
    public bool pinned;
    public long time;

    public Note(string header, string text, string folder, long time){
        this.header = header;
        this.text = text;
        this.folder = folder;
        this.time = time;
    }

    //DateTime time = DateTime.Now;
    //    long timeTicks = time.Ticks;

}
