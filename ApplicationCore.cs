using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdajovkySem1
{
    public class ApplicationCore
    {
        private KDTree<GPSPosition> _plotsOfLandTree;
        private KDTree<GPSPosition> _realEstatesTree;
        private KDTree<GPSPosition> _allGPSPositionsTree;
        private OperationGenerator _operationGenerator;

        public ApplicationCore()
        {
            _plotsOfLandTree = new KDTree<GPSPosition>();
            _realEstatesTree = new KDTree<GPSPosition>();
            _allGPSPositionsTree = new KDTree<GPSPosition>();
            _operationGenerator = new OperationGenerator();
        }

        public string PrintPlotsOfLandTree()
        {
            return _plotsOfLandTree.Print();
        }

        public string PrintRealEstatesTree()
        {
            return _realEstatesTree.Print();
        }

        public string PrintAllGPSPositionsTree()
        {
            return _allGPSPositionsTree.Print();
        }

        public void GenerateInsert(int count, double x1_min, double x1_max, double y1_min, double y1_max, double x2_min, double x2_max, double y2_min, double y2_max)
        {
            for (int i = 0; i < count; i++)
            {

                GPSPosition gpsPosition = _operationGenerator.GenerateGPSPosition();
                _plotsOfLandTree.Insert(gpsPosition);
            }
        }

        public string PrintSelectedTree(string selectedTree)
        {
            switch (selectedTree)
            {
                case "Plots of Land Tree":
                    return PrintPlotsOfLandTree();
                case "Real Estates Tree":
                    return PrintRealEstatesTree();
                case "All GPS Positions Tree":
                    return PrintAllGPSPositionsTree();
                default:
                    return string.Empty;
            }
        }

        public string FindRealEstate(char directionX, char directionY, double x, double y)
        {
            if (x < 0 || y < 0)
            {
                throw new ArgumentException("GPS coordinates must be positive.");
            }

            GPSPosition gpsPosition = new GPSPosition(directionX, directionY, x, y,null,null);
            List<GPSPosition> foundRealEstates = _realEstatesTree.Find(gpsPosition);
            string foundRealEstatesString = string.Empty;
            foreach (GPSPosition foundRealEstate in foundRealEstates)
            {
                foundRealEstatesString += foundRealEstate.ToString() + Environment.NewLine;
            }
            return foundRealEstatesString;
        }

        public string FindPlotOfLand(char directionX, char directionY, double x, double y)
        {
            if (x < 0 || y < 0)
            {
                throw new ArgumentException("GPS coordinates must be positive.");
            }

            GPSPosition gpsPosition = new GPSPosition(directionX, directionY, x, y,null,null);
            List<GPSPosition> foundPlotsOfLand = _plotsOfLandTree.Find(gpsPosition);
            string foundPlotsOfLandString = string.Empty;
            foreach (GPSPosition foundPlotOfLand in foundPlotsOfLand)
            {
                foundPlotsOfLandString += foundPlotOfLand.ToString() + Environment.NewLine;
            }
            return foundPlotsOfLandString;
        }

        public string FindAll(char directionX1, char directionY1, double x1, double y1, char directionX2, char directionY2, double x2, double y2)
        {
            if (x1 < 0 || y1 < 0 || x2 < 0 || y2 < 0)
            {
                throw new ArgumentException("GPS coordinates must be positive.");
            }

            GPSPosition gpsPosition1 = new GPSPosition(directionX1, directionY1, x1, y1, null, null);
            GPSPosition gpsPosition2 = new GPSPosition(directionX2, directionY2, x2, y2, null, null);
            List<GPSPosition> foundAll1 = _allGPSPositionsTree.Find(gpsPosition1);
            List<GPSPosition> foundAll2 = _allGPSPositionsTree.Find(gpsPosition2);
            string foundAllString = string.Empty;
            foreach (GPSPosition foundAll in foundAll1)
            {
                foundAllString += foundAll.ToString() + Environment.NewLine;
            }
            foreach (GPSPosition foundAll in foundAll2)
            {
                foundAllString += foundAll.ToString() + Environment.NewLine;
            }
            return foundAllString;
        }

        public void InsertRealEstate(int number, string description, char directionX1, char directionY1, double x1, double y1, char directionX2, char directionY2, double x2, double y2)
        {
            if (x1 < 0 || y1 < 0 || x2 < 0 || y2 < 0)
            {
                throw new ArgumentException("GPS coordinates must be positive.");
            }
            GPSPosition gpsPosition1 = new GPSPosition(directionX1, directionY1, x1, y1, null, null);
            GPSPosition gpsPosition2 = new GPSPosition(directionX2, directionY2, x2, y2, null, null);
            RealEstate realEstate = new RealEstate(number, description, gpsPosition1, gpsPosition2);
            gpsPosition1.RealEstate = realEstate;
            gpsPosition2.RealEstate = realEstate;
            _realEstatesTree.Insert(gpsPosition1);
            _realEstatesTree.Insert(gpsPosition2);

            // locate all GPS positions in the plot of land tree
            List<GPSPosition> allGPSPositions = _plotsOfLandTree.Find(gpsPosition1);
            foreach (GPSPosition gpsPosition in allGPSPositions)
            {
                // add real estate to all plot of lands on this GPS position
                gpsPosition.PlotOfLand.AddRealEstate(realEstate);

                // add real estate to all plot of lands on this GPS position
                gpsPosition1.RealEstate.AddPlotOfLand(gpsPosition.PlotOfLand);
            }

            // add these gps positions to the all gps positions tree
            _allGPSPositionsTree.Insert(gpsPosition1);
            _allGPSPositionsTree.Insert(gpsPosition2);
        }
    }
}
