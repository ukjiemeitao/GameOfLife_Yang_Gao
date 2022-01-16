using badlife;
using NUnit.Framework;
using System;
using System.IO;

namespace GameOfLifeTests
{
    public class GameOfLifeTests
    {
        private string _validInput;
        private bool[][] _worldInitialization;
        private string _invalidInputWrongLength;
        private string _invalidInputWrongChar;
        private bool[][] _worldEvolveOnce;
        private bool[][] _worldEvolveTwice;
        private bool[][] _worldEvolveThreeTimes;
        private string _largerGridFilePath;
        private string _largerGridCorrectResultPath;

        [SetUp]
        public void Setup()
        {
            _largerGridFilePath = "sample_input_larger_grid.txt";
            _largerGridCorrectResultPath = "sample_input_larger_grid_result.txt";
            _validInput = "_____\r\n__*__\r\n_***_\r\n__*__\r\n_____";
            _invalidInputWrongLength = "_____\r\n__*__\r\n_***_\r\n__*__\r\n____";
            _invalidInputWrongChar = "_____\r\n__-__\r\n_***_\r\n__*__\r\n_____";

            _worldInitialization = new bool[][]{
                new bool[]{ false,false, false,false,false},
                new bool[]{ false,false, true,false,false},
                new bool[]{ false,true, true,true,false},
                new bool[]{ false,false, true,false,false},
                new bool[]{ false,false, false,false,false}
            };
            _worldEvolveOnce = new bool[][]
            {
                new bool[]{ false,false, false,false,false},
                new bool[]{ false,true, true,true,false},
                new bool[]{ false,true, false,true,false},
                new bool[]{ false,true, true,true,false},
                new bool[]{ false,false, false,false,false}
            };

            _worldEvolveTwice = new bool[][]
            {
                new bool[]{ false,false, true,false,false},
                new bool[]{ false,true, false,true,false},
                new bool[]{ true,false, false,false,true},
                new bool[]{ false,true, false,true,false},
                new bool[]{ false,false, true,false,false}
            };

            _worldEvolveThreeTimes = new bool[][]
           {
                new bool[]{ false,true, true,true,false},
                new bool[]{ true,true, true,true,true},
                new bool[]{ true,true, false,true,true},
                new bool[]{ true,true, true,true,true},
                new bool[]{ false,true, true,true,false}
           };


        }

        [Test]
        public void InitializeWorld_validInput_WorldCorrectlyPopulated()
        {
            Validator validator = new Validator();
            GameOfLife game = new GameOfLife(validator);
            game.InitializeWorld(_validInput);
            Assert.AreEqual(_worldInitialization, game.World.Value);

        }

        [Test]
        public void InitializeWorld_InvalidInputWrongLength_ThrowArgumentException()
        {
            Validator validator = new Validator();
            GameOfLife game = new GameOfLife(validator);
            var errorMessasge = Assert.Throws<ArgumentException>(() => game.InitializeWorld(_invalidInputWrongChar)).Message;
            Assert.AreEqual("Contain invalid character", errorMessasge);

        }

        [Test]
        public void InitiallizeWorld_InvalidInputWrongChar_ThrowArgumentException()
        {
            Validator validator = new Validator();
            GameOfLife game = new GameOfLife(validator);
            var errorMessasge = Assert.Throws<ArgumentException>(() => game.InitializeWorld(_invalidInputWrongLength)).Message;
            Assert.AreEqual("Input has invalid line length.", errorMessasge);
        }

        [Test]
        public void Evolve_Once_AsExpected()
        {
            Validator validator = new Validator();
            GameOfLife game = new GameOfLife(validator);
            game.InitializeWorld(_validInput);
            game.Evolve();
            Assert.AreEqual(_worldEvolveOnce, game.World.Value);
        }

        [Test]
        public void Evolve_Twice_AsExpected()
        {
            Validator validator = new Validator();
            GameOfLife game = new GameOfLife(validator);
            game.InitializeWorld(_validInput);
            game.Evolve();
            game.Evolve();
            Assert.AreEqual(_worldEvolveTwice, game.World.Value);
        }

        [Test]
        public void Evolve_ThreeTimes_AsExpected()
        {
            Validator validator = new Validator();
            GameOfLife game = new GameOfLife(validator);
            game.InitializeWorld(_validInput);
            game.Evolve();
            game.Evolve();
            game.Evolve();
            Assert.AreEqual(_worldEvolveThreeTimes, game.World.Value);
        }

        [Test]
        public void Evolve_SenventyTimes_LargerGrid_AsExpected()
        {
            string all_text;
            string result_all_test;

            using (var input = new StreamReader("sample_input_larger_grid.txt"))
            {
                all_text = input.ReadToEnd();
            }
            Validator validator = new Validator();
            GameOfLife game = new GameOfLife(validator);
            game.InitializeWorld(all_text);
            for(int i = 0; i < 70; i++)
            {
                game.Evolve();
            }

            using (var input = new StreamReader("sample_input_larger_grid_result.txt"))
            {
                result_all_test = input.ReadToEnd();
            }

            GameOfLife expectedGameResult = new GameOfLife(validator);
            expectedGameResult.InitializeWorld(result_all_test);

            Assert.AreEqual(expectedGameResult.World.Value, game.World.Value);


        }


    }
}