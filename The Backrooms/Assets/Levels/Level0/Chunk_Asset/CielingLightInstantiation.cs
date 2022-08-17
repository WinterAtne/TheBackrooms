using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CielingLightInstantiation : MonoBehaviour
{
    [SerializeField] GameObject[] lamps;
    [SerializeField] GameObject _Light;
    [SerializeField] int lightChance;

    void Start() {
        Transform thisTransform = this.transform;

        for(int i = 0; i <= lamps.Length - 1; i++) {
            int rand = Random.Range(1,101);
            if (rand > lightChance) {
                GameObject lamp = Instantiate(_Light, lamps[i].transform.position, lamps[i].transform.rotation);
                lamp.transform.parent = thisTransform;
                Destroy(lamps[i]);
            }
        }
        Destroy(this);
    }
}
