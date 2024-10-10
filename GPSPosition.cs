using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdajovkySem1
{
    internal class GPSPosition : IComparable
    {
        private char _width;
        private double _widthPosition;
        private char _length;
        private double _lengthPosition;

        public GPSPosition(char width, double widthPosition, char length, double lengthPosition)
        {
            _width = width;
            _widthPosition = widthPosition;
            _length = length;
            _lengthPosition = lengthPosition;
        }

        public int CompareTo(object obj, int depth)
        {
            GPSPosition other = obj as GPSPosition;
            if (other == null)
            {
                throw new ArgumentException("Object is not a GPSPosition");
            }

            if (other._widthPosition < _widthPosition)
            {
                return -1;
            }
            if (other._widthPosition > _widthPosition)
            {
                return 1;
            }
            return 0;
        }

        public bool Equals(object obj)
        {
            GPSPosition other = obj as GPSPosition;
            if (other == null)
            {
                throw new ArgumentException("Object is not a GPSPosition");
            }

            return _width == other._width && _widthPosition == other._widthPosition &&
                   _length == other._length && _lengthPosition == other._lengthPosition;

        }

        public override string ToString()
        {
            return $"{_width} {_widthPosition} {_length} {_lengthPosition}";
        }
    }
}
