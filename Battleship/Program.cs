using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleship
{

    public class Program
    {
        private const String PossibleColumns = "ABCDEFGH";

        private bool _gameOver;

        public ShipLocation Board1ShipLocation { get; set; }
        public ShipLocation Board2ShipLocation { get; set; }

        public readonly List<Location> Board1HitLocations = new List<Location>();
        public readonly List<Location> Board2HitLocations = new List<Location>();
        private GamePiece[,] _p1GameBoard;
        private GamePiece[,] _p2GameBoard;


        static void Main(string[] args)
        {
            new Program().Run();
        }

        private void Run()
        {
            WriteLine("Game Starting...");
            Board1ShipLocation = ReadShipLocation(1);
            Board2ShipLocation = ReadShipLocation(2);

            var isPlayer1Turn = true;
            while (!_gameOver)
            {
                var playerShot = ReadPlayerShot(isPlayer1Turn);
                if (isPlayer1Turn)
                {
                    Board1HitLocations.Add(playerShot);
                }
                else
                {
                    Board2HitLocations.Add(playerShot);
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


        //Note it is important Blank is the first entry
        //As arrays initialized of Enum types initialize everything to the
        //First index in this enum
        enum GamePiece
        {
            Blank, // LEAVE AS FIRST!
            Ship,
            ShipHit,
            Miss
        }


        public bool CheckGameOver(bool isPlayer1Turn)
        {
            var shipLocation = isPlayer1Turn ? Board1ShipLocation : Board2ShipLocation;
            var hitLocations = isPlayer1Turn ? Board1HitLocations : Board2HitLocations;

            GamePiece[,] gameBoard;
            if (isPlayer1Turn)
            {
                _p1GameBoard = new GamePiece[8, 8];
                gameBoard = _p1GameBoard;
            }
            else
            {
                _p2GameBoard = new GamePiece[8, 8];
                gameBoard = _p2GameBoard;
            }

            foreach (var location in shipLocation.GetLocations())
            {
                gameBoard[location.Row, location.Column] = GamePiece.Ship;
            }

            int shipHitCount = 0;
            foreach (var hitLocation in hitLocations)
            {
                if (gameBoard[hitLocation.Row, hitLocation.Column] == GamePiece.Ship)
                {
                    gameBoard[hitLocation.Row, hitLocation.Column] = GamePiece.ShipHit;
                    shipHitCount++;
                }
                else
                {
                    gameBoard[hitLocation.Row, hitLocation.Column] = GamePiece.Miss;
                }
            }
            return shipHitCount == 3;
        }


        public String GetAsciiBoard(bool player1)
        {
            var player = player1 ? 1 : 2;
            var result = $"Player {player} Board:\n";
            result += "  A B C D E F G H\n";


          
        

            return result;
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
