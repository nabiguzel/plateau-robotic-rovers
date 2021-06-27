using System;
namespace PlateauRoboticRovers
{
    public class RoboticRover
    {
        public int X { get; set; }

        public int Y { get; set; }

        public Directions Direction { get; set; }

        public String Instructions { get; set; }

        public RoboticRover()
        {
            this.X = 0;
            this.Y = 0;
            this.Direction = Directions.N;
        }

        public RoboticRover(int x, int y, Directions direction = Directions.N)
        {
            if (x < 0 || y < 0)
                throw new Exception($"Positions (x, y):({x}, {y}) can not be less than (0, 0)");

            this.X = x;
            this.Y = y;
            this.Direction = direction;
        }

        private void Rotate(CommandTypes rotationDirection)
        {
            switch (this.Direction)
            {
                case Directions.N:
                    switch (rotationDirection)
                    {
                        case CommandTypes.L:
                            this.Direction = Directions.W;
                            break;
                        case CommandTypes.R:
                            this.Direction = Directions.E;
                            break;
                        default:
                            break;
                    }

                    break;
                case Directions.S:
                    switch (rotationDirection)
                    {
                        case CommandTypes.L:
                            this.Direction = Directions.E;
                            break;
                        case CommandTypes.R:
                            this.Direction = Directions.W;
                            break;
                        default:
                            break;
                    }

                    break;
                case Directions.E:
                    switch (rotationDirection)
                    {
                        case CommandTypes.L:
                            this.Direction = Directions.N;
                            break;
                        case CommandTypes.R:
                            this.Direction = Directions.S;
                            break;
                        default:
                            break;
                    }

                    break;
                case Directions.W:
                    switch (rotationDirection)
                    {
                        case CommandTypes.L:
                            this.Direction = Directions.S;
                            break;
                        case CommandTypes.R:
                            this.Direction = Directions.N;
                            break;
                        default:
                            break;
                    }

                    break;
                default:
                    break;
            }
        }

        private void Move()
        {
            switch (this.Direction)
            {
                case Directions.N:
                    this.Y += 1;
                    break;
                case Directions.S:
                    this.Y -= 1;
                    break;
                case Directions.E:
                    this.X += 1;
                    break;
                case Directions.W:
                    this.X -= 1;
                    break;
                default:
                    break;
            }
        }

        public void Drive(Plateau plateau)
        {
            if (plateau.X < this.X || plateau.Y < this.Y)
                throw new Exception($"Plateau Coordinates (X, Y):({plateau.X}, {plateau.Y}) can not be less than Rover Coordinates (X, Y):({this.X}, {this.Y})");

            foreach (var command in Instructions)
            {
                CommandTypes commandType;
                if (Enum.TryParse<CommandTypes>(command.ToString(), out commandType))
                {
                    if (commandType == CommandTypes.M)
                    {
                        this.Move();
                    }
                    else
                    {
                        this.Rotate(commandType);
                    }
                }
                else
                {
                    throw new Exception($"{command} is an invalid character!");
                }

                if (this.X < 0 || this.X > plateau.X || this.Y < 0 || this.Y > plateau.Y)
                {
                    //TODO: sınırda olduğunu bildiğinden durmalı ve son adımı geri mi almalı yoksa hata mı vermeli!
                    throw new Exception($"Position can not be beyond Plateau's bounderies (0 , 0) and ({plateau.X} , {plateau.Y})");
                }
            }
        }
    }
}
