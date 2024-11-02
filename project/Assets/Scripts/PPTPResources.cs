using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Duarto.Utilities;

namespace Duarto.Utilities{

    public class PPTPResources : Singleton<PPTPResources> {

        public float Round(float num, int pos){
            float aux = Mathf.Pow(10.0f, pos);
            return (Mathf.Round(num/aux) * aux);
        }
    }
}