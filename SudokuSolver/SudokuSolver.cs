using System;
using System.Windows.Forms;

namespace SudokuSolver
{
    public partial class SudokuSolver : Form
    {
        private const int SudokuSize = 9;
        private const int SquareSize = 3;

        public SudokuSolver()
        {
            InitializeComponent();
        }

        private void SolveButton_Click(object sender, EventArgs e)
        {
            var sudoku = new short[SudokuSize, SudokuSize];

            for (var rowIndex = 0; rowIndex < SudokuSize; rowIndex++)
            {
                for (var columnIndex = 0; columnIndex < SudokuSize; columnIndex++)
                {
                    var number = _sudokuPanel.GetControlFromPosition(rowIndex, columnIndex).Text;
                    if (!string.IsNullOrEmpty(number))
                    {
                        sudoku[rowIndex, columnIndex] = short.Parse(number);
                    }
                }
            }

            var solved = SolveSudoku(sudoku, 0, 0);

            if (solved)
            {
                for (var rowIndex = 0; rowIndex < SudokuSize; rowIndex++)
                {
                    for (var columnIndex = 0; columnIndex < SudokuSize; columnIndex++)
                    {
                        _sudokuPanel.GetControlFromPosition(rowIndex, columnIndex).Text = sudoku[rowIndex, columnIndex].ToString();
                    }
                }
            }
        }

        private bool SolveSudoku(short[,] sudoku, int rowIndex, int columnIndex)
        {
            if (rowIndex == SudokuSize - 1 && columnIndex == SudokuSize)
            {
                return true;
            }

            if (columnIndex == SudokuSize)
            {
                rowIndex++;
                columnIndex = 0;
            }

            if (sudoku[rowIndex,columnIndex] != 0)
            {
                return SolveSudoku(sudoku, rowIndex, columnIndex + 1);
            }

            for (var number = 1; number <= SudokuSize; number++)
            {
                if (isValid(sudoku, rowIndex, columnIndex, number))
                {
                    sudoku[rowIndex,columnIndex] = (short)number;

                    if (SolveSudoku(sudoku, rowIndex, columnIndex + 1))
                    {
                        return true;
                    }
                }

                sudoku[rowIndex,columnIndex] = 0;
            }

            return false;
        }

        private bool isValid(short[,] sudoku, int row, int column, int number)
        {
            for (var x = 0; x < SudokuSize; x++)
            {
                if (sudoku[x,column] == number)
                {
                    return false;
                }
            }
                    
            for (var y = 0; y < SudokuSize; y++)
            {
                if (sudoku[row,y] == number)
                {
                    return false;
                }
            }

            var startRow = row - row % SquareSize;
            var startCol = column - column % SquareSize;

            for (var i = 0; i < SquareSize; i++)
            {
                for (var j = 0; j < SquareSize; j++)
                {
                    if (sudoku[i + startRow,j + startCol] is number)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private void TextBoxInputLogic(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            var textBoxText = textBox.Text;
            if (string.IsNullOrEmpty(textBoxText))
            {
                return;
            }

            if (textBoxText.Length > 1)
            {
                textBoxText = textBoxText[0].ToString();
            }

            textBox.Text = CheckTextIsValidShort(textBoxText)
                ? textBoxText
                : string.Empty;

            textBox.SelectionStart = 1;
        }

        private bool CheckTextIsValidShort(string text) =>
            Int16.TryParse(text, out short result) && result != 0;
    }
}
