// Patrol.cs
using UnityEngine;
using System.Collections;


public class MoveTo : MonoBehaviour {

	public Transform[] points;
	private int destPoint = 0;
	private UnityEngine.AI.NavMeshAgent agent;


	void Start () {
		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

		// Disabling auto-braking allows for continuous movement
		// between points (ie, the agent doesn't slow down as it
		// approaches a destination point).
		//自動制動を無効にすると、ポイント間の連続的な移動が可能になります（つまり、エージェントは目的地に近づくにつれて速度が低下しません）。
		agent.autoBraking = false;

		GotoNextPoint();
	}


	void GotoNextPoint() {
		// Returns if no points have been set upポイントが設定されていない場合は返します。
		if (points.Length == 0)
			return;

		// Set the agent to go to the currently selected destination.現在選択されている宛先に移動するようにエージェントを設定します。
		agent.destination = points[destPoint].position;

		// Choose the next point in the array as the destination,
		// cycling to the start if necessary.
		//配列内の次の点を宛先として選択し、
		//必要に応じて開始までサイクリングする。
		destPoint = (destPoint + 1) % points.Length;
	}


	void Update () {
		// Choose the next destination point when the agent gets
		// close to the current one.
		//エージェントが現在の目的地に近づくと、次の目的地点を選択します。
		if (agent.remainingDistance < 0.5f)
			GotoNextPoint();
	}
}