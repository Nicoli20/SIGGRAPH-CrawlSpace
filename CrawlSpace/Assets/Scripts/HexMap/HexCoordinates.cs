using UnityEngine;

// Defines and determines the coordinates of a hexcell.
[System.Serializable]
public struct HexCoordinates
{
    [SerializeField]
    private int x, z;

	public int X { get { return x; } }
    public int Z { get { return z; } }

    // Coordinates are defined by the x and z position of the grid.
    public HexCoordinates (int x, int z)
    {
        this.x = x;
        this.z = z;
    }

    // While z is easy to define, x is much different than in a square grid.
    public static HexCoordinates FromOffsetCoordinates (int x, int z)
    {
        return new HexCoordinates(x - z / 2, z);
    }

    // A third coordinate can be derived from two known coordinates.
    public int Y
    {
        get { return -X - Z; }
    }

    // A string representing the coordinates.
    public override string ToString()
    {
        return "(" + X.ToString() + ", " + Y.ToString() + ", " + Z.ToString() + ")";
    }

    // A segmented string representing the coordinates.
    public string ToStringOnSeparateLines()
    {
        return X.ToString() + "\n" + Y.ToString() + "\n" + Z.ToString();
    }

    // Coordinates derived from a hexcell's position.
    public static HexCoordinates FromPosition(Vector3 position)
    {
        float x = position.x / (HexMetrics.innerRadius * 2f);
        float y = -x;
        float offset = position.z / (HexMetrics.outerRadius * 3f);
        x -= offset;
        y -= offset;
        
        // Rounds the values.
        int iX = Mathf.RoundToInt(x);
        int iY = Mathf.RoundToInt(y);
        int iZ = Mathf.RoundToInt(-x -y);

        // The three coordinates should alaways add up to zero.
        if (iX + iY + iZ != 0)
        {          
            float dX = Mathf.Abs(x - iX);
            float dY = Mathf.Abs(y - iY);
            float dZ = Mathf.Abs(-x -y - iZ);

            if (dX > dY && dX > dZ)
            {
                iX = -iY - iZ;
            }
            else if (dZ > dY)
            {
                iZ = -iX - iY;
            }
        }

        return new HexCoordinates(iX, iZ);
    }
}
