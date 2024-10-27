using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdajovkySem1
{
    public class RealEstate : ILocatable
    {
        public int Number { get; set; }
        public string Description { get; set; }
        public List<PlotOfLand> PlotsOfLand { get; set; }
        public List<GPSPosition> GpsPositions { get; set; }

        public RealEstate(int number, string description, GPSPosition X, GPSPosition Y)
        {
            GpsPositions = new List<GPSPosition>();
            PlotsOfLand = new List<PlotOfLand>();
            Number = number;
            Description = description;
            GpsPositions.Add(X);
            GpsPositions.Add(Y);
        }

        public void AddPlotOfLand(PlotOfLand plotOfLand)
        {
            PlotsOfLand.Add(plotOfLand);
        }

        public override string ToString()
        {
            return $"Real Estate: {Number}, Description: {Description}";
        }
    }
}
