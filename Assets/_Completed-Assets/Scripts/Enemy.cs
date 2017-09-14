using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
	public float m_StartingHealth = 1f;               // 開始時の各タンクの体力の値
	public GameObject m_ExplosionPrefab;                // Awake でインスタンスにされ、その後、タンクが倒されると常に使用されるプレハブ


//	private AudioSource m_ExplosionAudio;               // タンクが爆発したときに再生するオーディオ
	private ParticleSystem m_ExplosionParticles;        // タンクが破壊されたときに再生するパーティクルシステム
	private float m_CurrentHealth;                      // 現在のタンクの体力値
	private bool m_Dead;                                // タンクの体力値が 0 を下回ったかどうか


	private void Awake ()
	{
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


	public void TakeDamage (float amount)
	{
		// 受けたダメージに基づいて現在の体力を削減
		m_CurrentHealth -= amount;

		// 現在の体力が 0 を下回り、かつ、まだ登録されていなければ、 OnDeath を呼び出します。
		if (m_CurrentHealth <= 0f && !m_Dead)
		{
			OnDeath ();
		}
	}


	private void OnDeath ()
	{
		// フラグを設定して、この関数が1 度しか呼び出されないようにします。
		m_Dead = true;

		// インスタンスにした爆発のプレハブをタンクの位置に移動し、有効にします。
		m_ExplosionParticles.transform.position = transform.position;
		m_ExplosionParticles.gameObject.SetActive (true);

		// タンクの爆発のパーティクルシステムを再生します。
		m_ExplosionParticles.Play ();

		// タンクの爆発のサウンドエフェクトを再生します。
//		m_ExplosionAudio.Play();

		// タンクをオフにします。
		gameObject.SetActive (false);
	}
		
}