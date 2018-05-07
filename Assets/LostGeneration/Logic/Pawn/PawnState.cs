using System.Linq;
using System.Collections.Generic;

namespace LostGen {
    public class PawnState {
        public long ID;
        public string Name;
        public Point Position { get; set; }
        public IEnumerable<Point> Footprint {
            get {
                return _footprint;
            }
            set {
                _footprint = value.ToArray();
            }
        }
        public Dictionary<string, object> ComponentStates;
        private Point[] _footprint;
    }
}