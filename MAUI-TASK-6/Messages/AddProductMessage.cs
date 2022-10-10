using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace MAUI_TASK_6.Messages
{
    public class AddProductMessage : ValueChangedMessage<bool>
    {
        public AddProductMessage(bool value) : base(value)
        {

        }
    }
}
