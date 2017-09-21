using SimulationProject01.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimulationProject01.Models;


namespace SimulationProject01.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult Home()
        {
            return View();
        }
        [HttpPost]
        public ActionResult HomeResult(HomeModel homeModel)
        {
            int from = 1; //
            int to = 8;
            ArrayList timeBetweenArrivalFinalArrayList = TimeBetweenArrivalMechanisms.makeArrayListOFTimeBetweenArrivalDistribution(from, to);

            //

            int serviceFrom = 1;
            int serviceTo= 6;
            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            dictionary.Add(1, "0.10");
            dictionary.Add(2, "0.20");
            dictionary.Add(3, "0.30");
            dictionary.Add(4, "0.25");
            dictionary.Add(5, "0.10");
            dictionary.Add(6, "0.05");

            ArrayList serviceTimeFinalArrayList = ServiceTimeMechanisms.makeArrayListOFServiceTimeDistribution(serviceFrom, serviceTo, dictionary);
            //


            double probabilityForArrival = Convert.ToDouble(((double)from / (double)to));//0.125                                                                      //static double probabilityVariable = probabilityVariable+ probabilityFinal;
            int arrivalDecimalCount = probabilityForArrival.ToString().Split('.')[1].Length;
            double arrivalMultiplyWith = Math.Pow(10, arrivalDecimalCount);
            List<int> list = new List<int>();
            Random rnd = new Random();
            for (int i=0;i<homeModel.numberOfSimulation;i++)
            {
                
                int randomNumber = rnd.Next(1, Convert.ToInt32(arrivalMultiplyWith));  
                list.Add(randomNumber);   // .Add(randomNumber);
            }
            int[] randomDigitForInterArrivalTimeArray = list.ToArray();
            ArrayList timeBetweenArrivalDetermination = TimeBetweenArrivalMechanisms.makeArrayListOFTimeBetweenArrivalDetermination(timeBetweenArrivalFinalArrayList, randomDigitForInterArrivalTimeArray);
            //ViewBag.ArrivalDetermination = timeBetweenArrivalDetermination;

            //





            //double probabilityForService = Convert.ToDouble(((double)serviceFrom / (double)serviceTo));//0.125                                                                      //static double probabilityVariable = probabilityVariable+ probabilityFinal;
           // double.Parse(source).ToString();
            
            int serviceDecimalCount = dictionary[1].ToString().Split('.')[1].Length;
            double serviceMultiplyWith = Math.Pow(10, serviceDecimalCount);
            List<int> listForservice= new List<int>();
            Random rnd2 = new Random();
            for (int i = 0; i < homeModel.numberOfSimulation; i++)
            {

                int randomNumber = rnd2.Next(1, Convert.ToInt32(serviceMultiplyWith));
                listForservice.Add(randomNumber);   // .Add(randomNumber);
            }
            int[] randomDigitForServiceTimeArray = listForservice.ToArray();
            ArrayList timeBetweenServiceDetermination = ServiceTimeMechanisms.makeArrayListOFTimeBetweenServiceDetermination(randomDigitForServiceTimeArray, serviceTimeFinalArrayList);

            //
            ArrayList simulationTableQueueingArrayList = SimulationTableQueueingMechanisms.makeSimulationTableQueuingSolution(timeBetweenArrivalDetermination, timeBetweenServiceDetermination,homeModel.numberOfSimulation);
            //


            /*ViewBag.ArrivalDistribution = timeBetweenArrivalFinalArrayList;
            ViewBag.ServiceDistribution = serviceTimeFinalArrayList;
            ViewBag.ArrivalDetermination = timeBetweenArrivalDetermination;
            ViewBag.ServiceDetermination = timeBetweenServiceDetermination;
            ViewBag.simulationTableFinal = simulationTableQueueingArrayList;*/

            ArrayList all = new ArrayList();
            all.Add(timeBetweenArrivalFinalArrayList);
            all.Add(serviceTimeFinalArrayList);
            //to remove extra row
            //timeBetweenArrivalDetermination.Reverse();
            //timeBetweenArrivalDetermination.Remove(0);
            //timeBetweenArrivalDetermination.Reverse();
            //
            all.Add(timeBetweenArrivalDetermination);
            all.Add(timeBetweenServiceDetermination);
            all.Add(simulationTableQueueingArrayList);

            ViewBag.allInfo = all;

            int totalCustomer = simulationTableQueueingArrayList.Count;
            int totalServiceTime = 0;
            int totalTimeCustomerWaitsInQueue = 0;
            int totalTimeCustomerSpendsInSystem = 0;
            int totalIdleTimeOfServer = 0;
            int numberOfCustomerWhoWait = 0;
            int totalTimeBetweenArrivals = 0;
                        foreach (SimulationTableQueueingModel ipp in simulationTableQueueingArrayList)
            {
                totalServiceTime += ipp.serviceTime;
                if(ipp.timeCustomerWaitsInQueue!=0)
                {
                    totalTimeCustomerWaitsInQueue++;
                }
                totalTimeCustomerSpendsInSystem +=ipp.timeCustomerSpendsInSystem;
                totalIdleTimeOfServer += ipp.idleTimeOfServer;
                if(ipp.timeCustomerWaitsInQueue!=0)
                {
                    numberOfCustomerWhoWait++;
                }
                totalTimeBetweenArrivals += ipp.timeBetweenArrival;
                
            }

            double averageWaitingTime = Math.Round(((double)totalTimeCustomerWaitsInQueue / (double)totalCustomer), 2);
            double probabilityOfWaitingInQueue = Math.Round( ((double)numberOfCustomerWhoWait / (double)totalCustomer), 2);
            //
            simulationTableQueueingArrayList.Reverse();
            SimulationTableQueueingModel okka = (SimulationTableQueueingModel)simulationTableQueueingArrayList[0];

            int lastTimeServiceEnds = okka.serviceTimeEnds;
            simulationTableQueueingArrayList.Reverse();
            double probabilityOfIdleServer = Math.Round( ((double)totalIdleTimeOfServer / (double)lastTimeServiceEnds), 2);
            //

            double averageServiceTime = Math.Round( ((double)totalServiceTime / (double)totalCustomer), 2);

            double averageTimeBetweenArivals = Math.Round( ((double)totalTimeBetweenArrivals / ((double)totalCustomer - 1)), 2);

            double averageWaitingTimeOfThoseWhoWait = Math.Round( ((double)totalTimeCustomerWaitsInQueue / (double)numberOfCustomerWhoWait), 2);

            double averageTimeCustomerSpendsInSystem = Math.Round( ((double)totalTimeCustomerSpendsInSystem / (double)totalCustomer), 2);


            double averageTimeCustomerSpendsInTheSystem = Math.Round( (  (double)Convert.ToDouble(averageWaitingTime)  + (double)Convert.ToDouble(averageServiceTime) ), 2);



            ArrayList calculationInfo = new ArrayList();
            calculationInfo.Add(totalCustomer);
            calculationInfo.Add(totalServiceTime);
            calculationInfo.Add(totalTimeCustomerWaitsInQueue);
            calculationInfo.Add(totalTimeCustomerSpendsInSystem);
            calculationInfo.Add(totalIdleTimeOfServer);
            calculationInfo.Add(averageWaitingTime);
            calculationInfo.Add(probabilityOfWaitingInQueue);
            calculationInfo.Add(probabilityOfIdleServer);
            calculationInfo.Add(averageServiceTime);
            calculationInfo.Add(averageTimeBetweenArivals);
            calculationInfo.Add(averageWaitingTimeOfThoseWhoWait);
            calculationInfo.Add(averageTimeCustomerSpendsInSystem);
            calculationInfo.Add(averageTimeCustomerSpendsInTheSystem);
            ViewBag.CalculationInfo = calculationInfo;

            return View();
        }



    }
}