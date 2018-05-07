using System.Collections.Generic;

namespace LostGen.Board.Actions {
    public class AddPawns {
        public IEnumerable<PawnState> Pawns;
    }

    public class RemovePawns {
        public IEnumerable<long> IDs;
    }

    public class MovePawn {
        public long ID;
    }

    public class SetBlocks {
        public Dictionary<Point, Block> Blocks;
    }

    public class Step { }
}