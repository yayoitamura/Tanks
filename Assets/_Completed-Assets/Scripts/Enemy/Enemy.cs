using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    //animater
	private Animator animator;

	private void Awake ()
	{
		//animatorの取得
		animator = GetComponent<Animator> ();

	}


	private void OnEnable()
	{

	}


	void OnTriggerEnter(Collider other) {
        Debug.Log(other.tag + "tag");

		//砲弾に当たるとDamage再生、削除
		if (other.CompareTag("Shell"))
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