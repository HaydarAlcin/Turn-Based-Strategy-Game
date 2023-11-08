using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitRagdoll : MonoBehaviour
{
    [SerializeField] private Transform ragdollRootBone;

    public void Setup(Transform originalRootBone)
    {
        MatchAllChildTransforms(originalRootBone, ragdollRootBone);

        //Her ragdoll ayni sekilde dusmemesi icin random kullandik
        Vector3 randomDir = new Vector3(Random.Range(-1f, +1f), 0, Random.Range(-1f, +1f));
        ApplyExplosionToRagdoll(ragdollRootBone, 400f, transform.position+randomDir, 10f);
    }

    private void MatchAllChildTransforms(Transform root,Transform clone)
    {
        foreach (Transform child in root)
        {
            Transform cloneChild = clone.Find(child.name);
            if (cloneChild!=null)
            {
                cloneChild.position = child.position;
                cloneChild.rotation = child.rotation;

                MatchAllChildTransforms(child, cloneChild);
            }
            
        }
    }


    private void ApplyExplosionToRagdoll(Transform root,float explosionForce,Vector3 explosionPos,float explosionRange)
    {
        foreach (Transform child in root)
        {
            if (child.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
            {
                rigidbody.AddExplosionForce(explosionForce, explosionPos, explosionRange);
            }
            ApplyExplosionToRagdoll(child, explosionForce, explosionPos, explosionRange);
        }
    }
}
