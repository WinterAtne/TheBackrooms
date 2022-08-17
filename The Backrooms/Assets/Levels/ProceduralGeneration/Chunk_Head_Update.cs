using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk_Head_Update : MonoBehaviour
{

    [SerializeField] float renderDistance = 2;
    private float chunkUnitSize = 30f;

    public int biome;
    [SerializeField] float biomeChangeTime = 60f;
    [SerializeField] int[] biomeChances; //In Order down the list in the specified loadhead;


    public float refinedRenderDistance;

    private Transform player;
    private Transform thisTransform;
    [SerializeField] GameObject chunkHead;

    void Start() {
        player = GameObject.Find("Player").GetComponent<Transform>();
        thisTransform = this.transform;

        refinedRenderDistance = renderDistance *  chunkUnitSize;

        for(int i = 0; i < biomeChances.Length; i++) {
            if (i != 0) {
                biomeChances[i] = biomeChances[i] + biomeChances[i - 1];
            }
        }


        CreateLoadHeads();
        StartCoroutine(ChangeBiome());
    }

    
    void FixedUpdate()
    {
        MoveToPlayer();
    }

    void MoveToPlayer() { //Moves to the nearest transform.position that can be expressed purely in multiples of 30 to the player
        float trueX = player.position.x;
        float trueZ = player.position.z;
        int x = Convert.ToInt32(trueX);
        int z = Convert.ToInt32(trueZ);


        x = Round30(x);
        z = Round30(z);

        thisTransform.position = new Vector3(x, 0, z); 
    }

    int Round30(int x) {
        while(x % chunkUnitSize != 0) {
            x++;
        }
        return(x);
    }

    void CreateLoadHeads() {
        Vector3 startingPoint = (Vector3.right * -refinedRenderDistance) + (Vector3.forward * -refinedRenderDistance);
        Vector3 instanceTransform = startingPoint;


        //Creates a renderDistance by renderDistance square of chunkHeads that are then made children of this object, so that when this object movess to the player, they follow.
        for(int xi = 1; xi <= renderDistance * 2 + 1; xi++) {
            for(int zi = 0; zi <= renderDistance * 2; zi++) {
                GameObject head = Instantiate(chunkHead, instanceTransform, Quaternion.identity);
                head.transform.parent = thisTransform;
                instanceTransform += Vector3.forward * chunkUnitSize;
            }

            instanceTransform = startingPoint + (Vector3.right * chunkUnitSize * xi);
        }

        
    }

    IEnumerator ChangeBiome() {
        while(true) {
            DetermineBiome();

            yield return new WaitForSeconds(biomeChangeTime);
        }
    }


    void DetermineBiome() {
        int rand = UnityEngine.Random.Range(0,100);
        if (rand < biomeChances[0]) {
            biome = 0;
        } else if (rand > biomeChances[0] && rand < biomeChances[1]) {
            biome = 1;
        } else if (rand > biomeChances[1] && rand < biomeChances[2]) {
            biome = 2;
        } else if (rand > biomeChances[2] && rand < biomeChances[3]) {
            biome = 3;
        } else if (rand > biomeChances[3] && rand < biomeChances[4]) {
            biome = 4;
        } else if (rand > biomeChances[5] && rand < biomeChances[6]) {
            biome = 5;
        } else if (rand > biomeChances[7] && rand < biomeChances[8]) {
            biome = 6;
        }
    }
}
