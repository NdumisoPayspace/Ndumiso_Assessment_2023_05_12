namespace Ndumiso_Assessment_2023_05_12.APIControllers
{
    using FluentValidation;

    using Microsoft.AspNetCore.Mvc;

    using Ndumiso_Assessment_2023_05_12.Data;
    using Ndumiso_Assessment_2023_05_12.Models;
    using Ndumiso_Assessment_2023_05_12.Validators;
    using Newtonsoft.Json;

    [Route("api/[controller]")]
    public class InventoryAPIController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IValidator<Product> productValidator;

        public InventoryAPIController(ApplicationDbContext context, IValidator<Product> productValidator)
        {
            this.context = context;
            this.productValidator = productValidator;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var products = context.Products;
            return Ok(products);
        }

        [HttpPost]
        public IActionResult Post(string values)
        {
            var newProduct = new Product();
            JsonConvert.PopulateObject(values, newProduct);

            var result = productValidator.Validate(newProduct, _ => _.IncludeRuleSets("Create"));

            if (!result.IsValid) 
            {
                result.AddToModelState(ModelState);

                return BadRequest(ModelState);
            }

            context.Products.Add(newProduct);
            context.SaveChanges();

            return Ok(newProduct);
        }

        [HttpPut]
        public IActionResult Put(int key, string values)
        {
            var product = context.Products.FirstOrDefault(_ => _.Id == key);

            if (product == null)
            {
                NotFound();
            }

            JsonConvert.PopulateObject(values, product!);

            var result = productValidator.Validate(product, _ => _.IncludeRuleSets("Edit"));

            if (!result.IsValid)
            {
                result.AddToModelState(ModelState);

                return BadRequest(ModelState);
            }

            context.Products.Update(product!);
            context.SaveChanges();

            return Ok(product);
        }

        [HttpDelete]
        public IActionResult Delete(int key)
        {
            var product = context.Products.FirstOrDefault(_ => _.Id == key);

            if (product == null)
            {
                NotFound();
            }

            var result = productValidator.Validate(product, _ => _.IncludeRuleSets("Delete"));

            if (!result.IsValid)
            {
                result.AddToModelState(ModelState);

                return BadRequest(ModelState);
            }

            context.Products.Remove(product);
            context.SaveChanges();

            return Ok();
        }
    }
}
