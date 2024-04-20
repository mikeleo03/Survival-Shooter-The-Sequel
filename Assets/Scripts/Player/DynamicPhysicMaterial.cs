using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicPhysicMaterial : MonoBehaviour {

    private PhysicMaterial pMat;

    private void Start()
    {
        pMat = new PhysicMaterial();

        pMat.dynamicFriction = 0.5f;
        pMat.frictionCombine = PhysicMaterialCombine.Minimum;

        pMat.bounciness = 0.5f;
        pMat.bounceCombine = PhysicMaterialCombine.Maximum;
    }
}
