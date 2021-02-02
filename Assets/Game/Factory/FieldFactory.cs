using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

public static class FieldFactory
{
    private static Vector3[] directions = new Vector3[6]
    {
            new Vector3(1, -1, 0),
            new Vector3(1, 0, -1),
            new Vector3(0, 1, -1),
            new Vector3(-1, 1, 0),
            new Vector3(-1, 0, 1),
            new Vector3(0, -1, 1),
    };

    public static IEnumerable<Tile> SimpleField1()
    {
        return RadialCreation(1);
    }

    public static IEnumerable<Tile> SimpleField2()
    {
        return RadialCreation(2);
    }

    public static IEnumerable<Tile> SimpleField3()
    {
        return RadialCreation(3);
    }

    public static IEnumerable<Tile> CustomField3()
    {
        IEnumerable<Vector3> exclude = new[]{
            new Vector3(1, 0, -1),
            new Vector3(-1, 1, 0),
            new Vector3(0, -1, 1),
        };
        IEnumerable<Tile> field = RadialCreation(3);
        field = field.Where(t => exclude.Contains(t.Position) == false).ToList();
        return field;
    }

    private static IEnumerable<Tile> RadialCreation(int radius)
    {
        Vector3 origin = new Vector3(0, 0, 0);

        IEnumerable<Vector3> rawCoordinates = RadialRucursion(origin, radius);
        IEnumerable<Vector3> RadialRucursion(Vector3 initialPosition, int recurs)
        {
            List<Vector3> result = new List<Vector3>();

            if (recurs == 0)
            {
                return result;
            }
            result.Add(initialPosition);

            foreach (var direction in directions)
            {
                IEnumerable<Vector3> neighbours = RadialRucursion(initialPosition + direction, recurs - 1);
                result.AddRange(neighbours);
            }
            return result.Distinct();
        }
        return ConvertRawCoordinates(rawCoordinates);
    }

    private static IEnumerable<Tile> ConvertRawCoordinates(IEnumerable<Vector3> rawCoordinates)
    {
        List<Tile> field = new List<Tile>();

        foreach (var coord in rawCoordinates)
        {
            field.Add(new Tile(coord, 
                field.Where(tile => directions.Select(direction => direction + coord).Contains(tile.Position))));
        }
        return field;
    }
}
