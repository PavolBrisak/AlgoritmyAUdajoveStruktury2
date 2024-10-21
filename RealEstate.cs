using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdajovkySem1
{
    public class RealEstate
    {
        public int Number { get; set; }
        public string Description { get; set; }
        private List<PlotOfLand> _plotsOfLand = new List<PlotOfLand>();
        private GPSPosition _x;
        private GPSPosition _y;

        public RealEstate(int number, string description, GPSPosition X, GPSPosition Y)
        {
            Number = number;
            Description = description;
            _x = X;
            _y = Y;
        }

        public void AddPlotOfLand(PlotOfLand plotOfLand)
        {
            _plotsOfLand.Add(plotOfLand);
        }

        public override string ToString()
        {
            return $"RealEstate number: {Number}, description: {Description}";
        }
    }
}
