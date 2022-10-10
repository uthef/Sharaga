using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUI_TASK_6.Messages
{
    public class ClearSignatureMessage : ValueChangedMessage<bool>
    {
        public ClearSignatureMessage(bool value) : base(value)
        {

        }
    }
}
