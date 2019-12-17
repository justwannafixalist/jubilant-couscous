using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotion : MonoBehaviour
{

    public Vector3 position;
    public GameObject currentTile;
    public Vector3 newPosition;
    public GameObject clickmanager;
    public GameObject TargetTile;
    public GameObject Board;
    public List<GameObject> steps;
    public GameObject NextTile;
    public float speed;
    public float stepx;
    public float stepy;
    public bool selected = false;
    public Camera cam;
    // Start is called before the first frame update
    
    private void Start()
    {
        cam = Camera.main;
        Board = GameObject.Find("Board");

    }

    public void motion()
    {
        if(currentTile == null)
        {
            CheckPosition();
        }
        GameObject clickedTile = cam.GetComponent<ClickManager>().TileSelect;
        if (TargetTile != clickedTile)
        {
            steps.Clear();
            TargetTile = clickedTile;
            float targetx, targety;

            stepx = currentTile.transform.position.x;
            stepy = currentTile.transform.position.y;
            
            targetx = TargetTile.transform.position.x;
            targety = TargetTile.transform.position.y;

            GenerateSteps(targetx, targety, stepx, stepy);
            if(currentTile != TargetTile)
            {
                StartCoroutine(TakeSteps());   
            }
        }
    }

    public void GenerateSteps(float targetx, float targety, float stepx, float stepy)
    {
        if (stepx != targetx || stepy != targety)
        {
            if (stepx < targetx)
            {
                stepx += 1;
                steps.Add(Board.transform.Find(stepx.ToString() + "," + stepy.ToString()).gameObject);
            }
            if (stepx > targetx)
            {
                stepx -= 1;
                steps.Add(Board.transform.Find(stepx.ToString() + "," + stepy.ToString()).gameObject);
            }
            if (stepy < targety)
            {
                stepy += 1;
                steps.Add(Board.transform.Find(stepx.ToString() + "," + stepy.ToString()).gameObject);
            }
            if (stepy > targety)
            {
                stepy -= 1;
                steps.Add(Board.transform.Find(stepx.ToString() + "," + stepy.ToString()).gameObject);
            }
            if (stepx != targetx || stepy != targety)
            {
                GenerateSteps(targetx, targety, stepx, stepy);
            }
        }
    }

    public void CheckPosition()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        Debug.Log(x.ToString() + "," + y.ToString());

        if(Board.transform.Find(x.ToString() + "," + y.ToString()).gameObject != null)
        {
            currentTile= Board.transform.Find(x.ToString() + "," + y.ToString()).gameObject;
        }
        else
        {
            transform.position = new Vector2(1, 1);
            currentTile = Board.transform.Find(x.ToString() + "," + y.ToString()).gameObject;
        }
          
    }
    public IEnumerator TakeSteps()
    {
        for(int num = 0; num < steps.Count; num++)
        {
            yield return new WaitForSeconds(speed);
            if (num < steps.Count)
            {
                NextTile = steps[num];
                Debug.Log("num: " + num.ToString() + ". index count: " + steps.Count.ToString() + ", current tile: " + currentTile.name + ", targettile: " + TargetTile.name);
                transform.position = NextTile.transform.position;
                currentTile = NextTile;
            }
        }
    }
    public void selectCheck()
    {
        if(cam.GetComponent<ClickManager>().UnitSelect == gameObject)
        {
            selected = true;
            gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        }
        else
        {
            selected = false;
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;

        }
    }

    // Update is called once per frame
    void Update()
    {
        selectCheck();
        if (selected == true)
        {
            motion();
        }        
        
    }
}
