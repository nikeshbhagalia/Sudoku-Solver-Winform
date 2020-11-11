using System;
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

            var solved = SolveSudoku(sudoku, 0, 0);

            if (solved)
            {
                for (var i = 0; i < SudokuSize; i++)
                {
                    for (var j = 0; j < SudokuSize; j++)
                    {
                        _sudokuPanel.GetControlFromPosition(i, j).Text = sudoku[i, j].ToString();
                    }
                }
            }
        }

        private bool SolveSudoku(short[,] sudoku, int row, int column)
        {
            if (row == SudokuSize - 1 && column == SudokuSize)
                return true;

            if (column == SudokuSize)
            {
                row++;
                column = 0;
            }

            if (sudoku[row,column] != 0)
            {
                return SolveSudoku(sudoku, row, column + 1);
            }

            for (var number = 1; number <= SudokuSize; number++)
            {
                if (isValid(sudoku, row, column, number))
                {
                    sudoku[row,column] = (short)number;

                    if (SolveSudoku(sudoku, row, column + 1))
                        return true;
                }

                sudoku[row,column] = 0;
            }

            return false;
        }

        private bool isValid(short[,] sudoku, int row, int column, int number)
        {
            for (var y = 0; y <= 8; y++)
                if (sudoku[row,y] == number)
                    return false;

            for (var x = 0; x <= 8; x++)
                if (sudoku[x,column] == number)
                    return false;

            var startRow = row - row % 3;
            var startCol = column - column % 3;

            for (var i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (sudoku[i + startRow,j + startCol] == number)
                        return false;

            return true;
        }

        private void TextBoxInputLogic(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            var textBoxText = textBox.Text;
            if (textBoxText == string.Empty)
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
