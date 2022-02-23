using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShopApi.Authorization;
using ShopApi.Entities;
using ShopApi.Exeptions;
using ShopApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ShopApi.Services
{
    public interface IShopService
    {
        PageResult<ShopDto> GetAll(PaginationQuery query);
        PageResult<ShopDto> GetOwn(PaginationQuery query, int user_Id);
        ShopDto GetById(int id);
        int Create(CreateShopDto dto);
        void Update(UpdateShopDto dto, int id);
        void Delete(int id);
    }

    public class ShopService : IShopService
    {
        private readonly ShopDBContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;
        private readonly IAuthorizationService _authorizationService;
        private readonly ILogger _logger;
        private readonly ShopDBContext _dBContext;

        public ShopService(ShopDBContext dbContext, IMapper mapper, ShopDBContext dBContext, IUserContextService userContextService
            ,IAuthorizationService authorizationService, ILogger<ShopService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _userContextService = userContextService;
            _authorizationService = authorizationService;
            _logger = logger;
            _dBContext = dBContext;
        }

        public PageResult<ShopDto> GetOwn(PaginationQuery query, int user_Id)
        {

            var basequery = _dbContext
                .Shops
                .Include(r => r.Address)
                .Include(r => r.Product)
                .Where(r => r.CreatedById == user_Id)
                .Where(r => query.searchPhrase == null || (r.Name.ToLower().Contains(query.searchPhrase.ToLower())
                                                     || r.Description.ToLower().Contains(query.searchPhrase.ToLower())));

            if (!string.IsNullOrEmpty(query.SortBy))
            {
                var columnsSelectors = new Dictionary<string, Expression<Func<Shop, object>>>
                {
                    { nameof(Shop.Name), r => r.Name },
                    { nameof(Shop.Description), r => r.Description },
                    { nameof(Shop.Category), r => r.Category },
                };

                var selectedColumn = columnsSelectors[query.SortBy];

                basequery = query.SortDirection == SortDirection.ASC
                    ? basequery.OrderBy(selectedColumn)
                    : basequery.OrderByDescending(selectedColumn);
            }

            var shops = basequery
                .Skip(query.PageSize * (query.PageNumber - 1))
                .Take(query.PageSize)
                .ToList();

            var totaItemsCount = basequery.Count();
            var shopsDtos = _mapper.Map<List<ShopDto>>(shops);

            var result = new PageResult<ShopDto>(shopsDtos, totaItemsCount, query.PageSize, query.PageNumber);

            return result;

        }

        public PageResult<ShopDto> GetAll(PaginationQuery query)
        {
            var basequery = _dbContext
                .Shops
                .Include(r => r.Address)
                .Include(r => r.Product)
                .Where(r => query.searchPhrase == null || (r.Name.ToLower().Contains(query.searchPhrase.ToLower())
                                                     || r.Description.ToLower().Contains(query.searchPhrase.ToLower())));

            if (!string.IsNullOrEmpty(query.SortBy))
            {
                var columnsSelectors = new Dictionary<string, Expression<Func<Shop, object>>>
                {
                    { nameof(Shop.Name), r => r.Name },
                    { nameof(Shop.Description), r => r.Description },
                    { nameof(Shop.Category), r => r.Category },
                };

                var selectedColumn = columnsSelectors[query.SortBy];

                basequery = query.SortDirection == SortDirection.ASC
                    ? basequery.OrderBy(selectedColumn)
                    : basequery.OrderByDescending(selectedColumn);
            }

            var shops = basequery
                .Skip(query.PageSize * (query.PageNumber - 1))
                .Take(query.PageSize)
                .ToList();

            var totaItemsCount = basequery.Count();
            var shopsDtos = _mapper.Map<List<ShopDto>>(shops);

            var result = new PageResult<ShopDto>(shopsDtos, totaItemsCount, query.PageSize, query.PageNumber);

            return result;

        }

        public ShopDto GetById(int id)
        {
            var shop = _dbContext
                .Shops
                .Include(r => r.Address)
                .Include(r => r.Product)
                .FirstOrDefault(r => r.Id == id);

            if (shop is null)
                throw new NotFoundExeption("Shop not found");

            var resault = _mapper.Map<ShopDto>(shop);
            return resault;
        }

        public int Create(CreateShopDto dto)
        {
            var shop = _mapper.Map<Shop>(dto);
            shop.CreatedById = _userContextService.GetUserId;
            _dBContext.Shops.Add(shop);
            _dBContext.SaveChanges();
            return shop.Id;
        }

        public void Update(UpdateShopDto dto, int id)
        {

            var shop = _dBContext
                .Shops
                .FirstOrDefault(r => r.Id == id);
         
            
            if (shop is null)
                throw new NotFoundExeption("Shop not found");

            var autorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, shop, new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if (!autorizationResult.Succeeded)
                throw new ForbidExeption();

            _mapper.Map<UpdateShopDto, Shop>(dto, shop);
            _dBContext.Shops.Update(shop);
            _dBContext.SaveChanges();

        }

        public void Delete(int id)
        {
            _logger.LogWarning($"Shop with id: {id} DELETE action invoked");
            var shop = _dBContext
                .Shops
                .FirstOrDefault(r => r.Id == id);

            if (shop == null)
                throw new NotFoundExeption("Restaurant not found");

            var autorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, shop, new ResourceOperationRequirement(ResourceOperation.Delete)).Result;

            if (!autorizationResult.Succeeded)
                throw new ForbidExeption();

            _dBContext.Shops.Remove(shop);
            _dBContext.SaveChanges();




        }

    }
}
