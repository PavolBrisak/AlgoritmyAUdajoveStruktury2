using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdajovkySem1
{
    internal class PlotOfLand : IComparable
    {
        private int _number;
        private string _description;
        private List<RealEstate> _realEstates = new List<RealEstate>();
        public List<GPSPosition> GpsPositions = new List<GPSPosition>();

        public PlotOfLand(int number, string description, GPSPosition X, GPSPosition Y)
        {
            _number = number;
            _description = description;
            GpsPositions.Add(X);
            GpsPositions.Add(Y);
        }

        public void AddRealEstate(RealEstate realEstate)
        {
            _realEstates.Add(realEstate);
        }

        public int CompareTo(object obj, int depth)
        {
            if (obj == null)
            {
                throw new ArgumentException("Object is null");
            }

            PlotOfLand otherPlot = obj as PlotOfLand;
            if (otherPlot == null)
            {
                throw new ArgumentException("Object is not a PlotOfLand");
            }

            int result = depth % GpsPositions.Count;

            return GpsPositions[result].CompareTo(otherPlot.GpsPositions[result], depth);
        }

        public bool Equals(object obj)
        {
            PlotOfLand otherPlot = obj as PlotOfLand;
            if (otherPlot == null)
            {
                throw new ArgumentException("Object is not a PlotOfLand");
            }
            return _number == otherPlot._number && _description == otherPlot._description &&
                   GpsPositions[0].Equals(otherPlot.GpsPositions[0]) && GpsPositions[1].Equals(otherPlot.GpsPositions[1]);
        }


        public override string ToString()
        {
            return $"Plot of Land: {_number}, Description: {_description}, GPS Position: {GpsPositions[0]}, {GpsPositions[1]}";
        }
    }
}
