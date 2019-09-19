using System;
using Microsoft.Extensions.Logging;

namespace spheredistance
{
    public class SphereDistanceApp
    {
        private readonly ILogger<SphereDistanceApp> _logger;
        public SphereDistanceApp(ILogger<SphereDistanceApp> logger)
        {
            _logger = logger;
        }
        public void run()
        {
            _logger.LogError("test");
        }
    }
}
