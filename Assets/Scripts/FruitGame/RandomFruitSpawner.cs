using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fruit
{
    public class RandomFruitSpawner : MonoBehaviour
    {
        public GameObject watermelon;
        public GameObject banana;

        private float _lastFruit;
        private float _horizontalForce = 280.0f;

        private Rigidbody SpawnFruit()
        {
            var pos = new Vector3(-0.9f + Random.value * 1.5f, 0, 0.8f);
            if (Random.value > 0.5f)
                return Instantiate(watermelon, pos, Quaternion.identity).GetComponent<Rigidbody>();
            return Instantiate(banana, pos, Quaternion.identity).GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        private void Update()
        {
            _lastFruit += Time.deltaTime;
            if (_lastFruit > 1)
            {
                var newFruit = SpawnFruit();
                if (newFruit.position.x < 0)
                    newFruit.AddForce(new Vector3(Random.value * 0.2f, 1, 0) * _horizontalForce);
                else
                    newFruit.AddForce(new Vector3(-Random.value * 0.1f, 1, 0) * _horizontalForce);
                newFruit.AddTorque(new Vector3(Random.value, Random.value, Random.value) * 200);
                _lastFruit = 0;
            }
        }
    }
}