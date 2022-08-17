using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/* 
The Bug will not move for a period of time, instead waiting a bit.
Then it will "Turn On" before following the player for another short period of time.
Finnally it will attack the player.
*/

public class Bug_Behavior : MonoBehaviour
{
    public enum State {
        Sleep,
        Follow,
        Attack
    };

    public State currentstate;

    //Sleep Variables;
    [SerializeField] int minimumSleepTime = 30;
    [SerializeField] int maximumSleepTime = 60;
    int secondsTillStateAdvance = 0;
    private int alarmChance = 20;

    //Follow Variables;
    [SerializeField] int minimumFollowTime = 10;
    [SerializeField] int maximumFollowTime = 20;
    [SerializeField] float followSpeed = 3.5f;


    //Attack Variables;
    Transform player;
    NavMeshAgent nav;
    [SerializeField] float attackSpeed = 8.5f;

    void Awake() {
        player = GameObject.Find("Player").transform;
        nav = GetComponent<NavMeshAgent>();
        StartCoroutine(Sleep());
    }

    void OnCollisionEnter(Collision col) {
        Debug.Log("HIT!");
        if (col.gameObject.name == "Player" && currentstate == State.Attack) {
            Debug.Log("HIIIT!");
            Destroy(col.gameObject);
        }
    }

    void AdvanceState() {
        secondsTillStateAdvance = 0;
        StopAllCoroutines();
        if (currentstate == State.Sleep) {
            StartCoroutine(Follow());
        } else if (currentstate == State.Follow) {
            StartCoroutine(Attack());
        }
    }

    void CheckStateAdvance(int minTime, int maxTime) {
        secondsTillStateAdvance++;
        if (secondsTillStateAdvance == maxTime) {
            AdvanceState();
        } else if (secondsTillStateAdvance > minTime) {
            int i = Random.Range(1, alarmChance + 1);
            if (i == alarmChance) {
                AdvanceState();
            }  
        }
    }


    IEnumerator Sleep() {
        currentstate = State.Sleep;
        while(true) {
            yield return new WaitForSeconds(1f);
            secondsTillStateAdvance++;
            CheckStateAdvance(minimumSleepTime, maximumSleepTime);
        }
    }

    IEnumerator Follow() {
        currentstate = State.Follow;
        nav.speed = followSpeed;
        while(true) {
            yield return new WaitForSeconds(1f);
            CheckStateAdvance(minimumFollowTime, maximumFollowTime);
            nav.SetDestination(player.position);
        }

    }

    IEnumerator Attack() {
        currentstate = State.Attack;
        nav.speed = attackSpeed;
        nav.stoppingDistance = 0;
        while(true) {
            yield return new WaitForSeconds(0.5f);
            nav.SetDestination(player.position);
        }
    }
}
