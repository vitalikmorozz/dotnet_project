using System;

namespace Lab1.Entities.Parameters
{
    public class StoppageParameters : QueryStringParameters
    {
        public DateTime MinArrivalTime { get; set; }
        public DateTime MaxArrivalTime { get; set; }
        public DateTime MinDepartureTime { get; set; }
        public DateTime MaxDepartureTime { get; set; }
    }
}