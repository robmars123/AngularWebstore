using DAL.Models;

namespace Infrastructure.Processors
{
    public class ProductUploadProcessor
    {
        public IEnumerable<Product> Process(string data)
        {
            //map the array into the object
            var product = new Product();
            var productList = new List<Product>();

            List<string> columns = new List<string>();
            List<string> records = new List<string>();
            int lineNumber = 0;

            var values = data.Split("\r\n");
            while (lineNumber < values.Length - 1)
            {
                //get row 0 for columns
                if (!columns.Any())
                    columns.Add(values[lineNumber]);

                //row greater than 0 are records.
                if (lineNumber > 0)
                    records.Add(values[lineNumber]);
                lineNumber++;


                //map each record to a product object
                var columnValueList = new string[1];
                if (records.Any())
                {
                    foreach (var record in records)
                    {
                        columnValueList = record.Split(",").ToArray();
                        product.Product_Name = columnValueList[0];
                        product.Description = columnValueList[1];
                        product.Price = decimal.Parse(columnValueList[2]);
                        product.QuantityPerUnit = int.Parse(columnValueList[3]);
                        product.Category_id = int.Parse(columnValueList[4]);
                        product.Subcategory_id = int.Parse(columnValueList[5]);
                        //add product to list
                        productList.Add(product);

                        records = new List<string>(); //refresh
                        product = new Product(); //refresh
                    }

                }
            }

            if (!productList.Any())
                return new List<Product>();

            return productList;
        }
    }
}
