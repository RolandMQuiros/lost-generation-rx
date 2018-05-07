using System;

namespace LostGen {
    public struct AABB : IEquatable<AABB>{
        public Point Lower;
        public Point Upper;

        public AABB(Point lower, Point upper) {
            Lower = lower;
            Upper = upper;
        }

        public bool Contains(Point point) {
            return Point.WithinBox(point, Lower, Upper);
        }

        public bool Equals(AABB other) {
            return Lower == other.Lower &&
                   Upper == other.Upper;
        }

        public override bool Equals(object obj) {
            return Equals((AABB)obj);
        }

        public override int GetHashCode() {
            return base.GetHashCode();
        }

        public static bool operator ==(AABB b1, AABB b2) {
            return b1.Equals(b2);
        }

        public static bool operator !=(AABB b1, AABB b2) {
            return !b1.Equals(b2);
        }
    }
}