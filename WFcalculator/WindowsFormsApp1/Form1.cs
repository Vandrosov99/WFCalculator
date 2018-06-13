using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        static Dictionary<char, double> variables;
        public Form1()
        {
            InitializeComponent();
            variables = new Dictionary<char, double>();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string text = textBox1.Text + "#";
            Equation eq = new Equation(ref text, 1);
            textBox4.Text = eq.ReturnValue().ToString();
        }
        public static double GetValue(string variable)
        {
            if (variable.Length == 1)
            {
                char var = variable.ToCharArray()[0];
                if (variables.Keys.Contains(var))
                {
                    return variables[var];
                }
                else
                {
                    throw new Exception($"Incorrect name of variable {variable}");
                }
            }
            else
            {
                throw new Exception($"Incorrect name of variable {variable}");
            }
        }
        double preValue = 0;
        char preVar = ' ';
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                string variableName = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                if (variableName.Length == 1)
                {
                    char variable = variableName.ToCharArray()[0];
                    if (variable == 'e')
                    {
                        MessageBox.Show("АШИБОЧКА");
                        dataGridView1.Rows[e.RowIndex].Cells[0].Value = string.Empty;
                    }
                    else
                    {
                        if (preVar == ' ')
                        {
                            if (variables.Keys.Contains(variable))
                            {
                                MessageBox.Show($"{variable} variable is already in list");
                                dataGridView1.Rows[e.RowIndex].Cells[0].Value = string.Empty;
                            }
                            else if (Letters.letters.Contains(variable))
                            {
                                variables.Add(variableName.ToCharArray()[0], 0);
                                dataGridView1.Rows[e.RowIndex].Cells[1].Value = 0;
                            }
                            else
                            {
                                MessageBox.Show("Only letters are allowed");
                                dataGridView1.Rows[e.RowIndex].Cells[0].Value = string.Empty;
                            }
                        }
                        else
                        {
                            if (Letters.letters.Contains(variable))
                            {
                                variables.Remove(preVar);
                                variables.Add(variableName.ToCharArray()[0], double.Parse(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString()));
                            }
                            else
                            {
                                MessageBox.Show("Only letters are allowed");
                                dataGridView1.Rows[e.RowIndex].Cells[0].Value = preVar;
                            }
                        }
                    }
                }
            }
            else if (e.ColumnIndex == 1)
            {
                double digit = 0;
                bool pasrsable = double.TryParse(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString(), out digit);
                if (pasrsable)
                {
                    variables[dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString().ToCharArray()[0]] = digit;
                }
                else
                {
                    dataGridView1.Rows[e.RowIndex].Cells[1].Value = preValue;
                }
            }
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                if (dataGridView1.Rows[e.RowIndex].Cells[0].Value == null || dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString() == "")
                {
                    preVar = ' ';
                }
                else
                {
                    preVar = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString().ToCharArray()[0];
                }
            }
            if (e.ColumnIndex == 1)
            {
                if (dataGridView1.Rows[e.RowIndex].Cells[0].Value == null)
                {
                    MessageBox.Show($"Variable isn`t assigned in {e.RowIndex} row");
                    dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[0];
                }
                else
                {
                    preValue = double.Parse(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string text = textBox2.Text + "#";
            BoolEquation eq = new BoolEquation(ref text, true);
            textBox3.Text = eq.ReturnValue().ToString();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string text = textBox5.Text + "#";
            MatrixEquation eq = new MatrixEquation(ref text,1);
            Matrix m = eq.ReturnValue();
            textBox5.Text = m.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string text = textBox6.Text + "#";
            Equation1 eq = new Equation1(ref text, 1);
            List<int> ee = eq.ReturnValue();
            string s = "";
            s += ee[0].ToString() + " + ";
            for (int i = 1; i < ee.Count; i++)
            {
                if (i < 8 || ee[i] != 0)
                {
                    s += ee[i].ToString() + " * " + "x^" + i + " + ";
                }
            }
            s = s.Remove(s.Length - 2, 2);
            textBox6.Text = s;
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
