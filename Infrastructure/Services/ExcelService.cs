using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ExcelService : IExcelService
    {
        public byte[] ExportProductsToExcelAsync(List<ProductDto> products)
        {
           using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Products");
            // Add headers
            worksheet.Cells[1, 1].Value = "Name";
            worksheet.Cells[1, 2].Value = "ProductCode";
            worksheet.Cells[1, 3].Value = "Description";
            worksheet.Cells[1, 4].Value = "Price";
            worksheet.Cells[1, 5].Value = "ImageUrl";

            // Add product data
            for (int i = 0; i < products.Count; i++)
            {
               // decimal price =0;
                var product = products[i];
                //if (!decimal.TryParse(product.Price.ToString(), out price))
                //    continue;
              
                worksheet.Cells[i + 2, 1].Value = product.Name;
                worksheet.Cells[i + 2, 2].Value = product.ProductCode;
                worksheet.Cells[i + 2, 3].Value = product.Description;
                worksheet.Cells[i + 2, 4].Value = product.Price;
                worksheet.Cells[i + 2, 5].Value = product.ImageUrl;
            }



            return package.GetAsByteArray();
        }

        public async Task<List<ProductDto>> ImportProductsAsync(IFormFile file)
        {
            var products = new List<ProductDto>();

            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            stream.Position = 0;

            using var package = new ExcelPackage(stream);
            var worksheet = package.Workbook.Worksheets[0];
            var rowCount = worksheet.Dimension.Rows;

            for (int row = 2; row <= rowCount; row++) // skip header
            {
                var product = new ProductDto
                {
                    Name = worksheet.Cells[row, 1].Text,
                    Description = worksheet.Cells[row, 2].Text,
                    CategoryName = worksheet.Cells[row, 3].Text,
                    CategoryId = int.TryParse(worksheet.Cells[row, 4].Text, out var categoryId) ? categoryId : 0,
                    Price = decimal.TryParse(worksheet.Cells[row, 5].Text, out var price) ? price : 0
                };

                products.Add(product);
            }

            return products;

        }
    }
}
