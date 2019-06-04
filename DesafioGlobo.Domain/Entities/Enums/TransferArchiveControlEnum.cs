using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioGlobo.Domain.Entities.Enums
{
    public enum TransferArchiveControlEnum
    {
        COPY = 1,
        MOVE = 2,
        DELETE = 3,
        TRANSFERBYFTP = 4,
        GETALL = 5,
        GETPARTIALNAME = 6
    }
}
