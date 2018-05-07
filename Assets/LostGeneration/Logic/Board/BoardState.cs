using System.Collections.Generic;

namespace LostGen {
    public class BoardState {
        public Board.Block[,,] Blocks;
        public Point BucketSize = Point.One;
        public Dictionary<long, PawnState> Pawns;
    }
}