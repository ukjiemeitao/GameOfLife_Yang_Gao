using System;
using System.Collections.Generic;
using badlife;
using System.Text;
using NUnit.Framework;

namespace GameOfLifeTests
{
    public class ValidatorTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void ContainInvalidChar_HasInValidChar_True()
        {
            string invalid = "____-__";
            Validator validator = new Validator();
            Assert.IsTrue(validator.ContainInvalidChar(invalid));
        }

        [Test]
        public void ContainInvalidChar_HasNoInValidChar_False()
        {
            string valid = "______";
            Validator validator = new Validator();
            Assert.IsFalse(validator.ContainInvalidChar(valid));
        }

        [Test]
        public void HasInvalidLength_LineWithDifferentLength_True()
        {
            string[] lines = new string[]
            {
                "_____",
                "*****",
                "____"
            };
            Validator validator = new Validator();
            Assert.IsTrue(validator.HasInvalidLength(lines));

        }

        [Test]
        public void HasInvalidLength_LineWithSameLength_False()
        {
            string[] lines = new string[]
            {
                "_____",
                "*****",
                "_____"
            };
            Validator validator = new Validator();
            Assert.IsFalse(validator.HasInvalidLength(lines));

        }

    }
}
