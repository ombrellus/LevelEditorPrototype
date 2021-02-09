using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorObject : MonoBehaviour
{
    //needed to detect what object is
    public int ID;

    public void OnMouseDrag()
    {
        //move around the objects, delete and scale them
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        transform.Translate(pos);

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.localScale += new Vector3(0.1f, 0, 0);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.localScale += new Vector3(-0.1f, 0, 0);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.localScale += new Vector3(0, 0.1f, 0);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.localScale += new Vector3(0, -0.1f, 0);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            Destroy(gameObject);
        }
    }
}
