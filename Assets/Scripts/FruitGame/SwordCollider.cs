using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FruitGame
{
    public class SwordCollider : MonoBehaviour
    {
        // Update is called once per frame
        void Update()
        {
            transform.SetPositionAndRotation(this.transform.position, Quaternion.identity);
        }
    }
}