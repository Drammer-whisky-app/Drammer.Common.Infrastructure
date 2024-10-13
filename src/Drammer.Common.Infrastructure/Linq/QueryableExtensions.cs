using System.Linq.Expressions;
using Drammer.Common.Paging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Drammer.Common.Infrastructure.Linq;

/// <summary>
/// The queryable extensions.
/// </summary>
public static class QueryableExtensions
{
    /// <summary>
    /// Converts a query to a paged list.
    /// </summary>
    /// <param name="query">
    /// The query.
    /// </param>
    /// <param name="pageIndex">
    /// The page index. The first page = 1.
    /// </param>
    /// <param name="pageSize">
    /// The page size.
    /// </param>
    /// <typeparam name="TEntity">
    /// The entity type.
    /// </typeparam>
    /// <returns>
    /// The <see cref="PagedList{T}"/>.
    /// </returns>
    public static PagedList<TEntity> ToPagedList<TEntity>(this IQueryable<TEntity> query, int pageIndex, int pageSize)
        where TEntity : class
    {
        if (pageIndex < 1)
        {
            pageIndex = 1;
        }

        if (pageSize < 1)
        {
            pageSize = 20;
        }

        var skip = (pageIndex - 1) * pageSize;

        var result = new PagedList<TEntity>(pageIndex, pageSize);
        result.AddRange(query.Skip(skip).Take(pageSize).SetAsNoTracking(true));
        return result;
    }

    /// <summary>
    /// Selects to a paged list.
    /// </summary>
    /// <param name="query">
    /// The query.
    /// </param>
    /// <param name="pageIndex">
    /// The page index.
    /// </param>
    /// <param name="pageSize">
    /// The page size.
    /// </param>
    /// <param name="selector">
    /// The selector.
    /// </param>
    /// <typeparam name="TEntity">
    /// The entity type.
    /// </typeparam>
    /// <typeparam name="TResult">
    /// The result type.
    /// </typeparam>
    /// <returns>
    /// The <see cref="PagedList{T}"/>.
    /// </returns>
    public static PagedList<TResult> ToPagedList<TEntity, TResult>(
        this IQueryable<TEntity> query,
        int pageIndex,
        int pageSize,
        Expression<Func<TEntity, TResult>> selector)
        where TEntity : class
        where TResult : class
    {
        if (pageIndex < 1)
        {
            pageIndex = 1;
        }

        if (pageSize < 1)
        {
            pageSize = 20;
        }

        var skip = (pageIndex - 1) * pageSize;

        var result = new PagedList<TResult>(pageIndex, pageSize);
        result.AddRange(query.Skip(skip).Take(pageSize).SetAsNoTracking(true).Select(selector));
        return result;
    }

    /// <summary>
    /// Converts a query to a paged list.
    /// </summary>
    /// <param name="query">
    /// The query.
    /// </param>
    /// <param name="pageIndex">
    /// The page index. The first page = 1.
    /// </param>
    /// <param name="pageSize">
    /// The page size.
    /// </param>
    /// <typeparam name="TEntity">
    /// The entity type.
    /// </typeparam>
    /// <returns>
    /// The <see cref="PagedList{T}"/>.
    /// </returns>
    public static async Task<PagedList<TEntity>> ToPagedListAsync<TEntity>(
        this IQueryable<TEntity> query,
        int pageIndex,
        int pageSize,
        CancellationToken cancellationToken = default)
        where TEntity : class
    {
        if (pageIndex < 1)
        {
            pageIndex = 1;
        }

        if (pageSize < 1)
        {
            pageSize = 20;
        }

        var skip = (pageIndex - 1) * pageSize;

        var result = new PagedList<TEntity>(pageIndex, pageSize);
        result.AddRange(
            await query.Skip(skip).Take(pageSize).SetAsNoTracking(true)
                .ToListAsync(cancellationToken: cancellationToken).ConfigureAwait(false));
        return result;
    }

    /// <summary>
    /// Selects to a paged list.
    /// </summary>
    /// <param name="query">
    /// The query.
    /// </param>
    /// <param name="pageIndex">
    /// The page index.
    /// </param>
    /// <param name="pageSize">
    /// The page size.
    /// </param>
    /// <param name="selector">
    /// The selector.
    /// </param>
    /// <typeparam name="TEntity">
    /// The entity type.
    /// </typeparam>
    /// <typeparam name="TResult">
    /// The result type.
    /// </typeparam>
    /// <returns>
    /// The <see cref="PagedList{T}"/>.
    /// </returns>
    public static async Task<PagedList<TResult>> ToPagedListAsync<TEntity, TResult>(
        this IQueryable<TEntity> query,
        int pageIndex,
        int pageSize,
        Expression<Func<TEntity, TResult>> selector,
        CancellationToken cancellationToken = default)
        where TEntity : class
        where TResult : class
    {
        if (pageIndex < 1)
        {
            pageIndex = 1;
        }

        if (pageSize < 1)
        {
            pageSize = 20;
        }

        var skip = (pageIndex - 1) * pageSize;

        var result = new PagedList<TResult>(pageIndex, pageSize);
        result.AddRange(
            await query.Skip(skip).Take(pageSize).SetAsNoTracking(true).Select(selector)
                .ToListAsync(cancellationToken: cancellationToken).ConfigureAwait(false));
        return result;
    }

    /// <summary>
    /// Sets the IQueryable no tracking property.
    /// </summary>
    /// <param name="source">
    /// The source.
    /// </param>
    /// <param name="asNoTracking">
    /// The as no tracking.
    /// </param>
    /// <typeparam name="T">
    /// </typeparam>
    /// <returns>
    /// The <see cref="IQueryable"/>.
    /// </returns>
    public static IQueryable<T> SetAsNoTracking<T>(this IQueryable<T> source, bool asNoTracking)
        where T : class
    {
        return asNoTracking ? source.AsNoTracking() : source;
    }

    /// <summary>
    /// Loads the reference if not loaded.
    /// </summary>
    /// <param name="reference">
    /// The reference.
    /// </param>
    /// <typeparam name="T">
    /// The entity type.
    /// </typeparam>
    public static void LoadIfNotLoaded<T>(this ReferenceEntry<T, T> reference)
        where T : class
    {
        if (reference.IsLoaded == false)
        {
            reference.Load();
        }
    }

    public static async Task LoadIfNotLoadedAsync<T>(
        this ReferenceEntry<T, T> reference,
        CancellationToken cancellationToken = default)
        where T : class
    {
        if (reference.IsLoaded == false)
        {
            await reference.LoadAsync(cancellationToken).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Loads the reference if not loaded.
    /// </summary>
    /// <param name="reference">
    /// The reference.
    /// </param>
    /// <typeparam name="TEntity">
    /// The entity type.
    /// </typeparam>
    /// <typeparam name="TElement">
    /// The element type.
    /// </typeparam>
    public static void LoadIfNotLoaded<TEntity, TElement>(this ReferenceEntry<TEntity, TElement> reference)
        where TEntity : class
        where TElement : class
    {
        if (reference.IsLoaded == false)
        {
            reference.Load();
        }
    }

    public static async Task LoadIfNotLoadedAsync<TEntity, TElement>(
        this ReferenceEntry<TEntity, TElement> reference,
        CancellationToken cancellationToken = default)
        where TEntity : class
        where TElement : class
    {
        if (reference.IsLoaded == false)
        {
            await reference.LoadAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}