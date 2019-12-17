using UnityEngine;
using System.Collections;

public class ClickManager : MonoBehaviour
{
    public GameObject clickedObject;
    public GameObject UnitSelect;
    public GameObject TileSelect;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.gameObject.name);
                clickedObject = hit.collider.gameObject;
                UnitMoveCheck();
            }
            
        }
    }
    public void UnitMoveCheck()
    {
        if (clickedObject.gameObject.tag == "Player")
        {
            TileSelect = null;
            UnitSelect = clickedObject;

        }
        if (clickedObject.gameObject.tag == "Floor" && UnitSelect != null)
        {
            TileSelect = clickedObject;
        }
        if(clickedObject.gameObject.tag!="Floor" && clickedObject.gameObject.tag != "Player")
        {
            UnitSelect = null;
            TileSelect = null;
        }
            
    }

}