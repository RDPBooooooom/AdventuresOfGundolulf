using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Levels.Rooms;
using UnityEngine;
using Random = System.Random;

namespace Levels
{
    public class LevelGenerator
    {
        #region Fields

        private int _numberOfRoomsSqr;
        private readonly bool _drawDebug;

        #endregion

        #region Properties

        public int NumberOfRooms { get; set; }
        public int NumberOfTreasureRooms { get; set; }
        public int NumberOfShopRooms { get; set; }
        public int DistanceToBossRoom { get; set; }
        public List<Room> PossibleRooms { get; set; }

        #endregion

        #region Constructor

        public LevelGenerator(List<Room> possibleRooms, int numberOfRooms, int numberOfShopRooms = 1,
            int numberOfTreasureRooms = 1, int distanceToBossRoom = 4, bool drawDebug = false)
        {
            PossibleRooms = possibleRooms;
            NumberOfRooms = numberOfRooms;
            NumberOfTreasureRooms = numberOfTreasureRooms;
            NumberOfShopRooms = numberOfShopRooms;
            DistanceToBossRoom = distanceToBossRoom;
            _drawDebug = drawDebug;
        }

        public LevelGenerator(List<Room> possibleRooms, int numberOfRooms, bool drawDebug, int numberOfShopRooms = 1,
            int numberOfTreasureRooms = 1, int distanceToBossRoom = 4) : this(possibleRooms, numberOfRooms,
            numberOfShopRooms, numberOfTreasureRooms, distanceToBossRoom, drawDebug)
        {
        }

        #endregion

        public List<Room> GenerateLevel()
        {
            List<Room> rooms = new List<Room>();
            List<Cell> cells = DepthFirstSearch();

            foreach (Cell cell in cells)
            {
                try
                {
                    Room roomPrefab = GetRandomRoom<CombatRoom>(cell.DoorDirections);
                    rooms.Add(GameObject.Instantiate(roomPrefab, cell.worldPos, Quaternion.identity));
                    rooms.Last().name = cell.name;
                }
                catch (NoRoomFoundException _)
                {
                    Debug.LogWarning("No room was found for " + cell.DoorDirections + ". Skiping room");
                }
            }

            return rooms;
        }

        #region Layout generation

        private List<Cell> DepthFirstSearch()
        {
            List<Cell> cellGrid = new List<Cell>();
            _numberOfRoomsSqr = Mathf.CeilToInt(Mathf.Sqrt(NumberOfRooms));

            float worldPosZ = 0;

            for (int i = 0; i < _numberOfRoomsSqr; i++)
            {
                worldPosZ += 25.5f;
                float worldPosX = 0;

                for (int j = 0; j < _numberOfRoomsSqr; j++)
                {
                    worldPosX += 35;
                    cellGrid.Add(new Cell());
                    cellGrid.Last().worldPos = new Vector3(worldPosX, 0, worldPosZ);
                    cellGrid.Last().name = "Cell [" + i + "," + j + "]";
                }
            }

            if (_drawDebug)
            {
                for (int i = 0; i < cellGrid.Count; i++)
                {
                    DrawNeighbours(i, cellGrid);
                }
            }

            int currentCell = NumberOfRooms / 2;
            Stack<int> path = new Stack<int>();
            int visitedCells = 0;

            while (visitedCells <= NumberOfRooms)
            {
                Cell current = cellGrid[currentCell];
                current.Visited = true;
                current.Neighbours = GetNeighbours(currentCell, cellGrid);

                if (current.Neighbours.Count == 0)
                {
                    if (path.Count == 0) break;

                    currentCell = path.Pop();
                }
                else
                {
                    path.Push(currentCell);
                    visitedCells++;
                    
                    Random rand = new Random();

                    int newCell = current.Neighbours[rand.Next(0, current.Neighbours.Count - 1)];

                    if (newCell > currentCell)
                    {
                        // Is top or right
                        if (newCell - 1 == currentCell)
                        {
                            current.DoorDirections |= DoorDirections.right;
                            currentCell = newCell;
                            cellGrid[newCell].DoorDirections |= DoorDirections.left;

                            DrawDebugLine(current.worldPos, cellGrid[newCell].worldPos, Color.magenta);
                        }
                        else
                        {
                            current.DoorDirections |= DoorDirections.top;
                            currentCell = newCell;
                            cellGrid[newCell].DoorDirections |= DoorDirections.bottom;

                            DrawDebugLine(current.worldPos, cellGrid[newCell].worldPos, Color.magenta);
                        }
                    }
                    else
                    {
                        // Is down or left
                        if (newCell + 1 == currentCell)
                        {
                            current.DoorDirections |= DoorDirections.left;
                            currentCell = newCell;
                            cellGrid[newCell].DoorDirections |= DoorDirections.right;

                            DrawDebugLine(current.worldPos, cellGrid[newCell].worldPos, Color.magenta);
                        }
                        else
                        {
                            current.DoorDirections |= DoorDirections.bottom;
                            currentCell = newCell;
                            cellGrid[newCell].DoorDirections |= DoorDirections.top;

                            DrawDebugLine(current.worldPos, cellGrid[newCell].worldPos, Color.magenta);
                        }
                    }
                }
            }

            return cellGrid;
        }

        #endregion


        #region Debugs

        private void DrawNeighbours(int cell, List<Cell> cellGrid)
        {
            if (!_drawDebug) return;
            if (cell - _numberOfRoomsSqr >= 0)
            {
                Debug.DrawLine(cellGrid[cell].worldPos + new Vector3(2, 0, 0),
                    cellGrid[cell - _numberOfRoomsSqr].worldPos + new Vector3(2, 0, 0), Color.black,
                    1000000);
            }

            if (cell + _numberOfRoomsSqr < cellGrid.Count)
                Debug.DrawLine(cellGrid[cell].worldPos + new Vector3(-2, 0, 0),
                    cellGrid[cell + _numberOfRoomsSqr].worldPos + new Vector3(-2, 0, 0), Color.yellow,
                    1000000); //Bot
            if ((cell + 1) % _numberOfRoomsSqr != 0)
                Debug.DrawLine(cellGrid[cell].worldPos + new Vector3(0, 0, 2),
                    cellGrid[cell + 1].worldPos + new Vector3(0, 0, 2), Color.red,
                    1000000); // Right
            if (cell % _numberOfRoomsSqr != 0)
                Debug.DrawLine(cellGrid[cell].worldPos + new Vector3(0, 0, -2),
                    cellGrid[cell + -1].worldPos + new Vector3(0, 0, -2), Color.green,
                    1000000); // Left
        }

        private void DrawDebugLine(Vector3 start, Vector3 end, Color color)
        {
            Debug.DrawLine(start, end, color,
                1000000);
        }

        #endregion

        #region Helper Methods

        private T GetRandomRoom<T>(DoorDirections directions) where T : Room
        {
            List<T> randomRooms = new List<T>();
            randomRooms.AddRange(PossibleRooms.OfType<T>().Where(r => (r.Data.Directions == directions)));

            if (randomRooms.Count == 0) throw new NoRoomFoundException();

            Random rand = new Random();
            int roomIndex = rand.Next(0, randomRooms.Count - 1);
            return randomRooms[roomIndex];
        }

        private List<int> GetNeighbours(int cell, List<Cell> cellGrid)
        {
            List<int> neighbours = new List<int>();

            if (cell - _numberOfRoomsSqr >= 0 && !cellGrid[cell - _numberOfRoomsSqr].Visited)
                neighbours.Add(cell - _numberOfRoomsSqr); // Top
            if (cell + _numberOfRoomsSqr < cellGrid.Count && !cellGrid[cell + _numberOfRoomsSqr].Visited)
                neighbours.Add(cell + _numberOfRoomsSqr); //Bot
            if ((cell + 1) % _numberOfRoomsSqr != 0 && !cellGrid[cell + 1].Visited) neighbours.Add(cell + 1); // Right
            if (cell % _numberOfRoomsSqr != 0 && !cellGrid[cell - 1].Visited) neighbours.Add(cell - 1); // Left

            return neighbours;
        }

        #endregion
    }

    #region Helper Classes

    internal class Cell
    {
        public String name;
        public bool Visited = false;
        public DoorDirections DoorDirections;
        public Vector3 worldPos;
        public List<int> Neighbours;
    }

    public class NoRoomFoundException : Exception
    {
    }

    #endregion
}