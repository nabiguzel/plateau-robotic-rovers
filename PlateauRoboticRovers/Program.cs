using System;
using System.Linq;

namespace PlateauRoboticRovers
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            string tryAgain = "Y";
            Plateau plateau;
            RoboticRover[] roboticRovers;
            char[] validInstructions = System.Enum.GetNames(typeof(CommandTypes)).Select(c => Convert.ToChar(c)).ToArray(); //{ 'L', 'R', 'M' };
            do
            {
                try
                {
                    Console.WriteLine("Plateau upper-right coordinates:");
                    var plateauSizes = Console.ReadLine().Trim().ToUpper().Split(' ').Select(p => Convert.ToInt32(p)).ToList();

                    if (plateauSizes.Count != 2)
                        throw new Exception("The plateau upper-right coordinates input is not valid!");

                    plateau = new Plateau(plateauSizes[0], plateauSizes[1]);

                    int roverCount = 2, currentRover = 0;
                    roboticRovers = new RoboticRover[roverCount];
                    do
                    {
                        Console.WriteLine($"{(currentRover + 1)}. Rover's positions and Direction:");
                        var roverPositions = Console.ReadLine().Trim().ToUpper().Split(' ').ToList();
                        if (roverPositions.Count == 3)
                        {
                            roboticRovers[currentRover] = new RoboticRover
                            {
                                X = Convert.ToInt32(roverPositions[0]),
                                Y = Convert.ToInt32(roverPositions[1]),
                                Direction = (Directions)Enum.Parse(typeof(Directions), roverPositions[2])
                            };

                            if (plateau.X < roboticRovers[currentRover].X || plateau.Y < roboticRovers[currentRover].Y)
                                throw new Exception($"The Rover Coordinates (X, Y):({roboticRovers[currentRover].X}, {roboticRovers[currentRover].Y}) are not beyond Plateau's bounderies (X, Y):({plateau.X}, {plateau.Y})");
                        }
                        else
                        {
                            throw new Exception("The Rover Positions input is not valid!");
                        }

                        Console.WriteLine("Rover's instructions:");
                        bool isNotValid = true;
                        do //Eğer istenirse, yukarıdaki gibi hata fırlatmak yerine uygun değerler girilene kadar tekrar tekrar giriş istenir.
                        {
                            roboticRovers[currentRover].Instructions = Console.ReadLine().ToUpper();
                            isNotValid = roboticRovers[currentRover].Instructions.Any(ch => !validInstructions.Contains(ch));
                            if (isNotValid)
                                Console.WriteLine("Please Enter the valid Rover's instructions that just contains by 'L', 'R', 'M' characters:");
                        } while (isNotValid);
                        currentRover++;
                    } while (roverCount > currentRover);

                    Console.WriteLine("Final Rovers' Coordinates:");
                    foreach (var roboticRover in roboticRovers)
                    {
                        roboticRover.Drive(plateau);
                        Console.WriteLine($"{roboticRover.X} {roboticRover.Y} {roboticRover.Direction.ToString()}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                Console.Write("Do you want to try again? (Y/N): ");
                tryAgain = Console.ReadLine().ToUpper();
            } while (tryAgain != "N");

            Console.ReadLine();
        }
    }
}
