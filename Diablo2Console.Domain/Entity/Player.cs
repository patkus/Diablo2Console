using Diablo2Console.Domain.Common;

namespace Diablo2Console.Domain.Entity
{
    public class Player : BaseEntity
    {
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public char[,] PlayerMap { get; set; }
    }
}
