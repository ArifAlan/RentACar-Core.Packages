using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Core.Application.Pipelines.Caching
{
    public class CacheRemovingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
     where TRequest : IRequest<TResponse>, ICacheRemoverRequest
    {
        private readonly IDistributedCache _cache;

        public CacheRemovingBehavior(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
           // await next() ifadesi, middleware zincirindeki sonraki adıma(middleware'e) geçmeyi ifade eder. Bu, bu middleware'in geri kalanının çalışmasını tetikler.
            if (request.BypassCache)
            {
                return await next();
            }

            TResponse response = await next();

            if (request.CacheGroupKey != null)
            {
                byte[]? cachedGroup = await _cache.GetAsync(request.CacheGroupKey, cancellationToken);
                if (cachedGroup != null)
                {
                    HashSet<string> keysInGroup = JsonSerializer.Deserialize<HashSet<string>>(Encoding.Default.GetString(cachedGroup))!;
                    foreach (string key in keysInGroup)
                    {
                        await _cache.RemoveAsync(key, cancellationToken);
                    }

                    await _cache.RemoveAsync(request.CacheGroupKey, cancellationToken);
                    await _cache.RemoveAsync(key: $"{request.CacheGroupKey}SlidingExpiration", cancellationToken);
                }
            }

            if (request.CacheKey != null)
            {
                await _cache.RemoveAsync(request.CacheKey, cancellationToken);
            }
            return response;
        }
    }
}
