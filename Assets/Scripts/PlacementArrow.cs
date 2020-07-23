using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementArrow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // add a new instance of that prefab thingy at current position
    private void OnMouseDown()
    {
        Debug.Log("At position " + x_pos + y_pos);
        GameObject.Find("GameManager").GetComponent<GameManager>().AddTile(x_pos, y_pos);
    }
}
