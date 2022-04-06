namespace Levels.Rooms
{
    public class StartRoom : Room
    {
        public override void Enter()
        {
            base.Enter();

            if (WasVisited) return;
            
            OnRoomCleared();
        }
    }
}