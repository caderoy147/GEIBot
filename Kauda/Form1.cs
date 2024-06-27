using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kauda
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            serialPort1.Open();
        }

        private void xStepStart_Click(object sender, EventArgs e)
        {
            //send steps to stepper (arduino)
            string m1 = "X" + xStepNum.Text;
            serialPort1.Write(m1);
        }

        private void xStepNum_TextChanged(object sender, EventArgs e)
        {

        }

        private void xCounterClock_Click(object sender, EventArgs e)
        {
            // Send steps to stepper (arduino) in counterclockwise direction
            int steps = int.Parse(xStepNum.Text);
            string direction = "-" + steps.ToString(); // Negative value for counterclockwise
            serialPort1.Write("X" + direction);
        }

        private void xClock_Click(object sender, EventArgs e)
        {
            //send steps to stepper (arduino)
            string m1 = "X" + xStepNum.Text;
            serialPort1.Write(m1);
        }

        private void yCounterClock_Click(object sender, EventArgs e)
        {
            // Send steps to stepper (arduino) in counterclockwise direction
            int steps = int.Parse(yStepNum.Text);
            string direction = "-" + steps.ToString(); // Negative value for counterclockwise
            serialPort1.Write("Y" + direction);
        }

        private void yClock_Click(object sender, EventArgs e)
        {
            //send steps to stepper (arduino)
            string m1 = "Y" + yStepNum.Text;
            serialPort1.Write(m1);
        }

        private void zCounterClock_Click(object sender, EventArgs e)
        {
            // Send steps to stepper (arduino) in counterclockwise direction
            int steps = int.Parse(zStepNum.Text);
            string direction = "-" + steps.ToString(); // Negative value for counterclockwise
            serialPort1.Write("Z" + direction);
        }

        private void zClock_Click(object sender, EventArgs e)
        {
            //send steps to stepper (arduino)
            string m1 = "Z" + zStepNum.Text;
            serialPort1.Write(m1);
            Console.WriteLine("z");
        }

        private void zStepNum_TextChanged(object sender, EventArgs e)
        {

        }

        private void yStepNum_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
