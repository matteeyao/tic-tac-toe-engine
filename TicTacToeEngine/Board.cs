using System;
using System.Collections.Generic;
using System.Linq;

namespace App
{
    public class Board
    {
        public enum Marks { x, o };
        public enum Dimensions
        {
            ThreeByThree = 3,
            FourByFour = 4,
            FiveByFive = 5
        };
        private readonly int dimension;
        private readonly string[] grid;

        public Board() : this(Dimensions.ThreeByThree, null) { }

        public Board(Dimensions dimension, string[] grid = null)
        {
            this.dimension = (int) dimension;
            this.grid = grid ?? GenerateGrid((int) dimension);
        }

        public int GetDimension()
        {
            return this.dimension;
        }

        public string[,] GetGrid()
        {
            return this.GetRows();
        }

        public string GetField(int position)
        {
            return this.grid[position];
        }

        public void SetField(int position, string mark)
        {
            this.grid[position] = mark;
        }

        public virtual bool IsValidField(int position)
        {
            int numFields = (int)Math.Pow(this.dimension, 2);
            return !(position < 0 || numFields <= position);
        }

        public virtual bool IsEmptyField(int position)
        {
            return !Enum.IsDefined(typeof(Marks), this.GetField(position));
        }
        
        public Board Duplicate()
        {
            string[] dupedGrid = (string[]) this.grid.Clone();
            return new Board((Dimensions) Enum.ToObject(typeof(Dimensions), this.dimension), dupedGrid);
        }

        public string Winner()
        {
            string[,] rows = this.GetRows();
            string[,] cols = this.GetColumns();
            string[,] diags = this.GetDiagonals();

            return WinnerHelper(rows) ?? WinnerHelper(cols) ?? WinnerHelper(diags);
        }

        private string WinnerHelper(string[,] sequences)
        {
            int rowLength = sequences.GetLength(0);
            int colLength = sequences.GetLength(1);
            for (int rowIdx = 0; rowIdx < rowLength; rowIdx++)
            {
                string[] sequence = new string[colLength]; 
                for (int colIdx = 0; colIdx < colLength; colIdx++)
                {
                    sequence[colIdx] = sequences[rowIdx, colIdx];
                }

                if (sequence.Distinct().Count() == 1)
                {
                    return sequence[0];
                }
            }

            return null;
        }
        
        public bool HasWinner()
        {
            return this.Winner() != null;
        }

        public bool IsTied()
        {
            return !this.HasWinner() && this.IsFilled();
        }
        
        private bool IsFilled()
        {
            foreach (string field in this.grid)
            {
                Int32.TryParse(field, out int val);
                if (val != 0)
                {
                    return false;
                }
            }

            return true;
        }

        protected bool Equals(Board other)
        {
            return dimension == other.dimension && Enumerable.SequenceEqual(grid, other.grid);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Board) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(dimension, grid);
        }

        private static string[] GenerateGrid(int dimension)
        {
            int numFields = (int)Math.Pow(dimension, 2);
            return Enumerable.Range(1, numFields).Select(i => i.ToString()).ToArray();
        }

        private string[,] GetRows()
        {
            int dim = this.dimension;
            string[,] rows = new string[dim, dim];
            for (int i = 0; i < this.grid.Length; i++)
            {
                rows[i / dim, i % dim] = this.grid[i];
            }

            return rows;
        }
        
        private string[,] GetColumns()
        {
            int dim = this.dimension;
            string[,] cols = new string[dim, dim];
            for (int i = 0; i < this.grid.Length; i++)
            {
                cols[i % dim, i / dim] = this.grid[i];
            }

            return cols;
        }
        
        private string[,] GetDiagonals()
        {
            int dim = this.dimension;
            string[,] diags = new string[2, dim];
            for (int i = 0; i < this.grid.Length; i += (dim + 1))
            {
                diags[0, i % dim] = this.grid[i];
            }

            for (int i = dim - 1; i < this.grid.Length - 1; i += (dim - 1))
            {
                diags[1, i / dim] = this.grid[i];
            }

            return diags;
        }
    }
}
