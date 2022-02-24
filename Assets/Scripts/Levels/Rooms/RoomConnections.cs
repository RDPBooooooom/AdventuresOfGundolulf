namespace Levels.Rooms
{
    public struct RoomConnections
    {
        public Room Left { get; set; }
        public Room Right { get; set; }
        public Room Bottom { get; set; }
        public Room Top { get; set; }
    }
}