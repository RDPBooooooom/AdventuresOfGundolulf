using System;
using System.Collections.Generic;
using System.Linq;
using Levels.Rooms;
using UnityEngine;
using Utils;
using Object = UnityEngine.Object;
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
            
           

            Dictionary<Cell, CellTypes> roomDefinitions = GetRoomDefinitions(cells);

            GameObject levelParent = new GameObject("Level");

            foreach (KeyValuePair<Cell, CellTypes> kvp in roomDefinitions)
            {
                Cell cell = kvp.Key;
                try
                {
                    Room roomPrefab = null;

                    switch (kvp.Value)
                    {
                        case CellTypes.Boss:
                            roomPrefab = GetRandomRoom<BossRoom>(cell.DoorDirections);
                            break;
                        case CellTypes.Combat:
                            roomPrefab = GetRandomRoom<CombatRoom>(cell.DoorDirections);
                            break;
                        case CellTypes.Shop:
                            roomPrefab = GetRandomRoom<ShopRoom>(cell.DoorDirections);
                            break;
                        case CellTypes.Treasure:
                            roomPrefab = GetRandomRoom<TreasureRoom>(cell.DoorDirections);
                            break;
                        case CellTypes.Start:
                            roomPrefab = GetRandomRoom<StartRoom>(cell.DoorDirections);
                            break;
                        default:
                            Debug.LogWarning("No type found for CellType: " + kvp.Value);
                            break;
                    }

                    if (roomPrefab == null) throw new NoRoomFoundException();

                    Room room = Object.Instantiate(roomPrefab, cell.WorldPos, Quaternion.identity,
                        levelParent.transform);
                    room.name = cell.Name;
                    rooms.Add(room);
                }
                catch (NoRoomFoundException)
                {
                    Debug.LogWarning("No room was found for " + cell.DoorDirections + ". Skipping room");
                }
            }

            return SetRoomConnections(rooms);
        }

        private List<Room> SetRoomConnections(List<Room> rooms)
        {
            // Set Connected Rooms
            for (int i = 0; i < rooms.Count; i++)
            {
                Room room = rooms[i];

                RoomConnections connections = room.RoomConnections;

                if ((room.Data.Directions & DoorDirections.Bottom) != 0)
                    connections.Bottom = rooms[i - _numberOfRoomsSqr];
                if ((room.Data.Directions & DoorDirections.Top) != 0)
                    connections.Top = rooms[i + _numberOfRoomsSqr];
                if ((room.Data.Directions & DoorDirections.Left) != 0)
                    connections.Left = rooms[i - 1];
                if ((room.Data.Directions & DoorDirections.Right) != 0)
                    connections.Right = rooms[i + 1];


                room.RoomConnections = connections;
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
                    worldPosX += 35.5f;
                    cellGrid.Add(new Cell());
                    cellGrid.Last().WorldPos = new Vector3(worldPosX, 0, worldPosZ);
                    cellGrid.Last().Name = "Cell [" + i + "," + j + "]";
                    cellGrid.Last().CellId = cellGrid.Count - 1;
                }
            }
           
            if (_drawDebug)
            {
                for (int i = 0; i < cellGrid.Count; i++)
                {
                    DrawNeighbours(i, cellGrid);
                }
            }

            Cell currentCell = cellGrid[NumberOfRooms / 2];
            Stack<Cell> path = new Stack<Cell>();
            int visitedCells = 0;

            while (visitedCells <= NumberOfRooms)
            {
                currentCell.Visited = true;
                currentCell.Neighbours = GetNeighbours(currentCell, cellGrid);

                if (currentCell.Neighbours.Count == 0)
                {
                    if (path.Count == 0) break;

                    currentCell = path.Pop();
                }
                else
                {
                    path.Push(currentCell);
                    visitedCells++;

                    Random rand = new Random();
                    
                    Cell newCell = currentCell.Neighbours[rand.Next(0, currentCell.Neighbours.Count - 1)];
                    
                    if (newCell.CellId > currentCell.CellId)
                    {
                        // Is top or right
                        if (newCell.CellId - 1 == currentCell.CellId)
                        {
                            currentCell.DoorDirections |= DoorDirections.Right;

                            DrawDebugLine(currentCell.WorldPos, newCell.WorldPos, Color.magenta);

                            currentCell = newCell;
                            currentCell.DoorDirections |= DoorDirections.Left;
                        }
                        else
                        {
                            currentCell.DoorDirections |= DoorDirections.Top;

                            DrawDebugLine(currentCell.WorldPos, newCell.WorldPos, Color.magenta);

                            currentCell = newCell;
                            currentCell.DoorDirections |= DoorDirections.Bottom;
                        }
                    }
                    else
                    {
                        // Is down or left
                        if (newCell.CellId + 1 == currentCell.CellId)
                        {
                            currentCell.DoorDirections |= DoorDirections.Left;

                            DrawDebugLine(currentCell.WorldPos, newCell.WorldPos, Color.magenta);

                            currentCell = newCell;
                            currentCell.DoorDirections |= DoorDirections.Right;
                        }
                        else
                        {
                            currentCell.DoorDirections |= DoorDirections.Bottom;

                            DrawDebugLine(currentCell.WorldPos, newCell.WorldPos, Color.magenta);

                            currentCell = newCell;
                            currentCell.DoorDirections |= DoorDirections.Top;
                        }
                    }
                }
            }
            cellGrid.ForEach(cell => cell.Neighbours = LoadNeighbours(cell, cellGrid));
            return cellGrid;
        }

        #endregion

        #region Room definitions

        private Dictionary<Cell, CellTypes> GetRoomDefinitions(List<Cell> cells)
        {
            Dictionary<Cell, CellTypes> roomDefinitions = new Dictionary<Cell, CellTypes>();

            foreach (Cell cell in cells)
            {
                roomDefinitions.Add(cell, CellTypes.Combat);
            }

            // Place Boss Room
            PlaceBossRoom(roomDefinitions);
            //Place Treasure room
            PlaceTreasureRoom(roomDefinitions);
            //Place Shop room
            PlaceShopRoom(roomDefinitions);
            //Place Start room
            PlaceStartRoom(roomDefinitions);

            return roomDefinitions;
        }

        #region Room placement

        private void PlaceBossRoom(Dictionary<Cell, CellTypes> roomDefinitions)
        {
            List<Cell> possibleCells = new List<Cell>();

            // Am Anfang sind alle Räume Combat Räume. Nur diese dürfen in andere Räume umgewandelt werden. Der Bossraum darf nur an einem Ende des Levels spawnen. Also nur 1 Direction.
            foreach (KeyValuePair<Cell, CellTypes> kvp in roomDefinitions.Where(kvp =>
                         kvp.Value == CellTypes.Combat && kvp.Key.GetNumberOfDirections() == 1))
            {
                possibleCells.Add(kvp.Key);
            }

            Cell cell = ListUtils.GetRandomElement(possibleCells);
            roomDefinitions[cell] = CellTypes.Boss;
        }

        private void PlaceTreasureRoom(Dictionary<Cell, CellTypes> roomDefinitions)
        {
            List<Cell> possibleCells = new List<Cell>();

            // Am Anfang sind alle Räume Combat Räume. Nur diese dürfen in andere Räume umgewandelt werden.
            foreach (KeyValuePair<Cell, CellTypes> kvp in roomDefinitions.Where(kvp => kvp.Value == CellTypes.Combat))
            {
                possibleCells.Add(kvp.Key);
            }

            List<Cell> cells = ListUtils.GetRandomElements(possibleCells, NumberOfTreasureRooms);

            cells.ForEach(cell => roomDefinitions[cell] = CellTypes.Treasure);
        }

        private void PlaceShopRoom(Dictionary<Cell, CellTypes> roomDefinitions)
        {
            List<Cell> possibleCells = new List<Cell>();

            // Am Anfang sind alle Räume Combat Räume. Nur diese dürfen in andere Räume umgewandelt werden.
            foreach (KeyValuePair<Cell, CellTypes> kvp in roomDefinitions.Where(kvp => kvp.Value == CellTypes.Combat))
            {
                possibleCells.Add(kvp.Key);
            }

            List<Cell> cells = ListUtils.GetRandomElements(possibleCells, NumberOfShopRooms);

            cells.ForEach(cell => roomDefinitions[cell] = CellTypes.Shop);
        }

        private void PlaceStartRoom(Dictionary<Cell, CellTypes> roomDefinitions)
        {
            List<Cell> possibleCells = new List<Cell>();

            Cell bossRoom = roomDefinitions.First(kvp => kvp.Value == CellTypes.Boss).Key;

            // Am Anfang sind alle Räume Combat Räume. Nur diese dürfen in andere Räume umgewandelt werden.
            foreach (KeyValuePair<Cell, CellTypes> kvp in roomDefinitions.Where(kvp => kvp.Value == CellTypes.Combat))
            {
                List<Cell> pathToBoss = AStar.FindPath(kvp.Key, bossRoom);

                if(pathToBoss == null) continue;
                
                // Wenn Pfad - 2 (Boss und Start Raum ausgeschlossen) grösser als DistanceToBossRoom sind kann die Cell als Start verwendet werden
                if (pathToBoss.Count - 2 > DistanceToBossRoom) possibleCells.Add(kvp.Key);
            }

            Cell cell = ListUtils.GetRandomElement(possibleCells);

            if (cell == null) return;
            
            roomDefinitions[cell] = CellTypes.Start;
        }

        #endregion

        #endregion

        #region Debugs

        private void DrawNeighbours(int cell, List<Cell> cellGrid)
        {
            if (!_drawDebug) return;
            if (cell - _numberOfRoomsSqr >= 0)
            {
                Debug.DrawLine(cellGrid[cell].WorldPos + new Vector3(2, 0, 0),
                    cellGrid[cell - _numberOfRoomsSqr].WorldPos + new Vector3(2, 0, 0), Color.black,
                    1000000);
            }

            if (cell + _numberOfRoomsSqr < cellGrid.Count)
                Debug.DrawLine(cellGrid[cell].WorldPos + new Vector3(-2, 0, 0),
                    cellGrid[cell + _numberOfRoomsSqr].WorldPos + new Vector3(-2, 0, 0), Color.yellow,
                    1000000); //Bot
            if ((cell + 1) % _numberOfRoomsSqr != 0)
                Debug.DrawLine(cellGrid[cell].WorldPos + new Vector3(0, 0, 2),
                    cellGrid[cell + 1].WorldPos + new Vector3(0, 0, 2), Color.red,
                    1000000); // Right
            if (cell % _numberOfRoomsSqr != 0)
                Debug.DrawLine(cellGrid[cell].WorldPos + new Vector3(0, 0, -2),
                    cellGrid[cell + -1].WorldPos + new Vector3(0, 0, -2), Color.green,
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

        private List<Cell> GetNeighbours(Cell cell, List<Cell> cellGrid)
        {
            List<Cell> neighbours = new List<Cell>();

            if (cell.CellId - _numberOfRoomsSqr >= 0 && !cellGrid[cell.CellId - _numberOfRoomsSqr].Visited)
                neighbours.Add(cellGrid[cell.CellId - _numberOfRoomsSqr]); // Bot
            if (cell.CellId + _numberOfRoomsSqr < cellGrid.Count && !cellGrid[cell.CellId + _numberOfRoomsSqr].Visited)
                neighbours.Add(cellGrid[cell.CellId + _numberOfRoomsSqr]); //Top
            if ((cell.CellId + 1) % _numberOfRoomsSqr != 0 && !cellGrid[cell.CellId + 1].Visited)
                neighbours.Add(cellGrid[cell.CellId + 1]); // Right
            if (cell.CellId % _numberOfRoomsSqr != 0 && !cellGrid[cell.CellId - 1].Visited)
                neighbours.Add(cellGrid[cell.CellId - 1]); // Left

            return neighbours;
        }

        private List<Cell> LoadNeighbours(Cell cell, List<Cell> cellGrid)
        {
            List<Cell> neighbours = new List<Cell>();

            if ((cell.DoorDirections & DoorDirections.Bottom) != 0)
                neighbours.Add(cellGrid[cell.CellId - _numberOfRoomsSqr]); // Bot
            if ((cell.DoorDirections & DoorDirections.Top) != 0)
                neighbours.Add(cellGrid[cell.CellId + _numberOfRoomsSqr]); //Top
            if ((cell.DoorDirections & DoorDirections.Right) != 0)
                neighbours.Add(cellGrid[cell.CellId + 1]); // Right
            if ((cell.DoorDirections & DoorDirections.Left) != 0)
                neighbours.Add(cellGrid[cell.CellId - 1]); // Left

            return neighbours;
        }

        #endregion
    }

    #region Helper Classes

    internal class Cell : IComparable<Cell>
    {
        public String Name;
        public int CellId;
        public bool Visited;
        public DoorDirections DoorDirections;
        public Vector3 WorldPos;
        public List<Cell> Neighbours;
        public float Weight;

        public int GetNumberOfDirections()
        {
            int numberOfDirections = 0;
            if ((DoorDirections & DoorDirections.Bottom) != 0) numberOfDirections++;
            if ((DoorDirections & DoorDirections.Top) != 0) numberOfDirections++;
            if ((DoorDirections & DoorDirections.Left) != 0) numberOfDirections++;
            if ((DoorDirections & DoorDirections.Right) != 0) numberOfDirections++;

            return numberOfDirections;
        }

        public int CompareTo(Cell other)
        {
            if (Weight > other.Weight) return 1;
            if (Weight < other.Weight) return -1;
            return 0;
        }
    }


    internal static class AStar
    {
        public static List<Cell> FindPath(Cell start, Cell end)
        {
            PriorityQueue<Cell> priorityQueue = new PriorityQueue<Cell>();
            Dictionary<Cell, Cell> directionMap = new Dictionary<Cell, Cell>();

            priorityQueue.Enqueue(start);
            directionMap.Add(start, null);

            while (priorityQueue.Count > 0)
            {
                Cell currentCell = priorityQueue.Dequeue();

                Debug.Log(currentCell.Name + " Neighbours: " + currentCell.Neighbours.Count);
                
                foreach (Cell neighbour in currentCell.Neighbours.Where(neighbour => !directionMap.ContainsKey(neighbour)))
                {
                    // Cost & Weight is always 1 since there is no difficulty in passing Terrain and all Nodes have the same distance between each other
                    
                    neighbour.Weight = 1 + Vector3.Distance(neighbour.WorldPos, end.WorldPos);;
                    directionMap.Add(neighbour, currentCell);
                    priorityQueue.Enqueue(neighbour);
                }
            }

            if (directionMap.Count <= 1 || !directionMap.TryGetValue(end, out Cell value))
            {
                Debug.LogError("Unable to resolve Path");
                return null;
            }

            Cell nextCell = end;
            List<Cell> path = new List<Cell>();
            while (nextCell != null)
            {
                path.Add(nextCell);
                if (!directionMap.TryGetValue(nextCell, out nextCell))
                {
                    Debug.LogError("Well.. Shit");
                }
            }

            path.Reverse();
            return path;
        }
    }

    internal enum CellTypes
    {
        Boss,
        Combat,
        Shop,
        Treasure,
        Start
    }

    public class NoRoomFoundException : Exception
    {
    }

    #endregion
}