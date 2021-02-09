using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

//here is the complicated shit
public class EditorManager : MonoBehaviour
{
    //IDset used to check what item is being used
    public int IDset;
    private bool isPlaying;

    //here are the array for things you need to the object of the editor and the on for the game !THE PLACE NUMBER NEEDS TO BE THE SAME AS THE ID!
    [SerializeField] GameObject[] EditThings;
    [SerializeField] GameObject[] EditThingsPlay;
    [SerializeField] GameObject[] EditorPaused;

    [SerializeField] GameObject UIMen;
    [SerializeField] string FileName;
    [SerializeField] InputField NameChoser;

    // Update is called once per frame
    void Update()
    {
        FileName = NameChoser.text;

        //this is needed to create objects in the editor
        if (!isPlaying)
        {
            if (Input.GetMouseButtonDown(1))
            {

                Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                Instantiate(EditThings[IDset], pos, Quaternion.identity);
            }
        }


        if (Input.GetKeyDown(KeyCode.Escape) && isPlaying)
        {
                EndTesting();
        }
    }

    //chnge the ID selected for the editor
    public void ChangeID(int ID)
    {
        IDset = ID;
    }

    //start the play mode
    public void Testing()
    {
        isPlaying = true;
        UIMen.SetActive(false);

        GameObject[] objects = GameObject.FindGameObjectsWithTag("EditorOBJ");
        EditorPaused = objects;

        //check all the editor objects and replace them with game objects
        foreach (GameObject item in objects)
        {
            GameObject gino = Instantiate(EditThingsPlay[item.GetComponent<EditorObject>().ID], item.transform.position, Quaternion.identity);

            gino.transform.parent = transform;

            gino.transform.localScale = item.transform.localScale;

            item.SetActive(false);
        }
    }

    //return to editor
    public void EndTesting()
    {
        isPlaying = false;
        UIMen.SetActive(true);

        //destroy all the game objects and re-active the editor ones
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        foreach (GameObject item in EditorPaused)
        {
            item.SetActive(true);
        }
    }

    //saving the levle (thanks to brakeys save and load tutorial)
    public void SaveLevel()
    {
        BinaryFormatter bin = new BinaryFormatter();

        // needed for custom level names
        string path = Application.persistentDataPath + "/" + FileName + ".fsl";

        FileStream stream = new FileStream(path, FileMode.Create);

        GameObject[] objects = GameObject.FindGameObjectsWithTag("EditorOBJ");

        EditorData data = new EditorData(objects);

        Debug.Log(path);

        bin.Serialize(stream, data);
        stream.Close();

    }

    //loading the level (thanks to brakeys save and load tutorial)
    public void LoadLevel()
    {
        //needed for chosing the level
        string path = Application.persistentDataPath + "/" + FileName + ".ple";

        BinaryFormatter bin = new BinaryFormatter();

        FileStream stream = new FileStream(path, FileMode.Open);

        GameObject[] objects = GameObject.FindGameObjectsWithTag("EditorOBJ");

        //destroy all previus objects
        foreach (GameObject item in objects)
        {
            Destroy(item.gameObject);
        }

        EditorData data = bin.Deserialize(stream) as EditorData;
        stream.Close();

        //checks for the ammount of object and give them their position, size and type
        for (int i = 0; i < data.ID.Length; i++)
        {
            Debug.Log(i);
            GameObject gino = Instantiate(EditThings[data.ID[i]], new Vector2(data.Psx[i], data.Psy[i]), Quaternion.identity);

            gino.transform.localScale = new Vector2(data.Sclx[i], data.Scly[i]);

        }
    }
}
