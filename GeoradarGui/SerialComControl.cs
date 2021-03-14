using GeoradarGui.Properties;
using System;
using System.IO.Ports;//.IO.Ports;
using System.Windows.Forms;


namespace GeoradarGui
{
    partial class SerialComControl : UserControl
    {
        /// <summary>
        /// Logging using log4net.
        /// </summary>
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Shorthand to settings.
        Settings settings = Settings.Default;

        public delegate void SerialPortControlHandler();
        public event SerialPortControlHandler SerialPortControlClicked;

        // Serial line.
        public static readonly string[] comboBoxItemsBaudRate = { "1200", "2400", "4800", "9600", "19200", "38400", "57600", "115200" };
        public static readonly string[] comboBoxItemsParity = Enum.GetNames(typeof(Parity));
        public static readonly string[] comboBoxItemsDataBits = { "5", "6", "7", "8" };
        // The SerialPort class throws an ArgumentOutOfRangeException exception when you set the StopBits property to "None"
        // and it throws an exception "The parameter is incorrect" when you set the StopBits property to "OnePointFive" after call function Open().
        public static readonly string[] comboBoxItemsStopBits = { StopBits.One.ToString(), StopBits.Two.ToString() };

        public SerialComControl()
        {
            InitializeComponent();

            // Set options for comboBoxes.
            comboBoxBaudRate.Items.AddRange(Constants.comboBoxItemsBaudRate);
            comboBoxParity.Items.AddRange(Constants.comboBoxItemsParity);
            comboBoxDataBits.Items.AddRange(Constants.comboBoxItemsDataBits);
            comboBoxStopBits.Items.AddRange(Constants.comboBoxItemsStopBits);
        }

        /// <summary>
        /// Sets text of button.
        /// </summary>
        public void UpdateControls(bool portIsOpen)
        {
            button.Text = portIsOpen ? "&Close Port " + settings.PortName : "&Open Port";
            groupBox.Enabled = !portIsOpen;
        }

        /// <summary>
        /// Raises the HandleCreated event.
        /// </summary>
        protected override void OnHandleCreated(EventArgs e)
        {
            LoadSettings();
            base.OnHandleCreated(e);
        }

        /// <summary>
        /// This method loads the user's settings.
        /// </summary>
        private void LoadSettings()
        {
            // Load port name (scan all serial ports).
            try
            {
                // Add list of com-ports to combo box.
                var PortIndex = 0;
                foreach (var port in SerialPort.GetPortNames())
                {
                    if (port == settings.PortName)
                    {
                        PortIndex = comboBoxPortName.Items.Count;
                    }
                    comboBoxPortName.Items.Add(port);
                }
                comboBoxPortName.Text = comboBoxPortName.Items[PortIndex].ToString();
                settings.PortName = comboBoxPortName.Text;
            }
            catch (Exception ex)
            {
                log.Error("Loading the Settings failed.", ex);
            }

            comboBoxBaudRate.Text = settings.BaudRate.ToString();
            comboBoxParity.Text = settings.Parity.ToString();
            comboBoxDataBits.Text = settings.DataBits.ToString();
            comboBoxStopBits.Text = settings.StopBits.ToString();
        }

        #region Event Handlers
        /// <summary>
        /// Occurs when the control is clicked.
        /// </summary>
        private void button_Click(object sender, EventArgs e)
        {
            if (SerialPortControlClicked != null)
            {
                // Call event.
                SerialPortControlClicked();
            }
        }

        /// <summary>
        /// Occurs when the SelectedIndex property has changed. 
        /// </summary>
        private void comboBoxPortName_SelectedIndexChanged(object sender, EventArgs e)
        {
            settings.PortName = comboBoxPortName.Text;
        }

        /// <summary>
        /// Occurs when the SelectedIndex property has changed. 
        /// </summary>
        private void comboBoxBaudRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            settings.BaudRate = int.Parse(comboBoxBaudRate.Text);
        }

        /// <summary>
        /// Occurs when the SelectedIndex property has changed. 
        /// </summary>
        private void comboBoxParity_SelectedIndexChanged(object sender, EventArgs e)
        {
            settings.Parity = (Parity)Enum.Parse(typeof(Parity), comboBoxParity.Text);
        }

        /// <summary>
        /// Occurs when the SelectedIndex property has changed. 
        /// </summary>
        private void comboBoxDataBits_SelectedIndexChanged(object sender, EventArgs e)
        {
            settings.DataBits = int.Parse(comboBoxDataBits.Text);
        }

        /// <summary>
        /// Occurs when the SelectedIndex property has changed. 
        /// </summary>
        private void comboBoxStopBits_SelectedIndexChanged(object sender, EventArgs e)
        {
            settings.StopBits = (StopBits)Enum.Parse(typeof(StopBits), comboBoxStopBits.Text);
        }
        #endregion
    }
}
