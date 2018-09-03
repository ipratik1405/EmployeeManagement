using CorrelationId;
using Serilog.Core;
using Serilog.Events;

namespace EmployeeManagement.WebApi.Logging.Serilog
{
    public class CorrelationIdEnricher : ILogEventEnricher
    {
        private readonly ICorrelationContextAccessor _correlationContext;

        public CorrelationIdEnricher(ICorrelationContextAccessor correlationContext)
        {
            _correlationContext = correlationContext;
        }

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            if (_correlationContext?.CorrelationContext?.CorrelationId == null) return;

            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("CorrelationId", _correlationContext.CorrelationContext.CorrelationId));
        }
    }
}
