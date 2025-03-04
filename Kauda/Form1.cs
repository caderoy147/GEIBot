using System;
using System.IO;
using System.IO.Ports;
using System.Windows.Forms;
using System.Diagnostics; // Add this at the top




namespace Kauda
{
    public partial class Form1 : Form
    {
       
       // private StyledPanel panel7;

        private const int W_MIN_ACTUAL = 10;
        private const int W_MAX_DISPLAY = 180; // Assuming 100 is the maximum value

        public Form1()
        {
            InitializeComponent();
            PopulateSerialPorts();

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


        //new for serial port detection
        private void PopulateSerialPorts()
        {
            port_connect.Items.Clear(); // Clear old list
            string[] ports = SerialPort.GetPortNames(); // Get available ports

            foreach (string port in ports)
            {
                string displayName = port; // Default: Just show COMx

                // Try to identify if it's an Arduino
                using (SerialPort tempSerial = new SerialPort(port, 9600)) // Change baud rate if needed
                {
                    try
                    {
                        tempSerial.Open();
                        tempSerial.WriteLine("V"); // Example: Ask Arduino for version
                        System.Threading.Thread.Sleep(100); // Wait for response

                        string response = tempSerial.ReadExisting();
                        if (response.Contains("Arduino"))
                        {
                            displayName += " (Arduino)";
                        }
                        tempSerial.Close();
                    }
                    catch { /* Ignore errors for non-Arduino devices */ }
                }

                port_connect.Items.Add(displayName);
            }

            if (port_connect.Items.Count > 0)
            {
                port_connect.SelectedIndex = 0; // Auto-select first port
            }
        }


        //SIDE BAR FEATURES

        //port selector
        private void port_connect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (port_connect.SelectedItem != null)
            {
                string selectedPort = port_connect.SelectedItem.ToString().Split(' ')[0]; // Extract "COMx"
                serialPort1.PortName = selectedPort;
            }

        }

        //port connection status
        private void connection_status_Click(object sender, EventArgs e)
        {

        }








        //End of side bar



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


        private void speedBtnH_Click(object sender, EventArgs e)
        {
            SendSpeedCommand('H', hTrackBarSpeed.Value);
            //this or buttons like this are for controlling 
            // saving the speed
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
            //function like this is for controlling angle.
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
            //these and buttons like this are for 'run'
            //when clicked the serail sends omething and the arm robot moves
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

        private void manual_Click(object sender, EventArgs e)
        {
            string filePath = Path.Combine(Application.StartupPath, "Resources", "USER-manual.docx");

            if (File.Exists(filePath))
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = filePath,
                    UseShellExecute = true
                });
            }
            else
            {
                MessageBox.Show("Manual not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
     
}