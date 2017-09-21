using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationProject01
{
    class TimeBetweenArrivalMechanisms
    {
        public static ArrayList makeArrayListOFTimeBetweenArrivalDistribution(int from,int to)
        {
            double probabilityFinal = Convert.ToDouble(((double)from / (double)to));//0.125
                                                                                    //static double probabilityVariable = probabilityVariable+ probabilityFinal;
            int decimalCount = probabilityFinal.ToString().Split('.')[1].Length;
            double multiplyWith = Math.Pow(10, decimalCount);

            Stack<double> cuProbStack = new Stack<double>();
            Stack<double> randDigStartStack = new Stack<double>();
            Stack<double> randDigEndStack = new Stack<double>();
            ArrayList timeBetweenArrivalaArraylist = new ArrayList();
            for (int i = from; i <= to; i++)
            {
                RandomDigitAssignmentModel obj = new RandomDigitAssignmentModel();

                if (timeBetweenArrivalaArraylist.Count.Equals(0))
                {
                    obj.time= i;//1
                    obj.probability = Convert.ToDouble(probabilityFinal);//0.125
                    obj.cumulativeProbability = probabilityFinal;//0.125
                    cuProbStack.Push(probabilityFinal);//0.125
                    obj.randomDigitAssignmentStarts = 1;
                    randDigStartStack.Push(obj.randomDigitAssignmentStarts);//1
                    obj.randomDigitAssignmentEnds = (int)(obj.cumulativeProbability * multiplyWith);//125
                    randDigEndStack.Push(obj.randomDigitAssignmentEnds);//125

                }
                else
                {

                    obj.time= i;//2
                    obj.probability = probabilityFinal;//0.125
                    //obj.cumulativeProbability=probabilityVariable                    
                    obj.cumulativeProbability = Convert.ToDouble((cuProbStack.Peek() + probabilityFinal));//0.250
                    cuProbStack.Push(obj.cumulativeProbability);//0.250                    
                    obj.randomDigitAssignmentStarts = Convert.ToInt32(randDigEndStack.Peek() + 1);//126
                    randDigStartStack.Push(obj.randomDigitAssignmentStarts);
                    obj.randomDigitAssignmentEnds = Convert.ToInt32(obj.cumulativeProbability * multiplyWith);//250
                    randDigEndStack.Push(obj.randomDigitAssignmentEnds);

                }
                timeBetweenArrivalaArraylist.Add(obj);

            }

            return timeBetweenArrivalaArraylist;

        }
        
        public static ArrayList makeArrayListOFTimeBetweenArrivalDetermination(ArrayList timeBetweenArrivalFinalArrayList,int [] randomDigitForInterArrivalTimeArray)
        {
            ArrayList timeBetweenArrivalDeterminationArrayList= new ArrayList();

            //changed  1 to 0  and =  
            for (int i=1;i<randomDigitForInterArrivalTimeArray.Length;i++)
            {
                DeterminationForRandomDigitModel determinationForRandomDigitModel = new DeterminationForRandomDigitModel();
                determinationForRandomDigitModel.customer = i+1;
                determinationForRandomDigitModel.randomDigit = randomDigitForInterArrivalTimeArray[i-1];//changed i-1 to i
                
                foreach (var first in timeBetweenArrivalFinalArrayList )
                {
                    RandomDigitAssignmentModel obj = new RandomDigitAssignmentModel();
                    obj =(RandomDigitAssignmentModel) first;
                    int random = determinationForRandomDigitModel.randomDigit;
                    int randomStarts = obj.randomDigitAssignmentStarts;
                    int randomEnds = obj.randomDigitAssignmentEnds;
                    if(randomStarts <= random && random <= randomEnds)
                    {
                        determinationForRandomDigitModel.time = obj.time;
                    }

                }

                timeBetweenArrivalDeterminationArrayList.Add(determinationForRandomDigitModel);
            }
            

            return timeBetweenArrivalDeterminationArrayList;
        }

    }
}
