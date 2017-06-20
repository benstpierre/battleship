﻿using Battleship;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BattleshipTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestParseValidShipLocation()
        {
            var shipLocation = new Program().ParseShipLocation("A3 A5");
            Assert.IsNotNull(shipLocation);
            Assert.AreEqual(shipLocation.Start.Column, 'A');
            Assert.AreEqual(shipLocation.Start.Row, 3);
            Assert.AreEqual(shipLocation.End.Column, 'A');
            Assert.AreEqual(shipLocation.End.Row, 5);
        }

        [TestMethod]
        public void TestParseValidShipLocation2()
        {
            var shipLocation = new Program().ParseShipLocation("a2 a5");
            Assert.IsNotNull(shipLocation);
            Assert.AreEqual(shipLocation.Start.Column, 'A');
            Assert.AreEqual(shipLocation.Start.Row, 2);
            Assert.AreEqual(shipLocation.End.Column, 'A');
            Assert.AreEqual(shipLocation.End.Row, 5);
        }


        [TestMethod]
        public void TestParseLocation()
        {
            var location = new Program().ParseLocation("A5");
            Assert.IsNotNull(location);
            Assert.AreEqual(location.Column, 'A');
            Assert.AreEqual(location.Row, 5);
        }

        [TestMethod]
        public void TestParseLocationInvalidValue()
        {
            Assert.IsNull(new Program().ParseLocation("AA5"));
        }

        [TestMethod]
        public void TestParseLocationInvalidValue2()
        {
            Assert.IsNull(new Program().ParseLocation("A"));
        }

        [TestMethod]
        public void TestParseLocationInvalidValue3()
        {
            Assert.IsNull(new Program().ParseLocation("J1"));
        }

        [TestMethod]
        public void TestParseLocationInvalidValue4()
        {
            Assert.IsNull(new Program().ParseLocation("A9"));
        }


        [TestMethod]
        public void TestValidateValidShipLocation()
        {
            var shipLocation = new ShipLocation
            {
                Start = new Location
                {
                    Row = 1,
                    Column = 1
                },
                End = new Location
                {
                    Row = 3,
                    Column = 1
                }
            };
            Assert.IsTrue(shipLocation.Valid());
        }

        [TestMethod]
        public void TestValidateInvalidShipLocation()
        {
            var shipLocation = new ShipLocation
            {
                Start = new Location
                {
                    Row = 1,
                    Column = 1
                },
                End = new Location
                {
                    Row = 5,
                    Column = 1
                }
            };
            Assert.IsFalse(shipLocation.Valid());
        }


        [TestMethod]
        //Test a vertical ship
        public void TestGetShipLocations()
        {
            var shipLocation = new ShipLocation
            {
                Start = new Location
                {
                    Row = 1,
                    Column = 3
                },
                End = new Location
                {
                    Row = 3,
                    Column = 3
                }
            };

            var locations = shipLocation.GetLocations();

            Assert.AreEqual(3, locations.Count, "Should be 3 locations");
            Assert.AreEqual(shipLocation.Start, locations[0]);
            Assert.AreEqual(new Location { Row = 2, Column = 3 }, locations[1]);
            Assert.AreEqual(shipLocation.End, locations[2]);
        }

        [TestMethod]
        //Test a horizontal ship
        public void TestGetShipLocations2()
        {
            var shipLocation = new ShipLocation
            {
                Start = new Location
                {
                    Row = 3,
                    Column = 3
                },
                End = new Location
                {
                    Row = 3,
                    Column = 5
                }
            };

            var locations = shipLocation.GetLocations();

            Assert.AreEqual(3, locations.Count, "Should be 3 locations");
            Assert.AreEqual(shipLocation.Start, locations[0]);
            Assert.AreEqual(new Location { Row = 3, Column = 4 }, locations[1]);
            Assert.AreEqual(shipLocation.End, locations[2]);
        }

        [TestMethod]
        //Test a horizontal ship defined backwards (start is to the right of end)
        public void TestGetShipLocations3()
        {
            var shipLocation = new ShipLocation
            {
                Start = new Location
                {
                    Row = 2,
                    Column = 5
                },
                End = new Location
                {
                    Row = 2,
                    Column = 3
                }
            };

            var locations = shipLocation.GetLocations();

            Assert.AreEqual(3, locations.Count, "Should be 3 locations");
            Assert.AreEqual(shipLocation.Start, locations[0]);
            Assert.AreEqual(new Location { Row = 2, Column = 4 }, locations[1]);
            Assert.AreEqual(shipLocation.End, locations[2]);
        }

        [TestMethod]
        //Test a vertical ship defined backwards (start is to the bottom of end)
        public void TestGetShipLocations4()
        {
            var shipLocation = new ShipLocation
            {
                Start = new Location
                {
                    Row = 4,
                    Column = 1
                },
                End = new Location
                {
                    Row = 6,
                    Column = 1
                }
            };

            var locations = shipLocation.GetLocations();

            Assert.AreEqual(3, locations.Count, "Should be 3 locations");
            Assert.AreEqual(shipLocation.Start, locations[0]);
            Assert.AreEqual(new Location { Row = 5, Column = 1 }, locations[1]);
            Assert.AreEqual(shipLocation.End, locations[2]);
        }




    }
}
