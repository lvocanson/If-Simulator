namespace Game.Level
{
    public class BaseRoom : Room
    {
        public override void InitializeRoom()
        {
            _roomType = RoomType.Base;
            _isCleared = true;
        }

        protected override void OnPlayerEnteredRoom()
        {
            
        }

        protected override void OnPlayerExitedRoom()
        {
            
        }

        protected override void OnPlayerClearedRoom()
        {
            
        }
    }
}