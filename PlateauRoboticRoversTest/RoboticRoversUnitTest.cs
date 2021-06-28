using NUnit.Framework;
using PlateauRoboticRovers;

namespace PlateauRoboticRoversTest
{
    public class Tests
    {
        [Test]
        public void TestSample_55_12N_LMLMLMLMM()
        {
            var plateau = new Plateau(5, 5);
            var roboticRover = new RoboticRover(1, 2, Directions.N);
            roboticRover.Instructions = "LMLMLMLMM";
            roboticRover.Drive(plateau);
            var output = $"{roboticRover.X} {roboticRover.Y} {roboticRover.Direction.ToString()}";
            var expectedOutput = "1 3 N";
            Assert.AreEqual(expectedOutput, output);
        }

        [Test]
        public void TestSample_55_33E_MMRMMRMRRM()
        {
            var plateau = new Plateau(5, 5);
            var roboticRover = new RoboticRover(3, 3, Directions.E);
            roboticRover.Instructions = "MMRMMRMRRM";
            roboticRover.Drive(plateau);
            var output = $"{roboticRover.X} {roboticRover.Y} {roboticRover.Direction.ToString()}";
            var expectedOutput = "5 1 E";
            Assert.AreEqual(expectedOutput, output);
        }
    }
}