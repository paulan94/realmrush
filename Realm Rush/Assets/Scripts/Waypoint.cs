using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{

    public bool isExplored = false; //public ok as is a data class
    public Waypoint exploredFrom;
    public bool isPlaceable = true;

    Vector2Int gridPos;
    const int gridSize = 10;

    public int GetGridSize()
    {
        return gridSize;
    }


    public Vector2Int getGridPos()
    {
        return new Vector2Int(
            Mathf.RoundToInt(transform.position.x / gridSize),
            Mathf.RoundToInt(transform.position.z / gridSize)
        );
    }


    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isPlaceable)
            {
                FindObjectOfType<TowerFactory>().AddTower(this);
            }
            else
            {
                print("cant place here bruh");
            }
        }
    }
}
