using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationProject01
{
    class SimulationTableQueueingMechanisms
    {

        public static ArrayList makeSimulationTableQueuingSolution(ArrayList timeBetweenArrivalDetermination,ArrayList timeBetweenServiceDetermination,int numberOfCustomer)
        {
            //
            DeterminationForRandomDigitModel sample = new DeterminationForRandomDigitModel();
            sample.customer = 1;
            sample.randomDigit = 0;
            sample.time = 0;
            timeBetweenArrivalDetermination.Insert(0, sample);
            //timeBetweenArrivalDetermination.RemoveAt(numberOfCustomer-1);//changed here 10 t0 9
            

            //



            ArrayList simulationQueueingArrayList = new ArrayList();
            

            int count = 0;
            //changed here 0 to 1 and =
            for (int i = 0; i < timeBetweenServiceDetermination.Count; i++)
            {

                SimulationTableQueueingModel simulationTableQueueingModel = new SimulationTableQueueingModel();


                //DeterminationForRandomDigitModel serviceModelPrevious = new DeterminationForRandomDigitModel();
                //serviceModelPrevious = (DeterminationForRandomDigitModel)timeBetweenServiceDetermination[i];

                

                



                if (simulationQueueingArrayList.Count == 0)
                {
                    DeterminationForRandomDigitModel serviceModelCurrent= new DeterminationForRandomDigitModel();
                    serviceModelCurrent= (DeterminationForRandomDigitModel)timeBetweenServiceDetermination[i];


                    simulationTableQueueingModel.customer = i + 1;
                    simulationTableQueueingModel.timeBetweenArrival = 0;
                    simulationTableQueueingModel.arrivalTime = 0;
                    simulationTableQueueingModel.serviceTime = serviceModelCurrent.time;
                    simulationTableQueueingModel.serviceTimeBegins = 0;
                    simulationTableQueueingModel.timeCustomerWaitsInQueue = 0;
                    simulationTableQueueingModel.serviceTimeEnds = simulationTableQueueingModel.arrivalTime + simulationTableQueueingModel.serviceTime;
                    simulationTableQueueingModel.timeCustomerSpendsInSystem = simulationTableQueueingModel.serviceTime + simulationTableQueueingModel.timeCustomerWaitsInQueue;
                    simulationTableQueueingModel.idleTimeOfServer = 0;
                }
                else
                {

                    SimulationTableQueueingModel simulationQueuingPrevious = new SimulationTableQueueingModel();
                    simulationQueuingPrevious = (SimulationTableQueueingModel)simulationQueueingArrayList[i-1];//changed i-1 to i-2

                    DeterminationForRandomDigitModel serviceModelCurrent = new DeterminationForRandomDigitModel();
                    serviceModelCurrent = (DeterminationForRandomDigitModel)timeBetweenServiceDetermination[i];//changed i to i-1




//                    DeterminationForRandomDigitModel arrivalModelPrevious = new DeterminationForRandomDigitModel();
  //                  arrivalModelPrevious = (DeterminationForRandomDigitModel)timeBetweenServiceDetermination[i - 1];

                    DeterminationForRandomDigitModel arrivalModelCurrent = new DeterminationForRandomDigitModel();
                    arrivalModelCurrent = (DeterminationForRandomDigitModel)timeBetweenArrivalDetermination[i]; //changed i to i-2  ***

                    simulationTableQueueingModel.customer = i + 1;
                    simulationTableQueueingModel.timeBetweenArrival = arrivalModelCurrent.time;

                    //
                    simulationTableQueueingModel.arrivalTime = arrivalModelCurrent.time + simulationQueuingPrevious.arrivalTime;// obj.arrivalTime + serviceModel.time;
                    //



                    simulationTableQueueingModel.serviceTime = serviceModelCurrent.time;

                    //
                    if (simulationQueuingPrevious.serviceTimeEnds < simulationTableQueueingModel.arrivalTime)
                    {
                        simulationTableQueueingModel.serviceTimeBegins = simulationTableQueueingModel.arrivalTime;
                    }
                    else
                    {
                        simulationTableQueueingModel.serviceTimeBegins = simulationQueuingPrevious.serviceTimeEnds;
                    }
                    //


                    //
                    simulationTableQueueingModel.timeCustomerWaitsInQueue = simulationTableQueueingModel.serviceTimeBegins - simulationTableQueueingModel.arrivalTime;
                    //



                    //
                    simulationTableQueueingModel.serviceTimeEnds = simulationTableQueueingModel.serviceTimeBegins + simulationTableQueueingModel.serviceTime;
                    //

                    simulationTableQueueingModel.timeCustomerSpendsInSystem = simulationTableQueueingModel.serviceTime + simulationTableQueueingModel.timeCustomerWaitsInQueue;


                    simulationTableQueueingModel.idleTimeOfServer = simulationTableQueueingModel.arrivalTime - simulationQueuingPrevious.serviceTimeEnds;
                    if(simulationTableQueueingModel.idleTimeOfServer<0)
                    {
                        simulationTableQueueingModel.idleTimeOfServer = 0;
                    }
                    
                }
                simulationQueueingArrayList.Add(simulationTableQueueingModel);
            }
            return simulationQueueingArrayList;
        }
    }
}


//foreach (var inn in timeBetweenServiceDetermination)
//{
//    //var serviceArray = timeBetweenArrivalDetermination.ToArray();
//    DeterminationForRandomDigitModel serviceModel = new DeterminationForRandomDigitModel();
//    serviceModel = (DeterminationForRandomDigitModel) inn;
//                        //var arrivalArray = timeBetweenArrivalDetermination.ToArray();

//        SimulationTableQueueingModel simulationTableQueueingModel = new SimulationTableQueueingModel();


//    foreach (var onn in timeBetweenArrivalDetermination)
//    {
//        DeterminationForRandomDigitModel arrivalModel = new DeterminationForRandomDigitModel();
//        arrivalModel = (DeterminationForRandomDigitModel)onn;

//        {
//            var simulationQueueingArrayListToArray = simulationQueueingArrayList.ToArray();
//            var singleModel = simulationQueueingArrayListToArray[count - 1];
//            SimulationTableQueueingModel obj = new SimulationTableQueueingModel();
//            obj = (SimulationTableQueueingModel)singleModel;

//            /*var a = serviceTimeFinalArrayList.ToArray();
//    var b = a[2];
//    RandomDigitAssignmentModel juu = new RandomDigitAssignmentModel();
//    juu = (RandomDigitAssignmentModel)b;
//    int jj = juu.randomDigitAssignmentEnds;*/
//            if (simulationQueueingArrayList.Count == 0)
//    {

//        simulationTableQueueingModel.customer = count + 1;
//        simulationTableQueueingModel.timeBetweenArrival = 0;
//        simulationTableQueueingModel.arrivalTime = 0;
//        simulationTableQueueingModel.serviceTime = serviceModel.time;
//        simulationTableQueueingModel.serviceTimeBegins = 0;
//        simulationTableQueueingModel.timeCustomerWaitsInQueue = 0;
//        simulationTableQueueingModel.serviceTimeEnds = simulationTableQueueingModel.arrivalTime + simulationTableQueueingModel.serviceTime;
//        simulationTableQueueingModel.timeCustomerSpendsInSystem = simulationTableQueueingModel.serviceTime + simulationTableQueueingModel.timeCustomerWaitsInQueue;
//        simulationTableQueueingModel.idleTimeOfServer = 0;
//        count++;
//    }
//    else
//    {



//                simulationTableQueueingModel.customer = count + 1;
//                simulationTableQueueingModel.timeBetweenArrival = arrivalModel.time;

//                //
//                simulationTableQueueingModel.arrivalTime = obj.arrivalTime + serviceModel.time;
//                //



//                simulationTableQueueingModel.serviceTime = serviceModel.time;

//                //
//                if (obj.serviceTimeEnds < arrivalModel.time)
//                {
//                    simulationTableQueueingModel.serviceTimeBegins = arrivalModel.time;
//                }
//                else
//                {
//                    simulationTableQueueingModel.serviceTimeBegins = obj.serviceTimeEnds;
//                }
//                //


//                //
//                simulationTableQueueingModel.timeCustomerWaitsInQueue = simulationTableQueueingModel.serviceTimeBegins - simulationTableQueueingModel.arrivalTime;
//                //



//                //
//                simulationTableQueueingModel.serviceTimeEnds = simulationTableQueueingModel.serviceTimeBegins + simulationTableQueueingModel.serviceTime;
//                //

//                simulationTableQueueingModel.timeCustomerSpendsInSystem = simulationTableQueueingModel.serviceTime + simulationTableQueueingModel.timeCustomerWaitsInQueue;


//                simulationTableQueueingModel.idleTimeOfServer = simulationTableQueueingModel.arrivalTime - obj.serviceTimeEnds;
//                count++;
//            }
//            //simulationQueueingArrayList.Add(simulationTableQueueingModel);
//        }
//    }
//      //simulationQueueingArrayList.Add(simulationTableQueueingModel);

//}

//return simulationQueueingArrayList;
// }
// }
//}
