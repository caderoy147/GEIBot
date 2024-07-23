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
        private const int W_MIN_ACTUAL = 10;
        private const int W_MAX_DISPLAY = 180; // Assuming 100 is the maximum value

        public Form1()
        {
            InitializeComponent();
            serialPort1.Open();
            // Add event handlers for TextChanged events
            zStepNum.TextChanged += StepNum_TextChanged;
            yStepNum.TextChanged += StepNum_TextChanged;
            hStepNum.TextChanged += StepNum_TextChanged;
            vStepNum.TextChanged += StepNum_TextChanged;
            wStepNum.TextChanged += StepNum_TextChanged;

            // Set up W axis controls
            wTrackBar.Minimum = 0;
            wTrackBar.Maximum = W_MAX_DISPLAY;
            wStepNum.Text = "0";
        }

        private void StepNum_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox textBox && int.TryParse(textBox.Text, out int value))
            {
                switch (textBox.Name)
                {
                    case "zStepNum":
                        zTrackBar.Value = value;
                        break;
                    case "yStepNum":
                        yTrackBar.Value = value;
                        break;
                    case "hStepNum":
                        hTrackBar.Value = value;
                        break;
                    case "vStepNum":
                        vTrackBar.Value = value;
                        break;
                    case "wStepNum":
                        wTrackBar.Value = Math.Min(Math.Max(value, 0), W_MAX_DISPLAY);
                        break;
                }
            }
        }

        private void zAxisBtn_Click(object sender, EventArgs e)
        {
            SendCommand("Z", zStepNum.Text);
        }

        private void yAxisBtn_Click(object sender, EventArgs e)
        {
            SendCommand("Y", yStepNum.Text);
        }

        private void hAxisBtn_Click(object sender, EventArgs e)
        {
            SendCommand("H", hStepNum.Text);
        }

        private void vAxisBtn_Click(object sender, EventArgs e)
        {
            SendCommand("V", vStepNum.Text);
        }

        private void wAxisBtn_Click(object sender, EventArgs e)
        {
            SendCommand("W", wStepNum.Text);
        }

        private void SendCommand(string axis, string steps)
        {
            if (int.TryParse(steps, out int stepValue))
            {
                if (axis == "W")
                {
                    // Map the display value (0-100) to actual value (10-100)
                    int actualValue = MapWValue(stepValue);
                    string command = $"W{actualValue}\n";
                    serialPort1.Write(command);
                }
                else
                {
                    string command = $"{axis}{stepValue}\n";
                    serialPort1.Write(command);
                }
            }
            else
            {
                MessageBox.Show("Invalid step value. Please enter a valid number.");
            }
        }

        private int MapWValue(int displayValue)
        {
            // Map 0-100 to 10-100
            return (int)Math.Round((double)(W_MAX_DISPLAY - displayValue) / W_MAX_DISPLAY * (W_MAX_DISPLAY - W_MIN_ACTUAL) + W_MIN_ACTUAL);
        }

        private void gripBtn_Click(object sender, EventArgs e)
        {
            serialPort1.Write("G");
        }

        private void releaseBtn_Click(object sender, EventArgs e)
        {
            serialPort1.Write("R");
        }

        private void zTrackBar_Scroll(object sender, EventArgs e)
        {
            zStepNum.Text = zTrackBar.Value.ToString();
        }

        private void yTrackBar_Scroll(object sender, EventArgs e)
        {
            yStepNum.Text = yTrackBar.Value.ToString();
        }

        private void hTrackBar_Scroll(object sender, EventArgs e)
        {
            hStepNum.Text = hTrackBar.Value.ToString();
        }

        private void vTrackBar_Scroll(object sender, EventArgs e)
        {
            vStepNum.Text = vTrackBar.Value.ToString();
        }

        private void wTrackBar_Scroll(object sender, EventArgs e)
        {
            wStepNum.Text = wTrackBar.Value.ToString();
        }
    }
}