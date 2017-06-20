using System;
using System.Collections.Generic;

namespace Battleship
{

    public class Location
    {
        public int Row { get; set; }
        public int Column { get; set; }

        protected bool Equals(Location other)
        {
            return Row == other.Row && Column == other.Column;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Location) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Row * 397) ^ Column;
            }
        }
    }

    public class ShipLocation
    {

        public Location Start { get; set; }
        public Location End { get; set; }

        public bool Valid()
        {
            if (Start.Column != End.Column && Start.Row != End.Row)
            {
                return false;
            }
            if (Start.Column == End.Column)
            {
                if (Start.Row > End.Row)
                {
                    return Start.Row - End.Row == 2;
                }
                if (Start.Row < End.Row)
                {
                    return End.Row - Start.Row == 2;
                }
                return false;
            }
            if (Start.Row == End.Row)
            {
                if (Start.Column > End.Column)
                {
                    return Math.Abs(Start.Column - End.Column) == 2;
                }
                if (Start.Column < End.Column)
                {
                    return Math.Abs(Start.Column - End.Column) == 2;
                }
                return false;
            }
            return false;
        }

        public List<Location> GetLocations()
        {
            if (!Valid())
            {
                throw new Exception("Invalid Ship, cannot compute locations");
            }
            var isVertical = Start.Column == End.Column;
            var locations = new List<Location>();
            if (isVertical)
            {
                locations.Add(Start);
                {
                    int row;
                    if (Start.Row < End.Row)
                    {
                        row = End.Row - 1;
                    }
                    else
                    {
                        row = Start.Row - 1;
                    }
                    locations.Add(new Location
                    {
                        Column = Start.Column,
                        Row = row
                    });
                }
                locations.Add(End);
            }
            else
            {
                locations.Add(Start);
                {
                    int column;
                    if (Start.Column < End.Column)
                    {
                        column = End.Column - 1;
                    }
                    else
                    {
                        column = Start.Column - 1;
                    }
                    locations.Add(new Location
                    {
                        Column = column,
                        Row = Start.Row
                    });
                    locations.Add(End);
                }
            }
            return locations;
        }
    }
}