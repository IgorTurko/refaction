using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using refactor_me.Domain;
using refactor_me.Domain.Entities;

namespace refactor_me.Controllers
{
    [RoutePrefix("products")]
    public class ProductsController : ApiController
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductOptionRepository _productOptionRepository;

        public ProductsController(IProductRepository productRepository, IProductOptionRepository productOptionRepository)
        {
            _productRepository = productRepository;
            _productOptionRepository = productOptionRepository;
        }

        [Route]
        [HttpGet]
        public IEnumerable<Product> GetAll()
        {
            return _productRepository.GetAll();
        }

        [Route]
        [HttpGet]
        public IEnumerable<Product> GetByName(string name)
        {
            return _productRepository.GetByName(name);
        }

        [Route("{id}")]
        [HttpGet]
        public Product GetById(Guid id)
        {
            return _productRepository.GetById(id);
        }

        [Route]
        [HttpPost]
        public Product Add(Product product)
        {
            if (product == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            try
            {
                return _productRepository.Add(product);
            }
            catch (FluentValidation.ValidationException)
            {
                //TODO: here we can add a lot of code
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }

        [Route]
        [HttpPut]
        public void Update(Product product)
        {
            if (product == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            try
            {
                if (!_productRepository.Update(product))
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
            }
            catch (FluentValidation.ValidationException)
            {
                //TODO: here we can add a lot of validatio handling
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }

        [Route("{id}")]
        [HttpDelete]
        public void Delete(Guid id)
        {
            if (!_productRepository.Delete(id))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        [Route("{productId}/options")]
        [HttpGet]
        public IEnumerable<ProductOption> GetOptions(Guid productId)
        {
            return _productOptionRepository.GetAll(productId);
        }

        [Route("{productId}/options/{id}")]
        [HttpGet]
        public ProductOption GetOption(Guid id)
        {
            return _productOptionRepository.GetById(id);
        }

        [Route("{productId}/options")]
        [HttpPost]
        public ProductOption CreateOption(Guid productId, ProductOption productOption)
        {
            if (productOption == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            try
            {
                productOption.ProductId = productId;
                return _productOptionRepository.Add(productOption);
            }
            catch (FluentValidation.ValidationException)
            {
                //TODO: here we can add a lot of code
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }

        [Route("{productId}/options/{id}")]
        [HttpPut]
        public void UpdateOption(Guid id, ProductOption productOption)
        {
            if (productOption == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            try
            {
                productOption.Id = id;

                if (!_productOptionRepository.Update(productOption))
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
            }
            catch (FluentValidation.ValidationException)
            {
                //TODO: here we can add a lot of validatio handling
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }

        [Route("{productId}/options/{id}")]
        [HttpDelete]
        public void DeleteOption(Guid id)
        {
            if (!_productOptionRepository.Delete(id))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }
    }
}
