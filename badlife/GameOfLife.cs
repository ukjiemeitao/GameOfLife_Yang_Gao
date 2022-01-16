using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace badlife
{
    public class GameOfLife : IGameOfLife
    {
        private readonly IValidator _validator;
        private Lazy<bool[][]> _world;
        
        public GameOfLife(IValidator validator)
        {
            _validator = validator;
        }

        public Lazy<bool[][]> World {
            get { return _world; }
        }

        public int Columns
        {
            get { return _world.Value[0].Length; }
        }

        public int Rows
        {
            get { return _world.Value.Length; }
        }

        public void InitializeWorld(string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentNullException(nameof(input));

            string[] lines = input.Split(new string[] { "\r\n" }, StringSplitOptions.None);

            if (_validator.HasInvalidLength(lines))
                throw new ArgumentException("Input has invalid line length.");

            if (lines.Where(x => _validator.ContainInvalidChar(x)).Count() > 0)
                throw new ArgumentException("Contain invalid character");

            _world = new Lazy<bool[][]>(()=>new bool[lines.Length][]);

            for (int x = 0; x < lines.Length; ++x)
            {
                _world.Value[x] = new bool[lines[x].Length];
                for (int c = 0; c < lines[x].Length; c++)
                {
                    if (lines[x][c] == '_')
                    {
                        _world.Value[x][c] = false;
                    }
                    else if (lines[x][c] == '*')
                    {
                        _world.Value[x][c] = true;
                    }
                }
            }
        }

        public void Evolve()
        {
            //use three buffers rather than create another 2-D array
            bool[] bufferLine0 = new bool[Columns];
            bool[] buffer1 = new bool[Columns];
            bool[] buffer2 = new bool[Columns];

            int row = 1;

            try
            {
                //Store first line in bufferLine0, update first line in the grid after processing last line becuase
                //first line is last line's neighbour.
                GenerateBufferForLine((_=>true), 0, ref bufferLine0);

                while (row <= Columns + 1)
                {
                    UpdateGridWithBuffer((i => i > 2 && row % 2 == 1), ref buffer1, row);
                    UpdateGridWithBuffer((i => i > 2 && row % 2 == 0), ref buffer2, row);
                    GenerateBufferForLine((i => i % 2 == 1 && i < Columns), row, ref buffer1);
                    GenerateBufferForLine((i => i % 2 == 0 && i < Columns), row, ref buffer2);
                    row++;
                }

                bufferLine0.CopyTo(_world.Value[0], 0);
            }
            catch (Exception)
            {
                throw;
            }

        }

        private void UpdateGridWithBuffer(Func<int,bool> lineSelector, ref bool[] buffer, int row)
        {
            if (lineSelector.Invoke(row))
            {
                buffer.CopyTo(_world.Value[row - 2], 0);
                Array.Clear(buffer, 0, Columns);
            }
          
        }

        private void GenerateBufferForLine(Func<int,bool> lineSelector, int row, ref bool[] buffer)
        {
            if (lineSelector.Invoke(row))
            {
                try
                {
                    for (var k = 0; k < Columns; ++k)
                    {
                        int neighbours = GetNeighbourNum(row, k);

                        if (_world.Value[row][k] && neighbours < 2) buffer[k] = false;
                        if (_world.Value[row][k] && neighbours == 2 || neighbours == 3) buffer[k] = true;
                        if (_world.Value[row][k] && neighbours > 3) buffer[k] = false;
                        if (!_world.Value[row][k] && neighbours == 3) buffer[k] = true;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
              
            }
          
        }

        private int GetNeighbourNum(int row, int col)
        {
            var neighbourCount = 0;

            try
            {
                if (_world.Value[row - 1 < 0 ? Rows - 1 : row - 1][col - 1 < 0 ? Columns - 1 : col - 1]) neighbourCount++;
                if (_world.Value[row - 1 < 0 ? Rows - 1 : row - 1][col]) neighbourCount++;
                if (_world.Value[row - 1 < 0 ? Rows - 1 : row - 1][col + 1 == Columns ? 0 : col + 1]) neighbourCount++;

                if (_world.Value[row][col - 1 < 0 ? Columns - 1 : col - 1]) neighbourCount++;
                if (_world.Value[row][col + 1 == Columns ? 0 : col + 1]) neighbourCount++;

                if (_world.Value[row + 1 == Rows ? 0 : row + 1][col - 1 < 0 ? Columns - 1 : col - 1]) neighbourCount++;
                if (_world.Value[row + 1 == Rows ? 0 : row + 1][col]) neighbourCount++;
                if (_world.Value[row + 1 == Rows ? 0 : row + 1][col + 1 == Columns ? 0 : col + 1]) neighbourCount++;
            }
            catch (Exception)
            {

                throw;
            }

            return neighbourCount;
        }

        public void OutputNewWorld()
        {
            try
            {
                for (int a = 0; a < _world.Value.Length; a++)
                {
                    string line = "";
                    for (int b = 0; b < _world.Value[0].Length; ++b)
                    {
                        if (_world.Value[a][b])
                            line = line + "*";
                        else
                        {
                            line = line + "_";
                        }
                    }
                    Console.WriteLine(line);
                }
            }
            catch (Exception)
            {
                throw;
            }
          
        }
    }
}
