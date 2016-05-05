using System;

namespace Kiehl.App.Data
{
    public interface IAmAuditable
    {
        DateTime Created { get; set; }
        DateTime Updated { get; set; }
    }
}
