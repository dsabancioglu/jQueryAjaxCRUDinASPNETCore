using jQueryAjaxCRUDinASPNETCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jQueryAjaxCRUDinASPNETCore.Entities
{
    public class TransactionManagementContext : DbContext
    {
        public TransactionManagementContext(DbContextOptions<TransactionManagementContext> options) : base (options)
        { }

        public DbSet<TransactionModel> Transactions { get; set; } //TransactionModel -> Transaction olması (mapper kullandığında)
    }
}
