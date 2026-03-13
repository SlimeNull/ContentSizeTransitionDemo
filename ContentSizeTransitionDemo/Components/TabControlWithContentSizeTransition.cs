using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ContentSizeTransitionDemo.Components
{
    public class TabControlWithContentSizeTransition : TabControl
    {
        static TabControlWithContentSizeTransition()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TabControlWithContentSizeTransition), new System.Windows.FrameworkPropertyMetadata(typeof(TabControlWithContentSizeTransition)));
        }
    }
}
