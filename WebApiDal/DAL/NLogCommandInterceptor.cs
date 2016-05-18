using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace DAL
{
    public class NLogCommandInterceptor : IDbCommandInterceptor
    {
        private readonly ILogger _logger; // = LogManager.GetCurrentClassLogger();

        public NLogCommandInterceptor(ILogger logger)
        {
            _logger = logger;
        }

        public void NonQueryExecuting(
            DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            LogIfNonAsync(command, interceptionContext, nameof(NonQueryExecuting));
        }

        public void NonQueryExecuted(
            DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            LogIfError(command, interceptionContext);
        }

        public void ReaderExecuting(
            DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            LogIfNonAsync(command, interceptionContext, nameof(ReaderExecuting));
        }

        public void ReaderExecuted(
            DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            LogIfError(command, interceptionContext);
        }

        public void ScalarExecuting(
            DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            LogIfNonAsync(command, interceptionContext, nameof(ScalarExecuting));
        }

        public void ScalarExecuted(
            DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            LogIfError(command, interceptionContext);
        }

        private void LogIfNonAsync<TResult>(
            DbCommand command, DbCommandInterceptionContext<TResult> interceptionContext, string caller)
        {
            if (!interceptionContext.IsAsync)
            {
                var line = caller +": Non-async command used:\n" + command.CommandText;
                foreach (var parameter in command.Parameters.Cast<DbParameter>().ToList())
                {
                    //line = line + "\n" + "Parameter: " + parameter.ParameterName + " = " + parameter.Value;
                    line = line.Replace(oldValue: "@" + parameter.ParameterName, newValue: parameter.Value.ToString());
                }

                _logger.Info(line);
            }
            else
            {
                _logger.Info("Async command used:\n {0}", command.CommandText);
            }
        }

        private void LogIfError<TResult>(
            DbCommand command, DbCommandInterceptionContext<TResult> interceptionContext)
        {
            if (interceptionContext.Exception != null)
            {
                _logger.Error("Command {0} failed with exception {1}",
                    command.CommandText, interceptionContext.Exception);
            }
        }
    }
}