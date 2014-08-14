using System.Globalization;
using System.Linq;
using System.Windows.Media.Imaging;
using GoToWindow.Extensibility;
using System.Windows.Controls;
using System.Windows;
using log4net;

namespace GoToWindow.Plugins.Debug.ViewModel
{
    public class DebugSearchResult : IGoToWindowSearchResult
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(DebugSearchResult).Assembly, "GoToWindow");

        public UserControl View { get; private set; }

        public DebugSearchResult(UserControl view)
        {
            View = view;
        }

        public void Select()
        {
            Log.Debug("Debug: Selected");
        }

        public bool IsShown(string searchQuery)
        {
            Log.Debug(string.Format("Debug: Filter: '{0}'", searchQuery));
            return true;
        }

        private BitmapFrame LoadIcon()
        {
            return null;
        }
    }
}