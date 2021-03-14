using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GeoradarGui.Properties;

namespace GeoradarGui
{
    public partial class ControlForm : BaseDockContent
    {
        public delegate void ControlSendPacketToBoard(UInt16 command, UInt16 parameter);
        public event ControlSendPacketToBoard controlSendPacketToBoard;

        public delegate void UpdateControlVariables();
        public event UpdateControlVariables updateControlVariables;

        public delegate void SavePictureToFile();
        public event SavePictureToFile savePictureToFile;



        // Shorthand to settings.
        Settings settings = Settings.Default;
        MyAppSettings app_settings;

        public ControlForm()
        {
            app_settings = MyAppSettings.Load();
            InitializeComponent();
        }

        private void btn_LoadDeviceParameters_Click(object sender, EventArgs e)
        {
            controlSendPacketToBoard(PacketDefinition.GEORADAR_LOAD_PARAMETERS_REQUEST, 0);
        }

        private void btnScanStart_Click(object sender, EventArgs e)
        {
            btnScanStart.Visible = false;
            btnScanStop.Visible = true;
            btn_LoadParametersFromDevice.Enabled = false;
            btn_SaveParametersToDevice.Enabled = false;

            btnScanStop.Focus();

            // Add send START packet
            controlSendPacketToBoard(PacketDefinition.GEORADAR_START_REQUEST, 0);
        }

        private void btnScanStop_Click(object sender, EventArgs e)
        {
            btnScanStop.Visible = false;
            btnScanStart.Visible = true;
            btn_LoadParametersFromDevice.Enabled = true;
            btn_SaveParametersToDevice.Enabled = true;

            btnScanStart.Focus();
            // Add send STOP packet
            controlSendPacketToBoard(PacketDefinition.GEORADAR_STOP_REQUEST, 0);
        }

        private void ControlForm_Load(object sender, EventArgs e)
        {
            UpdateControlForm();
        }

        public void UpdateControlForm()
        {
            nmr_InputDataScaleFactor.Value = settings.ScaleFactor;
            nmr_ScanStep.Value = settings.ScanStep;
            cmb_VisualisationMode.Text = settings.VisualisationMode;
            nmr_StaticDelay.Value = settings.StaticDelay;
            nmr_AveragingLineWindow.Value = settings.AveragingLineWindow;
            nmr_AverageWindow.Value = settings.AverageWindow;
            nmr_AveragePoints.Value = settings.AveragePoints;
            nmr_DepthPoints.Value = settings.DepthPoints;
            chkFilter.Checked = settings.FilterCheckState;
            nmr_TxPulseWidth.Value = settings.TxPulseWidth;
            lblTxPulseWidthValue.Text = "-   " + (1000000000 * (float)(nmr_TxPulseWidth.Value) * ((1.0 / (168000000.0 / 2.0)) * 3.0)).ToString("0.0") + " nS";
            nmr_TxPulsePeriod.Value = settings.TxPulsePeriod;
            lblTxPulsePeriodValue.Text = "-   " + (1.0 / ((float)(nmr_TxPulsePeriod.Value) * ((1.0 / (168000000.0 / 2.0)) * 3.0))).ToString("0.0") + " Hz";
            chkDataEmulation.Checked = settings.DataEmulation;
            nmrDielectricPermittivity.Value = settings.DielectricPermitivity;
            chkDielectricPermittivity.Checked = settings.DistanceTimeCheckState;
        }

        public void StoreDataFromControlForm()
        {
            app_settings.InputDataScaleFactor = settings.ScaleFactor;
            app_settings.ScanStep = settings.ScanStep;
            app_settings.VisualisationMode = settings.VisualisationMode;
            app_settings.StaticDelay = settings.StaticDelay;
            app_settings.AveragingLineWindow = settings.AveragingLineWindow;
            app_settings.AverageWindow = settings.AverageWindow;
            app_settings.AveragePoints = settings.AveragePoints;
            app_settings.DepthPoints = settings.DepthPoints;
            app_settings.chkFilter = settings.FilterCheckState;
            app_settings.TxPulseWidth = settings.TxPulseWidth;
            app_settings.TxPulsePeriod = settings.TxPulsePeriod;
            app_settings.chkDataEmulation = settings.DataEmulation;
        }

        public void RestoreDataFromControlForm()
        {
            settings.ScaleFactor = app_settings.InputDataScaleFactor;
            settings.ScanStep = app_settings.ScanStep;
            settings.VisualisationMode = app_settings.VisualisationMode;
            settings.StaticDelay = app_settings.StaticDelay;
            settings.AveragingLineWindow = app_settings.AveragingLineWindow;
            settings.AverageWindow = app_settings.AverageWindow;
            settings.AveragePoints = app_settings.AveragePoints;
            settings.DepthPoints = app_settings.DepthPoints;
            settings.FilterCheckState = app_settings.chkFilter;
            settings.TxPulseWidth = app_settings.TxPulseWidth;
            settings.TxPulsePeriod = app_settings.TxPulsePeriod;
            settings.DataEmulation = app_settings.chkDataEmulation;
        }

        



        private void btn_SaveParametersToDevice_Click(object sender, EventArgs e)
        {
            controlSendPacketToBoard(PacketDefinition.GEORADAR_SAVE_PARAMETERS_REQUEST, 0);
        }

        public int GetScanStep()
        {
            return (int)nmr_ScanStep.Value;
        }
        public string GetVisualisationMode()
        {
            return (string)cmb_VisualisationMode.Text;
        }

        public int GetStaticDelay()
        {
          return (int)nmr_StaticDelay.Value;
        }

        public int GetInputDataScaleFactor()
        {
            return (int)nmr_InputDataScaleFactor.Value;
        }

        public int GetAveragingLineWindow()
        {
            return (int)nmr_AveragingLineWindow.Value;
        }

        public int GetAverageWindow()
        {
            return (int)nmr_AverageWindow.Value;
        }

        public int GetAveragePoints()
        {
            return (int)nmr_AveragePoints.Value;
        }

        public int GetDepthPoints()
        {
            return (int)nmr_DepthPoints.Value;
        }

        public bool GetchkFilterState()
        {
            return (bool)chkFilter.Checked; 
        }

        public int GetDielectricPermittivity()
        {
            return (int)nmrDielectricPermittivity.Value;
                    
        }
        public bool GetChkDistanceTimeState()
        {
            return (bool)chkDielectricPermittivity.Checked; 
        }



        // Changing Event Handlers : 
        private void nmr_ScanStep_ValueChanged(object sender, EventArgs e)
        {
            settings.ScanStep = (int)nmr_ScanStep.Value; 
            updateControlVariables();
        }
        private void nmr_DepthPoints_ValueChanged(object sender, EventArgs e)
        {
            settings.DepthPoints = (int)nmr_DepthPoints.Value;
            updateControlVariables();
        }
        private void nmr_AveragePoints_ValueChanged(object sender, EventArgs e)
        {
            settings.AveragePoints = (int)nmr_AveragePoints.Value;
            updateControlVariables();
        }
        private void nmr_InputDataScaleFactor_ValueChanged(object sender, EventArgs e)
        {
            settings.ScaleFactor = (int)nmr_InputDataScaleFactor.Value;
            updateControlVariables();
        }

        private void nmr_AveragingLineWindow_ValueChanged(object sender, EventArgs e)
        {
            settings.AveragingLineWindow = (int)nmr_AveragingLineWindow.Value;
            updateControlVariables();
        }

        private void nmr_AverageWindow_ValueChanged(object sender, EventArgs e)
        {
            settings.AverageWindow = (int)nmr_AverageWindow.Value;
            updateControlVariables();
        }
        private void cmb_VisualisationMode_TextChanged(object sender, EventArgs e)
        {
            settings.VisualisationMode = cmb_VisualisationMode.Text;
            updateControlVariables();
        }

        private void nmr_StaticDelay_ValueChanged(object sender, EventArgs e)
        {
            settings.StaticDelay = (int)nmr_StaticDelay.Value;
            updateControlVariables();
        }

        private void chkFilter_CheckedChanged(object sender, EventArgs e)
        {
            settings.FilterCheckState =  chkFilter.Checked;
            updateControlVariables();
        }

        private void nmr_TxPulseWidth_ValueChanged(object sender, EventArgs e)
        {
            lblTxPulseWidthValue.Text = "-   " + (1000000000 * (float)(nmr_TxPulseWidth.Value) * ((1.0 / (168000000.0 / 2.0)) * 3.0)).ToString("0.0") + " nS";
            settings.TxPulseWidth = (int)nmr_TxPulseWidth.Value;
            updateControlVariables();
        }

        private void nmr_TxPulsePeriod_ValueChanged(object sender, EventArgs e)
        {
            lblTxPulsePeriodValue.Text = "-   " + (1.0 / ((float)(nmr_TxPulsePeriod.Value) * ((1.0 / (168000000.0 / 2.0)) * 3.0))).ToString("0.0") + " Hz";
            settings.TxPulsePeriod = (int)nmr_TxPulsePeriod.Value;
            updateControlVariables();
        }

        private void chkDataEmulation_CheckedChanged(object sender, EventArgs e)
        {
            settings.DataEmulation = chkDataEmulation.Checked;
            updateControlVariables();
        }

        private void btnSavePicture_Click(object sender, EventArgs e)
        {
            savePictureToFile();
        }

        private void btnSaveParametersToFile_Click(object sender, EventArgs e)
        {
            StoreDataFromControlForm();
            SaveFileDialog ofd = new SaveFileDialog();
            ofd.InitialDirectory = "";
            ofd.Filter = "App Configuration Files | *.json";
            ofd.DefaultExt = "json";
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                app_settings.Save(ofd.FileName);
            }
        }

        private void btnLoadParametersFromFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = "";
            ofd.Filter = "App Configuration Files | *.json";
            ofd.DefaultExt = "json";
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                app_settings = MyAppSettings.Load(ofd.FileName);
                RestoreDataFromControlForm();
                UpdateControlForm();
                //updateControlVariables();
            }
       
        }

        private void chkDielectricPermittivity_CheckedChanged(object sender, EventArgs e)
        {
            settings.DistanceTimeCheckState = chkDielectricPermittivity.Checked;
            updateControlVariables();
        }

        private void nmrDielectricPermittivity_ValueChanged(object sender, EventArgs e)
        {
            settings.DielectricPermitivity = (int)nmrDielectricPermittivity.Value;
            updateControlVariables();
        }
    }

    /*
        class MyDataFile : AppSettings<MyDataFile>
        {
            public int myScanCounter = 1;
            public int myDataSize = 1;
            public string VisualisationMode = "green";
            public bool chkFilter = true;
            public double[] data_set = new double[1000];
        }
    */

    class MyAppSettings : AppSettings<MyAppSettings>
    {
        public string myString = "Hello World";
        public int myInteger = 1;

        public int InputDataScaleFactor = 1;
        public int ScanStep = 250;
        public string VisualisationMode = "green";
        public int StaticDelay = 1000;
        public int AveragingLineWindow = 1;
        public int AverageWindow = 1;
        public int AveragePoints = 1;
        public int DepthPoints = 768;
        public bool chkFilter = true;
        public int TxPulseWidth = 15;
        public int TxPulsePeriod = 0x7FFF;
        public bool chkDataEmulation = false;

    }





    static class ControlFormExtension
    {
        delegate int SetControlFormGetScanStepCallback(NumericUpDown nmr_ScanStep);
        delegate int SetControlFormGetDepthPointsCallback(NumericUpDown nmr_DepthPoints);
        delegate int SetControlFormGetAveragePointsCallback(NumericUpDown nmr_AveragePoints);
        delegate int SetControlFormGetAverageWindowCallback(NumericUpDown nmr_AverageWindow);
        delegate int SetControlFormGetAveragingLineWindowCallback(NumericUpDown nmr_AveragingLineWindow);
        delegate int SetControlFormGetInputDataScaleFactorCallback(NumericUpDown nmr_InputDataScaleFactor);
        delegate int SetControlFormGetStaticDelayCallback(NumericUpDown nmr_StaticDelay);
        delegate string SetControlFormGetVisualisationModeCallback(ComboBox cmb_VisualisationMode);



        public static string getVisualisationMode(this ComboBox cmb_VisualisationMode)
        {
            // If this method was called from the another thread.
            if (cmb_VisualisationMode.InvokeRequired)
            {
                var deleg = new SetControlFormGetVisualisationModeCallback(getVisualisationMode);
                return (string)cmb_VisualisationMode.Invoke(deleg, new object[] { cmb_VisualisationMode });

            }
            else
            {
                return (string)cmb_VisualisationMode.Text;
            }
        }

        public static int getStaticDelay(this NumericUpDown nmr_StaticDelay)
        {
            // If this method was called from the another thread.
            if (nmr_StaticDelay.InvokeRequired)
            {
                var deleg = new SetControlFormGetAverageWindowCallback(getStaticDelay);

                return (int)nmr_StaticDelay.Invoke(deleg, new object[] { nmr_StaticDelay });

            }
            else
            {
                return (int)nmr_StaticDelay.Value;
            }
        }

        public static int getInputDataScaleFactor(this NumericUpDown nmr_InputDataScaleFactor)
        {
            // If this method was called from the another thread.
            if (nmr_InputDataScaleFactor.InvokeRequired)
            {
                var deleg = new SetControlFormGetAverageWindowCallback(getInputDataScaleFactor);

                return (int)nmr_InputDataScaleFactor.Invoke(deleg, new object[] { nmr_InputDataScaleFactor });

            }
            else
            {
                return (int)nmr_InputDataScaleFactor.Value;
            }
        }

        public static int getAveragingLineWindow(this NumericUpDown nmr_AveragingLineWindow)
        {
            // If this method was called from the another thread.
            if (nmr_AveragingLineWindow.InvokeRequired)
            {
                var deleg = new SetControlFormGetAverageWindowCallback(getAverageWindow);

                return (int)nmr_AveragingLineWindow.Invoke(deleg, new object[] { nmr_AveragingLineWindow });

            }
            else
            {
                return (int)nmr_AveragingLineWindow.Value;
            }
        }

        public static int getAverageWindow(this NumericUpDown nmr_AverageWindow)
        {
            // If this method was called from the another thread.
            if (nmr_AverageWindow.InvokeRequired)
            {
                var deleg = new SetControlFormGetAverageWindowCallback(getAverageWindow);

                return (int)nmr_AverageWindow.Invoke(deleg, new object[] { nmr_AverageWindow });

            }
            else
            {
                return (int)nmr_AverageWindow.Value;
            }
        }

        public static int getAveragePoints(this NumericUpDown nmr_AveragePoints)
        {
            // If this method was called from the another thread.
            if (nmr_AveragePoints.InvokeRequired)
            {
                var deleg = new SetControlFormGetAveragePointsCallback(getAveragePoints);

                return (int)nmr_AveragePoints.Invoke(deleg, new object[] { nmr_AveragePoints });

            }
            else
            {
                return (int)nmr_AveragePoints.Value;
            }
        }

        public static int getDepthPoints(this NumericUpDown nmr_DepthPoints)
        {
            // If this method was called from the another thread.
            if (nmr_DepthPoints.InvokeRequired)
            {
                var deleg = new SetControlFormGetDepthPointsCallback(getDepthPoints);

                return (int)nmr_DepthPoints.Invoke(deleg, new object[] { nmr_DepthPoints });

            }
            else
            {
                return (int)nmr_DepthPoints.Value;
            }
        }

        public static int getScanStep(this NumericUpDown nmr_ScanStep)
        {
            // If this method was called from the another thread.
            if (nmr_ScanStep.InvokeRequired)
            {
                var deleg = new SetControlFormGetScanStepCallback(getScanStep);

                return (int)nmr_ScanStep.Invoke(deleg, new object[] { nmr_ScanStep });

            }
            else
            {
                return (int)nmr_ScanStep.Value;
            }
        }

    }


}
