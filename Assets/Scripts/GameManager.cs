using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Prefab for adding the tiles
    public GameObject prefab1;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddTile(float x_pos, float y_pos)
    {
        Instantiate(prefab1, new Vector3(x_pos, y_pos), Quaternion.identity);
    }
}
