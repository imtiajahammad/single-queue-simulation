using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationProject01
{
    class ServiceTimeMechanisms
    {
        static Dictionary<int, string> dictionary = new Dictionary<int, string>();
        public static ArrayList makeArrayListOFServiceTimeDistribution(int from, int to, Dictionary<int,string> diction)
        {
            dictionary = diction;
            //double probabilityFinal = Convert.ToDouble(((double)from / (double)to));//0.125
                                                                                    //static double probabilityVariable = probabilityVariable+ probabilityFinal;
            int decimalCount = dictionary[1].ToString().Split('.')[1].Length;
            double multiplyWith = Math.Pow(10, decimalCount);

            Stack<double> cuProbStack = new Stack<double>();
            Stack<double> randDigStartStack = new Stack<double>();
            Stack<double> randDigEndStack = new Stack<double>();
            ArrayList timeBetweenArrivalaArraylist = new ArrayList();
            for (int i = 1; i <= dictionary.Count; i++)
            {
                RandomDigitAssignmentModel obj = new RandomDigitAssignmentModel();
                if (cuProbStack.Count == 0)
                {
                    obj.time = i;
                    obj.probability = Convert.ToDouble(dictionary[i]);
                    obj.cumulativeProbability = Convert.ToDouble(dictionary[i]);
                    obj.cumulativeProbability = obj.probability;
                    cuProbStack.Push(obj.cumulativeProbability);
                    obj.randomDigitAssignmentStarts = 1;//126
                    randDigStartStack.Push(obj.randomDigitAssignmentStarts);
                    obj.randomDigitAssignmentEnds = Convert.ToInt32(obj.cumulativeProbability * multiplyWith);//250
                    randDigEndStack.Push(obj.randomDigitAssignmentEnds);


                }
                else { 
                obj.time = i;
                obj.probability = Convert.ToDouble(dictionary[i]);
                obj.cumulativeProbability = Convert.ToDouble(dictionary[i]);
                obj.cumulativeProbability = Convert.ToDouble((cuProbStack.Peek() + obj.probability));
                cuProbStack.Push(obj.cumulativeProbability);
                obj.randomDigitAssignmentStarts = Convert.ToInt32(randDigEndStack.Peek() + 1);//126
                randDigStartStack.Push(obj.randomDigitAssignmentStarts);
                obj.randomDigitAssignmentEnds = Convert.ToInt32(obj.cumulativeProbability * multiplyWith);//250
                randDigEndStack.Push(obj.randomDigitAssignmentEnds);
                }

                timeBetweenArrivalaArraylist.Add(obj);
            }

            return timeBetweenArrivalaArraylist;

        }

        public static ArrayList makeArrayListOFTimeBetweenServiceDetermination(int[] randomDigitForServiceTimeArray, ArrayList serviceTimeFinalArrayList)
        {
            ArrayList serviceTimeDeterminationArrayList = new ArrayList();

            for (int i = 0; i < randomDigitForServiceTimeArray.Length; i++)
            {
                DeterminationForRandomDigitModel determinationForRandomDigitModel = new DeterminationForRandomDigitModel();
                determinationForRandomDigitModel.customer = i + 1;
                determinationForRandomDigitModel.randomDigit = randomDigitForServiceTimeArray[i];
                
                foreach (var first in serviceTimeFinalArrayList)
                {
                    RandomDigitAssignmentModel obj = new RandomDigitAssignmentModel();
                    obj = (RandomDigitAssignmentModel) first;
                    int random = determinationForRandomDigitModel.randomDigit;
                    int randomStarts = obj.randomDigitAssignmentStarts;
                    int randomEnds = obj.randomDigitAssignmentEnds;
                    if (randomStarts <= random && random <= randomEnds)
                    {
                        determinationForRandomDigitModel.time = obj.time;
                    }

                }

                serviceTimeDeterminationArrayList.Add(determinationForRandomDigitModel);
            }


            return serviceTimeDeterminationArrayList;
        }
            
        }

    }
