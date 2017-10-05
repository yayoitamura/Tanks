using UnityEngine;
using UnityEngine.UI;

namespace Complete
{
    public class Enemy : MonoBehaviour
    {
        public TankHealth script; //UnityChanScriptが入る変数


		//public TankHealth TankHealth;
		//animater
		private Animator animator;

        // Use this for initialization
        void Start()
        {            
        }

        private void Awake()
        {
            //animatorの取得
            animator = GetComponent<Animator>();

        }


        private void OnEnable()
        {

        }


        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")) { //targetHealth
                Destroy(gameObject);
        } else if (other.CompareTag("Shell")) {
                 SlimDamege().SetTrigger("Damage");
                 Destroy(gameObject, 2f);
                 }
        }

        public Animator SlimDamege()
        {
            return animator;
        }


    }
}