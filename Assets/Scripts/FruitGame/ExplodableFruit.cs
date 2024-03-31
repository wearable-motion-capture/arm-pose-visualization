using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fruit
{
    public class ExplodableFruit : MonoBehaviour
    {
        [SerializeField] private MeshRenderer fruitRenderer;
        [SerializeField] private ParticleSystem explosion;
        [SerializeField] private Collider trigger;

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
                trigger.isTrigger = false;
                fruitRenderer.enabled = false;
                Destroy(gameObject, explosion.main.duration);
            }
        }
    }
}