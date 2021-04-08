using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using firstHWwithSingleton.Data;
using firstHWwithSingleton.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace firstHWwithSingleton.Controller
{
    [ApiController]
    [EnableCors("AllowSpecificOrigin"), Route("api/[controller]")]
    public class ProductsController: ControllerBase
    {

        private MyData _myData;

        public ProductsController()
        {
            _myData = MyData.Instance;
        }

        [HttpGet]
        public List<ProductModel> Get()
        {
            return _myData.Products;
        }

        [HttpGet("{id}")]
        public ProductModel Get(int id)
        {
            // firstordefault at ve datayı returnle.
            var newData = _myData.Products.FirstOrDefault(x => id == x.Id);
            return newData;
        }

        [HttpPost]
        public IActionResult Post([FromBody] ProductModel product)
        {
            try
            {
                _myData.Products.Add(product);
                return Ok("New Product Successfully Added.");
            }
            catch (System.Exception exp)
            {
                return BadRequest(exp.Message);
            }
            
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ProductModel product)
        {
            try
            {
                var updateData = _myData.Products.FirstOrDefault(x => id == x.Id);
                if (updateData != null)
                {
                    var updateDataSecond = updateData;
                    _myData.Products.Remove(updateData);
                    updateDataSecond = product;
                    _myData.Products.Add(updateDataSecond);
                }
                else
                {
                    throw new ArgumentException("Opps! We can't find the given id number.");
                }
                return Ok("Selected Product Successfully Updated.");
            }
            catch (System.Exception exp)
            {
                return BadRequest(exp.Message);
            }
            
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var deleteData = _myData.Products.FirstOrDefault(x => id == x.Id);
                if (deleteData != null)
                { 
                    _myData.Products.Remove(deleteData);
                }
                else
                {
                    throw new ArgumentException("Opps! We can't find the given id number.");
                }
                return Ok("Selected Product Successfully Deleted.");
            }
            catch (System.Exception exp)
            {
                return BadRequest(exp.Message);
            }
        }
        
        /*[HttpOptions("/find")]
        public IActionResult FindOptions()
        {
            // Get the origin header as Uri
            var origin = GetOrigin();
            // Check whether the caller is allowed or not
            var isAllowed = IsOriginAllowed(origin);
            if(isAllowed)
            {
                Response.Headers.Add("Access-Control-Allow-Origin", new[] { (string)Request.Headers["Origin"] });
                Response.Headers.Add("Access-Control-Allow-Headers", new[] { "Origin, X-Requested-With, Content-Type, Accept" });
                Response.Headers.Add("Access-Control-Allow-Methods", new[] { "POST, OPTIONS" }); // new[] { "GET, POST, PUT, DELETE, OPTIONS" }
                Response.Headers.Add("Access-Control-Allow-Credentials", new[] { "true" });
                return NoContent();
            }
            // return an error status code
            return BadRequest(); 
        }*/

    }
}