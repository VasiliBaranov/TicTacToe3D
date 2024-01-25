using System.Collections.Generic;
using TicTacToe3D.GameInfo;

namespace TicTacToe3D.AI
{
    public class HardComputerPlayer:ComputerPlayer
    {
        protected override Cell ChooseTheMostAppropriateCellFromEvaluatedCells(List<ValuableCell> evaluatedCells)
        {
            evaluatedCells.Sort();

            evaluatedCells.Reverse();

            return evaluatedCells[0];
        }

        protected override Cell ChooseTheMostAppropriateCellWhenGameFieldIsEmpty()
        {
            return new Cell(1, 1, 1);
        }
    }
}
