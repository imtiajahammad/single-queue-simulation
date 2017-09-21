using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationProject01
{
    public class SimulationTableQueueingModel
    {
        public int customer;    
        public int timeBetweenArrival;
        public int arrivalTime;
        public int serviceTime;
        public int serviceTimeBegins;
        public int timeCustomerWaitsInQueue;
        public int serviceTimeEnds;
        public int timeCustomerSpendsInSystem;
        public int idleTimeOfServer;
    }
}
