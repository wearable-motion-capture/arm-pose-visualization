using System.Collections.Generic;
using UnityEngine;

namespace Listener
{
    public abstract class Basis : MonoBehaviour
    {
        public abstract void MoveBoneMap(Dictionary<string, GameObject> boneMap);
    }
}