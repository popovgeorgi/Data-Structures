namespace _02.VaniPlanning
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Agency : IAgency
    {
        private IDictionary<string, Invoice> byNumber;

        private IDictionary<string, HashSet<Invoice>> byCompany;

        private IDictionary<Department, HashSet<Invoice>> byDepartment;

        public Agency()
        {
            this.byDepartment = new Dictionary<Department, HashSet<Invoice>>();
            this.byNumber = new Dictionary<string, Invoice>();
            this.byCompany = new Dictionary<string, HashSet<Invoice>>();
        }
        public void Create(Invoice invoice)
        {
            if (this.Contains(invoice.SerialNumber))
            {
                throw new ArgumentException();
            }

            if (!this.byCompany.ContainsKey(invoice.CompanyName))
            {
                this.byCompany.Add(invoice.CompanyName, new HashSet<Invoice>());
            }

            if (!this.byDepartment.ContainsKey(invoice.Department))
            {
                this.byDepartment.Add(invoice.Department, new HashSet<Invoice>());
            }

            this.byDepartment[invoice.Department].Add(invoice);
            this.byCompany[invoice.CompanyName].Add(invoice);
            this.byNumber.Add(invoice.SerialNumber, invoice);
        }

        public void ThrowInvoice(string number)
        {
            if (!this.Contains(number))
            {
                throw new ArgumentException();
            }

            this.byNumber.Remove(number);
        }

        public void ThrowPayed()
        {
            var payed = this.byNumber.Values.Where(x => x.Subtotal == 0);
            foreach (var item in payed)
            {
                this.byNumber.Remove(item.SerialNumber);
            }
        }

        public int Count()
        {
            return this.byNumber.Count;
        }

        public bool Contains(string number)
        {
            return this.byNumber.ContainsKey(number);
        }

        public void PayInvoice(DateTime due)
        {
            var dues = this.byNumber.Values.Where(x => x.DueDate == due);
            if (dues.Count() == 0)
            {
                throw new ArgumentException();
            }

            foreach (var item in dues)
            {
                item.Subtotal = 0;
            }
        }

        public IEnumerable<Invoice> GetAllInvoiceInPeriod(DateTime start, DateTime end)
        {
            var invoices = this.byNumber.Values.Where(x => x.IssueDate >= start && x.IssueDate <= end);
            if (invoices.Count() == 0)
            {
                return new List<Invoice>();
            }

            return invoices.OrderBy(x => x.IssueDate).ThenBy(x => x.DueDate);
        }

        public IEnumerable<Invoice> SearchBySerialNumber(string serialNumber)
        {
            var matching = this.byNumber.Where(x => x.Key.Contains(serialNumber)).Select(x => x.Value).OrderByDescending(x => x.SerialNumber).ToList();
            if (matching.Count() == 0)
            {
                throw new ArgumentException();
            }

            return matching;
        }

        public IEnumerable<Invoice> ThrowInvoiceInPeriod(DateTime start, DateTime end)
        {
            var removed = this.byNumber.Values.Where(x => x.DueDate > start && x.DueDate < end).ToList();
            if (removed.Count() == 0)
            {
                throw new ArgumentException();
            }

            foreach (var item in removed)
            {
                this.byNumber.Remove(item.SerialNumber);
            }

            return removed;
        }

        public IEnumerable<Invoice> GetAllFromDepartment(Department department)
        {
            if (!this.byDepartment.ContainsKey(department))
            {
                return Enumerable.Empty<Invoice>();
            }
            var departmentInvoices = this.byDepartment[department];
            if (departmentInvoices == null || departmentInvoices.Count == 0)
            {
                return Enumerable.Empty<Invoice>();
            }

            return departmentInvoices.OrderByDescending(x => x.Subtotal).ThenBy(x => x.IssueDate);
        }

        public IEnumerable<Invoice> GetAllByCompany(string company)
        {
            if (!this.byCompany.ContainsKey(company))
            {
                return Enumerable.Empty<Invoice>();
            }
            var companyInvoices = this.byCompany[company];
            if (companyInvoices == null || companyInvoices.Count == 0)
            {
                return new List<Invoice>();
            }

            return companyInvoices.OrderByDescending(x => x.SerialNumber);
        }

        public void ExtendDeadline(DateTime dueDate, int days)
        {
            var invoices = this.byNumber.Values.Where(x => x.DueDate == dueDate);
            if (invoices.Count() == 0)
            {
                throw new ArgumentException();
            }

            foreach (var item in invoices)
            {
                item.DueDate = item.DueDate.AddDays(days);
            }
        }
    }
}
