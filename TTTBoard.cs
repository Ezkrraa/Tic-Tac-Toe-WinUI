using Microsoft.UI;
using System;
using System.ComponentModel.Design;
using System.Threading;

public class TTTBoard
{
    public char[,] Board;

    public TTTBoard()
    {
        Board = new char[,]{ { ' ', ' ', ' ' }, { ' ', ' ', ' ' }, { ' ', ' ', ' ' } };
    }
    
    public void Reset()
    {
        Board = new char[,] { { ' ', ' ', ' ' }, { ' ', ' ', ' ' }, { ' ', ' ', ' ' } };
    }

    public bool TryMoveHere(int row, int col, char player)
    {
        if (Board[row, col] == ' ')
        {
            Board[row, col] = player;
            return true;
        }
        return false;
    }

    public bool TryMoveHere(char row, char col, char player)
    {
        return TryMoveHere(int.Parse(row.ToString()), int.Parse(col.ToString()), player);
    }

    public bool HasWon()
    {
        for (int i = 0; i < 3; i++)
        {
            if (Board[i, 0] != ' ' && Board[i, 0] == Board[i, 1] && Board[i, 1] == Board[i, 2])
            {
                return true;
            }
            else if (Board[0, i] != ' ' && Board[0, i] == Board[1, i] && Board[1, i] == Board[2, i])
            {
                return true;
            }
        }
        if (Board[0, 0] != ' ' && Board[0, 0] == Board[1,1] && Board[1,1] == Board[2,2])
        {
            return true;
        }
        else if (Board[0, 2] != ' ' && Board[0, 2] == Board[1,1] && Board[1,1] == Board[2,0]) return true;
        return false;
    }
}
