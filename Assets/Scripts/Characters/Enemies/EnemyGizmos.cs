using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyGizmos : MonoBehaviour
{
    [Header("Patrolling variables")]
    public Transform from;
    public Transform to;
    [Header("Sight")]
    public Transform pivot;

    [Header("EnemyBehaviour variables from script")]
    public Enemy enemy;

    void OnDrawGizmosSelected()
    {
        if (from != null && to != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(from.position, to.position);
            Gizmos.DrawSphere(from.position, 0.15f);
            Gizmos.DrawSphere(to.position, 0.15f);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(pivot.position, enemy.maxRadius);
        Gizmos.DrawWireSphere(pivot.position, enemy.attack_radius);

        Vector3 FOVline1 = Quaternion.AngleAxis(enemy.maxAngle/2, transform.up) * transform.forward * enemy.maxRadius;
        Vector3 FOVline2 = Quaternion.AngleAxis(-enemy.maxAngle/2, transform.up) * transform.forward * enemy.maxRadius;

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(pivot.position, FOVline1);
        Gizmos.DrawRay(pivot.position, FOVline2);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(pivot.position, (enemy.player.transform.GetChild(0).position - pivot.position).normalized * enemy.maxRadius);

        Gizmos.color = Color.black;
        Gizmos.DrawRay(pivot.position, transform.forward * enemy.maxRadius);
    }
}
