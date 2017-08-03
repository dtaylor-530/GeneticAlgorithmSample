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
using MyClassLibrary;

namespace GeneticAlgorithmLibrary
{

    public class Variable: IKeyValue
    {
        public int Index { get; set; }
        public string Key { get; set; }
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
        public double Value { get; set; }

    }




    partial class chromosome
    {



        //public static FloatingPointChromosome default2Chromosome(double[] maxValues)
        //{
        //    //     public FloatingPointChromosome(double[] minValue, double[] maxValue, int[] totalBits, int[] fractionBits);

        //    int size = maxValues.Count();

        //    var chromosome = new FloatingPointChromosome(
        //    minValue: Enumerable.Repeat(0.0, size).ToArray(),
        //    maxValue: maxValues,
        //    totalBits: Enumerable.Repeat(10, size).ToArray(),
        //    fractionBits: Enumerable.Repeat(0, size).ToArray());

        //    return chromosome;

        //}

        public static FloatingPointChromosome default3Chromosome(List<Variable> variables, int totalBits, int fractionBits)
        {
            //     public FloatingPointChromosome(double[] minValue, double[] maxValue, int[] totalBits, int[] fractionBits);

            var f=variables.Select(x => x.MinValue);

            // TotalBits
            //The total bits used to represent each number.
            // e.g 998, would require 10 bits since 2^10=1024
            // FractionBits
            //The number of bits from total bits that must be used the fraction(scale or decimal) part of the number. 
            // e.g 12.04 -two fraction bits

           var chromosome = new FloatingPointChromosome(
            minValue: variables.Select(x => x.MinValue).ToArray(),
            maxValue: variables.Select(x => x.MaxValue).ToArray(),
            totalBits: Enumerable.Repeat(totalBits, variables.Count()).ToArray(),
            fractionBits: Enumerable.Repeat(fractionBits, variables.Count()).ToArray());

            return chromosome;

        }

    }




    partial class GAFactory
    {

      

        //public static GeneticSharp.Domain.GeneticAlgorithm Default2GeneticAlgorithm(double[] maxValues, Func<FloatingPointChromosome, double> func)
        //{


        //    var population = new Population(20, 100, chromosome.default2Chromosome(maxValues));

        //    var fitness = new FuncFitness((c) =>
        //    {
        //        return func(c as FloatingPointChromosome);
        //    });

        //    var selection = new EliteSelection();
        //    var crossover = new UniformCrossover(0.5f);
        //    var mutation = new FlipBitMutation();

        //    var ga = new GeneticSharp.Domain.GeneticAlgorithm(
        //       population,
        //       fitness,
        //       selection,
        //       crossover,
        //       mutation);

        //    var termination = new FitnessStagnationTermination(100);

        //    ga.Termination = termination;


        //    Console.WriteLine("Generation: (x1, y1), (x2, y2) = distance");

        //    return ga;

        //}


        public static GeneticSharp.Domain.GeneticAlgorithm Default2GeneticAlgorithm(
            List<Variable> variables, 
            Func<FloatingPointChromosome, double> func,
            int totalBits,int fractionBits)

        {


            var population = new Population(5, 10, chromosome.default3Chromosome(variables, totalBits,fractionBits));

            var fitness = new FuncFitness((c) =>
            {
                return func(c as FloatingPointChromosome);
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

            // var termination = new FitnessStagnationTermination();
            // var termination = new FitnessThresholdTermination(1);
            var termination = new GenerationNumberTermination(200);
            ga.Termination = termination;


            Console.WriteLine("Generation: (x1, y1), (x2, y2) = distance");

            return ga;

        }
    }



    public class GeneticAlgoirthm
    {

        public event Action<double[]> NotifyOfImprovement;


        private GeneticSharp.Domain.GeneticAlgorithm ga;
        public double latestFitness { get; private set; }

        public int GenerationNumber
        {
            get
            {
                return ga.GenerationsNumber;
            }
        }

        public Dictionary<string, List<double>> Improvements { get; set; } = new Dictionary<string, List<double>>();
        private List<Variable> variables;

        public GeneticAlgoirthm(List<Variable> variables, Func<FloatingPointChromosome, double> func, int totalBits,int fractionBits)
        {
            latestFitness = 0.0;
            ga = GAFactory.Default2GeneticAlgorithm( variables,  func, totalBits,fractionBits);

            ga.GenerationRan += NewGeneration;

            this.variables = variables;

           // NotifyOfImprovement += GeneticAlgorithm_NotifyOfImprovement;
        }


        // start the algorithm running/learning
        public void Run()
        {
            ga.Start();


        }

        // notify subsribers of improvement to algorithm
        private void GeneticAlgorithm_NotifyOfImprovement(double[] phenotype)
        {
          
            for (int i=0; i<phenotype.Count();i++)
            {
                Improvements[variables[i].Key].Add(phenotype[i]);
            }
            //Console.WriteLine(
            //                "Generation {0}: ({1},{2}),({3},{4}) = {5}",
            //                ga.GenerationsNumber, arg1.X, arg1.Y, arg2.X, arg2.Y, latestFitness);
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

                NotifyOfImprovement(phenotype);

            }

        }




    }


}