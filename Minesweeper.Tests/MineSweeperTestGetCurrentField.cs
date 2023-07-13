using Minesweeper.Core;
using Minesweeper.Core.Enums;
using Minesweeper.Core.Models;
using NUnit.Framework;

namespace Minesweeper.Tests
{
    [TestFixture]
    [Author("Kaan Cinar", "cnrkaan98@gmail.com")]
    [Category("GetCurrentFieldMehod")]
    public class MineSweeperTestGetCurrentField
    {
        // We define attribute that we will use in methods
        private GameProcessor _gameProcessor;
        private Random _random;
        private int _coordinate_X, _coordinate_Y;
        private int _width, _height, _mines;


        [OneTimeSetUp]
        public void InitRandomClass ()
        {
            _random = new Random();
        }

        [SetUp]
        public void Init ()
        {
            _width = _random.Next(9, 16); // We define width of the plane as randomly way (independent from Difficulty Manager)
            _height = _random.Next(9, 30); // We define _height of the plane as randomly way (independent from Difficulty Manager) 
            // We indicate the mines count which is located in the plane as randomly (independent from Difficulty Manager)
            if (_width * _height < 100) // Easy
                _mines = _random.Next(10, 15);
            else if (_width * _height > 100 && _width * _height < 256) // Normal
                _mines = _random.Next(15, 40);
            else // Hard
                _mines = _random.Next(40, 99);

            // We initialize the game , each time before passed new test case
            var field = FieldGenerator.GetRandomField(_width, _height, _mines);
            _gameProcessor = new GameProcessor(field);
        }

        // We test total number of cells whether is opened or not.
        [Test]
        public void IsOpenedAnyCellSoFar ()
        {
            var field = _gameProcessor.GetCurrentField();
            int closedCell = field.Cast<PointState>().Where(s => s == PointState.Close).Count();
            Assert.AreEqual(_width * _height, closedCell);
        }

        // We test the mines which is printed on the screen whether equals total mines number or not.
        [Test]
        public void IsOpenedAnyMineSoFar()
        {

            _coordinate_X = _random.Next(0, _width - 1);
            _coordinate_Y = _random.Next(0, _height - 1);

            var state = _gameProcessor.Open(_coordinate_X, _coordinate_Y);
            var field = _gameProcessor.GetCurrentField();

            while (state != GameState.Lose)
            {
                _coordinate_X = _random.Next(0, _width - 1);
                _coordinate_Y = _random.Next(0, _height - 1);

                state = _gameProcessor.Open(_coordinate_X, _coordinate_Y);
                field = _gameProcessor.GetCurrentField();
            }

            int numberOfMine = field.Cast<PointState>().Where(s => s == PointState.Mine).Count();

            Assert.Multiple(() =>
            {
                Assert.AreEqual(numberOfMine, _mines);
                Assert.AreEqual(GameState.Lose, state);
            });
        }

        // We test that if the user triggered same value multiple times , How would be changes on the screen ?
        [Test]
        public void IsOpenedTwoTimesSameCell ()
        {
            _coordinate_X = _random.Next(0, _width - 1);
            _coordinate_Y = _random.Next(0, _height - 1);

            var status = _gameProcessor.Open(_coordinate_X, _coordinate_Y);
            var beforeField = _gameProcessor.GetCurrentField();

            status = _gameProcessor.Open(_coordinate_X, _coordinate_Y);
            var afterField = _gameProcessor.GetCurrentField();
        
            bool result = beforeField.Cast<PointState>().SequenceEqual(afterField.Cast<PointState>());

            Assert.True(result);
        }

        // We test neighbor values whether is printed on the console or not
        [Test]
        public void IsOpenedNeighborsCount()
        {
            _coordinate_X = _random.Next(0, _width - 1);
            _coordinate_Y = _random.Next(0, _height - 1);

            var status = _gameProcessor.Open(_coordinate_X, _coordinate_Y);
            var field = _gameProcessor.GetCurrentField();

            var reduced = field.Cast<PointState>().Where(f => f != PointState.Close && f != PointState.Mine);

            string neighbor = reduced.ElementAt(0).ToString();

            switch(neighbor)
            {
                case "Neighbors0":
                    Assert.AreEqual(reduced.ElementAt(0), PointState.Neighbors0);
                    break;
                case "Neighbors1":
                    Assert.AreEqual(reduced.ElementAt(0), PointState.Neighbors1);
                    break;
                case "Neighbors2":
                    Assert.AreEqual(reduced.ElementAt(0), PointState.Neighbors2);
                    break;
                case "Neighbors3":
                    Assert.AreEqual(reduced.ElementAt(0), PointState.Neighbors3);
                    break;
                case "Neighbors4":
                    Assert.AreEqual(reduced.ElementAt(0), PointState.Neighbors4);
                    break;
                case "Neighbors5":
                    Assert.AreEqual(reduced.ElementAt(0), PointState.Neighbors5);
                    break;
                case "Neighbors6":
                    Assert.AreEqual(reduced.ElementAt(0), PointState.Neighbors6);
                    break;
                case "Neighbors7":
                    Assert.AreEqual(reduced.ElementAt(0), PointState.Neighbors7);
                    break;
                case "Neighbors8":
                    Assert.AreEqual(reduced.ElementAt(0), PointState.Neighbors8);
                    break;
            }

        }

    }
}
