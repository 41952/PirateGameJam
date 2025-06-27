using System.Collections.Generic;
using UnityEngine;

public class WallCheck : MonoBehaviour
{
    public bool onWall;
    public Vector3 poc;

    private List<Collider> cols = new List<Collider>();

    private void Update()
    {
        // Если коллайдеров нет — больше не на стене
        onWall = cols.Count > 0;
        if (onWall)
        {
            poc = GetClosestPoint();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger && (other.gameObject.layer == 8 || other.gameObject.layer == 24))
        {
            cols.Add(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (cols.Contains(other))
        {
            cols.Remove(other);
        }
    }

    private Vector3 GetClosestPoint()
    {
        Vector3 closest = Vector3.zero;
        float minDist = float.MaxValue;

        foreach (Collider col in cols)
        {
            if (col != null)
            {
                Vector3 point = col.ClosestPoint(transform.position);
                float dist = Vector3.Distance(transform.position, point);
                if (dist < minDist)
                {
                    minDist = dist;
                    closest = point;
                }
            }
        }

        return closest;
    }
}
