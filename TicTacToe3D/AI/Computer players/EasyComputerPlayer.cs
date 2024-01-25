using System;
using System.Collections.Generic;
using TicTacToe3D.GameInfo;

namespace TicTacToe3D.AI
{
    public class EasyComputerPlayer:ComputerPlayer
    {
        #region Fields

        private int _cellValueToCompareTo;

        #endregion

        protected override Cell ChooseTheMostAppropriateCellFromEvaluatedCells(List<ValuableCell> evaluatedCells)
        {
            evaluatedCells.Sort();

            evaluatedCells.Reverse();

            _cellValueToCompareTo = evaluatedCells[0].Value;

            int lastIndex = evaluatedCells.FindLastIndex(ValueEqualToComparedValue);

            if (evaluatedCells.Count > lastIndex)
            {
                _cellValueToCompareTo = evaluatedCells[lastIndex + 1].Value;

                lastIndex = evaluatedCells.FindLastIndex(ValueEqualToComparedValue);
            }

            Random randomizer = new Random();

            int randomCellIndex = randomizer.Next(0, lastIndex);

            return evaluatedCells[randomCellIndex];
        }

        protected override Cell ChooseTheMostAppropriateCellWhenGameFieldIsEmpty()
        {
            Random randomizer = new Random();
            int i = randomizer.Next(0, GameInfo.GameField.GameFieldParameters.SizeAlongX);
            int j = randomizer.Next(0, GameInfo.GameField.GameFieldParameters.SizeAlongY);
            int k = randomizer.Next(0, GameInfo.GameField.GameFieldParameters.SizeAlongZ);
            return new Cell(i, j, k);
        }

        /// <summary>
        /// Determines if a Valuable cell value equals a private field _cellValueToCompareTo; method corresponds to System.Predicate delegate 
        /// (which is used in List.FindLastIndex methods)
        /// </summary>
        /// <param name="valuableCell">Compared cell</param>
        /// <returns>True, if valuableCell value equals a private field _cellValueToCompareTo</returns>
        private bool ValueEqualToComparedValue(ValuableCell valuableCell)
        {
            return (valuableCell.Value == _cellValueToCompareTo);
        }
    }
}
