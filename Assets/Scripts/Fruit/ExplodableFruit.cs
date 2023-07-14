using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fruit
{
    public class ExplodableFruit : MonoBehaviour
    {
        [SerializeField] private GameObject fruitModel;
        [SerializeField] private ParticleSystem explosion;
        [SerializeField] private Collider collider;

        // Start is called before the first frame update
        private void Start()
        {
        }

        // Update is called once per frame
        private void Update()
        {
            if (transform.position.y < -1) Destroy(gameObject);
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("sword"))
            {
                explosion.Play(); // play the explosion
                collider.isTrigger = false;
                Destroy(fruitModel);
                Destroy(gameObject, explosion.main.duration);
            }
        }
    }
}