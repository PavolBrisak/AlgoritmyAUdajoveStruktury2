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
        private char _width;
        private double _widthPosition;
        private char _length;
        private double _lengthPosition;
        public PlotOfLand PlotOfLand { get; set; }
        public RealEstate RealEstate { get; set; }

        public GPSPosition(char width, char length, double widthPosition, double lengthPosition, PlotOfLand plotOfLand, RealEstate realEstate)
        {
            _width = width;
            _length = length;
            _widthPosition = widthPosition;
            _lengthPosition = lengthPosition;
            ChangeSigns();
            PlotOfLand = plotOfLand;
            RealEstate = realEstate;
        }

        public int CompareTo(object obj, int depth)
        {
            GPSPosition other = obj as GPSPosition;
            if (other == null)
            {
                throw new ArgumentException("Object is not a GPSPosition");
            }

            int compareValue = depth % 2;
            if (compareValue == 0)
            {
                if (_widthPosition < other._widthPosition)
                {
                    return -1;
                }
                if (_widthPosition > other._widthPosition)
                {
                    return 1;
                }
                return 0;
            }
            else
            {
                if (_lengthPosition < other._lengthPosition)
                {
                    return -1;
                }
                if (_lengthPosition > other._lengthPosition)
                {
                    return 1;
                }
                return 0;
            }
        }

        public bool Equals(object obj)
        {
            GPSPosition other = obj as GPSPosition;
            if (other == null)
            {
                throw new ArgumentException("Object is not a GPSPosition");
            }

            return _widthPosition == other._widthPosition &&
                   _lengthPosition == other._lengthPosition;

        }

        private void ChangeSigns()
        {
            if (_width == 'S')
            {
                _widthPosition = -_widthPosition;
            }
            if (_length == 'W')
            {
                _lengthPosition = -_lengthPosition;
            }
        }

        public override string ToString()
        {
            if (PlotOfLand != null)
            {
                return $"Plot of land number: {PlotOfLand.Number}, description: {PlotOfLand.Description}:\n" +
                       $" ({_widthPosition},{_width}), ({_lengthPosition}, {_length})";
            }
            else
            {
                return $"Real estate number: {RealEstate.Number}, description: {RealEstate.Description}:\n" +
                       $" ({_widthPosition},{_width}), ({_lengthPosition}, {_length})";
            }
        }
    }
}
