using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class SearchPlayer : MonoBehaviour {

	public Rigidbody m_Shell;                   // Prefab of the shell.
	public Transform m_FireTransform;
	private float m_CurrentLaunchForce = 5;
    private bool flag = true;

//    public Transform target;
	private NavMeshAgent agent;

	//void Start () {
	//	agent = GetComponent<NavMeshAgent>();
		//InvokeRepeating("Atack", 2f, 3f);
	//}

	IEnumerator Start()
	{
		agent = GetComponent<NavMeshAgent>();

		while (true)
		{
            Atack();
			yield return new WaitForSeconds(1f);
		}
	}

    void Update () {

	}

    private void Atack() {
        if (flag == true)
        {
            Rigidbody shellInstance =
            Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;
            shellInstance.velocity = m_CurrentLaunchForce * m_FireTransform.forward;
        }
	}

	//BossのPlayer探索範囲につけた当たり判定
    void OnTriggerStay(Collider other) {
		Vector3 target = GameObject.FindGameObjectWithTag("Player").transform.position;

        if (other.CompareTag("Player")){ 

			//追跡
			agent.destination = target;
            flag = true;

			// 砲弾のインスタンスを作成して、そのリジッドボディへの参照を格納
			//Rigidbody shellInstance =
			//	Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;

			// 射撃位置の前方方向で、砲弾速度を発射力に設定
			//shellInstance.velocity = m_CurrentLaunchForce * m_FireTransform.forward;

        } else {
            flag = false;
        }
 	}

}



/*
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 *
 *
 * 
 * 
 * 
 */