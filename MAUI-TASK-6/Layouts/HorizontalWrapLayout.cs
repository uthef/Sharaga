using Microsoft.Maui.Layouts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUI_TASK_6.Layouts
{
    public class HorizontalWrapLayout : StackLayout
    {
        public HorizontalWrapLayout()
        {
        }

        protected override ILayoutManager CreateLayoutManager()
        {
            return new HorizontalWrapLayoutManager(this);
        }
    }
}
