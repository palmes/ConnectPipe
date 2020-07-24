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
        float pos_x = gameObject.transform.position.x;
        float pos_y = gameObject.transform.position.y;
        GameObject.Find("GameManager").GetComponent<GameManager>().AddTile(pos_x, pos_y);
    }
}
