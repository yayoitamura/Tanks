using UnityEngine;
using UnityEngine.UI;

namespace Complete
{
    public class TankHealth : MonoBehaviour
    {
        public float m_StartingHealth = 500f;               // The amount of health each tank starts with.
        public Slider m_Slider;                             // 現在のタンクの体力を示すスライダー
		public Image m_FillImage;                           // スライダーの Image コンポーネント
		public Color m_FullHealthColor = Color.green;       // The color the health bar will be when on full health.
        public Color m_ZeroHealthColor = Color.red;         // The color the health bar will be when on no health.
        public GameObject m_ExplosionPrefab;                // タンクが倒されると使用されるプレハブ

		private AudioSource m_ExplosionAudio;               // The audio source to play when the tank explodes.
        private ParticleSystem m_ExplosionParticles;        // The particle system the will play when the tank is destroyed.
        private float m_CurrentHealth;                      // 現在のタンクの体力値
		private bool m_Dead;                                // Has the tank been reduced beyond zero health yet?

        private void Awake ()
        {
            Debug.Log(m_StartingHealth + "Awake");
            Debug.Log(m_CurrentHealth + "Awake");

			//爆発のプレハブをインスタンスにして、そのパーティクルシステムへの参照を取得
			// インスタンスにしたプレハブのオーディオソースへの参照を取得
			// プレハブを無効にし、必要な時に有効にできるようにします。
			// Instantiate the explosion prefab and get a reference to the particle system on it.
			m_ExplosionParticles = Instantiate (m_ExplosionPrefab).GetComponent<ParticleSystem> ();

            // Get a reference to the audio source on the instantiated prefab.
            m_ExplosionAudio = m_ExplosionParticles.GetComponent<AudioSource> ();

            // Disable the prefab so it can be activated when it's required.
            m_ExplosionParticles.gameObject.SetActive (false);
        }


        private void OnEnable()
        {
			// タンクが有効にされるとき、タンクの体力をリセットし、倒されていない状態にリセットします。
			// 体力スライダーの値と色を更新
			// When the tank is enabled, reset the tank's health and whether or not it's dead.
			m_CurrentHealth = m_StartingHealth;
            m_Dead = false;

            // Update the health slider's value and color.
            SetHealthUI();
        }

        public void EnemyTrigger() {
            Debug.Log("helth.Enemytrigger");
            gameObject.SetActive(false);
        }

        public void TakeDamage (float amount)
        {
            Debug.Log(m_CurrentHealth + "Tank_Current_mae");
			// 受けたダメージに基づいて現在の体力を削減
			// 適切な UI 要素に変更
			// 現在の体力が 0 を下回り、かつ、まだ登録されていなければ、 OnDeath を呼び出します。
			// Reduce current health by the amount of damage done.
			m_CurrentHealth -= amount;

            // Change the UI elements appropriately.
            SetHealthUI ();

            // If the current health is at or below zero and it has not yet been registered, call OnDeath.
            if (m_CurrentHealth <= 0f && !m_Dead)
            {
                OnDeath ();
            }
            Debug.Log(amount + "Tank_Damage");
            Debug.Log(m_CurrentHealth + "Tank_Current");
        }


        private void SetHealthUI ()
        {
			// スライダーに適切な値を設定
			// 開始時に対する現在の体力のパーセントに基づいて、選択した色でバーを満たします。
			// Set the slider's value appropriately.
			m_Slider.value = m_CurrentHealth;

            // Interpolate the color of the bar between the choosen colours based on the current percentage of the starting health.
            m_FillImage.color = Color.Lerp (m_ZeroHealthColor, m_FullHealthColor, m_CurrentHealth / m_StartingHealth);
        }


        public void OnDeath ()
        {
            Debug.Log("THealth.OnDerth");
			// フラグを設定して、この関数が1 度しか呼び出されないようにします。
			// インスタンスにした爆発のプレハブをタンクの位置に移動し、有効にします。
			// タンクの爆発のパーティクルシステムを再生します。
			// タンクの爆発のサウンドエフェクトを再生します。
			// タンクをオフにします。
			// Set the flag so that this function is only called once.
			m_Dead = true;

            // Move the instantiated explosion prefab to the tank's position and turn it on.
            m_ExplosionParticles.transform.position = transform.position;
            m_ExplosionParticles.gameObject.SetActive (true);

            // Play the particle system of the tank exploding.
            m_ExplosionParticles.Play ();

            // Play the tank explosion sound effect.
            m_ExplosionAudio.Play();

            // Turn the tank off.
            gameObject.SetActive (false);
        }



		void OnTriggerEnter(Collider other) {
			if (other.ToString () == "Hp (UnityEngine.BoxCollider)")
			{
				m_CurrentHealth += 10;
				Destroy (other.gameObject);

			}

			else if (other.tag == "Enemy")
				{
                OnDeath();
				//float damage = 500;
				//TakeDamage (damage);

				}				
		}

    }

}