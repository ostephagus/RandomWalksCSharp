using System.Diagnostics;

namespace RandomWalks
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            (double, double)[,] results = Simulate(6, 100);
            sw.Stop();
            Console.WriteLine($"Time taken: {sw.ElapsedMilliseconds}ms");
            File.WriteAllText("results.txt", ConvertToPythonString(results));
        }

        static string ConvertToPythonString((double, double)[,] values)
        {
            Console.WriteLine("Creating string to write:");
            string pythonString = "[";
            for (int i = 0; i < values.GetLength(0); i++)
            {
                Console.Write($",{i}");
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