using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this [System.Serializable] is neaded to make the script a file that can be writed on the PC
[System.Serializable]

//this class is needed to save all the objects ID, position etc...
public class EditorData
{
    // we need some array to get all the values
    public int[] ID;
    public float[] Psx;
    public float[] Psy;
    public float[] Sclx;
    public float[] Scly;
    //this is needed to create a new class out of nothing in the script, it uses a Gameobject array to get all the data
    public EditorData (GameObject[] objects)
    {
        // lists are used the add all the things we need
       List<int> IDs = new List<int>();
       List<float> Posx = new List<float>();
       List<float> Posy = new List<float>();
       List<float> Scaly = new List<float>();
       List<float> Scalx = new List<float>();

        foreach (GameObject item in objects)
        {
            IDs.Add(item.GetComponent<EditorObject>().ID);
            Posx.Add(item.transform.position.x);
            Posy.Add(item.transform.position.y);
            Scalx.Add(item.transform.localScale.x);
            Scaly.Add(item.transform.localScale.y);
        }
        // then they need to become arrays
        ID = IDs.ToArray();
        Psx = Posx.ToArray();
        Psy = Posy.ToArray();
        Sclx = Scalx.ToArray();
        Scly = Scaly.ToArray();
    }
}
