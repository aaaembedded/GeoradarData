
namespace GeoradarGui
{
    partial class ControlForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.nmr_StaticDelay = new System.Windows.Forms.NumericUpDown();
            this.lblStaticDelayValue = new System.Windows.Forms.Label();
            this.lblNumberOfDipPoints = new System.Windows.Forms.Label();
            this.nmr_DepthPoints = new System.Windows.Forms.NumericUpDown();
            this.lblScanStep = new System.Windows.Forms.Label();
            this.nmr_ScanStep = new System.Windows.Forms.NumericUpDown();
            this.lblAveragePoints = new System.Windows.Forms.Label();
            this.nmr_AveragePoints = new System.Windows.Forms.NumericUpDown();
            this.btnScanStart = new System.Windows.Forms.Button();
            this.btnScanStop = new System.Windows.Forms.Button();
            this.btnSavePicture = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.nmr_TxPulseWidth = new System.Windows.Forms.NumericUpDown();
            this.lblTxPulseWidth = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkDielectricPermittivity = new System.Windows.Forms.CheckBox();
            this.lblDielectricPermittivity = new System.Windows.Forms.Label();
            this.nmrDielectricPermittivity = new System.Windows.Forms.NumericUpDown();
            this.nmr_InputDataScaleFactor = new System.Windows.Forms.NumericUpDown();
            this.lblScaleFactorOfInputData = new System.Windows.Forms.Label();
            this.nmr_AveragingLineWindow = new System.Windows.Forms.NumericUpDown();
            this.lblAveragingLiveData = new System.Windows.Forms.Label();
            this.nmr_AverageWindow = new System.Windows.Forms.NumericUpDown();
            this.lblAverageWindow = new System.Windows.Forms.Label();
            this.chkFilter = new System.Windows.Forms.CheckBox();
            this.btn_LoadParametersFromDevice = new System.Windows.Forms.Button();
            this.btn_SaveParametersToDevice = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cmb_VisualisationMode = new System.Windows.Forms.ComboBox();
            this.lblTypeOfVisualisationMode = new System.Windows.Forms.Label();
            this.btnSaveParametersToFile = new System.Windows.Forms.Button();
            this.btnLoadParametersFromFile = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.chkDataEmulation = new System.Windows.Forms.CheckBox();
            this.lblTxPulsePeriodValue = new System.Windows.Forms.Label();
            this.lblTxPulseWidthValue = new System.Windows.Forms.Label();
            this.lblTxPulsePeriod = new System.Windows.Forms.Label();
            this.nmr_TxPulsePeriod = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nmr_StaticDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmr_DepthPoints)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmr_ScanStep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmr_AveragePoints)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmr_TxPulseWidth)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmrDielectricPermittivity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmr_InputDataScaleFactor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmr_AveragingLineWindow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmr_AverageWindow)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmr_TxPulsePeriod)).BeginInit();
            this.SuspendLayout();
            // 
            // nmr_StaticDelay
            // 
            this.nmr_StaticDelay.Increment = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.nmr_StaticDelay.Location = new System.Drawing.Point(10, 104);
            this.nmr_StaticDelay.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nmr_StaticDelay.Name = "nmr_StaticDelay";
            this.nmr_StaticDelay.Size = new System.Drawing.Size(108, 20);
            this.nmr_StaticDelay.TabIndex = 0;
            this.nmr_StaticDelay.Value = new decimal(new int[] {
            410000,
            0,
            0,
            0});
            this.nmr_StaticDelay.ValueChanged += new System.EventHandler(this.nmr_StaticDelay_ValueChanged);
            // 
            // lblStaticDelayValue
            // 
            this.lblStaticDelayValue.AutoSize = true;
            this.lblStaticDelayValue.Location = new System.Drawing.Point(9, 90);
            this.lblStaticDelayValue.Name = "lblStaticDelayValue";
            this.lblStaticDelayValue.Size = new System.Drawing.Size(113, 13);
            this.lblStaticDelayValue.TabIndex = 1;
            this.lblStaticDelayValue.Text = "Static Delay Value, pS";
            // 
            // lblNumberOfDipPoints
            // 
            this.lblNumberOfDipPoints.AutoSize = true;
            this.lblNumberOfDipPoints.Location = new System.Drawing.Point(8, 53);
            this.lblNumberOfDipPoints.Name = "lblNumberOfDipPoints";
            this.lblNumberOfDipPoints.Size = new System.Drawing.Size(122, 13);
            this.lblNumberOfDipPoints.TabIndex = 2;
            this.lblNumberOfDipPoints.Text = "Number Of Depth Points";
            // 
            // nmr_DepthPoints
            // 
            this.nmr_DepthPoints.Location = new System.Drawing.Point(9, 66);
            this.nmr_DepthPoints.Maximum = new decimal(new int[] {
            768,
            0,
            0,
            0});
            this.nmr_DepthPoints.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nmr_DepthPoints.Name = "nmr_DepthPoints";
            this.nmr_DepthPoints.Size = new System.Drawing.Size(120, 20);
            this.nmr_DepthPoints.TabIndex = 3;
            this.nmr_DepthPoints.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nmr_DepthPoints.ValueChanged += new System.EventHandler(this.nmr_DepthPoints_ValueChanged);
            // 
            // lblScanStep
            // 
            this.lblScanStep.AutoSize = true;
            this.lblScanStep.Location = new System.Drawing.Point(3, 16);
            this.lblScanStep.Name = "lblScanStep";
            this.lblScanStep.Size = new System.Drawing.Size(123, 13);
            this.lblScanStep.TabIndex = 4;
            this.lblScanStep.Text = "Scan Step, picoseconds";
            // 
            // nmr_ScanStep
            // 
            this.nmr_ScanStep.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.nmr_ScanStep.Increment = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.nmr_ScanStep.Location = new System.Drawing.Point(9, 32);
            this.nmr_ScanStep.Maximum = new decimal(new int[] {
            187500,
            0,
            0,
            0});
            this.nmr_ScanStep.Minimum = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.nmr_ScanStep.Name = "nmr_ScanStep";
            this.nmr_ScanStep.Size = new System.Drawing.Size(120, 20);
            this.nmr_ScanStep.TabIndex = 5;
            this.nmr_ScanStep.Value = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.nmr_ScanStep.ValueChanged += new System.EventHandler(this.nmr_ScanStep_ValueChanged);
            // 
            // lblAveragePoints
            // 
            this.lblAveragePoints.AutoSize = true;
            this.lblAveragePoints.Location = new System.Drawing.Point(7, 86);
            this.lblAveragePoints.Name = "lblAveragePoints";
            this.lblAveragePoints.Size = new System.Drawing.Size(78, 13);
            this.lblAveragePoints.TabIndex = 6;
            this.lblAveragePoints.Text = "Average points";
            // 
            // nmr_AveragePoints
            // 
            this.nmr_AveragePoints.Enabled = false;
            this.nmr_AveragePoints.Location = new System.Drawing.Point(9, 101);
            this.nmr_AveragePoints.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nmr_AveragePoints.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmr_AveragePoints.Name = "nmr_AveragePoints";
            this.nmr_AveragePoints.Size = new System.Drawing.Size(120, 20);
            this.nmr_AveragePoints.TabIndex = 7;
            this.nmr_AveragePoints.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmr_AveragePoints.ValueChanged += new System.EventHandler(this.nmr_AveragePoints_ValueChanged);
            // 
            // btnScanStart
            // 
            this.btnScanStart.Location = new System.Drawing.Point(165, 101);
            this.btnScanStart.Name = "btnScanStart";
            this.btnScanStart.Size = new System.Drawing.Size(75, 45);
            this.btnScanStart.TabIndex = 8;
            this.btnScanStart.Text = "Scan Start";
            this.btnScanStart.UseVisualStyleBackColor = true;
            this.btnScanStart.Click += new System.EventHandler(this.btnScanStart_Click);
            // 
            // btnScanStop
            // 
            this.btnScanStop.Location = new System.Drawing.Point(165, 101);
            this.btnScanStop.Name = "btnScanStop";
            this.btnScanStop.Size = new System.Drawing.Size(75, 45);
            this.btnScanStop.TabIndex = 9;
            this.btnScanStop.Text = "Scan Stop";
            this.btnScanStop.UseVisualStyleBackColor = true;
            this.btnScanStop.Visible = false;
            this.btnScanStop.Click += new System.EventHandler(this.btnScanStop_Click);
            // 
            // btnSavePicture
            // 
            this.btnSavePicture.Location = new System.Drawing.Point(165, 509);
            this.btnSavePicture.Name = "btnSavePicture";
            this.btnSavePicture.Size = new System.Drawing.Size(75, 50);
            this.btnSavePicture.TabIndex = 10;
            this.btnSavePicture.Text = "Save Picture";
            this.btnSavePicture.UseVisualStyleBackColor = true;
            this.btnSavePicture.Click += new System.EventHandler(this.btnSavePicture_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox2.Controls.Add(this.lblScanStep);
            this.groupBox2.Controls.Add(this.nmr_ScanStep);
            this.groupBox2.Controls.Add(this.lblNumberOfDipPoints);
            this.groupBox2.Controls.Add(this.nmr_DepthPoints);
            this.groupBox2.Controls.Add(this.lblAveragePoints);
            this.groupBox2.Controls.Add(this.nmr_AveragePoints);
            this.groupBox2.Location = new System.Drawing.Point(12, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(147, 128);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Runtime Parameters";
            // 
            // nmr_TxPulseWidth
            // 
            this.nmr_TxPulseWidth.Location = new System.Drawing.Point(10, 30);
            this.nmr_TxPulseWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmr_TxPulseWidth.Name = "nmr_TxPulseWidth";
            this.nmr_TxPulseWidth.Size = new System.Drawing.Size(69, 20);
            this.nmr_TxPulseWidth.TabIndex = 9;
            this.nmr_TxPulseWidth.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.nmr_TxPulseWidth.ValueChanged += new System.EventHandler(this.nmr_TxPulseWidth_ValueChanged);
            // 
            // lblTxPulseWidth
            // 
            this.lblTxPulseWidth.AutoSize = true;
            this.lblTxPulseWidth.Location = new System.Drawing.Point(7, 16);
            this.lblTxPulseWidth.Name = "lblTxPulseWidth";
            this.lblTxPulseWidth.Size = new System.Drawing.Size(79, 13);
            this.lblTxPulseWidth.TabIndex = 8;
            this.lblTxPulseWidth.Text = "Tx Pulse Width";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkDielectricPermittivity);
            this.groupBox3.Controls.Add(this.lblDielectricPermittivity);
            this.groupBox3.Controls.Add(this.nmrDielectricPermittivity);
            this.groupBox3.Controls.Add(this.nmr_InputDataScaleFactor);
            this.groupBox3.Controls.Add(this.lblScaleFactorOfInputData);
            this.groupBox3.Controls.Add(this.nmr_AveragingLineWindow);
            this.groupBox3.Controls.Add(this.lblAveragingLiveData);
            this.groupBox3.Controls.Add(this.nmr_AverageWindow);
            this.groupBox3.Controls.Add(this.lblAverageWindow);
            this.groupBox3.Controls.Add(this.chkFilter);
            this.groupBox3.Location = new System.Drawing.Point(12, 277);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(147, 219);
            this.groupBox3.TabIndex = 17;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Post Processing Options";
            // 
            // chkDielectricPermittivity
            // 
            this.chkDielectricPermittivity.AutoSize = true;
            this.chkDielectricPermittivity.Location = new System.Drawing.Point(10, 196);
            this.chkDielectricPermittivity.Name = "chkDielectricPermittivity";
            this.chkDielectricPermittivity.Size = new System.Drawing.Size(96, 17);
            this.chkDielectricPermittivity.TabIndex = 9;
            this.chkDielectricPermittivity.Text = "Distance/Time";
            this.chkDielectricPermittivity.UseVisualStyleBackColor = true;
            this.chkDielectricPermittivity.CheckedChanged += new System.EventHandler(this.chkDielectricPermittivity_CheckedChanged);
            // 
            // lblDielectricPermittivity
            // 
            this.lblDielectricPermittivity.AutoSize = true;
            this.lblDielectricPermittivity.Location = new System.Drawing.Point(9, 158);
            this.lblDielectricPermittivity.Name = "lblDielectricPermittivity";
            this.lblDielectricPermittivity.Size = new System.Drawing.Size(103, 13);
            this.lblDielectricPermittivity.TabIndex = 8;
            this.lblDielectricPermittivity.Text = "Dielectric permittivity";
            // 
            // nmrDielectricPermittivity
            // 
            this.nmrDielectricPermittivity.Location = new System.Drawing.Point(9, 173);
            this.nmrDielectricPermittivity.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmrDielectricPermittivity.Name = "nmrDielectricPermittivity";
            this.nmrDielectricPermittivity.Size = new System.Drawing.Size(120, 20);
            this.nmrDielectricPermittivity.TabIndex = 7;
            this.nmrDielectricPermittivity.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmrDielectricPermittivity.ValueChanged += new System.EventHandler(this.nmrDielectricPermittivity_ValueChanged);
            // 
            // nmr_InputDataScaleFactor
            // 
            this.nmr_InputDataScaleFactor.Location = new System.Drawing.Point(9, 32);
            this.nmr_InputDataScaleFactor.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nmr_InputDataScaleFactor.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmr_InputDataScaleFactor.Name = "nmr_InputDataScaleFactor";
            this.nmr_InputDataScaleFactor.Size = new System.Drawing.Size(120, 20);
            this.nmr_InputDataScaleFactor.TabIndex = 6;
            this.nmr_InputDataScaleFactor.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmr_InputDataScaleFactor.ValueChanged += new System.EventHandler(this.nmr_InputDataScaleFactor_ValueChanged);
            // 
            // lblScaleFactorOfInputData
            // 
            this.lblScaleFactorOfInputData.AutoSize = true;
            this.lblScaleFactorOfInputData.Location = new System.Drawing.Point(7, 16);
            this.lblScaleFactorOfInputData.Name = "lblScaleFactorOfInputData";
            this.lblScaleFactorOfInputData.Size = new System.Drawing.Size(137, 13);
            this.lblScaleFactorOfInputData.TabIndex = 5;
            this.lblScaleFactorOfInputData.Text = "Scale Factor Of Input Data ";
            // 
            // nmr_AveragingLineWindow
            // 
            this.nmr_AveragingLineWindow.Location = new System.Drawing.Point(10, 71);
            this.nmr_AveragingLineWindow.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmr_AveragingLineWindow.Name = "nmr_AveragingLineWindow";
            this.nmr_AveragingLineWindow.Size = new System.Drawing.Size(120, 20);
            this.nmr_AveragingLineWindow.TabIndex = 4;
            this.nmr_AveragingLineWindow.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmr_AveragingLineWindow.ValueChanged += new System.EventHandler(this.nmr_AveragingLineWindow_ValueChanged);
            // 
            // lblAveragingLiveData
            // 
            this.lblAveragingLiveData.AutoSize = true;
            this.lblAveragingLiveData.Location = new System.Drawing.Point(7, 55);
            this.lblAveragingLiveData.Name = "lblAveragingLiveData";
            this.lblAveragingLiveData.Size = new System.Drawing.Size(120, 13);
            this.lblAveragingLiveData.TabIndex = 3;
            this.lblAveragingLiveData.Text = "Averaging Live Window";
            // 
            // nmr_AverageWindow
            // 
            this.nmr_AverageWindow.Location = new System.Drawing.Point(10, 110);
            this.nmr_AverageWindow.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nmr_AverageWindow.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmr_AverageWindow.Name = "nmr_AverageWindow";
            this.nmr_AverageWindow.Size = new System.Drawing.Size(120, 20);
            this.nmr_AverageWindow.TabIndex = 2;
            this.nmr_AverageWindow.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmr_AverageWindow.ValueChanged += new System.EventHandler(this.nmr_AverageWindow_ValueChanged);
            // 
            // lblAverageWindow
            // 
            this.lblAverageWindow.AutoSize = true;
            this.lblAverageWindow.Location = new System.Drawing.Point(7, 94);
            this.lblAverageWindow.Name = "lblAverageWindow";
            this.lblAverageWindow.Size = new System.Drawing.Size(89, 13);
            this.lblAverageWindow.TabIndex = 1;
            this.lblAverageWindow.Text = "Average Window";
            // 
            // chkFilter
            // 
            this.chkFilter.AutoSize = true;
            this.chkFilter.Location = new System.Drawing.Point(10, 136);
            this.chkFilter.Name = "chkFilter";
            this.chkFilter.Size = new System.Drawing.Size(84, 17);
            this.chkFilter.TabIndex = 0;
            this.chkFilter.Text = "Filter On/Off";
            this.chkFilter.UseVisualStyleBackColor = true;
            this.chkFilter.CheckedChanged += new System.EventHandler(this.chkFilter_CheckedChanged);
            // 
            // btn_LoadParametersFromDevice
            // 
            this.btn_LoadParametersFromDevice.Location = new System.Drawing.Point(165, 8);
            this.btn_LoadParametersFromDevice.Name = "btn_LoadParametersFromDevice";
            this.btn_LoadParametersFromDevice.Size = new System.Drawing.Size(75, 47);
            this.btn_LoadParametersFromDevice.TabIndex = 18;
            this.btn_LoadParametersFromDevice.Text = "Load Parameters From Device";
            this.btn_LoadParametersFromDevice.UseVisualStyleBackColor = true;
            this.btn_LoadParametersFromDevice.Click += new System.EventHandler(this.btn_LoadDeviceParameters_Click);
            // 
            // btn_SaveParametersToDevice
            // 
            this.btn_SaveParametersToDevice.Location = new System.Drawing.Point(165, 55);
            this.btn_SaveParametersToDevice.Name = "btn_SaveParametersToDevice";
            this.btn_SaveParametersToDevice.Size = new System.Drawing.Size(75, 47);
            this.btn_SaveParametersToDevice.TabIndex = 19;
            this.btn_SaveParametersToDevice.Text = "Save Parameters to Device";
            this.btn_SaveParametersToDevice.UseVisualStyleBackColor = true;
            this.btn_SaveParametersToDevice.Click += new System.EventHandler(this.btn_SaveParametersToDevice_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cmb_VisualisationMode);
            this.groupBox4.Controls.Add(this.lblTypeOfVisualisationMode);
            this.groupBox4.Location = new System.Drawing.Point(12, 502);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(147, 65);
            this.groupBox4.TabIndex = 20;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Visualization";
            // 
            // cmb_VisualisationMode
            // 
            this.cmb_VisualisationMode.FormattingEnabled = true;
            this.cmb_VisualisationMode.Items.AddRange(new object[] {
            "gray",
            "blue",
            "green",
            "spectrum"});
            this.cmb_VisualisationMode.Location = new System.Drawing.Point(9, 32);
            this.cmb_VisualisationMode.Name = "cmb_VisualisationMode";
            this.cmb_VisualisationMode.Size = new System.Drawing.Size(117, 21);
            this.cmb_VisualisationMode.TabIndex = 1;
            this.cmb_VisualisationMode.Text = "spectrum";
            this.cmb_VisualisationMode.TextChanged += new System.EventHandler(this.cmb_VisualisationMode_TextChanged);
            // 
            // lblTypeOfVisualisationMode
            // 
            this.lblTypeOfVisualisationMode.AutoSize = true;
            this.lblTypeOfVisualisationMode.Location = new System.Drawing.Point(7, 16);
            this.lblTypeOfVisualisationMode.Name = "lblTypeOfVisualisationMode";
            this.lblTypeOfVisualisationMode.Size = new System.Drawing.Size(136, 13);
            this.lblTypeOfVisualisationMode.TabIndex = 0;
            this.lblTypeOfVisualisationMode.Text = "Type Of Visualisation Mode";
            // 
            // btnSaveParametersToFile
            // 
            this.btnSaveParametersToFile.Location = new System.Drawing.Point(165, 398);
            this.btnSaveParametersToFile.Name = "btnSaveParametersToFile";
            this.btnSaveParametersToFile.Size = new System.Drawing.Size(75, 50);
            this.btnSaveParametersToFile.TabIndex = 21;
            this.btnSaveParametersToFile.Text = "Save Parameters to File";
            this.btnSaveParametersToFile.UseVisualStyleBackColor = true;
            this.btnSaveParametersToFile.Click += new System.EventHandler(this.btnSaveParametersToFile_Click);
            // 
            // btnLoadParametersFromFile
            // 
            this.btnLoadParametersFromFile.Location = new System.Drawing.Point(165, 342);
            this.btnLoadParametersFromFile.Name = "btnLoadParametersFromFile";
            this.btnLoadParametersFromFile.Size = new System.Drawing.Size(75, 50);
            this.btnLoadParametersFromFile.TabIndex = 22;
            this.btnLoadParametersFromFile.Text = "Load Parameters From File";
            this.btnLoadParametersFromFile.UseVisualStyleBackColor = true;
            this.btnLoadParametersFromFile.Click += new System.EventHandler(this.btnLoadParametersFromFile_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.nmr_StaticDelay);
            this.groupBox5.Controls.Add(this.lblStaticDelayValue);
            this.groupBox5.Controls.Add(this.chkDataEmulation);
            this.groupBox5.Controls.Add(this.lblTxPulsePeriodValue);
            this.groupBox5.Controls.Add(this.lblTxPulseWidthValue);
            this.groupBox5.Controls.Add(this.lblTxPulsePeriod);
            this.groupBox5.Controls.Add(this.nmr_TxPulsePeriod);
            this.groupBox5.Controls.Add(this.lblTxPulseWidth);
            this.groupBox5.Controls.Add(this.nmr_TxPulseWidth);
            this.groupBox5.Location = new System.Drawing.Point(12, 147);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(228, 129);
            this.groupBox5.TabIndex = 23;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "HW, dangerous! ";
            // 
            // chkDataEmulation
            // 
            this.chkDataEmulation.AutoSize = true;
            this.chkDataEmulation.Location = new System.Drawing.Point(133, 12);
            this.chkDataEmulation.Name = "chkDataEmulation";
            this.chkDataEmulation.Size = new System.Drawing.Size(89, 17);
            this.chkDataEmulation.TabIndex = 14;
            this.chkDataEmulation.Text = "Emulation On";
            this.chkDataEmulation.UseVisualStyleBackColor = true;
            this.chkDataEmulation.CheckedChanged += new System.EventHandler(this.chkDataEmulation_CheckedChanged);
            // 
            // lblTxPulsePeriodValue
            // 
            this.lblTxPulsePeriodValue.AutoSize = true;
            this.lblTxPulsePeriodValue.Location = new System.Drawing.Point(83, 69);
            this.lblTxPulsePeriodValue.Name = "lblTxPulsePeriodValue";
            this.lblTxPulsePeriodValue.Size = new System.Drawing.Size(35, 13);
            this.lblTxPulsePeriodValue.TabIndex = 13;
            this.lblTxPulsePeriodValue.Text = "- .. Hz";
            // 
            // lblTxPulseWidthValue
            // 
            this.lblTxPulseWidthValue.AutoSize = true;
            this.lblTxPulseWidthValue.Location = new System.Drawing.Point(83, 31);
            this.lblTxPulseWidthValue.Name = "lblTxPulseWidthValue";
            this.lblTxPulseWidthValue.Size = new System.Drawing.Size(35, 13);
            this.lblTxPulseWidthValue.TabIndex = 12;
            this.lblTxPulseWidthValue.Text = "- .. nS";
            // 
            // lblTxPulsePeriod
            // 
            this.lblTxPulsePeriod.AutoSize = true;
            this.lblTxPulsePeriod.Location = new System.Drawing.Point(9, 53);
            this.lblTxPulsePeriod.Name = "lblTxPulsePeriod";
            this.lblTxPulsePeriod.Size = new System.Drawing.Size(110, 13);
            this.lblTxPulsePeriod.TabIndex = 11;
            this.lblTxPulsePeriod.Text = "Tx Pulse Timer Period";
            // 
            // nmr_TxPulsePeriod
            // 
            this.nmr_TxPulsePeriod.Location = new System.Drawing.Point(10, 66);
            this.nmr_TxPulsePeriod.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nmr_TxPulsePeriod.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nmr_TxPulsePeriod.Name = "nmr_TxPulsePeriod";
            this.nmr_TxPulsePeriod.Size = new System.Drawing.Size(70, 20);
            this.nmr_TxPulsePeriod.TabIndex = 10;
            this.nmr_TxPulsePeriod.Value = new decimal(new int[] {
            32768,
            0,
            0,
            0});
            this.nmr_TxPulsePeriod.ValueChanged += new System.EventHandler(this.nmr_TxPulsePeriod_ValueChanged);
            // 
            // ControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(255, 581);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.btnLoadParametersFromFile);
            this.Controls.Add(this.btnSaveParametersToFile);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.btn_SaveParametersToDevice);
            this.Controls.Add(this.btn_LoadParametersFromDevice);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnSavePicture);
            this.Controls.Add(this.btnScanStop);
            this.Controls.Add(this.btnScanStart);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ControlForm";
            this.Text = "Control Panel";
            this.Load += new System.EventHandler(this.ControlForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nmr_StaticDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmr_DepthPoints)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmr_ScanStep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmr_AveragePoints)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmr_TxPulseWidth)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmrDielectricPermittivity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmr_InputDataScaleFactor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmr_AveragingLineWindow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmr_AverageWindow)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmr_TxPulsePeriod)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nmr_StaticDelay;
        private System.Windows.Forms.Label lblStaticDelayValue;
        private System.Windows.Forms.Label lblNumberOfDipPoints;
        private System.Windows.Forms.NumericUpDown nmr_DepthPoints;
        private System.Windows.Forms.Label lblScanStep;
        private System.Windows.Forms.NumericUpDown nmr_ScanStep;
        private System.Windows.Forms.Label lblAveragePoints;
        private System.Windows.Forms.NumericUpDown nmr_AveragePoints;
        private System.Windows.Forms.Button btnScanStart;
        private System.Windows.Forms.Button btnScanStop;
        private System.Windows.Forms.Button btnSavePicture;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.NumericUpDown nmr_AverageWindow;
        private System.Windows.Forms.Label lblAverageWindow;
        private System.Windows.Forms.CheckBox chkFilter;
        private System.Windows.Forms.NumericUpDown nmr_InputDataScaleFactor;
        private System.Windows.Forms.Label lblScaleFactorOfInputData;
        private System.Windows.Forms.NumericUpDown nmr_AveragingLineWindow;
        private System.Windows.Forms.Label lblAveragingLiveData;
        private System.Windows.Forms.Button btn_LoadParametersFromDevice;
        private System.Windows.Forms.Button btn_SaveParametersToDevice;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label lblTypeOfVisualisationMode;
        private System.Windows.Forms.ComboBox cmb_VisualisationMode;
        private System.Windows.Forms.Button btnSaveParametersToFile;
        private System.Windows.Forms.Button btnLoadParametersFromFile;
        private System.Windows.Forms.NumericUpDown nmr_TxPulseWidth;
        private System.Windows.Forms.Label lblTxPulseWidth;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label lblTxPulsePeriod;
        private System.Windows.Forms.NumericUpDown nmr_TxPulsePeriod;
        private System.Windows.Forms.Label lblTxPulsePeriodValue;
        private System.Windows.Forms.Label lblTxPulseWidthValue;
        private System.Windows.Forms.CheckBox chkDataEmulation;
        private System.Windows.Forms.NumericUpDown nmrDielectricPermittivity;
        private System.Windows.Forms.Label lblDielectricPermittivity;
        private System.Windows.Forms.CheckBox chkDielectricPermittivity;
    }
}