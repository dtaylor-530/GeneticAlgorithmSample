using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using GeneticSharp.Domain;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Crossovers;
using GeneticSharp.Domain.Fitnesses;
using GeneticSharp.Domain.Mutations;
using GeneticSharp.Domain.Populations;
using GeneticSharp.Domain.Selections;
using GeneticSharp.Domain.Terminations;

namespace GeneticAlgorithmSample.Models
{




    partial class chromosome
    {
        //private double maxWidth = 98;
        //private double maxHeight = 68;

        //public chromosome(double maxWidth, double maxHeight)
        //{

        //    this.maxWidth = maxWidth;
        //    this.maxHeight = maxHeight;

        //}

        public static FloatingPointChromosome defaultChromosome(double maxWidth, double maxHeight)
        {
            //     public FloatingPointChromosome(double[] minValue, double[] maxValue, int[] totalBits, int[] fractionBits);

            var chromosome = new FloatingPointChromosome(
            minValue: new double[] { 0, 0, 0, 0 },
            maxValue: new double[] { maxWidth, maxHeight, maxWidth, maxHeight },
            totalBits: new int[] { 10, 10, 10, 10 },
            fractionBits: new int[] { 0, 0, 0, 0 });

            return chromosome;

        }




    }




    partial class GAFactory
    {

        public static GeneticSharp.Domain.GeneticAlgorithm DefaultGeneticAlgorithm(double maxh=200,double maxw=200)
        {


            var population = new Population(20, 40, chromosome.defaultChromosome(maxh, maxw));

            var fitness = new FuncFitness((c) =>
            {
                var fc = c as FloatingPointChromosome;

                var values = fc.ToFloatingPoints();
                var x1 = values[0];
                var y1 = values[1];
                var x2 = values[2];
                var y2 = values[3];

                return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
            });

            var selection = new EliteSelection();
            var crossover = new UniformCrossover(0.5f);
            var mutation = new FlipBitMutation();

            var ga = new GeneticSharp.Domain.GeneticAlgorithm(
               population,
               fitness,
               selection,
               crossover,
               mutation);

            var termination = new FitnessStagnationTermination(100);

            ga.Termination = termination;


            Console.WriteLine("Generation: (x1, y1), (x2, y2) = distance");

            return ga;

        }

     
    }



    public class GeneticAlgorithm
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


        public GeneticAlgorithm(double maxh,double maxw)
        {
            latestFitness = 0.0;
            ga = GAFactory.DefaultGeneticAlgorithm(maxh,maxw);

            ga.GenerationRan += NewGeneration;

            NotifyOfImprovement += GeneticAlgorithm_NotifyOfImprovement;
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