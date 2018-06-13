#region
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
#endregion

namespace WindowsFormsApp1
{
    enum MatrixType : int
    {
        ZeroConstant = 0,
        OneConstant = 1,
        Inversed = 2
    }
    class Matrix:IElem<Matrix>
    {
        int type = -1;
        int[,] matrix;
        int columns, rows, sign = 1;
        public Matrix(string sourceMatrix)
        {
            Proceed(sourceMatrix);
        }
        public Matrix(int type, string sourceMatrix, int sign):this(sourceMatrix, sign)
        {
            this.type = type;
        }
        public Matrix(string sourceMatrix, int sign)
        {
            Proceed(sourceMatrix);
            this.sign = sign;
        }
        public Matrix(int rows, int columns)
        {
            matrix = new int[rows, columns];
            this.rows = rows;
            this.columns = columns;
        }
        public Matrix(MatrixType type, int size)
        {
            this.type = (int)type;
            matrix = new int[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (type == MatrixType.OneConstant && i == j)
                    {
                        matrix[i, j] = 1;
                    }
                }
            }
            rows = columns = size;
        }
        void Proceed(string sourceMatrix)
        {
            sourceMatrix = sourceMatrix.Trim();
            int index = 0;
            List<List<int>> array = new List<List<int>>();
            while (sourceMatrix[index] != ']')
            {
                List<int> list = new List<int>();
                index++;
                while (sourceMatrix[index] != ';' && sourceMatrix[index] != ']')
                {
                    string number = "";
                    index++;
                    while (sourceMatrix[index] != ' ' && sourceMatrix[index] != ';' && sourceMatrix[index] != ']')
                    {
                        number += sourceMatrix[index].ToString();
                        index++;
                    }
                    int d = 0;
                    if (int.TryParse(number, out d))
                    {
                        list.Add(int.Parse(number) * sign);
                    }
                    
                }
                array.Add(list);
            }
            columns = GetLengthOfLongest(array);
            rows = array.Count;
            matrix = new int[rows, columns];
            FillMatrix(array);
        }
        void FillMatrix(List<List<int>> array)
        {
            for (int i = 0; i < array.Count; i++)
            {
                for (int j = 0; j < array[i].Count; j++)
                {
                    matrix[i, j] = array[i][j];
                }
            }
        }
        int GetLengthOfLongest(List<List<int>> array)
        {
            int num = array[0].Count;
            for (int i = 1; i < array.Count; i++)
            {
                if (array[i].Count > num)
                {
                    num = array[i].Count;
                }
            }
            return num;
        }
        public int this[int row, int column]
        {
            get
            {
                return matrix[row, column];
            }
            set
            {
                matrix[row, column] = value;
            }
        }
        public static Matrix operator +(Matrix m1, Matrix m2)
        {
            Matrix result = new Matrix(m1.rows, m1.columns);
            if (m1.rows == m2.rows && m1.columns == m2.columns)
            {
                for (int i = 0; i < m1.rows; i++)
                {
                    for (int j = 0; j < m1.columns; j++)
                    {
                        result[i, j] = m1[i, j] + m2[i, j];
                    }
                }
                return result;
            }
            else
            {
                throw new Exception("Matrix size difference");
            }
        }
        public static Matrix operator -(Matrix m1, Matrix m2)
        {
            Matrix result = new Matrix(m1.rows, m1.columns);
            if (m1.rows == m2.rows && m1.columns == m2.columns)
            {
                for (int i = 0; i < m1.rows; i++)
                {
                    for (int j = 0; j < m1.columns; j++)
                    {
                        result[i, j] = m1[i, j] - m2[i, j];
                    }
                }
                return result;
            }
            else
            {
                throw new Exception("Matrix size difference");
            }
        }
        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            Matrix result;
            Matrix a, b;
            if (m1.rows == m2.columns)
            {
                a = m1;
                b = m2;
            }
            else if (m2.rows == m1.columns)
            {
                a = m2;
                b = m1;
            }
            else
            {
                throw new Exception("Matrix size difference");
            }
            result = new Matrix(a.rows, b.columns);
            for (int indexARow = 0, indexBColumn = 0; indexARow < a.rows && indexBColumn < b.columns; indexARow++, indexBColumn++)
            {
                int sum = 0;
                for (int i = 0; i < a.rows; i++)
                {
                    sum += a[indexARow, i] * b[i, indexBColumn];
                }
                result[indexARow, indexBColumn] = sum;
            }
            return result;
        }
        public Matrix Inverse(Matrix m1)
        {
            if (m1.columns == m1.rows)
            {
                Matrix m = (Matrix)m1.MemberwiseClone();
                int det = Determinant(m.matrix);
                for (int i = 0; i < m.rows; i++)
                {
                    for (int j = 0; j < m.columns; j++)
                    {
                        m[i, j] /= det;
                    }
                }
                return m;
            }
            else
            {
                throw new Exception("Not square matrix");
            }
        }
        int Determinant(int[,] matrix)
        {
            int n = int.Parse(System.Math.Sqrt(matrix.Length).ToString());
            int nm1 = n - 1;
            int kp1;
            int p;
            int det = 1;
            for (int k = 0; k < nm1; k++)
            {
                kp1 = k + 1;
                for (int i = kp1; i < n; i++)
                {
                    p = matrix[i, k] / matrix[k, k];
                    for (int j = kp1; j < n; j++)
                        matrix[i, j] = matrix[i, j] - p * matrix[k, j];
                }
            }
            for (int i = 0; i < n; i++)
                det = det * matrix[i, i];
            return det;

        }
        public Matrix ReturnValue()
        {
            if (type == 2)
            {
                return Inverse(this);
            }
            else
            {
                return this;
            }
        }
        public override string ToString()
        {
            string s = "";
            DoString(ref s);
            return s;
        }
        void DoString(ref string s)
        {
            AddElement(ref s, "[");
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    AddElement(ref s, matrix[i, j].ToString());
                }
                AddElement(ref s, ";");
            }
            s = s.Remove(s.Length - 2, 1);
            AddElement(ref s, "]");
        }
        void AddElement(ref string s, string c)
        {
            s = s + c + ' ';
        }

        public void Tfest()
        {
            throw new NotImplementedException();
        }
    }
}