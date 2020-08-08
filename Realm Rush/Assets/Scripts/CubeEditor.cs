using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase] //makes it easier to click the parent class
public class CubeEditor : MonoBehaviour
{
    
    Waypoint waypoint;

    void Awake()
    {
        waypoint = GetComponent<Waypoint>();
    }

    void Start()
    {
      
    }

    void Update()
    {
        SnapToGrid();
        UpdateLabel();
    }

    private void SnapToGrid()
    {
        int gridSize = waypoint.GetGridSize();

        transform.position = new Vector3(
            waypoint.getGridPos().x * gridSize,
            0f,
            waypoint.getGridPos().y * gridSize
        );
    }

    private void UpdateLabel()
    {
        int gridSize = waypoint.GetGridSize();
        TextMesh textMesh = GetComponentInChildren<TextMesh>();

        string labelText =
            waypoint.getGridPos().x +
            "," +
            waypoint.getGridPos().y;
        textMesh.text = labelText;
        gameObject.name = labelText;
    }
}
