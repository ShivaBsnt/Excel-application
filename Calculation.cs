using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Assignment_ddocp
{

    public class Calculation
    {
        public List<double> Valuefromexcel = new List<double>();
        public double  sumofvalues(List<double> Testvalue)
        {
            Valuefromexcel = Testvalue;
            return Valuefromexcel.Sum();
         }

        public double  AverageofValues(List<double> Testvalue)
        {
            Valuefromexcel = Testvalue;
            return Valuefromexcel.Average();
        }

        public double MeanofValues(List<double> Testvalue)
        {
            int size = Testvalue.Count;
            Valuefromexcel = Testvalue;
            double sum = Valuefromexcel.Sum();
            double result = (sum / size);
            return result;
        }


        


        public double Medianofvalues (List<double> TestvalueforMedian)
        {
            {

                double median;
                TestvalueforMedian.Sort();
                int i = TestvalueforMedian.Count;
                int med = (i + 1) % 2;

                double index;
                int num;
                if (med != 0)
                {
                    index = (i + 1) / 2;
                    num = Convert.ToInt32(index);
                    median = (TestvalueforMedian[num] + TestvalueforMedian[num - 1]) / 2;
                    return median;
                }
                else
                {
                    index = (i + 1) / 2;
                    num = Convert.ToInt32(index);
                    return TestvalueforMedian[num - 1];
                }

            }



        }
        

       public double Modeofvalues(List<double> Testvalue)
       {
            Dictionary<int, int> counts = new Dictionary<int, int>();
            foreach (int a in Testvalue)
            {
                if (counts.ContainsKey(a))
                    counts[a] = counts[a] + 1;
                else
                    counts[a] = 1;
            }

            int result = int.MinValue;
            int max = int.MinValue;
            foreach (int key in counts.Keys)
            {
                if (counts[key] > max)
                {
                    max = counts[key];
                    result = key;
                }
            }
            return result;


        }
        public  double multiplicationofvalues(List<double> Testvalue)
        {
            Valuefromexcel = Testvalue;
            double result = Valuefromexcel[0] * Valuefromexcel[1];
            return result;


        }




    }
}
