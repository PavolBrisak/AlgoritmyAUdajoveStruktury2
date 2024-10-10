using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdajovkySem1
{
    internal class RealEstate
    {
        private int _number;
        private string _description;
        private List<PlotOfLand> _plotsOfLand = new List<PlotOfLand>();
        private GPSPosition _X;
        private GPSPosition _Y;

        public RealEstate(int number, string description, GPSPosition X, GPSPosition Y)
        {
            _number = number;
            _description = description;
            _X = X;
            _Y = Y;
        }

        public void AddPlotOfLand(PlotOfLand plotOfLand)
        {
            _plotsOfLand.Add(plotOfLand);
        }


    }
}
