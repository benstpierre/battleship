using System;
using System.Collections.Generic;

namespace Battleship
{

    public class Program
    {
        private const String PossibleColumns = "ABCDEFGH";

        private bool _gameOver;

        private ShipLocation _p1ShipLocation;
        private ShipLocation _p2ShipLocation;

        private readonly List<Location> _p1FireLocations = new List<Location>();
        private readonly List<Location> _p2FireLocations = new List<Location>();



        static void Main(string[] args)
        {
            new Program().Run();
        }

        private void Run()
        {
            WriteLine("Game Starting...");
            _p1ShipLocation = ReadShipLocation(1);
            _p2ShipLocation = ReadShipLocation(2);

            var isPlayer1Turn = true;
            while (!_gameOver)
            {
                var playerShot = ReadPlayerShot(isPlayer1Turn);
                if (isPlayer1Turn)
                {
                    _p1FireLocations.Add(playerShot);
                }
                else
                {
                    _p2FireLocations.Add(playerShot);
                }
                if (CheckGameOver(isPlayer1Turn))
                {
                    var player = isPlayer1Turn ? 1 : 2;
                    WriteLine($"Congratulations Player {player}, you sunk my battleship");
                    WriteLine("Game over....");
                    break;
                }
                isPlayer1Turn = !isPlayer1Turn;
            }

            ReadLine("Press any key to continue");
        }

        private bool CheckGameOver(bool isPlayer1Turn)
        {
            var shipLocation = isPlayer1Turn ? _p1ShipLocation : _p2ShipLocation;
            var hitLocations = isPlayer1Turn ? _p1FireLocations : _p2FireLocations;
            






            if ("".Equals(""))
            {
                _gameOver = true;
            }
            return _gameOver;
        }

        private Location ReadPlayerShot(Boolean isPlayer1Turn)
        {
            var player = isPlayer1Turn ? 1 : 2;
            var otherPlayer = isPlayer1Turn ? 2 : 1;
            var strLocation = ReadLine($"Player {player}: Provide a location to hit Player {otherPlayer}. Format: B5").Trim().ToUpper();
            var location = ParseLocation(strLocation);
            if (location == null)
            {
                WriteLine("Invalid Entry");
                return ReadPlayerShot(isPlayer1Turn);
            }
            return location;
        }


        public ShipLocation ReadShipLocation(int player)
        {
            var strLocation = ReadLine($"Please enter the ship location for Player {player}. Format A3 A5");
            var shipLocation = ParseShipLocation(strLocation);
            if (shipLocation == null || !shipLocation.Valid())
            {
                WriteLine("Invalid Entry");
                return ReadShipLocation(player);
            }
            return shipLocation;
        }

        public ShipLocation ParseShipLocation(String strShipLocation)
        {
            if (strShipLocation == null)
            {
                return null;
            }
            strShipLocation = strShipLocation.Trim().ToUpper();
            if (strShipLocation.Length != 5)
            {
                return null;
            }
            var strLocations = strShipLocation.Split(' ');
            if (strLocations.Length != 2)
            {
                return null;
            }
            var shipLocation = new ShipLocation
            {
                Start = ParseLocation(strLocations[0]),
                End = ParseLocation(strLocations[1])
            };
            if (shipLocation.Start == null || shipLocation.End == null)
            {
                return null;
            }
            return shipLocation;
        }

        public Location ParseLocation(String strLocation)
        {
            if (strLocation.Length != 2)
            {
                return null;
            }
            var column = strLocation.ToCharArray()[0];
            if (!PossibleColumns.Contains(column.ToString()))
            {
                return null;
            }
            var strRow = strLocation.ToCharArray()[1].ToString();
            if (!Int32.TryParse(strRow, out int row))
            {
                return null;
            }
            if (row < 1 || row > 8)
            {
                return null;
            }
            var location = new Location
            {
                Column = column,
                Row = row
            };
            return location;
        }


        private void WriteLine(String message)
        {
            Console.WriteLine(message);
        }


        private String ReadLine(String message)
        {
            if (message != null)
            {
                WriteLine(message);
            }
            return Console.ReadLine();
        }

    }
}
