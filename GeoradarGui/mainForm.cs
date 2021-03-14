using GeoradarGui.Properties;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using ZedGraph;

namespace GeoradarGui
{
    public partial class mainForm : Form
    {
        /// <summary>
        /// Logging using log4net.
        /// </summary>
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Shorthand to settings.
        Settings settings = Settings.Default;

        /// <summary>
        /// This class is used for communication via serial line.
        /// </summary>
        ByteArrayCom serialCom;

        /// <summary>
        /// Timer running on its own thread.
        /// </summary>
        System.Timers.Timer threadTimerReadData;
        GraphForm graphForm;
        LoadDataForm loadDataForm;
        SpectrumForm spectrumForm;
        ControlForm  controlForm;
        FilterClass  filterClass;
        private readonly Random _random = new Random();

        private UInt32 timeout_counter = 0;


        public mainForm()
        {

            InitializeComponent();
            serialCom = new ByteArrayCom();
            graphForm = new GraphForm();
            loadDataForm = new LoadDataForm();
            spectrumForm = new SpectrumForm();
            controlForm = new ControlForm();
            filterClass = new FilterClass();


            spectrumForm.addDataToGraphFormControl += new GeoradarGui.SpectrumForm.AddDataToGraphFormControl(AddGraphToSpectrum);
            spectrumForm.DockStateChanged += new EventHandler(dockState_Changed);

            serialCom.startDataProcessing += new GeoradarGui.ByteArrayCom.StartDataProcessing(startDataProcessing);

            // Data-Read button handler from Graphform:
            graphForm.DockStateChanged += new EventHandler(dockState_Changed);

            loadDataForm.DockStateChanged += new EventHandler(dockState_Changed);
            loadDataForm.loadDataToTextBoxEvent += new GeoradarGui.LoadDataForm.LoadDataToTextBoxDelegat(writeDataToGraph);

            controlForm.controlSendPacketToBoard += new GeoradarGui.ControlForm.ControlSendPacketToBoard(sendPacketToBoard);

            controlForm.updateControlVariables += new GeoradarGui.ControlForm.UpdateControlVariables(updateControlVariables);
            controlForm.savePictureToFile += new GeoradarGui.ControlForm.SavePictureToFile(saveSpectrumPictureToFile);
            controlForm.DockStateChanged += new EventHandler(dockState_Changed);

            // Create read data one time timer :
            threadTimerReadData = new System.Timers.Timer(Constants.readDataRate);
            threadTimerReadData.Elapsed += threadTimerReadData_Tick;

            /* Update controls based on the app's current state.*/
            UpdateControls(true);
            updateControlVariables();
        }

        private void saveSpectrumPictureToFile()
        {
            spectrumForm.SavePictureImage();
        }

        private void AddGraphToSpectrum(PointPairList _pointPairList, double parameret)
        {
            PointPairList output_pair_list;
            // Remove DC:
            double dc_mean = 0;

            if (controlForm.GetchkFilterState() == true)
            {
                for (int i = 0; i < _pointPairList.Count; i++)
                {
                    dc_mean = dc_mean + _pointPairList[i].X;
                }

                dc_mean = dc_mean / _pointPairList.Count;

                for (int i = 0; i < _pointPairList.Count; i++)
                {
                    _pointPairList[i].X = _pointPairList[i].X - dc_mean + 2;
                }
            }


            if (controlForm.GetchkFilterState() == true)
            {
                InputDataAverageFilter(out output_pair_list, _pointPairList);
                graphForm.writeOneOrdinateGraph(output_pair_list, (parameret/2) / 1000.0);
                spectrumForm.addDataLine(_pointPairList);


            }
            else
            {
                graphForm.writeOneOrdinateGraph(_pointPairList, (parameret / 2) / 1000.0);
                spectrumForm.addDataLine(_pointPairList);
            }
        }



        private void updateControlVariables()
        {
            spectrumForm.DielectricPermittivity = controlForm.GetDielectricPermittivity();
            spectrumForm.DistanceTimeState = controlForm.GetChkDistanceTimeState();

            spectrumForm.VisualisationMode = controlForm.GetVisualisationMode();
            spectrumForm.InputDataScaleFactor = controlForm.GetInputDataScaleFactor();

            spectrumForm.ScanStepValue = controlForm.GetScanStep();
            spectrumForm.DeptPointsValue = controlForm.GetDepthPoints();

            spectrumForm.GeneratedDataHeightDepth = controlForm.GetDepthPoints();
            spectrumForm.SetTimerState(settings.DataEmulation);
        }

        private void updateControlForm()
        {
            controlForm.UpdateControlForm();
        }

        private void startDataProcessing()
        {
            threadTimerReadData.Start();
        }

        private void sendPacketToBoard(UInt16 command, UInt16 parameter)
        {
            string message;

            if (!serialCom.IsOpen)
            {
                MessageBox.Show("Open COM port!", "COM port not connected.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            switch (command)
            {
                case PacketDefinition.GEORADAR_LOAD_PARAMETERS_REQUEST:

                    message = serialCom.SendCommandRequest(PacketDefinition.GEORADAR_LOAD_PARAMETERS_REQUEST, 0, null);
                    break;
                case PacketDefinition.GEORADAR_SAVE_PARAMETERS_REQUEST:
                    UInt32 u32_tmp;
                    byte[] data_load = new byte[24];
                    
                    u32_tmp = (UInt32)settings.ScanStep;
                    data_load[0] = (byte)((u32_tmp >> 0) & 0xFF);
                    data_load[1] = (byte)((u32_tmp >> 8) & 0xFF);
                    data_load[2] = (byte)((u32_tmp >> 16) & 0xFF);
                    data_load[3] = (byte)((u32_tmp >> 24) & 0xFF);

                    u32_tmp = (UInt32)settings.DepthPoints;
                    data_load[4] = (byte)((u32_tmp >> 0) & 0xFF);
                    data_load[5] = (byte)((u32_tmp >> 8) & 0xFF);
                    data_load[6] = (byte)((u32_tmp >> 16) & 0xFF);
                    data_load[7] = (byte)((u32_tmp >> 24) & 0xFF);

                    u32_tmp = (UInt32)settings.AveragePoints;
                    data_load[8] = (byte)((u32_tmp >> 0) & 0xFF);
                    data_load[9] = (byte)((u32_tmp >> 8) & 0xFF);
                    data_load[10] = (byte)((u32_tmp >> 16) & 0xFF);
                    data_load[11] = (byte)((u32_tmp >> 24) & 0xFF);

                    u32_tmp = (UInt32)settings.StaticDelay;
                    data_load[12] = (byte)((u32_tmp >> 0) & 0xFF);
                    data_load[13] = (byte)((u32_tmp >> 8) & 0xFF);
                    data_load[14] = (byte)((u32_tmp >> 16) & 0xFF);
                    data_load[15] = (byte)((u32_tmp >> 24) & 0xFF);

                    u32_tmp = (UInt32)settings.TxPulseWidth;
                    data_load[16] = (byte)((u32_tmp >> 0) & 0xFF);
                    data_load[17] = (byte)((u32_tmp >> 8) & 0xFF);
                    data_load[18] = (byte)((u32_tmp >> 16) & 0xFF);
                    data_load[19] = (byte)((u32_tmp >> 24) & 0xFF);

                    u32_tmp = (UInt32)settings.TxPulsePeriod;
                    data_load[20] = (byte)((u32_tmp >> 0) & 0xFF);
                    data_load[21] = (byte)((u32_tmp >> 8) & 0xFF);
                    data_load[22] = (byte)((u32_tmp >> 16) & 0xFF);
                    data_load[23] = (byte)((u32_tmp >> 24) & 0xFF);


                    message = serialCom.SendCommandRequest(PacketDefinition.GEORADAR_SAVE_PARAMETERS_REQUEST, 0, data_load);
                    break;
                case PacketDefinition.GEORADAR_START_REQUEST:
                    message = serialCom.SendCommandRequest(PacketDefinition.GEORADAR_START_REQUEST, 0, null);
                    break;
                case PacketDefinition.GEORADAR_STOP_REQUEST:
                    message = serialCom.SendCommandRequest(PacketDefinition.GEORADAR_STOP_REQUEST, 0, null);
                    break;

                default:
                    break;
            }

        }

        private void writeDataToGraph(PointPairList _pointPairList)
        {
            graphForm.writeDataToGraph(_pointPairList);
        }


        private void serialComControl_SerialPortControlClicked()
        {
            // If the port is open, close it.
            if (serialCom.IsOpen)
            {
                serialCom.Close();
            }
            else
            {
                // Set the port's settings.
                serialCom.SetParameters(settings.PortName, settings.BaudRate, settings.Parity, settings.DataBits, settings.StopBits);

                lock (serialCom.BufferLock)
                {
                    serialCom.Init();
                }
                try
                {
                    serialCom.Open();
                }
                catch (Exception ex)
                {
                    var message = "COM Port Unavailable";
                    log.Error(message, ex);
                    MessageBox.Show(ex.Message, message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            // Change the state of the form's controls.
            UpdateControls();
        }


        private void threadTimerReadData_Tick(object sender, EventArgs e)
        {
            ProcessReceivedBuffer();
            timeout_counter = 0;
        }

        private void ProcessReceivedBuffer()
        {
            byte[] receivedData;
            byte[] command_set;
            UInt16 packet_command;
            UInt16 packet_parameter;
            ResponseType response;
            PointPairList _pointPairList = new PointPairList();
            PointPairList output_pair_list;

            lock (serialCom.BufferLock)
            {
                response = serialCom.processGraphDataAnswer(out receivedData, out command_set);
                if (response == ResponseType.None)
                {
                    timeout_counter++;
                    if (timeout_counter > 10)
                    {
                        threadTimerReadData.Stop();
                    }
                }
                else
                {
                    threadTimerReadData.Stop();

                    packet_command = BitConverter.ToUInt16(command_set, 0);
                    switch (packet_command)
                    {
                        case PacketDefinition.GEORADAR_LOAD_PARAMETERS_RESPOND:
                            // Parsing input setting packet:
                            //    u32_ScanStep
                            //    u32_DepthPoints
                            //    u32_AveragePoints
                            //    u32_static_delay
                            // data: 
                            settings.ScanStep = (int)BitConverter.ToUInt32(receivedData,0);
                            settings.DepthPoints = (int)BitConverter.ToUInt32(receivedData, 4);
                            settings.AveragePoints = (int)BitConverter.ToUInt32(receivedData, 8);
                            settings.StaticDelay = (int)BitConverter.ToUInt32(receivedData, 12);
                            settings.TxPulseWidth = (int)BitConverter.ToUInt32(receivedData, 16);
                            settings.TxPulsePeriod = (int)BitConverter.ToUInt32(receivedData, 20);

                            // refresh data from setting in controlForm()
                            updateControlForm();
                            break;

                        case PacketDefinition.GEORADAR_SAVE_PARAMETERS_RESPOND:
                            toolStripStatusLastAction.Text = "Saved";
                            break;

                        case PacketDefinition.GEORADAR_START_RESPOND:
                            
                            break;

                        case PacketDefinition.GEORADAR_STOP_RESPOND:
                            
                            break;

                        case PacketDefinition.GEORADAR_DATA_PACKET:
                            PointPairList _localPointPairList = new PointPairList();
                            UInt16 u16DataLoadLength = BitConverter.ToUInt16(receivedData, 1);

                            //for (int i = 0; i < 768; i++)
                            for (int i = 0; i < (receivedData.Count()/2); i++)
                            {
                                Int16 i16_value = 0;
                                i16_value = BitConverter.ToInt16(receivedData, (i * 2));
                                PointPair _pointPair = new PointPair(i16_value, i);
                                _localPointPairList.Add(_pointPair);
                            }

                            // Check filter enabled:
                            

                            AddGraphToSpectrum(_localPointPairList, (double)controlForm.GetScanStep());

/*
                            if (controlForm.GetchkFilterState() == true)
                            {
                                InputDataAverageFilter(out output_pair_list, _localPointPairList);

                                graphForm.writeOneOrdinateGraph(output_pair_list);
                                spectrumForm.addDataLine(output_pair_list);

                            }
                            else
                            {
                                graphForm.writeOneOrdinateGraph(_localPointPairList);
                                spectrumForm.addDataLine(_localPointPairList);
                            }
*/
                            break;
                        default:
                            break;

                    }
                }
            }

        }

        private void InputDataAverageFilter(out PointPairList output_pair_list, PointPairList input_pair_list)
        {
            //output_pair_list = new PointPairList();

            for (int i = 0; i < input_pair_list.Count; i++)
            {
                input_pair_list[i].X = filterClass.iir(input_pair_list[i].X, 0);
            }
            output_pair_list = input_pair_list;
        }


        /// <summary>
        /// This method updates controls based on the app's current state.
        /// </summary>
        private void UpdateControls(bool firstUpdate = false)
        {
            var isOpen = serialCom.IsOpen;

            serialComControl.UpdateControls(isOpen);

            if (!firstUpdate)
            {
                string message;
                if (isOpen)
                {
                    message = string.Format("The serial port {0} was opened.", settings.PortName);
                }
                else
                {
                    message = "The serial port was closed.";
                }

                toolStripStatusLastAction.Text = message;
                log.Info(message);
            }
        }


        /// <summary>
        /// Updates items in main menu - View.
        /// </summary>
        private void UpdateMainMenuView()
        {
            graphPanelToolStripMenuItem.Checked = !graphForm.IsHidden;
            testDataPanelToolStripMenuItem.Checked = !loadDataForm.IsHidden;
            spectrumPanelToolStripMenuItem.Checked = !spectrumForm.IsHidden;
            controlPanelToolStripMenuItem.Checked = !controlForm.IsHidden;
        }


        /// <summary>
        /// This method saves the user's settings.
        /// </summary>
        private void SaveSettings()
        {
            // Copy window size and location to app settings.
            if (this.WindowState == FormWindowState.Normal)
            {
                settings.WindowSize = this.Size;
                settings.WindowLocation = this.Location;
            }
            else
            {
                settings.WindowSize = this.RestoreBounds.Size;
                settings.WindowLocation = this.RestoreBounds.Location;
            }

            // Save settings.
            settings.Save();
        }

        /// <summary>
        /// This method loads the user's settings.
        /// </summary>
        private void LoadSettings()
        {
            // Load window location.
            if (settings.WindowLocation != null)
            {
                this.Location = settings.WindowLocation;
            }

            // Load window size.
            if (settings.WindowSize != null)
            {
                this.Size = settings.WindowSize;
            }

            if (!Constants.logFileSizes.ContainsKey(settings.LogFileMaxSize))
            {
                settings.LogFileMaxSize = Constants.logFileSizes.Keys.First();
            }

            if (!Constants.logFileSeparators.ContainsKey(settings.LogFileSeparator))
            {
                settings.LogFileSeparator = Constants.logFileSeparators.Keys.First();
            }
        }


        /// <summary>
        /// Gets the appropriate dock content.
        /// </summary>
        private IDockContent GetContentFromPersistString(string persistString)
        {
            if (persistString == typeof(GraphForm).ToString())
            {
                return graphForm;
            }
            else if (persistString == typeof(LoadDataForm).ToString())
            {
                return loadDataForm;
            }
            else if (persistString == typeof(SpectrumForm).ToString())
            {
                return spectrumForm;
            }
            else if (persistString == typeof(ControlForm).ToString())
            {
                return controlForm;
            }
            return null;
        }


        private void mainForm_Load(object sender, EventArgs e)
        {
            LoadSettings();

            // Set properties of DockPanel.
            var layoutFileName = GetDockLayoutFileName();
            if (File.Exists(layoutFileName))
            {
                dockPanel.LoadFromXml(layoutFileName, new DeserializeDockContent(GetContentFromPersistString));
            }
            else
            {
                // Default - cardsForm is displayed.
                graphForm.Show(dockPanel);
            }

            UpdateMainMenuView();
        }

        /// <summary>
        /// This static method returns the full path to the file for storage dockable forms.
        /// </summary>
        private static string GetDockLayoutFileName()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Constants.dockLayoutFileName);
        }

        private void graphPanelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (graphForm.IsHidden)
            {
                graphForm.Show(dockPanel);
            }
            else
            {
                graphForm.Hide();
            }
            UpdateMainMenuView();
        }

        private void testDataPanelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (loadDataForm.IsHidden)
            {
                loadDataForm.Show(dockPanel);
            }
            else
            {
                loadDataForm.Hide();
            }
            UpdateMainMenuView();
        }

        private void dockState_Changed(object sender, EventArgs e)
        {
            UpdateMainMenuView();
        }

        private void mainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveSettings();

            serialCom.Close();

            // Save state of DockPanel to separate xml file.
            dockPanel.SaveAsXml(GetDockLayoutFileName());



        }

        private void spectrumPanelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (spectrumForm.IsHidden)
            {
                spectrumForm.Show(dockPanel);
            }
            else
            {
                spectrumForm.Hide();
            }
            UpdateMainMenuView();
        }

        private void controlPanelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (controlForm.IsHidden)
            {
                controlForm.Show(dockPanel);
            }
            else
            {
                controlForm.Hide();
            }
            UpdateMainMenuView();
        }
    }
}
