using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CamShake : MonoBehaviour
{
    public void ShakeCamera(){
        transform.DOShakePosition(0.5f, new Vector3(20f,10f,0), 10, 30, true, true);
    }
}
