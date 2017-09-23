using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
	public float m_StartingHealth = 1f;               // 開始時の各タンクの体力の値
	public GameObject m_ExplosionPrefab;                // Awake でインスタンスにされ、その後、タンクが倒されると常に使用されるプレハブ


	//	private AudioSource m_ExplosionAudio;               // タンクが爆発したときに再生するオーディオ
	private ParticleSystem m_ExplosionParticles;        // タンクが破壊されたときに再生するパーティクルシステム
	private float m_CurrentHealth;                      // 現在のタンクの体力値
	private bool m_Dead;                                // タンクの体力値が 0 を下回ったかどうか
	//a
	private Animator animator;

	private void Awake ()
	{

		//a
		animator = GetComponent<Animator> ();

		//爆発のプレハブをインスタンスにして、そのパーティクルシステムへの参照を取得
		m_ExplosionParticles = Instantiate (m_ExplosionPrefab).GetComponent<ParticleSystem> ();

		// インスタンスにしたプレハブのオーディオソースへの参照を取得
		//		m_ExplosionAudio = m_ExplosionParticles.GetComponent<AudioSource> ();

		// プレハブを無効にし、必要な時に有効にできるようにします。
		m_ExplosionParticles.gameObject.SetActive (false);
	}


	private void OnEnable()
	{
		// タンクが有効にされるとき、タンクの体力をリセットし、倒されていない状態にリセットします。
		m_CurrentHealth = m_StartingHealth;
		m_Dead = false;

	}


	void OnTriggerEnter(Collider other) {

		if (other.CompareTag("Player"))
		{
			//			SlimDamege().SetTrigger("Damage");
			SlimDead().SetTrigger("Dead");
		}
	}

	public Animator SlimDamege()
	{
		return animator;
	}

	public Animator SlimDead()
	{
		return animator;
	}
}