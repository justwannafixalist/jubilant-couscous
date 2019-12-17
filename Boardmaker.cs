using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boardmaker : MonoBehaviour
{
    public List<Vector3> gridPosition = new List<Vector3>();
    public int rows;
    public int columns;
    public GameObject[] floorTiles;
    private Transform Board;
    public GameObject instance;
    

    public void InitialiseList()
    {
        gridPosition.Clear();
        
        for (int x = 1; x < columns - 1; x++)
        {
            for (int y = 1; y < rows - 1; y++)
            {
                gridPosition.Add(new Vector3(x, y, 0f));

            }
        }
    }

    public void MakeBoard()
    {
        Board = new GameObject("Board").transform;
        for (int x = -1; x < columns - 1; x++)
        {
            for (int y = -1; y < rows - 1; y++)
            {
                GameObject toInstantiate = floorTiles[0];
                instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                
                instance.GetComponent<Settings>().x = x;
                instance.GetComponent<Settings>().y = y;
                
                instance.name = x.ToString() + "," + y.ToString();


                instance.transform.SetParent(Board);
            }
        }
    }
    public void Setup()
    {
        InitialiseList();
        MakeBoard();
    }

    // Start is called before the first frame update
    void Awake()
    {
        Setup();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
