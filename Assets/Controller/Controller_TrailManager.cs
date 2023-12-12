using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_TrailManager : MonoBehaviour
{
    [SerializeField] float speed = 4f;
    public GameObject[] trails;
    private int TrailLine = 0;

    void Start()
    {
        Debug.Log("Current Trail : " + TrailLine);
    }
    void Update()
    {
        if(Input.GetKey(KeyCode.A))
        {
            trails[TrailLine].transform.Translate(-speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            trails[TrailLine].transform.Translate(speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.S) && TrailLine < 2)
        {
            TrailLine++;
            Debug.Log("Current Trail : " + TrailLine);
        }
        if (Input.GetKeyDown(KeyCode.W) && TrailLine > 0)
        {
            TrailLine--; 
            Debug.Log("Current Trail : " + TrailLine);
        }
    }
}
