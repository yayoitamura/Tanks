using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
	public Rigidbody m_Shell;                   // Prefab of the shell.
	public Transform m_FireTransform;
	private float m_CurrentLaunchForce = 20f;

    //animater
	private Animator animator;

	private void Awake ()
	{
		//animatorの取得
		animator = GetComponent<Animator> ();


		Rigidbody shellInstance =
		Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;
		shellInstance.velocity = m_CurrentLaunchForce * m_FireTransform.forward;

	}


	private void OnEnable()
	{

	}


	void OnTriggerEnter(Collider other) {
		//砲弾に当たるとDamage再生、削除
		if (other.CompareTag("Player"))
		{
			SlimDamege().SetTrigger("Damage");
			Destroy (gameObject, 2f);
		}
	}

	public Animator SlimDamege()
	{
		return animator;
	}

}