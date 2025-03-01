using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;




namespace Kauda
{
    public partial class Form1 : Form
    {
       
       // private StyledPanel panel7;

        private const int W_MIN_ACTUAL = 10;
        private const int W_MAX_DISPLAY = 180; // Assuming 100 is the maximum value

        private Timer sensorTimer;

        public Form1()
        {
            InitializeComponent();
        
            // serialPort1.Open();

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



            SetupSpeedControls();
            SetupSpecialMovesCombo();
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

        /*  private void zAxisBtn_Click(object sender, EventArgs e)
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
          } */


        /*private void speedBtnH1(object sender, EventArgs e)
        {
            SendSpeedCommand('H', hTrackBarSpeed.Value);
        }

        private void speedBtnY1(object sender, EventArgs e)
        {
            SendSpeedCommand('Y', yTrackBarSpeed.Value);
        }

        private void speedBtnZ1(object sender, EventArgs e)
        {
            SendSpeedCommand('Z', zTrackBarSpeed.Value);
        }*/

        private void speedBtnH_Click(object sender, EventArgs e)
        {
            SendSpeedCommand('H', hTrackBarSpeed.Value);
        }

        private void speedBtnY_Click(object sender, EventArgs e)
        {
            SendSpeedCommand('Y', yTrackBarSpeed.Value);
        }

        private void speedBtnZ_Click(object sender, EventArgs e)
        {
            SendSpeedCommand('Z', zTrackBarSpeed.Value);

        }
        private void speedBtnV_Click_1(object sender, EventArgs e)
        {
            SendSpeedCommand('V', vTrackBarSpeed.Value);
        }

        private void speedBtnW_Click_1(object sender, EventArgs e)
        {
            SendSpeedCommand('W', wTrackBarSpeed.Value);
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

        //speed
        private void SetupSpeedControls()
        {
            // Set up speed controls for each axis
            SetupSpeedControl(hTrackBarSpeed, hSpeedText, progressBarSpdH, 'H');
            SetupSpeedControl(yTrackBarSpeed, ySpeedText, progressBarSpdY, 'Y');
            SetupSpeedControl(zTrackBarSpeed, zSpeedText, progressBarSpdZ, 'Z');
            SetupSpeedControl(vTrackBarSpeed, vSpeedText, progressBarSpdV, 'V');
            SetupSpeedControl(wTrackBarSpeed, wSpeedText, progressBarSpdW, 'W');
        }

        private void SetupSpeedControl(TrackBar trackBar, TextBox textBox, ProgressBar progressBar, char axis)
        {
            trackBar.Minimum = 0;
            trackBar.Maximum = 100;
            trackBar.Value = 50; // Default to middle speed
            textBox.Text = "50";
            progressBar.Minimum = 0;
            progressBar.Maximum = 100;
            progressBar.Value = 50;

            trackBar.Scroll += (sender, e) => UpdateSpeedControlUI(trackBar, textBox, progressBar);
            trackBar.MouseUp += (sender, e) => SendSpeedCommand(axis, trackBar.Value);
            textBox.Leave += (sender, e) => UpdateSpeedControlFromText(trackBar, textBox, progressBar, axis);
        }

        private void UpdateSpeedControlUI(TrackBar trackBar, TextBox textBox, ProgressBar progressBar)
        {
            textBox.Text = trackBar.Value.ToString();
            progressBar.Value = trackBar.Value;
        }

        private void UpdateSpeedControlFromText(TrackBar trackBar, TextBox textBox, ProgressBar progressBar, char axis)
        {
            if (int.TryParse(textBox.Text, out int value))
            {
                value = Math.Max(0, Math.Min(100, value));
                trackBar.Value = value;
                progressBar.Value = value;
                SendSpeedCommand(axis, value);
            }
        }

        private void SendSpeedCommand(char axis, int speed)
        {
            string command = $"S{axis}{speed:D3}\n";
            if (serialPort1.IsOpen)
            {
                serialPort1.Write(command);
                Console.WriteLine($"Sent speed command: {command}");
            }
            else
            {
                MessageBox.Show("Serial port is not open. Please check the connection.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SendMovementCommand(char axis, int steps)
        {
            string command = $"{axis}{steps}\n";
            serialPort1.Write(command);
        }

        private void zAxisBtn_Click(object sender, EventArgs e)
        {
            SendMovementCommand('Z', int.Parse(zStepNum.Text));
        }

        private void yAxisBtn_Click(object sender, EventArgs e)
        {
            SendMovementCommand('Y', int.Parse(yStepNum.Text));
        }

        private void hAxisBtn_Click(object sender, EventArgs e)
        {
            SendMovementCommand('H', int.Parse(hStepNum.Text));
        }

        private void vAxisBtn_Click(object sender, EventArgs e)
        {
            SendMovementCommand('V', int.Parse(vStepNum.Text));
        }

        private void wAxisBtn_Click(object sender, EventArgs e)
        {
            SendMovementCommand('W', int.Parse(wStepNum.Text));
        }


        //robot dance accle stepper moves

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void SetupSpecialMovesCombo()
        {
            specialBox.Items.Add("Crab Dance");
            specialBox.SelectedIndexChanged += SpecialMovesCombo_SelectedIndexChanged;
        }

        private void SpecialMovesCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (specialBox.SelectedIndex != -1 && specialBox.SelectedItem.ToString() == "Crab Dance")
            {
                ExecuteCrabDance();
            }
        }

        private void ExecuteCrabDance()
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.WriteLine("C");
                // Optionally, update UI to show dance is in progress
                UpdateStatus("Crab Dance in progress...");
            }
            else
            {
                MessageBox.Show("Serial port is not open. Please check the connection.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeSerialPort()
        {
            serialPort1.DataReceived += SerialPort1_DataReceived;
        }

        private void SerialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (serialPort1.BytesToRead > 0)
            {
                string data = serialPort1.ReadLine().Trim();
                this.Invoke(new Action(() => ProcessSerialData(data)));
            }
        }

        private void ProcessSerialData(string data)
        {
            if (data.StartsWith("MC") || data.StartsWith("DC"))
            {
                switch (data)
                {
                    case "MC":
                        HandleMovementComplete();
                        break;
                    case "DC":
                        HandleDanceComplete();
                        break;
                }
            }
            else if (data.StartsWith("DHT sensor error"))
            {
                UpdateStatus("DHT sensor error");
            }
            else
            {
                ProcessSensorData(data);
            }
        }

        private void HandleMovementComplete()
        {
            UpdateStatus("Movement Complete");
        }

        private void HandleDanceComplete()
        {
            UpdateStatus("Crab Dance Complete");
            // Reset the ComboBox
            this.Invoke(new Action(() => specialBox.SelectedIndex = -1));
        }

        private void UpdateStatus(string message)
        {
            // Assuming you have a status label on your form
            this.Invoke(new Action(() => danceStatus.Text = message));
        }

        //temperature sensor
        private void ProcessSensorData(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                UpdateTemperatureDisplay("No temperature detected", 0);
            }
            else
            {
                string[] parts = data.Split(',');
                if (parts.Length == 3 && float.TryParse(parts[1], out float temperatureC))
                {
                    UpdateTemperatureDisplay($"Temperature: {temperatureC:F1}°C", (int)temperatureC);
                }
                else
                {
                    UpdateTemperatureDisplay("Invalid temperature data", 0);
                }
            }
        }

        private void UpdateTemperatureDisplay(string text, int value)
        {
            this.Invoke(new Action(() => {
                temperatureCLabel.Text = text;
                tempReading.Value = value;
            }));
        }

        private void getTemperatureBtn_Click_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.WriteLine("T");
            }
            else
            {
                MessageBox.Show("Serial port is not open. Please check the connection.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void vTrackBarSpeed_Scroll(object sender, EventArgs e)
        {

        }

        private void label30_Click(object sender, EventArgs e)
        {

        }

        private void label36_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void tempReading_Click(object sender, EventArgs e)
        {

        }

        private void hTrackBarSpeed_Scroll(object sender, EventArgs e)
        {

        }
    }
     
}