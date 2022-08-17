using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Load_Chunk : MonoBehaviour
{
    [SerializeField] GameObject[] chunkOrigin;
    [SerializeField] LayerMask groundMask;
    private float chunkCheckRadius = 3f;
    private Transform thisTransform;
    private Chunk_Head_Update chunkHeadUpdate; //you come up with a better name!

    void Start() {
        thisTransform = this.transform;
        chunkHeadUpdate = GameObject.Find("Chunk_Load_Head(0,0,0)").GetComponent<Chunk_Head_Update>();
    }

    void FixedUpdate() {
        if (!CheckChunk()) {
            PlaceChunk(chunkHeadUpdate.biome);
        }
    }


    void PlaceChunk(int currentBiome) {
        Instantiate(chunkOrigin[currentBiome], thisTransform.position, chunkOrigin[currentBiome].transform.rotation);
    }

    bool CheckChunk() {
        return(Physics.CheckSphere(thisTransform.position, chunkCheckRadius, groundMask));
    }
}
