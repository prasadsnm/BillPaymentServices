using InvoicePaymentServices.Infra.DBEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicePaymentServices.Infra.DBContext
{
    public class InvoicePaymentDBContext : DbContext
    {
        public InvoicePaymentDBContext() { }

        public InvoicePaymentDBContext(DbContextOptions<InvoicePaymentDBContext> option)
            : base(option) { }

        public virtual DbSet<Invoice> Invoice { get; set; }
        public virtual DbSet<Payment> Payment { get; set; }
    }
}
