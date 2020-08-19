using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] float movementPeriod = .5f;
    [SerializeField] ParticleSystem goalExplodeParticle;
    //[SerializeField] GameObject objectToChangeColor;


    Waypoint endWaypoint;

    // Start is called before the first frame update
    void Start()
    {
        Pathfinder pathfinder = FindObjectOfType<Pathfinder>(); //dangerous if more than 1 instance of pathfinder
        var path = pathfinder.GetPath();
        StartCoroutine(FollowPath(path));
    }

    IEnumerator FollowPath(List<Waypoint> path)
    {
        foreach (Waypoint waypoint in path)
        {
            transform.position = waypoint.transform.position;
            //MeshRenderer meshRenderer = objectToChangeColor.GetComponent<MeshRenderer>();
            waypoint.transform.Find("Block_Friendly").GetComponent<MeshRenderer>().material.color = Color.cyan;


            yield return new WaitForSeconds(movementPeriod);
        }
        SelfDestruct(); //this is where endwaypoint is reached
    }

    private void SelfDestruct()
    {
        var vfx = Instantiate(goalExplodeParticle, transform.position, Quaternion.identity);
        vfx.Play();

        Destroy(vfx.gameObject, vfx.main.duration);
        Destroy(gameObject);
    }

}
