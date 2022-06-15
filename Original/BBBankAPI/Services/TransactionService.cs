using Entities;
using Entities.Responses;
using Infrastructure;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class TransactionService : ITransactionService
    {
        private readonly BBBankContext _bbBankContext;
        public TransactionService(BBBankContext BBBankContext)
        {
            _bbBankContext = BBBankContext;
        }
        public async Task<LineGraphData> GetLast12MonthBalances(string? userId)
        {
            var lineGraphData = new LineGraphData();

            var allTransactions = new List<Transaction>();
            Transaction transaction1 = new Transaction { Id = "1", TransactionAmount = 500, TransactionType = TransactionType.Deposit, TransactionDate = DateTime.Now.AddMonths(-1) };
            Transaction transaction2 = new Transaction { Id = "2", TransactionAmount = 700, TransactionType = TransactionType.Deposit, TransactionDate = DateTime.Now.AddMonths(-2) };
            Transaction transaction3 = new Transaction { Id = "3", TransactionAmount = 500, TransactionType = TransactionType.Deposit, TransactionDate = DateTime.Now.AddMonths(-3) };
            Transaction transaction4 = new Transaction { Id = "4", TransactionAmount = 200, TransactionType = TransactionType.Deposit, TransactionDate = DateTime.Now.AddMonths(-4) };
            Transaction transaction5 = new Transaction { Id = "5", TransactionAmount = 100, TransactionType = TransactionType.Deposit, TransactionDate = DateTime.Now.AddMonths(-5) };
            Transaction transaction6 = new Transaction { Id = "6", TransactionAmount = 900, TransactionType = TransactionType.Deposit, TransactionDate = DateTime.Now.AddMonths(-6) };
            Transaction transaction7 = new Transaction { Id = "7", TransactionAmount = 900, TransactionType = TransactionType.Deposit, TransactionDate = DateTime.Now.AddMonths(-7) };
            Transaction transaction8 = new Transaction { Id = "8", TransactionAmount = 800, TransactionType = TransactionType.Deposit, TransactionDate = DateTime.Now.AddMonths(-8) };
            Transaction transaction9 = new Transaction { Id = "9", TransactionAmount = 100, TransactionType = TransactionType.Deposit, TransactionDate = DateTime.Now.AddMonths(-9) };
            Transaction transaction10 = new Transaction { Id = "10", TransactionAmount = 500, TransactionType = TransactionType.Deposit, TransactionDate = DateTime.Now.AddMonths(-10) };
            Transaction transaction11 = new Transaction { Id = "11", TransactionAmount = 200, TransactionType = TransactionType.Deposit, TransactionDate = DateTime.Now.AddMonths(-11) };
            Transaction transaction12 = new Transaction { Id = "12", TransactionAmount = 200, TransactionType = TransactionType.Deposit, TransactionDate = DateTime.Now.AddMonths(-12) };


            allTransactions.Add(transaction1);
            allTransactions.Add(transaction2);
            allTransactions.Add(transaction3);
            allTransactions.Add(transaction4);
            allTransactions.Add(transaction5);
            allTransactions.Add(transaction6);
            allTransactions.Add(transaction7);
            allTransactions.Add(transaction8);
            allTransactions.Add(transaction9);
            allTransactions.Add(transaction10);
            allTransactions.Add(transaction11);
            allTransactions.Add(transaction12);
            if (allTransactions.Count() > 0)
            {
                var totalBalance = allTransactions.Sum(x => x.TransactionAmount);
                lineGraphData.TotalBalance = totalBalance;
                decimal lastMonthTotal = 0;
                for (int i = 12; i > 0; i--)
                {

                    var runningTotal = allTransactions.Where(x => x.TransactionDate >= DateTime.Now.AddMonths(-i) &&
                       x.TransactionDate < DateTime.Now.AddMonths(-i + 1)).Sum(y => y.TransactionAmount) + lastMonthTotal;
                    lineGraphData.Labels.Add(DateTime.Now.AddMonths(-i + 1).ToString("MMM yyyy"));
                    lineGraphData.Figures.Add(runningTotal);
                    lastMonthTotal = runningTotal;
                }
            }
            return lineGraphData;
        }
        //public async Task<LineGraphData> GetLast12MonthBalances(string? userId)
        //{
        //    var lineGraphData = new LineGraphData();

        //    var allTransactions = new List<Transaction>();
        //    if (userId == null)
        //    {
        //        allTransactions = _bbBankContext.Transactions.ToList();
        //    }
        //    else
        //    {
        //        allTransactions = _bbBankContext.Transactions.Where(x => x.Account.User.Id == userId).ToList();
        //    }
        //    if (allTransactions.Count() > 0)
        //    {
        //        var totalBalance = allTransactions.Sum(x => x.TransactionAmount);
        //        lineGraphData.TotalBalance = totalBalance;
        //        decimal lastMonthTotal = 0;
        //        for (int i = 12; i > 0; i--)
        //        {
        //            var runningTotal = allTransactions.Where(x => x.TransactionDate >= DateTime.Now.AddMonths(-i) &&
        //               x.TransactionDate < DateTime.Now.AddMonths(-i + 1)).Sum(y => y.TransactionAmount) + lastMonthTotal;
        //            lineGraphData.Labels.Add(DateTime.Now.AddMonths(-i + 1).ToString("MMM yyyy"));
        //            lineGraphData.Figures.Add(runningTotal);
        //            lastMonthTotal = runningTotal;
        //        }
        //    }
        //    return lineGraphData;
        //}
    }
}
