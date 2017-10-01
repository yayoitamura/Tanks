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
			BossDestroy = true;

            Destroy(gameObject.transform.parent.gameObject);
		}
	}

}