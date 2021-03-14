using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace GeoradarGui
{
    /// <summary>
    /// This class modifies the behavior of the base class DockContent when form closing.
    /// </summary>
    public class BaseDockContent : DockContent
    {
        /// <summary>
        /// Protected override method calls Hide() instead of Close().
        /// </summary>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
            base.OnFormClosing(e);
        }
    }
}