using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Turet : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform turetRotationPoint;
    [SerializeField] private LayerMask enamyMask;
    [SerializeField] private GameObject bulletpheFab;
    [SerializeField] private Transform firingPoint;

    [Header("Attributes")]
    [SerializeField] private float targetingRange = 5.0f;
    [SerializeField] private float rotationSpeed = 100.0f;
    [SerializeField] private float bps = 1f; 

    private Transform target;
    private float timeUntilFire;
    private void Update()
    {
        if (target == null)
        {
            FindTarget();
            return;
        }
        RotateTowardTarget();

        if (!ChecktargetIsinRange())
        {
            target = null;
        }
        else
        {
            timeUntilFire += Time.deltaTime;

            if (timeUntilFire >= 1 / bps )
            {
                Shoot();
                timeUntilFire = 0f;
            }
        }
    }

    private void Shoot()
    {
        GameObject bulletObj = Instantiate(bulletpheFab, firingPoint.position, Quaternion.identity);
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        bulletScript.SetTarget(target);
    }

    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange,(Vector2)transform.position, 0f, enamyMask);

        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }
    }

    private bool ChecktargetIsinRange()
    {
        return Vector2.Distance(target.position , transform.position) <= targetingRange;
    }
    private void RotateTowardTarget()
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y,target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f,0f,angle));
        turetRotationPoint.rotation = Quaternion.RotateTowards(turetRotationPoint.rotation, targetRotation , rotationSpeed * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position.normalized , transform.forward, targetingRange);
    }
}
