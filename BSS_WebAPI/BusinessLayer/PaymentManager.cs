using System;
using System.Collections.Generic;
using System.Linq;
using GM_WebAPI.DataAccessRepository;
using GM_WebAPI.Models;
using GM_WebAPI.Models.Entities;
using GM_WebAPI.Utils;

namespace GM_WebAPI.BusinessLayer
{
    public class PaymentManager
    {
        private GMEntities _context { get; } = new GMEntities();

        public List<PaymentViewModel> GetUserPayments(Guid userGuid)
        {             
            return _context.ReadCompanyPayments(userGuid)
                .Select(payment => new PaymentViewModel
                {
                    Id = payment.ID,
                    FromCompanyId = payment.FROM_COMPANY_ID,
                    FromCompanyName = payment.FROM_COMPANY_Name,
                    ToCompanyId = payment.TO_COMPANY_ID,
                    ToCompanyName = payment.TO_COMPANY_Name,
                    Amount = payment.AMOUNT,
                    Currency = payment.CURRENCY,
                    DateIssued = payment.DATE_ISSUED,
                    CrBy = payment.CR_BY,
                    CrAt = payment.CR_AT,
                    UpBy = payment.UP_BY,
                    UpAt = payment.UP_AT,
                    Guid = payment.GIUD,
                    Reference = payment.Reference
                })
                .Take(200)
                .ToList();
        }

        public List<PaymentViewModel> SearchPayments(DateTime fromDate, DateTime toDate)
        {
            fromDate = fromDate.Date;
            toDate = toDate.AddDays(1).Date;

            var lmsPayments = _context.PAYMENTs
                .Where(x => x.DATE_ISSUED >= fromDate && x.DATE_ISSUED < toDate)
                .OrderByDescending(x => x.CR_AT).Take(1000);
            return lmsPayments.Select(payment => new PaymentViewModel
            {
                Id = payment.ID,
                FromCompanyId = payment.FROM_COMPANY_ID,
                FromCompanyName = payment.COMPANY.NAME,
                ToCompanyId = payment.TO_COMPANY_ID,
                ToCompanyName = payment.COMPANY1.NAME,
                Amount = payment.AMOUNT,
                Currency = GlobalSettings.Instance.DefaultCurrency, //business requirement
                DateIssued = payment.DATE_ISSUED,
                CrBy = payment.CR_BY,
                CrAt = payment.CR_AT,
                UpBy = payment.UP_BY,
                UpAt = payment.UP_AT,
                Guid = payment.GIUD,
                Reference = payment.REFERENCE
            }).ToList();
            
        }

    }
}