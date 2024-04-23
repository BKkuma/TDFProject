using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TuretSlow : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LayerMask enamyMask;

    [Header("Attributes")]
    [SerializeField] private float targetingRange = 5.0f;
    [SerializeField] private float  aps = 4f;
    [SerializeField] private float freezTime = 1f;

    private float timeUntilFire;
    private void Update()
    {        
            timeUntilFire += Time.deltaTime;

            if (timeUntilFire >= 1f / aps)
            {
                
                FreezeEnemy();
                timeUntilFire = 0f;
            }
        
    }

    private void FreezeEnemy()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enamyMask);

        if (hits.Length > 0 )
        {
            for(int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];

                EnamyMovement em = hit.transform.GetComponent<EnamyMovement>();
                em.UpdateSpeed(0.5f);

                StartCoroutine(ResetEnemySpeed(em));
            }
        }
    }

    private IEnumerator ResetEnemySpeed(EnamyMovement em)
    {
        yield return new WaitForSeconds(freezTime);
    }

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position.normalized, transform.forward, targetingRange);
    }



}
