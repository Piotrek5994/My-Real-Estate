using Core.IRepositories;
using Infrastructure.Db;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly MongoDbContext _context;
    private readonly ILogger _log;

    public PaymentRepository(MongoDbContext context, ILogger<PaymentRepository> log)
    {
        _context = context;
        _log = log;
    }
}
