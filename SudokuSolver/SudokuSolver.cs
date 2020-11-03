using System;
using System.Linq;
using System.Windows.Forms;

namespace SudokuSolver
{
    public partial class SudokuSolver : Form
    {
        private const int SudokuSize = 9;

        public SudokuSolver()
        {
            InitializeComponent();
        }

        private void SolveButton_Click(object sender, EventArgs e)
        {
            var sudoku = new short[SudokuSize, SudokuSize];

            for (var i = 0; i < SudokuSize; i++)
            {
                for (var j = 0; j < SudokuSize; j++)
                {
                    var number = _sudokuPanel.GetControlFromPosition(i, j).Text;
                    if (!string.IsNullOrEmpty(number))
                    {
                        sudoku[i, j] = short.Parse(number);
                    }
                }
            }

            while (sudoku.Cast<short>().ToList().Any(s => s != 0))
            {
                for (var i = 0; i < SudokuSize; i++)
                {
                    for (var j = 0; j < SudokuSize; j++)
                    {
                        var square = sudoku[i, j];
                        if (square != 0)
                        {

                        }
                    }
                }
            }
        }
    }
}
