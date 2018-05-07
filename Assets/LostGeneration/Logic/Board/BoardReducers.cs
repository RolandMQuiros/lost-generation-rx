using System;
using System.Linq;
using System.Collections.Generic;
using LostGen;

namespace LostGen.Board {
    public static class Reducers {
        public static BoardState BoardStateReducer(BoardState previousState, object action) {
            return new BoardState() {
                Blocks = BlocksReducer(previousState.Blocks, action),
                Pawns = PawnsReducer(previousState.Pawns, action)
            };
        }

        #region Blocks
        private static Block[,,] BlocksReducer(Block[,,] previousState, object action) {
            Block[,,] nextState = previousState;
            if (action is Actions.SetBlocks) {
                nextState = SetBlocks(previousState, (Actions.SetBlocks)action);
            }
            return nextState;
        }

        private static Block[,,] SetBlocks(Block[,,] previousState, Actions.SetBlocks action) {
            Block[,,] nextState = previousState;
            if (action.Blocks.Any()) {
                // If any of the new blocks lie outside the current bounds of the array, resize the array
                Point size = new Point(nextState.GetLength(0), nextState.GetLength(1), nextState.GetLength(2));
                Point newSize = Point.UpperBound(Point.UpperBound(action.Blocks.Keys), size);
                nextState = new Block[newSize.X, newSize.Y, newSize.Z];
                Array.Copy(previousState, nextState, previousState.Length);

                // Assign the new blocks
                foreach (var pair in action.Blocks) {
                    nextState[pair.Key.X, pair.Key.Y, pair.Key.Z] = pair.Value;
                }
            }
            return nextState;
        }
        #endregion

        #region Pawns
        private static Dictionary<long, PawnState> PawnsReducer(Dictionary<long, PawnState> previousState, object action) {
            Dictionary<long, PawnState> nextState = previousState;
            if (action is Actions.AddPawns) {
                nextState = AddPawns(previousState, (Actions.AddPawns)action);
            }
            return nextState;
        }

        private static Dictionary<long, PawnState> AddPawns(Dictionary<long, PawnState> previousState, Actions.AddPawns action) {
            Dictionary<long, PawnState> nextState = previousState.Union(
                action.Pawns.Select(p => new KeyValuePair<long, PawnState>(p.ID, p))
            ).ToDictionary(pair => pair.Key, pair => pair.Value);
            return nextState;
        }
        #endregion
    }
}