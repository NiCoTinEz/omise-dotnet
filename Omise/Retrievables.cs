﻿using System.Threading.Tasks;
using Omise.Models;

namespace Omise
{
    // TODO: [vNext] - We should apply IRetrievable on X-SpecificResource and
    //   forego IListRetrievable for maximum consistency.
    public interface IRetrievable<TModel> : IResource<TModel> where TModel : ModelBase
    {
    }

    public interface IListRetrievable<TModel> : IResource<TModel> where TModel : ModelBase
    {
    }

    public static class Retrievables
    {
        public static async Task<TResult> Get<TResult>(
            this IRetrievable<TResult> resource
        ) where TResult : ModelBase
        {
            return await resource.Requester.Request<TResult>(
                resource.Endpoint,
                "GET",
                resource.BasePath
            ).ConfigureAwait(false);
        }

        public static async Task<TResult> Get<TResult>(
            this IListRetrievable<TResult> resource,
            string modelId
        ) where TResult : ModelBase
        {
            return await resource.Requester.Request<TResult>(
                resource.Endpoint,
                "GET",
                $"{resource.BasePath}/{modelId}"
            ).ConfigureAwait(false);
        }
    }
}