using UnityEngine;
using UnityEngine.UI;

namespace Complete
{
    public class Enemy : MonoBehaviour
    {
        public TankHealth healthDamage;
        //animater
        private Animator animator;

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
            //Collider[] colliders = Physics.OverlapSphere(transform.position, 1f);
            //for (int i = 0; i < colliders.Length; i++)
            //{
                //Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody>();
                //TankHealth targetHealth = targetRigidbody.GetComponent<TankHealth>();


                if (healthDamage) { //targetHealth
                    Debug.Log("point");
					healthDamage.TakeDamage(500);
      
                } else if (other.CompareTag("Shell")) {
                    SlimDamege().SetTrigger("Damage");
                    Destroy(gameObject, 2f);
                }
            //}
        }

        public Animator SlimDamege()
        {
            return animator;
        }


    }
}