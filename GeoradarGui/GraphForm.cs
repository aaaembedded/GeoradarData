using System;
using System.Collections.Generic;
using System.Drawing;
using ZedGraph;

namespace GeoradarGui
{

    struct GraphDotData
    {
        public Int16 _i16_X { get; set; }
        public Int16 _i16_Y { get; set; }
        public Int16 _i16_Z { get; set; }
    }


    //public partial class GraphForm : DockContent
    public partial class GraphForm : BaseDockContent
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //GraphPane graphPane;

        Dictionary<uint, ValueData> valueData;

        public GraphForm()
        {
            InitializeComponent();
            valueData = new Dictionary<uint, ValueData>();
            zedGraphControl1.GraphPane.XAxis.Title.Text = "Depth points, (each points is time of one scan step)";
            zedGraphControl1.GraphPane.YAxis.Title.Text = "Rx amplitude, points)";

            //zedGraphControl1.GraphPane.Chart.Fill.Color = SystemColors.ControlText;

            zedGraphControl1.GraphPane.Chart.Fill = new Fill(Color.White, Color.LightGoldenrodYellow, 45F);
            zedGraphControl1.GraphPane.Fill.Color = Color.LightGoldenrodYellow;
            //zedGraphControl1.GraphPane.Fill.Color = SystemColors.ControlText;

            //zedGraphControl1.GraphPane.YAxis.Scale.IsReverse = true;
        }

        public void dockform_MyGraphForm()
        {
            InitializeComponent();
        }


        public Type GetType(uint address)
        {
            Type type = null;
            lock (valueData)
            {
                ValueData data;
                if (valueData.TryGetValue(address, out data))
                {
                    type = data.Type;
                }
            }
            return type;
        }

        public void writeDataToGraph(PointPairList _pointPairList)
        {
            PointPairList _XpointPairList = new PointPairList();
            PointPairList _YpointPairList = new PointPairList(); ;
            PointPairList _ZpointPairList = new PointPairList(); ;

            // Creating 3 datasets for three curves:
            int counter = 0;
            foreach (PointPair i in _pointPairList)
            {
                PointPair _XpointPair = new PointPair(counter, i.X);
                _XpointPairList.Add(_XpointPair);

                PointPair _YpointPair = new PointPair(counter, i.Y);
                _YpointPairList.Add(_YpointPair);

                PointPair _ZpointPair = new PointPair(counter, i.Z);
                _ZpointPairList.Add(_ZpointPair);

                counter++;
            }
            // Remove old curves:
            //graphPane.CurveList.Clear();

            // Add new curves set and update ZedGraph components:
            zedGraphControl1.CurvesListCleanFromThread();
            zedGraphControl1.AddGraphFromThread("acc X", _XpointPairList, Color.Red, SymbolType.Circle);
            zedGraphControl1.AddGraphFromThread("acc Y", _YpointPairList, Color.Green, SymbolType.Circle);
            zedGraphControl1.AddGraphFromThread("acc Z", _ZpointPairList, Color.Blue, SymbolType.Circle);
        }


        public void writeOneOrdinateGraph(PointPairList _pointPairList, double parameter)
        {
            PointPairList _XpointPairList = new PointPairList();
            // Creating 3 datasets for three curves:
            int counter = 0;
            foreach (PointPair i in _pointPairList)
            {
                PointPair _XpointPair = new PointPair(counter * parameter, i.X);
                //PointPair _XpointPair = new PointPair(i.X, counter);
                _XpointPairList.Add(_XpointPair);
                counter++;
            }
            // Remove old curves:
            //graphPane.CurveList.Clear();
 
            // Add new curves set and update ZedGraph components:
            zedGraphControl1.CurvesListCleanFromThread();
            zedGraphControl1.AddGraphFromThread("Rx amplitude, points", _XpointPairList, Color.Red, SymbolType.Circle);
        }

    }


    static class ZedGraphExtension
    {
        delegate void SetGraphAddGraphCallback(ZedGraphControl graphBox, string label, IPointList points, Color color, SymbolType symbolType);
        delegate void SetGraphCurvesCleanCallback(ZedGraphControl graphBox);
        /// <summary>
        /// This extension method appends collored text.
        /// </summary>
        public static void AddGraphFromThread(this ZedGraphControl graphBox, string label, IPointList points, Color color, SymbolType symbolType)
        {
            // If this method was called from the another thread.
            if (graphBox.InvokeRequired)
            {
                var deleg = new SetGraphAddGraphCallback(AddGraphFromThread);
                graphBox.Invoke(deleg, new object[] {graphBox, label, points, color, symbolType });
            }
            else
            {
                graphBox.GraphPane.XAxis.Title.Text = "Depth points, (each points is time of one scan step), nS";
                graphBox.GraphPane.YAxis.Title.Text = "Rx amplitude, points)";
                graphBox.GraphPane.AddCurve(label, points, color, symbolType);
                graphBox.AxisChange();
                graphBox.Refresh();
                //graphBox.ZoomOutAll(graphBox.GraphPane);
            }
        }

        public static void CurvesListCleanFromThread(this ZedGraphControl graphBox)
        {
            // If this method was called from the another thread.
            if (graphBox.InvokeRequired)
            {
                var deleg = new SetGraphCurvesCleanCallback(CurvesListCleanFromThread);
                graphBox.Invoke(deleg, new object[] { graphBox});
            }
            else
            {
                graphBox.GraphPane.CurveList.Clear();
            }
        }

    }





}
