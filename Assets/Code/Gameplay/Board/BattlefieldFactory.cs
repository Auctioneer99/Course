using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    
    public static class BattlefieldFactory
    {
        public static BattlefieldSettings DefaultCreate(int players)
        {
            switch (players)
            {
                case 2:
                    case 0:
                    case 1:
                    return DefaultTwoPlayers();
                default:
                    throw new Exception("Wrong players count");
            }
        }

        private static BattlefieldSettings DefaultTwoPlayers()
        {
            int radius = 7;

            List<TileDefinition> definitions = CreateRadial(radius);

            Dictionary<TileDefinition, Dictionary<TileDefinition, bool>> adjencencyMatrix
                = GetAdjencencyMatrix(definitions);
            /*
            foreach(var i in adjencencyMatrix)
            {
                UnityEngine.Debug.Log(string.Join(", ", i.Value.Values));
            }*/

            //influence

            //for 1 player

            TileDefinition[] influencePlayer1 = definitions.Where(tile => tile.X >= 2).ToArray();

            TileDefinition[] influencePlayer2 = definitions.Where(tile => tile.X <= -2).ToArray();

            TileDefinition[] influenceNeutrals = definitions.Except(influencePlayer1).Except(influencePlayer2).ToArray();


            BattlefieldPlayerSettings player1 = new BattlefieldPlayerSettings(EPlayer.Player1, influencePlayer1, -1);
            BattlefieldPlayerSettings player2 = new BattlefieldPlayerSettings(EPlayer.Player2, influencePlayer2, -1);
            BattlefieldPlayerSettings neutral = new BattlefieldPlayerSettings(EPlayer.Neutral, influenceNeutrals, -1);

            BattlefieldPlayerSettings[] pSets = { player1, player2, neutral };

            BattlefieldSettings settings = new BattlefieldSettings(pSets, adjencencyMatrix);

            return settings;
        }

        private static Vector3[] directions = new Vector3[6]
        {
            new Vector3(1, -1, 0),
            new Vector3(1, 0, -1),
            new Vector3(0, 1, -1),
            new Vector3(-1, 1, 0),
            new Vector3(-1, 0, 1),
            new Vector3(0, -1, 1),
        };

        private static Dictionary<TileDefinition, Dictionary<TileDefinition, bool>> GetAdjencencyMatrix(List<TileDefinition> tiles)
        {
            int tilesCount = tiles.Count;
            Dictionary<TileDefinition, Dictionary<TileDefinition, bool>> adjencencyMatrix = 
                new Dictionary<TileDefinition, Dictionary<TileDefinition, bool>>(tilesCount);

            foreach(var tile1 in tiles)
            {
                adjencencyMatrix[tile1] = new Dictionary<TileDefinition, bool>(tilesCount);
                Vector3 tile1Pos = new Vector3(tile1.X, tile1.Y, -tile1.X - tile1.Y);

                foreach (var tile2 in tiles)
                {
                    Vector3 tile2Pos = new Vector3(tile2.X, tile2.Y, -tile2.X - tile2.Y);

                    adjencencyMatrix[tile1][tile2] = directions
                        .Select(dir => dir + tile2Pos)
                        .Any(tile => tile.Equals(tile1Pos));
                }
            }

            return adjencencyMatrix;
        }

        private static List<TileDefinition> CreateRadial(int radius)
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

        private static List<TileDefinition> ConvertRawCoordinates(IEnumerable<Vector3> rawCoordinates)
        {
            List<TileDefinition> field = new List<TileDefinition>();

            foreach (var coord in rawCoordinates)
            {
                field.Add(new TileDefinition((short)coord.X, (short)coord.Y));
            }
            return field;
        }

        public static IEnumerable<Vector3> GetConnections(Vector3 position, IEnumerable<Vector3> rawCoordinates)
        {
            return rawCoordinates.Intersect(directions.Select(direction => direction + position));
        }
    }
}
