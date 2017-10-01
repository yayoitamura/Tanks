using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
	public float m_StartingHealth = 100f;                 // 開始時の各タンクの体力の値
	public Slider m_Slider;                             // 現在のタンクの体力を示すスライダー
	public Image m_FillImage;                           // スライダーの Image コンポーネント
	public Color m_FullHealthColor = Color.green;       // The color the health bar will be when on full health.
	public Color m_ZeroHealthColor = Color.red;
	public GameObject m_ExplosionPrefab;                // タンクが倒されると使用されるプレハブ


	//	private AudioSource m_ExplosionAudio;               // タンクが爆発したときに再生するオーディオ
	private ParticleSystem m_ExplosionParticles;        // タンクが破壊されたときに再生するパーティクルシステム
	private float m_CurrentHealth;                      // 現在のタンクの体力値
    public bool BossDestroy;

	private void Awake ()
	{

        //BossDestroy = false;

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
        BossDestroy = false;
        SetHealthUI();
	}

	private void SetHealthUI()
	{
		// スライダーに適切な値を設定
		// 開始時に対する現在の体力のパーセントに基づいて、選択した色でバーを満たします。
		// Set the slider's value appropriately.
		m_Slider.value = m_CurrentHealth;

		// Interpolate the color of the bar between the choosen colours based on the current percentage of the starting health.
		m_FillImage.color = Color.Lerp(m_ZeroHealthColor, m_FullHealthColor, m_CurrentHealth / m_StartingHealth);
	}

	public void TakeDamage(float amount)
	{
		// 受けたダメージに基づいて現在の体力を削減
		// 適切な UI 要素に変更
		// 現在の体力が 0 を下回り、かつ、まだ登録されていなければ、 OnDeath を呼び出します。
		// Reduce current health by the amount of damage done.
		m_CurrentHealth -= amount;

		// Change the UI elements appropriately.
		SetHealthUI();

		// If the current health is at or below zero and it has not yet been registered, call OnDeath.
        if (m_CurrentHealth <= 0f && !BossDestroy)
		{
			OnDeath();
		}
	}

	private void OnDeath()
	{
		// フラグを設定して、この関数が1 度しか呼び出されないようにします。
		// インスタンスにした爆発のプレハブをタンクの位置に移動し、有効にします。
		// タンクの爆発のパーティクルシステムを再生します。
		// タンクの爆発のサウンドエフェクトを再生します。
		// タンクをオフにします。
		// Set the flag so that this function is only called once.
        BossDestroy = true;

		// Move the instantiated explosion prefab to the tank's position and turn it on.
		m_ExplosionParticles.transform.position = transform.position;
		m_ExplosionParticles.gameObject.SetActive(true);

		// Play the particle system of the tank exploding.
		m_ExplosionParticles.Play();

		// Play the tank explosion sound effect.
		//m_ExplosionAudio.Play();

		// Turn the tank off.
		gameObject.SetActive(false);
	}


	//void OnTriggerEnter(Collider other) {

	//	if (other.CompareTag("Player"))
	//	{
	//		Destroy (gameObject);
	//	}
	//}

	//void OnTriggerEnter(Collider other)
	//{

	//        if (other.tag == "Player")
	//       	{
	//		//float damage = 500;
	//		//TakeDamage(damage);
	//		Destroy(gameObject);

	//	    }


	//}

	void OnTriggerEnter(Collider other)
	{
		Debug.Log(other.tag + "tag");

		//砲弾に当たるとDamage再生、削除
		if (other.CompareTag("Shell"))
		{
			//BossDestroy = true;

			//Destroy(gameObject.transform.parent.gameObject);
			float damage = 50;
			TakeDamage(damage);

		}
	}

}