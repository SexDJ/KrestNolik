using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace krestiki_noliki
{
    public partial class GameWindow : Window
    {
        private char[,] board = new char[3, 3];
        private char currentPlayer = 'X';

        public GameWindow()
        {
            InitializeComponent();
            ResetGame();
        }

        private void ResetGame()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i, j] = ' ';
                }
            }
            currentPlayer = 'X';
            UpdateBoard();
        }

        private void UpdateBoard()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Button button = (Button)FindName($"button{i}{j}");
                    button.Content = board[i, j];
                    button.Foreground = (board[i, j] == 'X') ? Brushes.Blue : Brushes.Red;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int row = int.Parse(button.Name[6].ToString());
            int col = int.Parse(button.Name[7].ToString());

            if (board[row, col] == ' ')
            {
                board[row, col] = currentPlayer;
                UpdateBoard();
                CheckWin();
                currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
            }
        }

        private void CheckWin()
        {
            // Check rows
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2] && board[i, 0] != ' ')
                {
                    MessageBox.Show($"{board[i, 0]} Выйграл!");
                    ResetGame();
                    return;
                }
            }

            // Check columns
            for (int i = 0; i < 3; i++)
            {
                if (board[0, i] == board[1, i] && board[1, i] == board[2, i] && board[0, i] != ' ')
                {
                    MessageBox.Show($"{board[0, i]} Выйграл!");
                    ResetGame();
                    return;
                }
            }

            // Check diagonals
            if (board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2] && board[0, 0] != ' ')
            {
                MessageBox.Show($"{board[0, 0]} Выйграл!");
                ResetGame();
                return;
            }
            if (board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0] && board[0, 2] != ' ')
            {
                MessageBox.Show($"{board[0, 2]} Выйграл!");
                ResetGame();
                return;
            }

            // Check for a tie
            bool isTie = true;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == ' ')
                    {
                        isTie = false;
                        break;
                    }
                }
                if (!isTie)
                    break;
            }
            if (isTie)
            {
                MessageBox.Show("Ничья!");
                ResetGame();
            }
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            ResetGame();
        }
    }
}

