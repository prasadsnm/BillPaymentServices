using AutoMapper;
using AutoMapper.Features;
using InvoicePaymentServices.Core;
using InvoicePaymentServices.Core.Interfaces.Repositories;
using InvoicePaymentServices.Infra.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicePaymentServices.Infra.Repositories
{
    public class InvoiceRepository : IInvoiceRepository 
    {
        private readonly InvoicePaymentDBContext _dbcontext;
        private readonly IMapper _mapper;
        public InvoiceRepository(InvoicePaymentDBContext dbcontext, IMapper mapper)
        {
            _dbcontext = dbcontext ?? throw new ArgumentNullException(nameof(dbcontext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<Invoice>> GetAllInvoices()
        {
            var invoices = await _dbcontext.Invoice.ToListAsync().ConfigureAwait(false);
            if (invoices != null)
            {
                return _mapper.Map<IEnumerable<Invoice>>(invoices);
            }
            return null;
        }


    }

}
