using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioGlobo.Infra.CrossCutting.Helper.SendEmail
{
    public interface IMail
    {
        void SendEmail();
    }
}
