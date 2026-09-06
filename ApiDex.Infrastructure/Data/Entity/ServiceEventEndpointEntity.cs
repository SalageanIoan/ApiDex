namespace ApiDex.Infrastructure.Data.Entity;

public class ServiceEventEndpointEntity
{
    public Guid ServiceEventId { get; set; }

    public Guid ServiceEndpointId { get; set; }

    public ServiceEventEntity ServiceEvent { get; set; } = null!;

    public ServiceEndpointEntity ServiceEndpoint { get; set; } = null!;
}
