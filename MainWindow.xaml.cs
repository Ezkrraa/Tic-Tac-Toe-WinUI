using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Perception.Spatial;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Tic_Tac_Toe_Gui
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private TTTBoard board = new();
        private char currentPlayer = 'O';
        private int currentPlayedIndex = 0;

        private Dictionary<char, char> mapOne = new Dictionary<char, char> { { 'A', '0' }, { 'B', '1' }, { 'C', '2' } };
        private Dictionary<char, char> mapTwo = new Dictionary<char, char> { { '1', '0' }, { '2', '1' }, { '3', '2' } };
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void myButton_Click(object sender, RoutedEventArgs e)
        {
            ((Button)sender).Tag = "HAHAHA";
        }
        private async void tryMove(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var coords = button.Name;
            char row = mapOne[coords[0]];
            char col = mapTwo[coords[1]];
            if (board.TryMoveHere(row, col, currentPlayer))
            {
                System.Diagnostics.Debug.WriteLine($"Player: {currentPlayer}");
                currentPlayedIndex ++;
                if (board.HasWon())
                {
                    System.Diagnostics.Debug.WriteLine($"{currentPlayer} won!");
                    ShowWinPopup();
                    Reset();
                }
                else if (currentPlayedIndex >= 9)
                {
                    Reset();
                    while(true)
                    {
                        if (!Winner.IsOpen) break;
                        Thread.Sleep(20);
                    }
                }
                else
                {
                    switchPlayer();
                    updateBoard();
                }
            }
            else
            {
                tryMove(sender, e);
            }
        } 

        private void switchPlayer()
        {
            switch (currentPlayer)
            {
                case 'X':
                    currentPlayer = 'O';
                    break;
                case 'O':
                    currentPlayer = 'X';
                    break;
                default:
                    break;
            }
        }

        private void updateBoard()
        {
            A1.Content = board.Board[0, 0];
            A2.Content = board.Board[0, 1];
            A3.Content = board.Board[0, 2];
            B1.Content = board.Board[1, 0];
            B2.Content = board.Board[1, 1];
            B3.Content = board.Board[1, 2];
            C1.Content = board.Board[2, 0];
            C2.Content = board.Board[2, 1];
            C3.Content = board.Board[2, 2];
        }

        private void ShowWinPopup()
        {
            // always show before resetting as that would reset the winner too
            TextBlock TextElement = Winner.FindName("WinPopupText") as TextBlock;
            System.Diagnostics.Debug.WriteLine($"{currentPlayer} won!");
            TextElement.Text = $"{currentPlayer} won!";
            Winner.IsOpen = true;
        }

        private void CloseWinPopup(object sender, RoutedEventArgs e)
        {
            if (Winner.IsOpen)
            {
                Winner.IsOpen = false;
            }
        }

        private void Reset()
        {
            board.Reset();
            updateBoard();
            currentPlayer = 'X';
            currentPlayedIndex = 0;
        }
    }
}
