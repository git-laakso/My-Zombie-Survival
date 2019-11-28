using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    Animator PlayAnimator;
    CharacterController CharControl;
    private NavMeshAgent _nav;
    private Transform _player;
    public int health = 100;
    public int damage = 25;

    public GameObject addKC;
    void Start()
    {
        //Read NavMesh and player and character controller
        _nav = GetComponent<NavMeshAgent>();
        PlayAnimator = GetComponent<Animator>();
        CharControl = GetComponent<CharacterController>();
        PlayAnimator.SetInteger("run", 1);

        
        //Find player with tag
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        _nav.SetDestination(_player.position);
        
    }

    void OnCollisionStay(Collision col)
    {
        
        if (col.gameObject.name == "Player")
        {
            PlayAnimator.SetInteger("attack", 1);
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.name == "Player")
        {
            PlayAnimator.SetInteger("attack", 0);
            Debug.Log("Karkuun");
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Bullet(Clone)")
        {
            Debug.Log("Remaining hp:" + health);
            health -= damage;

            if (health < 0)
            {
                PlayAnimator.SetInteger("enemySlain", 1);
                Invoke("Die", 3);
            }
        }
    }
    public void Die()
    {
        Destroy(gameObject);
        //addKC = GetComponent<CountKR>();
        addKC.GetComponent<CountKR>().UpdateKC();
        //addKC.GetComponent<CountKR>().SpawnWave();





    }
}