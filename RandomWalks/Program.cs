using System.Diagnostics;

namespace RandomWalks
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int numOfParticles = 1;
            int iterations = 100;
            try
            {
                (numOfParticles, iterations) = GetCMDArgs(args);
                Console.WriteLine($"{numOfParticles}, {iterations}");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine($"{e.Message}, defaulting to 1 particle and 100 iterations.");
            }

            Stopwatch sw = new Stopwatch();
            sw.Start();
            (double, double)[,] results = Simulate(numOfParticles, iterations);
            sw.Stop();
            Console.WriteLine($"Time taken: {sw.ElapsedMilliseconds}ms");
            //File.WriteAllText("results.txt", ConvertToPythonString(results));
            WriteDoubleTuple2DArray(results, "intermediateresults.bin");
        }

        static (int, int) GetCMDArgs(string[] args)
        {
            int numOfParticles = 1;
            int iterations = 100;
            if (args.Length > 0) 
            {
                if (!int.TryParse(args[0], out numOfParticles))
                {
                    throw new ArgumentException("First argument was not in the correct format");
                }
                if (args.Length > 1)
                {
                    if (!int.TryParse(args[1], out iterations))
                    {
                        throw new ArgumentException("Second argument was not in the correct fomat");
                    }
                }
            }
            else
            {
                throw new ArgumentException("Insufficient arguments");
            }
            return (numOfParticles, iterations);
        }

        static void WriteDoubleTuple2DArray((double, double)[,] doubles, string filename)
        {
            FileStream outputStream = new FileStream(filename, FileMode.Create, FileAccess.ReadWrite);
            BinaryWriter writer = new BinaryWriter(outputStream);

            writer.Write(doubles.GetLength(0));
            writer.Write(doubles.GetLength(1));

            for (int i = 0; i < doubles.GetLength(0); i++)
            {
                for (int j = 0; j < doubles.GetLength(1); j++)
                {
                    writer.Write(doubles[i, j].Item1);
                    writer.Write(doubles[i, j].Item2);
                }
            }
            outputStream.Close();
        }

        static string ConvertToPythonString((double, double)[,] values)
        {
            Console.WriteLine("Creating string to write:");
            Console.Write("00%");
            string pythonString = "[";
            int length = values.GetLength(0);
            for (int i = 0; i < length; i++)
            {
                Console.Write($"\b\b\b{i * 100 / length:00}%");
                pythonString += "[";
                for (int j = 0; j < values.GetLength(1); j++)
                {
                    pythonString += $"({values[i,j].Item1},{values[i,j].Item2})"; //Append tuple representation
                    if (j < values.GetLength(1) - 1)
                    {
                        pythonString += ", ";
                    }
                }
                pythonString += "]";
                if (i < values.GetLength(0) - 1)
                {
                    pythonString += ", ";
                }
            }
            pythonString += "]";
            Console.WriteLine("\b\b\b100%");
            return pythonString;
        }

        static (double, double)[,] Simulate(int numOfParticles, int steps)
        {
            Particle[] particles = new Particle[numOfParticles];
            for (int i = 0; i < numOfParticles; i++)
            {
                particles[i] = new Particle(); //Initialise particles at (0,0)
            }
            (double, double)[,] positions = new (double, double)[numOfParticles, steps];
            for (int particleNo = 0; particleNo < numOfParticles; particleNo++)
            {
                for (int step = 0; step < steps; step++)
                {
                    positions[particleNo, step] = particles[particleNo].Position;
                    particles[particleNo].MoveRandomly();
                }
            }
            return positions;
        }
    }

    public class Particle
    {
        private (double, double) position;
        private Random Rng;

        public (double, double) Position { get => position; set => position = value; }

        public Particle()
        {
            position = (0, 0);
            Rng = new Random();
        }

        public Particle((double, double) startPosition)
        {
            position = startPosition;
            Rng = new Random();
        }

        public void MoveRandomly()
        {
            double angle = Rng.NextDouble() * 360;
            double x = Math.Cos(angle);
            double y = Math.Sin(angle);
            position.Item1 += x;
            position.Item2 += y;
        }
    }
}