using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generate_Chunk : MonoBehaviour
{
    [SerializeField] GameObject[] wallObjects;
    [SerializeField] GameObject[] doorObjects;
    [SerializeField] GameObject[] cornerObjects;
    [SerializeField] GameObject[] walls;
    [SerializeField] GameObject[] doors;
    [SerializeField] GameObject[] corners;
    [SerializeField] bool enableCorners;
    private Transform player;
    private Transform thisTransform;
    private int wallsLength;
    private int doorsLength;
    private int cornersLength;
    private float renderDistance = 200f;

    void Start() {
        player = GameObject.Find("Player").GetComponent<Transform>();
        thisTransform = this.transform;
        wallsLength = walls.Length;
        doorsLength = doors.Length;
        cornersLength = corners.Length;
        

        CreateDoors();
        CreateWalls();
        if (enableCorners) {
            CreateCorners();
        }
    }


    void FixedUpdate() {
        CheckIfOutOfRange();
    }



    void CreateWalls() {
        System.Random random = new System.Random();
        for (int i = 0; i < wallObjects.Length; i++) {
            int rand = random.Next(0, wallsLength);

            GameObject wall = Instantiate(walls[rand], wallObjects[i].transform.position, wallObjects[i].transform.rotation);
            wall.transform.parent = thisTransform;
            Destroy(wallObjects[i]); 
        }
    }

    void CreateDoors() {
        System.Random random = new System.Random();
        for (int i = 0; i < doorObjects.Length; i++) {
            int rand = random.Next(0, doorsLength);

            GameObject door = Instantiate(doors[rand], doorObjects[i].transform.position, doorObjects[i].transform.rotation);
            door.transform.parent = thisTransform;
            Destroy(doorObjects[i]); 
        }
    }

    void CreateCorners() {
        System.Random random = new System.Random();
        for (int i = 0; i < cornerObjects.Length; i++) {
            int rand = random.Next(0, cornersLength);

            GameObject wall = Instantiate(corners[rand], cornerObjects[i].transform.position, cornerObjects[i].transform.rotation);
            wall.transform.parent = thisTransform;
            Destroy(cornerObjects[i]);
        }
    }


    void CheckIfOutOfRange() {
        if (Vector3.Distance (player.position, thisTransform.position) > renderDistance) {
            Destroy(this.gameObject);
        }
    }
}
