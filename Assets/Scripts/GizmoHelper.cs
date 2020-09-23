using UnityEngine;

namespace DefaultNamespace
{
    public static class GizmoHelper
    {
        public static void DrawCircle(Vector3 center, float radius)
        {
            float theta = 0;
            float x = radius * Mathf.Cos(theta);
            float y = radius * Mathf.Sin(theta);
            Vector3 from = center + new Vector3(x, y, 0);
            Vector3 to;
            for (theta = 0.2f; theta < Mathf.PI * 2; theta += 0.2f)
            {
                x = radius * Mathf.Cos(theta);
                y = radius * Mathf.Sin(theta);
                to = center + new Vector3(x, y, 0);
                Gizmos.DrawLine(from, to);
                from = to;
            }
            x = radius * Mathf.Cos(0);
            y = radius * Mathf.Sin(0);
            Gizmos.DrawLine(from, center + new Vector3(x, y, 0));
        }
        
    }
}