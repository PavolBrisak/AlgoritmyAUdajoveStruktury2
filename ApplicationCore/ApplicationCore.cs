using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdajovkySem1
{
    public class ApplicationCore
    {
        private readonly KDTree<GPSPosition> _plotsOfLandTree;
        private readonly KDTree<GPSPosition> _realEstatesTree;
        private readonly KDTree<GPSPosition> _allGpsPositionsTree;

        //toto dat do potomka a v aplikacii nie je generator, a tie dva lsity
        private readonly OperationGenerator _operationGenerator;
        private readonly List<PlotOfLand> _plotsOfLand;
        private readonly List<RealEstate> _realEstates;
        private int _uniqueId = 0;

        public ApplicationCore()
        {
            _plotsOfLandTree = new KDTree<GPSPosition>();
            _realEstatesTree = new KDTree<GPSPosition>();
            _allGpsPositionsTree = new KDTree<GPSPosition>();
            _operationGenerator = new OperationGenerator(new Random());
            _plotsOfLand = new List<PlotOfLand>();
            _realEstates = new List<RealEstate>();
        }

        public string PrintSelectedTree(string selectedTree)
        {
            switch (selectedTree)
            {
                case "Plots of Land Tree":
                    return PrintTree(_plotsOfLandTree);
                case "Real Estates Tree":
                    return PrintTree(_realEstatesTree);
                case "All GPS Positions Tree":
                    return PrintTree(_allGpsPositionsTree);
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

            GPSPosition gpsPosition = new GPSPosition(directionX, directionY, x, y, null, null,0);
            List<GPSPosition> foundRealEstates = _realEstatesTree.Find(gpsPosition);
            if (foundRealEstates.Count == 0)
            {
                return "No real estates found.";
            }
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

            GPSPosition gpsPosition = new GPSPosition(directionX, directionY, x, y, null, null, 0);
            List<GPSPosition> foundPlotsOfLand = _plotsOfLandTree.Find(gpsPosition);
            if (foundPlotsOfLand.Count == 0)
            {
                return "No plots of land found.";
            }
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

            GPSPosition gpsPosition1 = new GPSPosition(directionX1, directionY1, x1, y1, null, null, 0);
            GPSPosition gpsPosition2 = new GPSPosition(directionX2, directionY2, x2, y2, null, null, 0);
            List<GPSPosition> foundAll1 = _allGpsPositionsTree.Find(gpsPosition1);
            List<GPSPosition> foundAll2 = _allGpsPositionsTree.Find(gpsPosition2);
            if (foundAll1.Count == 0 && foundAll2.Count == 0)
            {
                return "No GPS positions found.";
            }
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
            GPSPosition gpsPosition1 = new GPSPosition(directionX1, directionY1, x1, y1, null, null, _uniqueId);
            GPSPosition gpsPosition2 = new GPSPosition(directionX2, directionY2, x2, y2, null, null, _uniqueId);
            RealEstate realEstate = new RealEstate(number, description, gpsPosition1, gpsPosition2);
            gpsPosition1.RealEstate = realEstate;
            gpsPosition2.RealEstate = realEstate;
            _realEstatesTree.Insert(gpsPosition1);
            _realEstatesTree.Insert(gpsPosition2);
            _uniqueId++;

            List<GPSPosition> allGPSPositions1 = _plotsOfLandTree.Find(gpsPosition1);
            foreach (GPSPosition gpsPosition in allGPSPositions1)
            {
                gpsPosition.PlotOfLand.AddRealEstate(realEstate);

                gpsPosition1.RealEstate.AddPlotOfLand(gpsPosition.PlotOfLand);
            }

            List<GPSPosition> allGPSPositions2 = _plotsOfLandTree.Find(gpsPosition2);
            foreach (GPSPosition gpsPosition in allGPSPositions2)
            {
                gpsPosition.PlotOfLand.AddRealEstate(realEstate);

                gpsPosition2.RealEstate.AddPlotOfLand(gpsPosition.PlotOfLand);
            }
            _allGpsPositionsTree.Insert(gpsPosition1);
            _allGpsPositionsTree.Insert(gpsPosition2);

            PrintLogToConsole("Inserted data: " + realEstate.ToString());

            _realEstates.Add(realEstate);
        }

        public void InsertPlotOfLand(int number, string description, char directionX1, char directionY1, double x1, double y1, char directionX2, char directionY2, double x2, double y2)
        {
            if (x1 < 0 || y1 < 0 || x2 < 0 || y2 < 0)
            {
                throw new ArgumentException("GPS coordinates must be positive.");
            }
            GPSPosition gpsPosition1 = new GPSPosition(directionX1, directionY1, x1, y1, null, null,_uniqueId);
            GPSPosition gpsPosition2 = new GPSPosition(directionX2, directionY2, x2, y2, null, null, _uniqueId);
            PlotOfLand plotOfLand = new PlotOfLand(number, description, gpsPosition1, gpsPosition2);
            gpsPosition1.PlotOfLand = plotOfLand;
            gpsPosition2.PlotOfLand = plotOfLand;
            _plotsOfLandTree.Insert(gpsPosition1);
            _plotsOfLandTree.Insert(gpsPosition2);
            _uniqueId++;

            List<GPSPosition> allGPSPositions1 = _realEstatesTree.Find(gpsPosition1);
            foreach (GPSPosition gpsPosition in allGPSPositions1)
            {
                gpsPosition.RealEstate.AddPlotOfLand(plotOfLand);

                gpsPosition1.PlotOfLand.AddRealEstate(gpsPosition.RealEstate);
            }

            List<GPSPosition> allGPSPositions2 = _realEstatesTree.Find(gpsPosition2);
            foreach (GPSPosition gpsPosition in allGPSPositions2)
            {
                gpsPosition.RealEstate.AddPlotOfLand(plotOfLand);

                gpsPosition2.PlotOfLand.AddRealEstate(gpsPosition.RealEstate);
            }
            _allGpsPositionsTree.Insert(gpsPosition1);
            _allGpsPositionsTree.Insert(gpsPosition2);

            PrintLogToConsole("Inserted data: " + plotOfLand.ToString());

            _plotsOfLand.Add(plotOfLand);
        }

        public void GenerateInsertPlotOfLand(int count, double min, double max, int desMiesta)
        {
            CheckMinMaxCount(count, min, max);
            PrintLogToConsole("Inserting " + count + " items:");
            _operationGenerator.GenerateInsert(count, () =>
            {
                var (directionX1, directionY1, x1, y1, directionX2, directionY2, x2, y2, number, description) = GenerateRandomGPSData(min, max, desMiesta);
                InsertPlotOfLand(number, $"{description}", directionX1, directionY1, x1, y1, directionX2, directionY2, x2, y2);
            });
        }
        public void GenerateInsertRealEstate(int count, double min, double max, int desMiesta)
        {
            CheckMinMaxCount(count, min, max);
            PrintLogToConsole("Inserting " + count + " items:");
            _operationGenerator.GenerateInsert(count, () =>
            {
                var (directionX1, directionY1, x1, y1, directionX2, directionY2, x2, y2, number, description) = GenerateRandomGPSData(min, max, desMiesta);
                InsertRealEstate(number, $"{description}", directionX1, directionY1, x1, y1, directionX2, directionY2, x2, y2);
            });
        }

        private static void CheckMinMaxCount(int count, double min, double max)
        {
            if (count < 0)
            {
                throw new ArgumentException("Count must be positive.");
            }

            if (min < 0 || max < 0)
            {
                throw new ArgumentException("Min and Max values must be positive.");
            }

            if (min >= max)
            {
                throw new ArgumentException("Min value must be less than Max value.");
            }
        }

        private (char, char, double, double, char, char, double, double, int, string) GenerateRandomGPSData(double min, double max, int desMiesta)
        {
            char directionX1 = _operationGenerator.GenerateValueFromMinMax(0, 1) > 0.5 ? 'N' : 'S';
            char directionY1 = _operationGenerator.GenerateValueFromMinMax(0, 1) > 0.5 ? 'E' : 'W';
            double x1 = _operationGenerator.GenerateValueFromMinMax(min, max, desMiesta);
            double y1 = _operationGenerator.GenerateValueFromMinMax(min, max, desMiesta);
            int number = _operationGenerator.GenerateIntValue();
            string description = _operationGenerator.GenerateString(10);

            char directionX2 = _operationGenerator.GenerateValueFromMinMax(0, 1) > 0.5 ? 'N' : 'S';
            char directionY2 = _operationGenerator.GenerateValueFromMinMax(0, 1) > 0.5 ? 'E' : 'W';
            double x2 = _operationGenerator.GenerateValueFromMinMax(min, max, desMiesta);
            double y2 = _operationGenerator.GenerateValueFromMinMax(min, max, desMiesta);

            return (directionX1, directionY1, x1, y1, directionX2, directionY2, x2, y2, number, description);
        }

        public string GenerateFindBoth(int count)
        {
            if (count < 0)
            {
                throw new ArgumentException("Count must be positive.");
            }
            if (count > _plotsOfLand.Count || count > _realEstates.Count)
            {
                throw new ArgumentException("Count must be less than the number of plots of land and real estates.");
            }

            StringBuilder result = new StringBuilder();
            List<ILocatable> combinedList = new List<ILocatable>();
            combinedList.AddRange(_plotsOfLand);
            combinedList.AddRange(_realEstates);

            PrintLogToConsole("Finding " + count + " items:");

            _operationGenerator.GenerateFind(count, () =>
            {
                int randomIndex = _operationGenerator.GenerateIntValue(0, combinedList.Count);
                var randomItem = combinedList[randomIndex];

                GPSPosition randomGpsPosition = randomItem.GpsPositions[_operationGenerator.GenerateIntValue(0, randomItem.GpsPositions.Count)];

                List<GPSPosition> found = _allGpsPositionsTree.Find(randomGpsPosition);
                result.AppendLine(randomGpsPosition.ToString() + ":");
                result.AppendLine();

                foreach (GPSPosition foundGpsPosition in found)
                {
                    result.AppendLine(foundGpsPosition.ToString());
                }

                PrintLogToConsole("Found data for -> " + randomItem.ToString());
                foreach (GPSPosition gpsPosition in found)
                {
                    PrintLogToConsole(gpsPosition.ToString());
                }

            });
            if (result.Length == 0)
            {
                result.AppendLine("Not found");
            }
            else
            {
                result.Insert(0, "Found items:" + Environment.NewLine);
            }

            return result.ToString();
        }

        public string GenerateFindRealEstate(int count)
        {
            if (count < 0)
            {
                throw new ArgumentException("Count must be positive.");
            }
            if (count > _realEstates.Count)
            {
                throw new ArgumentException("Count must be less than the number of real estates.");
            }
            StringBuilder result = new StringBuilder();

            PrintLogToConsole("Finding " + count + " items:");

            _operationGenerator.GenerateFind(count, () =>
            {
                int randomIndex = _operationGenerator.GenerateIntValue(0, _realEstates.Count);
                RealEstate randomItem = _realEstates[randomIndex];

                GPSPosition randomGpsPosition = randomItem.GpsPositions[_operationGenerator.GenerateIntValue(0, randomItem.GpsPositions.Count)];

                List<GPSPosition> found = _realEstatesTree.Find(randomGpsPosition);
                result.AppendLine(randomGpsPosition.ToString() + ":");
                result.AppendLine();
                
                for (int i = 0; i < found.Count; i++)
                {
                    result.AppendLine(found[i].ToString());
                }

                PrintLogToConsole("Found data for -> " + randomItem.ToString());
                foreach (GPSPosition gpsPosition in found)
                {
                    PrintLogToConsole(gpsPosition.ToString());
                }

            });
            if (result.Length == 0)
            {
                result.AppendLine("Not found");
            }
            else
            {
                result.Insert(0, "Found items:" + Environment.NewLine);
            }

            return result.ToString();
        }

        public string GenerateFindPlotOfLand(int count)
        {
            if (count < 0)
            {
                throw new ArgumentException("Count must be positive.");
            }
            if (count > _plotsOfLand.Count)
            {
                throw new ArgumentException("Count must be less than the number of real estates.");
            }
            StringBuilder result = new StringBuilder();

            PrintLogToConsole("Finding " + count + " items:");

            _operationGenerator.GenerateFind(count, () =>
            {
                int randomIndex = _operationGenerator.GenerateIntValue(0, _plotsOfLand.Count);
                PlotOfLand randomItem = _plotsOfLand[randomIndex];

                GPSPosition randomGpsPosition = randomItem.GpsPositions[_operationGenerator.GenerateIntValue(0, randomItem.GpsPositions.Count)];

                List<GPSPosition> found = _plotsOfLandTree.Find(randomGpsPosition);
                result.AppendLine(randomGpsPosition.ToString() + ":");
                result.AppendLine();

                for (int i = 0; i < found.Count; i++)
                {
                    result.AppendLine(found[i].ToString());
                }

                PrintLogToConsole("Found data for -> " + randomItem.ToString());
                foreach (GPSPosition gpsPosition in found)
                {
                    PrintLogToConsole(gpsPosition.ToString());
                }

            });
            if (result.Length == 0)
            {
                result.AppendLine("Not found");
            }
            else
            {
                result.Insert(0, "Found items:" + Environment.NewLine);
            }

            return result.ToString();
        }
        public string PrintTree<T>(KDTree<T> tree) where T : IComparable
        {
            return tree.Print();
        }

        public void GenerateDeletePlotOfLand(int count)
        {   
            if (count < 0)
            {
                throw new ArgumentException("Count must be positive.");
            }
            if (count > _plotsOfLand.Count)
            {
                throw new ArgumentException("Count must be less than the number of plots of land.");
            }

            PrintLogToConsole("Deleting " + count + " items:");

            _operationGenerator.GenerateDelete(count, () =>
            {
                int randomIndex = _operationGenerator.GenerateIntValue(0, _plotsOfLand.Count);
                PlotOfLand randomPlotOfLand = _plotsOfLand[randomIndex];

                // find positions corresponding to the gps positions of the plot of land in the real estates tree
                foreach (GPSPosition gpsPosition in randomPlotOfLand.GpsPositions)
                {
                    List<GPSPosition> found = _realEstatesTree.Find(gpsPosition);
                    foreach (GPSPosition foundGpsPosition in found)
                    {
                        foundGpsPosition.RealEstate.PlotsOfLand.Remove(randomPlotOfLand);
                    }
                }

                // remove all gps positions of that plot of land from the all gps positions tree
                for (int j = 0; j < randomPlotOfLand.GpsPositions.Count; j++)
                {
                    _allGpsPositionsTree.Delete(randomPlotOfLand.GpsPositions[j]);
                }

                // remove all gps positions of that plot of land from the plot of land tree
                for (int j = 0; j < randomPlotOfLand.GpsPositions.Count; j++)
                {
                    _plotsOfLandTree.Delete(randomPlotOfLand.GpsPositions[j]);
                }

                PrintLogToConsole("Deleted data: " + randomPlotOfLand.ToString());

                // remove the plot of land from the list of all plots of land
                _plotsOfLand.Remove(randomPlotOfLand);
            });
        }

        public void GenerateDeleteRealEstate(int count)
        {
            if (count < 0)
            {
                throw new ArgumentException("Count must be positive.");
            }
            if (count > _realEstates.Count)
            {
                throw new ArgumentException("Count must be less than the number of real estates.");
            }

            PrintLogToConsole("Deleting " + count + " items:");
            _operationGenerator.GenerateDelete(count, () =>
            {
                int randomIndex = _operationGenerator.GenerateIntValue(0, _realEstates.Count);
                RealEstate randomRealEstate = _realEstates[randomIndex];

                // find positions corresponding to the gps positions of the real estate in the plot of land tree
                foreach (GPSPosition gpsPosition in randomRealEstate.GpsPositions)
                {
                    List<GPSPosition> found = _plotsOfLandTree.Find(gpsPosition);
                    foreach (GPSPosition foundGpsPosition in found)
                    {
                        foundGpsPosition.PlotOfLand.RealEstates.Remove(randomRealEstate);
                    }
                }

                // remove all gps positions of that real estate from the all gps positions tree
                for (int j = 0; j < randomRealEstate.GpsPositions.Count; j++)
                {
                    _allGpsPositionsTree.Delete(randomRealEstate.GpsPositions[j]);
                }

                // remove all gps positions of that real estate from the real estates tree
                for (int j = 0; j < randomRealEstate.GpsPositions.Count; j++)
                {
                    _realEstatesTree.Delete(randomRealEstate.GpsPositions[j]);
                }

                PrintLogToConsole("Deleted data: " + randomRealEstate.ToString());

                // remove the real estate from the list of all real estates
                _realEstates.Remove(randomRealEstate);
            });
        }

        private void CheckPercentage(double duplicityPercentage)
        {
            if (duplicityPercentage < 0 || duplicityPercentage > 1)
            {
                throw new ArgumentException("Duplicity percentage must be between 0 and 1.");
            }
        }

        public void PrintLogToConsole(string log)
        {
            Console.WriteLine(log);
        }
    }

    public class ApplicationOperationGenerator : ApplicationCore
    {

    }
}
