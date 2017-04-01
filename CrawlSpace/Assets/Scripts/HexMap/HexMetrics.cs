using UnityEngine;

// Defines constants for hexes.
public static class HexMetrics
{
    // Radii for hexes.
    public const float outerRadius = 10f;
    public const float innerRadius = outerRadius * 0.866025404f;

    // Position of vectors for a hex relative to the hex's radii.
    public static Vector3[] corners =
    {
        new Vector3(0f, 0f, outerRadius),
        new Vector3(innerRadius, 0f, 0.5f * outerRadius),
        new Vector3(innerRadius, 0f, -0.5f * outerRadius),
        new Vector3(0f, 0f, -outerRadius),
        new Vector3(-innerRadius, 0f, -0.5f * outerRadius),
        new Vector3(-innerRadius, 0f, 0.5f * outerRadius),
        new Vector3(0f, 0f, outerRadius)
    };
}
