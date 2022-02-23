using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
    public interface IProductService
    {
        int Create(int restaurantId, CreateUpdateProductDto dto);
        PageResult<ProductDto> GetAll(int shopId, PaginationQuery query);
        ProductDto GetById(int shopId, int productId);
        void Remove(int shopId, int productId);
        void RemoveAll(int shopId);
        void Update(int productId, int shopId, CreateUpdateProductDto dto);
    }

    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly ShopDBContext _dBContext;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;
        private readonly IFileService _fileservice;

        public ProductService(IMapper mapper, ShopDBContext dBContext, IAuthorizationService authorizationService,
            IUserContextService userContextService, IFileService fileservice)
        {
            _mapper = mapper;
            _dBContext = dBContext;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
            _fileservice = fileservice;
        }

        public int Create(int shopId, CreateUpdateProductDto dto)
        {
            var product = _mapper.Map<Product>(dto);
            product.ShopId = shopId;

            _dBContext.Products.Add(product);
            _dBContext.SaveChanges();

            return product.Id;
        }

        public void Update(int productId, int shopId, CreateUpdateProductDto dto)
        {
            if (dto.file != null && dto.file.Length > 0)
                _fileservice.Upload(dto.file);

            var shop = GetShopById(shopId);
            var product = _dBContext
                .Products
                .FirstOrDefault(r => r.Id == productId);


            if (product is null)
                throw new NotFoundExeption("Product not found");

            var autorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, shop, new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if (!autorizationResult.Succeeded)
                throw new ForbidExeption();

            

            var shopDtos = _mapper.Map<CreateUpdateProductDto, Product>(dto, product);
            shopDtos.ShopId = shopId;

            _dBContext.Products.Update(shopDtos);
            _dBContext.SaveChanges();
        }

        public PageResult<ProductDto> GetAll(int shopId, PaginationQuery query)
        {
            var basequery = _dBContext
                  .Products
                  .Include(r => r.Shop)
                  .Where(r => query.searchPhrase == null || (r.Name.ToLower().Contains(query.searchPhrase.ToLower())
                                                       || r.Description.ToLower().Contains(query.searchPhrase.ToLower())));
                  

            if (!string.IsNullOrEmpty(query.SortBy))
            {
                var columnsSelectors = new Dictionary<string, Expression<Func<Product, object>>>
                {
                    { nameof(Product.Name), r => r.Name },
                    { nameof(Product.Description), r => r.Description },
                    { nameof(Product.Category), r => r.Category },
                };

                var selectedColumn = columnsSelectors[query.SortBy];

                basequery = query.SortDirection == SortDirection.ASC
                    ? basequery.OrderBy(selectedColumn)
                    : basequery.OrderByDescending(selectedColumn);
            }

            var products = basequery
                .Skip(query.PageSize * (query.PageNumber - 1))
                .Take(query.PageSize)
                .ToList();

            var totaItemsCount = basequery.Count();
            var productsDtos = _mapper.Map<List<ProductDto>>(products);

            var result = new PageResult<ProductDto>(productsDtos, totaItemsCount, query.PageSize, query.PageNumber);

            return result;
        }

        public ProductDto GetById(int shopId, int productId )
        {
            var shop = GetShopById(shopId);
 
            var product = _dBContext
                .Products
                .Where(p => p.ShopId == shopId)
                .FirstOrDefault(r => r.Id == productId);

            if (product is null)
                throw new NotFoundExeption("Product not found");
           
            var productDto = _mapper.Map<ProductDto>(product);
            return productDto;
        }
        private Shop GetShopById(int shopId)
        {

            var shop = _dBContext
                .Shops
                .Include(r => r.Product)
                .FirstOrDefault(r => r.Id == shopId);

            if (shop is null)
                throw new NotFoundExeption("Shop not found");

            return shop;
        }


        public void RemoveAll(int shopId)
        {
            var shop = GetShopById(shopId);
            _dBContext.RemoveRange(shop.Product);
            _dBContext.SaveChanges();
        }

        public void Remove(int shopId, int productId)
        {
            var shop = GetShopById(shopId);

            var product = _dBContext
                .Products
                .Where(p => p.ShopId == shopId)
                .FirstOrDefault(r => r.Id == productId);

            if (product is null)
                throw new NotFoundExeption("Product not found");

            _dBContext.Remove(product);
            _dBContext.SaveChanges();
        }

    }
}
