using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdajovkySem1
{
    public class GPSPosition : IComparable
    {
        private readonly char _width;
        public double WidthPosition { get; set; }
        private readonly char _length;
        public double LengthPosition { get; set; }
        public PlotOfLand PlotOfLand { get; set; }
        public RealEstate RealEstate { get; set; }
        private const double Tolerance = 1e-5;
        public int UniqueId { get; set; }

        public GPSPosition(char width, char length, double widthPosition, double lengthPosition, PlotOfLand plotOfLand, RealEstate realEstate, int uniqueId)
        {
            _width = width;
            _length = length;
            WidthPosition = widthPosition;
            LengthPosition = lengthPosition;
            ChangeSigns();
            PlotOfLand = plotOfLand;
            RealEstate = realEstate;
            UniqueId = uniqueId;
        }

        public int CompareTo(object obj, int depth)
        {
            GPSPosition other = obj as GPSPosition;
            if (other == null)
            {
                throw new ArgumentException("Object is not a GPSPosition");
            }

            int compareValue = depth % GetDimension();

            if (compareValue == 0)
            {
                if (Math.Abs(Math.Abs(WidthPosition) - Math.Abs(other.WidthPosition)) < Tolerance)
                {
                    return 0;
                }
                return WidthPosition < other.WidthPosition ? -1 : 1;
            }
            else
            {
                if (Math.Abs(Math.Abs(LengthPosition) - Math.Abs(other.LengthPosition)) < Tolerance)
                {
                    return 0;
                }
                return LengthPosition < other.LengthPosition ? -1 : 1;
            }
        }

        public override bool Equals(object obj)
        {
            GPSPosition other = obj as GPSPosition;
            if (other == null)
            {
                throw new ArgumentException("Object is not a GPSPosition");
            }

            return Math.Abs(Math.Abs(WidthPosition) - Math.Abs(other.WidthPosition)) < Tolerance &&
                   Math.Abs(Math.Abs(LengthPosition) - Math.Abs(other.LengthPosition)) < Tolerance;
        }

        private void ChangeSigns()
        {
            if (_width == 'S')
            {
                WidthPosition = -WidthPosition;
            }
            if (_length == 'W')
            {
                LengthPosition = -LengthPosition;
            }
        }

        public override string ToString()
        {
            if (PlotOfLand != null)
            {
                return $"{PlotOfLand}, GPS Positions:({WidthPosition} {_width}; {LengthPosition} {_length})";
            }
            else
            {
                return $"{RealEstate}, GPS Positions:({WidthPosition} {_width}; {LengthPosition} {_length})";
            }
        }

        public bool SpecificEquals(object obj)
        {
            GPSPosition other = obj as GPSPosition;
            if (other == null)
            {
                throw new ArgumentException("Object is not a GPSPosition");
            }
            if (UniqueId == other.UniqueId)
            {
                return true;
            }
            return false;
        }

        public int GetDimension()
        {
            return 2;
        }

        public object GetUniqueId()
        {
            return UniqueId;
        }
    }
}
