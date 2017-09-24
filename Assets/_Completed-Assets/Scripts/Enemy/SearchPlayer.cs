﻿using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class SearchPlayer : MonoBehaviour {
    
//    public Transform target;
	private NavMeshAgent agent;

	void Start () {
		agent = GetComponent<NavMeshAgent>();
	}

	void Update () {
        //Vector3 target = GameObject.FindGameObjectWithTag("Player").transform.position;		
        //agent.destination = target;

	}

	//BossのPlayer探索範囲につけた当たり判定
	void OnTriggerStay(Collider other) {
		Vector3 target = GameObject.FindGameObjectWithTag("Player").transform.position;

		if (other.CompareTag("Player")){
		agent.destination = target;

		//Destroy(gameObject);
		//isDetectPlayer = true;
		//lookTarget = other.transform;
		}
	}

	//void OnTriggerExit(Collider other){
	//	if (other.CompareTag("Player")) {
	//		//isDetectPlayer = false;
	//		//lookTarget = null;
	//	}
	//}
}