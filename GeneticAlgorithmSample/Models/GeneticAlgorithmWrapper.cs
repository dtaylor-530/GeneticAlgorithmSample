using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneticSharp.Domain;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Crossovers;
using GeneticSharp.Domain.Fitnesses;
using GeneticSharp.Domain.Mutations;
using GeneticSharp.Domain.Populations;
using GeneticSharp.Domain.Selections;
using GeneticSharp.Domain.Terminations;



namespace GeneticAlgorithmSample
{
    public class GeneticAlgorithmWrapper
    {

        public event Action<System.Windows.Point, System.Windows.Point> NotifyOfImprovement;


        private GeneticSharp.Domain.GeneticAlgorithm ga;
        public double latestFitness { get; private set; }

        public int GenerationNumber
        {
            get
            {
                return ga.GenerationsNumber;
            }
        }


        public GeneticAlgorithmWrapper(double maxh , double maxw)
        {
            latestFitness = 0.0;

            var listminmax = new List<Tuple<double, double>>
            {
            { new Tuple<double,double>(0,maxw) },
                    { new Tuple<double, double>(0, maxw) },
            { new Tuple<double, double>(0, maxh) },
            { new Tuple<double, double>(0, maxh) }
            };



            ga = GAFactory.DefaultGeneticAlgorithm(FunctionToOptimise, listminmax.ToArray());

            ga.GenerationRan += NewGeneration;

            NotifyOfImprovement += GeneticAlgorithm_NotifyOfImprovement;
        }



        public double FunctionToOptimise(double[] values)
        {


            var x1 = values[0];
            var y1 = values[1];
            var x2 = values[2];
            var y2 = values[3];

            return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        }



        // start the algorithm running/learning
        public void Run()
        {
            ga.Start();


        }


        // notify subsribers of improvement to algorithm
        private void GeneticAlgorithm_NotifyOfImprovement(System.Windows.Point arg1, System.Windows.Point arg2)
        {
      
            Console.WriteLine(
                            "Generation {0}: ({1},{2}),({3},{4}) = {5}",
                            ga.GenerationsNumber, arg1.X, arg1.Y, arg2.X, arg2.Y, latestFitness);
        }



        // the procreation of new chromosomes
        private void NewGeneration(object sender, EventArgs e)
        {
            var bestChromosome = (sender as GeneticSharp.Domain.GeneticAlgorithm).BestChromosome as FloatingPointChromosome;
            var bestFitness = bestChromosome.Fitness.Value;

            if (bestFitness != latestFitness)
            {
                latestFitness = bestFitness;
                var phenotype = bestChromosome.ToFloatingPoints();

                var p1 = new System.Windows.Point(phenotype[0], phenotype[1]);
                var p2 = new System.Windows.Point(phenotype[2], phenotype[3]);

                NotifyOfImprovement(p1, p2);

            }

        }




    }
}
