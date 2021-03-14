using System;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using ZedGraph;


namespace GeoradarGui
{
    public partial class LoadDataForm : BaseDockContent
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        public delegate void LoadDataToTextBoxDelegat(PointPairList _pointPairList);
        public event LoadDataToTextBoxDelegat loadDataToTextBoxEvent;


        byte[] packet_data_load;


        public LoadDataForm()
        {
            InitializeComponent();
            packet_data_load = new byte[PacketDefinition.DEFAULT_FLASH_DATA_DOTS_NUMBER * 6];
        }

        public void writeDataToForm(PointPairList _pointPairList)
        {
            // Clear current data in Text Box:
            richTextBox1.ClearBoxTextFromThread();
            // Load
            String tmp_text = "";
            foreach (var i in _pointPairList)
            {
                tmp_text += i.X + "," + i.Y + "," + i.Z + Environment.NewLine; //" Appended text";
            }
            tmp_text = tmp_text.Replace(")", "");
            tmp_text = tmp_text.Replace("(", "");
            tmp_text = tmp_text.Replace(" ", "");
            richTextBox1.AppendTextFromThread(tmp_text, Color.Black);
        }

        public void writeDataToBox(string message)
        {
            richTextBox1.Text += message + Environment.NewLine;
        }


        private void btnClearAll_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            //ofd.InitialDirectory = "c:\\";
            ofd.InitialDirectory = "";
            // ofd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            ofd.FilterIndex = 2;
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                richTextBox1.LoadFile(ofd.FileName, RichTextBoxStreamType.PlainText);

                // Add loaded data to Graph
                PointPairList _pointPairList = new PointPairList();
                string[] one_pair = richTextBox1.Text.Split('\n');
                foreach (string pair in one_pair)
                {
                    string[] digits_pair = pair.Split(',');
                    try
                    {
                        if (digits_pair.Count() > 2)
                        {
                            PointPair _pointPair = new PointPair(Convert.ToInt32(digits_pair[0]), Convert.ToInt32(digits_pair[1]), Convert.ToInt32(digits_pair[2]));
                            _pointPairList.Add(_pointPair);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Some error happend during loaded file converting to digital array!");
                    }

                }

                putDataToPacketDataLoad(_pointPairList);

                // Call delegate function to show data on Graph:
                if (loadDataToTextBoxEvent != null)
                {
                    loadDataToTextBoxEvent(_pointPairList);
                }
            }
        }

        public byte[] getPacketDataLoadBuffer()
        {
            return packet_data_load;
        }

        public void putDataToPacketDataLoad(PointPairList _pointPairList)
        {
            Int32 index = 0;
            byte[] point;
            while (index < _pointPairList.Count())
            {
                point = BitConverter.GetBytes((short)_pointPairList[index].X);
                packet_data_load[(index * 6) + 0] = point[0];
                packet_data_load[(index * 6) + 1] = point[1];
                point = BitConverter.GetBytes((short)_pointPairList[index].Y);
                packet_data_load[(index * 6) + 2] = point[0];
                packet_data_load[(index * 6) + 3] = point[1];
                point = BitConverter.GetBytes((short)_pointPairList[index].Z);
                packet_data_load[(index * 6) + 4] = point[0];
                packet_data_load[(index * 6) + 5] = point[1];
                index++;
                // Checking out of array index: 
                if (index >= packet_data_load.Length)
                {
                    break;
                }
            }
        }





        private void btnSaveFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog ofd = new SaveFileDialog();
            ofd.InitialDirectory = "";
            ofd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            ofd.FilterIndex = 2;
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                richTextBox1.SaveFile(ofd.FileName, RichTextBoxStreamType.PlainText);
            }
        }

    }

    static class RichTextBoxExtensions
    {
        delegate void SetTextCallback(RichTextBox box, string text, Color color);
        delegate void SetClearBoxCallback(RichTextBox box);
        /// <summary>
        /// This extension method appends collored text.
        /// </summary>
        public static void AppendTextFromThread(this RichTextBox box, string text, Color color)
        {
            // If this method was called from the another thread.
            if (box.InvokeRequired)
            {
                var deleg = new SetTextCallback(AppendTextFromThread);
                box.Invoke(deleg, new object[] { box, text, color });
            }
            else
            {
                box.SelectionStart = box.TextLength;
                box.SelectionColor = color;
                box.SelectedText = text;

                if (Properties.Settings.Default.DebuggingTextBoxAutoScroll)
                {
                    box.ScrollToCaret();
                }
            }
        }

        /// <summary>
        /// This extension method appends collored text.
        /// </summary>
        public static void ClearBoxTextFromThread(this RichTextBox box)
        {
            // If this method was called from the another thread.
            if (box.InvokeRequired)
            {
                var deleg = new SetClearBoxCallback(ClearBoxTextFromThread);
                box.Invoke(deleg, new object[] { box});
            }
            else
            {
                box.Clear();
            }
        }


    }


}
