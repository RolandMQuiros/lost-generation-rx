using System.Linq;
using System.Collections.Generic;

namespace LostGen.Board {

    public class _PawnMap {
        public Point CellSize { get; private set; }
        public IEnumerable<PawnState> Pawns {
            get { return _pawnsByID.Values; }
        }

        private Dictionary<AABB, HashSet<PawnState>> _pawnCells = new Dictionary<AABB, HashSet<PawnState>>();
        private Dictionary<long, PawnState> _pawnsByID = new Dictionary<long, PawnState>();

        public _PawnMap(Point cellSize, IEnumerable<PawnState> pawns) {
            CellSize = cellSize;
            foreach (PawnState pawn in pawns) {
                AddPawn(pawn);
            }
        }

        public PawnState GetPawn(long id) {
            PawnState pawn;
            _pawnsByID.TryGetValue(id, out pawn);
            return pawn;
        }

        public IEnumerable<PawnState> PawnsAt(Point point) {
            HashSet<PawnState> cell = GetCellAt(point);
            if (cell != null) {
                return cell;
            } else {
                return Enumerable.Empty<PawnState>();
            }
        }

        public HashSet<PawnState> GetCellAt(Point point) {
            foreach (var cell in _pawnCells) {
                if (cell.Key.Contains(point)) {
                    return cell.Value;
                }
            }
            return null;
        }

        public bool AddPawn(PawnState pawn) {
            if (_pawnsByID.ContainsKey(pawn.ID)) {
                return false;
            }
            _pawnsByID.Add(pawn.ID, pawn);

            HashSet<PawnState> cell = GetCellAt(pawn.Position);
            if (cell == null) {
                Point lower = new Point(
                    pawn.Position.X % CellSize.X,
                    pawn.Position.Y % CellSize.Y,
                    pawn.Position.Z % CellSize.Z
                );
                Point upper = lower + CellSize;
                AABB newCellBounds = new AABB(lower, upper);
                cell = new HashSet<PawnState>();
                _pawnCells.Add(newCellBounds, cell);
            }
            return cell.Add(pawn);
        }
    }
}