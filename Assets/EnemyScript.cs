using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    public Transform target;

    NavMeshAgent agent;

    int enemyHP = 100;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.Find("FPSController");
        target = player.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.transform.position);
        if (enemyHP <= 0)
        {
            ScoreScript.enemycount += 1;

            Destroy(this.gameObject);
        }

    }

    public void Damage()
    {
        enemyHP -= 30;
    }
    public void Damage1()
    {
        enemyHP -= 15;  
    }
    public void HeadDamage()
    {
        enemyHP -= 45;
    }
    public void HeadDamage1()
    {
        enemyHP -= 30;
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "FPSController")
        {
            other.gameObject.SendMessage("PlayerDamage");
            Destroy(this.gameObject);
        }
    }
}
