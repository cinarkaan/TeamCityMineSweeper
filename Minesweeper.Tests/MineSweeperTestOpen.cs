using NUnit.Framework;
using Minesweeper.Core;
using Minesweeper.Core.Enums;

namespace Minesweeper.Tests
{
    [TestFixture]
    [Author("Kaan Cinar", "cnrkaan98@gmail.com")]
    [Category("OpenMethod")]
    public class MineSweeperTestOpen
    {

        // We define attribute that we will use in methods
        private GameProcessor? _gameProcessor; // In order to prevent code convention which not nullable field  , we used this symbol "?"
        private Random? _random; // In order to prevent code convention which not nullable field  , we used this symbol "?"
        private int _width, _height, _mines;
        private int _openedCell;

        // We can think this method like constractor because of only one time create new random class
        [OneTimeSetUp]
        public void InitRandom ()
        {
            _random = new Random();
        }

        // We adjust game settings as randomly as well as initialize the screen sizes  
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

        [TearDown]
        public void CleanUp ()
        {
            _openedCell = 0; // Was opened cells after the game start
        }

        // We test the game for boundary level analysis to under bound value (0, 0)
        [Test]
        public void BoundaryValueAnalysisToUnderLowerBound()
        {
            try
            {
                var state = _gameProcessor.Open(-1, -1);
            } catch(Exception ex)
            {
                Assert.AreEqual("IndexOutOfRangeException", ex.GetType().Name);
            }
        }

        // we test the game for boundary values to lower bound value
        [Test]
        public void BoundaryLevelAnalysisToLowerBoundValue()
        {
            var state = _gameProcessor.Open(0, 0);
            _openedCell = _gameProcessor.GetCurrentField().Cast<PointState>().Where(s => s == PointState.Close).Count();
            Assert.AreNotEqual(_width * _height, _openedCell);
            if (state == GameState.Active)
                Assert.AreEqual(state, GameState.Active);
            else
                Assert.AreEqual(state, GameState.Lose);
        }

        // We test the game for upper boundary values [(8 , 8) , (15, 15), (15 ,29)] 
        [Test]
        public void BoundaryLevelAnalysisToUpperBoundaryValue ()
        {
            var state = _gameProcessor.Open(_width - 1, _height - 1);
            _openedCell = _gameProcessor.GetCurrentField().Cast<PointState>().Where(s => s == PointState.Close).Count();
            Assert.AreNotEqual(_width * _height, _openedCell);
            if (state == GameState.Active)
                Assert.AreEqual(state, GameState.Active);
            else
                Assert.AreEqual(state, GameState.Lose);
        }

        // We test the game for boundary level analysis to over upper bound value
        [Test]
        public void BoundaryLevelAnalysisToOverUpperBoundValue ()
        {
            try
            {
                _gameProcessor.Open(_width, _height);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("IndexOutOfRangeException",ex.GetType().Name);
            }
        }

        // we test the game for random values (Intermediate value such that 5 , 8 , 9 , 2)
        [Test]
        public void BoundaryLevelAnalysisWithRandomValues ()
        {
            int coordinate_X = _random.Next(0, _width - 1);
            int coordinate_Y = _random.Next(0, _height - 1);
            var state = _gameProcessor.Open(coordinate_X, coordinate_Y);
            _openedCell = _gameProcessor.GetCurrentField().Cast<PointState>().Where(s => s == PointState.Close).Count();
            Assert.AreNotEqual(_width * _height, _openedCell);
            if (state == GameState.Active)
                Assert.AreEqual(state, GameState.Active);
            else
                Assert.AreEqual(state, GameState.Lose);
        }

        // If we enter any value except integer , the program must throw argumentexception
        [Test]
        public void EnterAnyInputExceptIntegerValue ()
        {
            Assert.Throws<FormatException>(ExceptionBody);     
        }


        private void ExceptionBody ()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ_?=)(/£#$%&/{*-+,.;:";
            char selected = Enumerable.Repeat(chars, chars.Length).Select(s => s[_random.Next(s.Length)]).ToList().ElementAt(0);
            Console.WriteLine(selected);
            _gameProcessor.Open(int.Parse(selected.ToString()), int.Parse(selected.ToString()));
        }



    }
}