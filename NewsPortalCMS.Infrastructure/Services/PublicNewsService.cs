using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using NewsPortalCMS.Application.Common.Caching;
using NewsPortalCMS.Application.DTOs.Public;
using NewsPortalCMS.Application.Interfaces.Repositories;
using NewsPortalCMS.Application.Interfaces.Services;

namespace NewsPortalCMS.Application.Services
{
    public class PublicNewsService : IPublicNewsService
    {
        private readonly IPublicNewsRepository _repository;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        private readonly ICacheService _cacheService;

        public PublicNewsService(
            IPublicNewsRepository repository,
            IMapper mapper,
            IMemoryCache cache,
            ICacheService cacheService)
        {
            _repository = repository;
            _mapper = mapper;
            _cache = cache;
            _cacheService = cacheService;
        }

        // ============================================================
        // LATEST NEWS
        // ============================================================

        public async Task<IEnumerable<PublicNewsDto>> GetLatestNewsAsync(
            int count)
        {
            count = NormalizeCount(count);

            var cacheKey =
                $"{PublicNewsCacheKeys.Latest}_{count}";

            if (_cache.TryGetValue(
                cacheKey,
                out IEnumerable<PublicNewsDto>? cachedNews))
            {
                return cachedNews!;
            }

            var news =
                await _repository.GetLatestNewsAsync(count);

            var result =
                _mapper.Map<IEnumerable<PublicNewsDto>>(news);

            _cache.Set(
                cacheKey,
                result,
                GetCacheOptions(TimeSpan.FromMinutes(2)));

            _cacheService.TrackKey(cacheKey);

            return result;
        }

        // ============================================================
        // FEATURED NEWS
        // ============================================================

        public async Task<IEnumerable<PublicNewsDto>> GetFeaturedNewsAsync(
            int count)
        {
            count = NormalizeCount(count);

            var cacheKey =
                $"{PublicNewsCacheKeys.Featured}_{count}";

            if (_cache.TryGetValue(
                cacheKey,
                out IEnumerable<PublicNewsDto>? cachedNews))
            {
                return cachedNews!;
            }

            var news =
                await _repository.GetFeaturedNewsAsync(count);

            var result =
                _mapper.Map<IEnumerable<PublicNewsDto>>(news);

            _cache.Set(
                cacheKey,
                result,
                GetCacheOptions(TimeSpan.FromMinutes(2)));

            _cacheService.TrackKey(cacheKey);

            return result;
        }

        // ============================================================
        // POPULAR NEWS
        // ============================================================

        public async Task<IEnumerable<PublicNewsDto>> GetPopularNewsAsync(
            int count)
        {
            count = NormalizeCount(count);

            var cacheKey =
                $"{PublicNewsCacheKeys.Popular}_{count}";

            if (_cache.TryGetValue(
                cacheKey,
                out IEnumerable<PublicNewsDto>? cachedNews))
            {
                return cachedNews!;
            }

            var news =
                await _repository.GetPopularNewsAsync(count);

            var result =
                _mapper.Map<IEnumerable<PublicNewsDto>>(news);

            _cache.Set(
                cacheKey,
                result,
                GetCacheOptions(TimeSpan.FromMinutes(2)));

            _cacheService.TrackKey(cacheKey);

            return result;
        }

        // ============================================================
        // NEWS BY CATEGORY
        // ============================================================

        public async Task<IEnumerable<PublicNewsDto>> GetNewsByCategoryAsync(
            int categoryId)
        {
            var cacheKey =
                $"{PublicNewsCacheKeys.Category}_{categoryId}";

            if (_cache.TryGetValue(
                cacheKey,
                out IEnumerable<PublicNewsDto>? cachedNews))
            {
                return cachedNews!;
            }

            var news =
                await _repository
                    .GetNewsByCategoryAsync(categoryId);

            var result =
                _mapper.Map<IEnumerable<PublicNewsDto>>(news);

            _cache.Set(
                cacheKey,
                result,
                GetCacheOptions(TimeSpan.FromMinutes(2)));

            _cacheService.TrackKey(cacheKey);

            return result;
        }
        // ============================================================
        // NEWS BY SUBCATEGORY
        // ============================================================

        public async Task<IEnumerable<PublicNewsDto>> GetNewsBySubcategoryAsync(
            int subcategoryId)
        {
            if (subcategoryId <= 0)
            {
                return Enumerable.Empty<PublicNewsDto>();
            }

            var cacheKey =
                $"{PublicNewsCacheKeys.Category}_subcategory_{subcategoryId}";

            if (_cache.TryGetValue(
                cacheKey,
                out IEnumerable<PublicNewsDto>? cachedNews))
            {
                return cachedNews!;
            }

            var news =
                await _repository
                    .GetNewsBySubcategoryAsync(subcategoryId);

            var result =
                _mapper.Map<IEnumerable<PublicNewsDto>>(news);

            _cache.Set(
                cacheKey,
                result,
                GetCacheOptions(TimeSpan.FromMinutes(2)));

            _cacheService.TrackKey(cacheKey);

            return result;
        }
        // ============================================================
        // SEARCH NEWS
        // ============================================================

        public async Task<IEnumerable<PublicNewsDto>> SearchNewsAsync(
            string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return Enumerable.Empty<PublicNewsDto>();
            }

            var normalizedKeyword =
                keyword.Trim().ToLowerInvariant();

            var cacheKey =
                $"{PublicNewsCacheKeys.Search}_{normalizedKeyword}";

            if (_cache.TryGetValue(
                cacheKey,
                out IEnumerable<PublicNewsDto>? cachedNews))
            {
                return cachedNews!;
            }

            var news =
                await _repository
                    .SearchNewsAsync(normalizedKeyword);

            var result =
                _mapper.Map<IEnumerable<PublicNewsDto>>(news);

            _cache.Set(
                cacheKey,
                result,
                GetCacheOptions(TimeSpan.FromSeconds(30)));

            _cacheService.TrackKey(cacheKey);

            return result;
        }

        // ============================================================
        // NEWS DETAILS BY SLUG
        // ============================================================

        public async Task<PublicNewsDetailsDto?> GetNewsBySlugAsync(
            string slug)
        {
            if (string.IsNullOrWhiteSpace(slug))
            {
                return null;
            }

            var normalizedSlug =
                slug.Trim().ToLowerInvariant();

            var cacheKey =
                $"{PublicNewsCacheKeys.Slug}_{normalizedSlug}";

            if (_cache.TryGetValue(
                cacheKey,
                out PublicNewsDetailsDto? cachedNews))
            {
                return cachedNews;
            }

            var news =
                await _repository
                    .GetNewsBySlugAsync(normalizedSlug);

            if (news == null)
            {
                return null;
            }

            var dto =
                _mapper.Map<PublicNewsDetailsDto>(news);

            // ========================================================
            // TAGS
            // ========================================================

            dto.Tags = news.NewsTags
                .Select(nt => nt.Tag.Name)
                .ToList();

            // ========================================================
            // APPROVED COMMENTS
            // ========================================================

            dto.Comments = news.Comments
                .Where(c =>
                    c.IsApproved &&
                    c.IsActive)
                .OrderByDescending(c => c.CreatedDate)
                .Select(c => new PublicCommentDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Content = c.Content,
                    CreatedDate = c.CreatedDate
                })
                .ToList();

            // ========================================================
            // CACHE DETAILS
            // ========================================================

            _cache.Set(
                cacheKey,
                dto,
                GetCacheOptions(TimeSpan.FromMinutes(2)));

            _cacheService.TrackKey(cacheKey);

            return dto;
        }

        // ============================================================
        // NORMALIZE COUNT
        // ============================================================

        private static int NormalizeCount(int count)
        {
            // Default count
            if (count <= 0)
            {
                return 10;
            }

            // Maximum count allowed
            return Math.Min(count, 50);
        }

        // ============================================================
        // CACHE OPTIONS
        // ============================================================

        private static MemoryCacheEntryOptions GetCacheOptions(
            TimeSpan expiration)
        {
            return new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration,

                SlidingExpiration =
                    TimeSpan.FromMinutes(1)
            };
        }
    }
}