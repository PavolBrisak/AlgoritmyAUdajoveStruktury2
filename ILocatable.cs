using System.Collections.Generic;

namespace UdajovkySem1
{
    public interface ILocatable
    {
        List<GPSPosition> GpsPositions { get; }
        string ToString();
    }
}