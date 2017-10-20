﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace twozerofoureight
{
    class TwoZeroFourEightModel : Model
    {
        protected int boardSize; // default is 4
        protected int[,] board;
        protected Random rand;
        protected int score = 0;
        protected bool Move = false;
        protected int[,] boardcopy;
        protected bool Over;

        public TwoZeroFourEightModel() : this(4)
        {
            // default board size is 4 
        }

        public int[,] GetBoard()
        {
            return board;
        }

        public int Getscore()
        {
            return score;
        }

        public bool GetOver()
        {
            Over = true;
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize - 1; j++)
                {
                    if(board[i,j] == 0 || board[i, j] == board[i, j + 1])
                    {
                        Over = false;
                        break;
                    }
                }
            }
            for (int j = 0; j < boardSize; j++)
            {
                for (int i = 0; i < boardSize - 1; i++)
                {
                    if (board[i, j] == 0 || board[i, j] == board[i+1, j])
                    {
                        Over = false;
                        break;
                    }
                }
            }
            return Over;
        }

        public TwoZeroFourEightModel(int size)
        {
            boardSize = size;
            board = new int[boardSize, boardSize];
            var range = Enumerable.Range(0, boardSize);
            foreach(int i in range) {
                foreach(int j in range) {
                    board[i,j] = 0;
                }
            }
            rand = new Random();
            board = Random(board);
            board = Random(board);
            NotifyAll();         
        }

        private int[,] Random(int[,] input)
        {
            while (true)
            {
                int x = rand.Next(boardSize);
                int y = rand.Next(boardSize);
                if (board[x, y] == 0)
                    {
                        board[x, y] = 2;
                        break;
                    }
                }    
            return input;
        }

        public void PerformDown()
        {
            boardcopy = new int[boardSize, boardSize];
            int[] buffer;
            int[] copyJ;
            int pos;
            int[] rangeX = Enumerable.Range(0, boardSize).ToArray();
            int[] rangeY = Enumerable.Range(0, boardSize).ToArray();
            Array.Reverse(rangeY);
            foreach (int i in rangeX)
            {
                pos = 0;
                buffer = new int[4];
                copyJ = new int[4];
                foreach (int k in rangeX)
                {
                    buffer[k] = 0;
                }
                //shift left
                foreach (int j in rangeY)
                {
                    copyJ[j] = board[j, i];
                    if (board[j, i] != 0)
                    {
                        buffer[pos] = board[j, i];
                        pos++;
                    }
                }
                // check duplicate
                foreach (int j in rangeX)
                {
                    if (j > 0 && buffer[j] != 0 && buffer[j] == buffer[j - 1])
                    {
                        buffer[j - 1] *= 2;
                        buffer[j] = 0;
                        score += buffer[j - 1];
                       
                    }
                }
                // shift left again
                pos = 3;
                foreach (int j in rangeX)
                {
                    if (buffer[j] != 0)
                    {
                        board[pos, i] = buffer[j];
                        pos--;
                    }
                }
                // copy back
                for (int k = pos; k != -1; k--)
                {
                    board[k, i] = 0;
                }
                for (int q = 0; q < boardSize; q++)
                {
                    if(copyJ[q] != board[q, i])
                    {
                        Move = true;
                    }
                }
            }             
            if(Move)
            {
                board = Random(board);              
                Move = false;
            }          
            NotifyAll();                     
        }

        public void PerformUp()
        {
            int[] buffer;
            int pos;
            int[] copyJ;
            int[] range = Enumerable.Range(0, boardSize).ToArray();
            foreach (int i in range)
            {
                pos = 0;
                buffer = new int[4];
                copyJ = new int[4];
                foreach (int k in range)
                {
                    buffer[k] = 0;
                }
                //shift left
                foreach (int j in range)
                {
                    copyJ[j] = board[j, i];
                    if (board[j, i] != 0)
                    {
                        buffer[pos] = board[j, i];
                        pos++;           
                    }
                }
                // check duplicate
                foreach (int j in range)
                {
                    if (j > 0 && buffer[j] != 0 && buffer[j] == buffer[j - 1])
                    {
                        buffer[j - 1] *= 2;
                        buffer[j] = 0;
                        score += buffer[j - 1];
                    }
                }
                // shift left again
                pos = 0;
                foreach (int j in range)
                {
                    if (buffer[j] != 0)
                    {
                        board[pos, i] = buffer[j];
                        pos++;                    
                    }
                }
                // copy back
                for (int k = pos; k != boardSize; k++)
                {
                    board[k, i] = 0;
                }
                for (int q = 0; q < boardSize; q++)
                {
                    if (copyJ[q] != board[q, i])
                    {
                        Move = true;
                    }
                }
            }
            if (Move)
            {
                board = Random(board);              
                Move = false;
            }
            NotifyAll();
        }

        public void PerformRight()
        {
            int[] buffer;
            int pos;
            int[] copyJ;
            int[] rangeX = Enumerable.Range(0, boardSize).ToArray();
            int[] rangeY = Enumerable.Range(0, boardSize).ToArray();
            Array.Reverse(rangeX);
            foreach (int i in rangeY)
            {
                pos = 0;
                buffer = new int[4];
                copyJ = new int[4];
                foreach (int k in rangeY)
                {
                    buffer[k] = 0;
                }
                //shift left
                foreach (int j in rangeX)
                {
                    copyJ[j] = board[i, j];
                    if (board[i, j] != 0)
                    {
                        buffer[pos] = board[i, j];
                        pos++;
                        
                    }
                }
                // check duplicate
                foreach (int j in rangeY)
                {
                    if (j > 0 && buffer[j] != 0 && buffer[j] == buffer[j - 1])
                    {
                        buffer[j - 1] *= 2;
                        buffer[j] = 0;
                        score += buffer[j - 1];
                    }
                }
                // shift left again
                pos = 3;
                foreach (int j in rangeY)
                {
                    if (buffer[j] != 0)
                    {
                        board[i, pos] = buffer[j];
                        pos--;
                        
                    }
                }
                // copy back
                for (int k = pos; k != -1; k--)
                {
                    board[i, k] = 0;
                }
                for (int q = 0; q < boardSize; q++)
                {
                    if (copyJ[q] != board[i, q])
                    {
                        Move = true;
                    }
                }
            }
            if (Move)
            {
                board = Random(board);
               
                Move = false;
            }
            NotifyAll();
        }

        public void PerformLeft()
        {
            int[] buffer;
            int pos;
            int[] copyJ;
            int[] range = Enumerable.Range(0, boardSize).ToArray();
            foreach (int i in range)
            {
                pos = 0;
                buffer = new int[boardSize];
                copyJ = new int[4];
                foreach (int k in range)
                {
                    buffer[k] = 0;
                }
                //shift left
                foreach (int j in range)
                {
                    copyJ[j] = board[i, j];
                    if (board[i, j] != 0)
                    {
                        buffer[pos] = board[i, j];
                        pos++;
                    }
                }
                // check duplicate
                foreach (int j in range)
                {
                    if (j > 0 && buffer[j] != 0 && buffer[j] == buffer[j - 1])
                    {
                        buffer[j - 1] *= 2;
                        buffer[j] = 0;
                        score += buffer[j - 1];
                    }
                }
                // shift left again
                pos = 0;
                foreach (int j in range)
                {
                    if (buffer[j] != 0)
                    {
                        board[i, pos] = buffer[j];
                        pos++;
                       
                    }
                }
                for (int k = pos; k != boardSize; k++)
                {
                    board[i, k] = 0;
                }
                for (int q = 0; q < boardSize; q++)
                {
                    if (copyJ[q] != board[i, q])
                    {
                        Move = true;
                    }
                }
            }
            if (Move)
            {
                board = Random(board);
                
                Move = false;
            }
            NotifyAll();
        }
    }
}
