using AutoMapper;
using AutoMapper.Features;
using InvoicePaymentServices.Core;
using InvoicePaymentServices.Core.Interfaces.Repositories;
using InvoicePaymentServices.Core.Models;
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

        public async Task<IEnumerable<Invoice>> GetInvoicesByAccountId(Guid accountId)
        {
            // Seemed to be a sqlite bug to deal with GUID. This is a workaround since no time for it now. 
            // This is a tech debt and need re-visit.
            var invoices = await _dbcontext.Invoice.Where(x => x.BillToId.ToString().Equals(accountId.ToString())).ToListAsync().ConfigureAwait(false);
            if (invoices != null)
            {
                var result = _mapper.Map<IEnumerable<Invoice>>(invoices);
                foreach (var invoice in result)
                {
                    invoice.AvailableAction = ConstructAvailableActionField(invoice.Id);
                };

                return result;
            }

            return null;
        }

        // This is a convenient way to add some links or other information here for front end.
        // In this way, front end does not know any changes in backend hence does not care.
        // Some changes in backend will not affect frontend side.
        private string ConstructAvailableActionField(int id)
        {
            return "https://This.is.what.you.can.do." + id;
        }
    }
}
