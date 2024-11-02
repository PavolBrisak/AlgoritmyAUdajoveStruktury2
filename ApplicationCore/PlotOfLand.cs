using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdajovkySem1
{
    public class PlotOfLand : ILocatable
    {
        public int Number { get; set; }
        public string Description { get; set; }
        public List<RealEstate> RealEstates { get; set; }

        public List<GPSPosition> GpsPositions { get; set; }

        public PlotOfLand(int number, string description, GPSPosition X, GPSPosition Y)
        {
            GpsPositions = new List<GPSPosition>();
            Number = number;
            Description = description;
            GpsPositions.Add(X);
            GpsPositions.Add(Y);
            RealEstates = new List<RealEstate>();

        }

        public void AddRealEstate(RealEstate realEstate)
        {
            RealEstates.Add(realEstate);
        }

        //public int CompareTo(object obj, int depth)
        //{
        //    if (obj == null)
        //    {
        //        throw new ArgumentException("Object is null");
        //    }

        //    PlotOfLand otherPlot = obj as PlotOfLand;
        //    if (otherPlot == null)
        //    {
        //        throw new ArgumentException("Object is not a PlotOfLand");
        //    }

        //    int result = depth % GpsPositions.Count;

        //    return GpsPositions[result].CompareTo(otherPlot.GpsPositions[result], depth);
        //}

        //public bool Equals(object obj)
        //{
        //    PlotOfLand otherPlot = obj as PlotOfLand;
        //    if (otherPlot == null)
        //    {
        //        throw new ArgumentException("Object is not a PlotOfLand");
        //    }
        //    return Number == otherPlot.Number && Description == otherPlot.Description &&
        //           GpsPositions[0].Equals(otherPlot.GpsPositions[0]) && GpsPositions[1].Equals(otherPlot.GpsPositions[1]);
        //}


        public override string ToString()
        {
            return $"Plot of Land: {Number}, Description: {Description}";
        }

        //public bool SpecificEquals(object obj)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
