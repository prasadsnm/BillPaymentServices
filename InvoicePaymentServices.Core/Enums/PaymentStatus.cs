﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicePaymentServices.Core.Enums
{
    public enum PaymentStatus
    {
        Scheduled,
        Paid,
        Canceled,
        Failed,
        Voided
    }
}
