using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelController : MonoBehaviour
{
    private void OnBecameVisible()
    {
        ObjectController.obj = this.gameObject;
    }

}
