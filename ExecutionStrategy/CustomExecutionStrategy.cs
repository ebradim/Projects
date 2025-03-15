using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace ExecutionStrategy;

public class CustomExecutionStrategy(ExecutionStrategyDependencies dependencies) : NpgsqlRetryingExecutionStrategy(dependencies)
{

    protected override bool ShouldRetryOn(Exception? exception)
    {
        // return exception is TimeoutException || exception is SqlException || exception is CutomException;
        return base.ShouldRetryOn(exception);
    }
}
