using UnityEngine;

// Represents a cell, which has coordinates, a color, and up to six neighbors.
public class HexCell : MonoBehaviour
{
    public HexCoordinates coordinates; // The cell's position.
    public Color color; // The cell's color.

    [SerializeField]
    HexCell[] neighbors; // The cell's neighbors.

    // Returns a cell's neighbor.
    public HexCell GetNeighbor(HexDirection direction)
    {
        return neighbors[(int)direction];
    }

    // Sets a cell's neighbor.
    public void SetNeighbor(HexDirection direction, HexCell cell)
    {
        neighbors[(int)direction] = cell;
        cell.neighbors[(int)direction.Opposite()] = this;
    }
}
