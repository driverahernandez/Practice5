using Microsoft.IdentityModel.Tokens;
using Practice5_Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer;
using Microsoft.Data.SqlClient;
using Practice5_DataAccess.Interface;
using com.sun.xml.@internal.bind.v2.model.core;

namespace Practice5_DataAccess.Data.AdoRepositories
{
    public class ADOProductsRepository : AdoRepository, IRepositoryProducts
    {
        // Provide the query string with a parameter placeholder.

        public List<Product> GetProducts()
        {
            queryString = "Select * FROM Products;";
            List<Product> ProductList = new List<Product>();

            using (SqlConnection connection = new(connectionString))
            {
                SqlCommand command = new(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ProductList.Add(new Product
                    {
                        ProductId = (int)reader[0],
                        ProductName = (string)reader[1],
                        Price = (double)reader[2]
                    });
                }
                reader.Close();
            }
            return ProductList;
        }

        public Product UpdateProduct(int? id)
        {
            Product obj = new Product();
            if (id == null || id == 0)
            {
                //create 
                return obj;
            }
            //edit 
            queryString = "SELECT TOP(1) * " +
                        "FROM Products " +
                        "WHERE ProductId = @id;";

            using (SqlConnection connection = new(connectionString))
            {
                SqlCommand command = new(queryString, connection);
                command.Parameters.AddWithValue("@id", id);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        obj.ProductId = (int)id;
                        obj.ProductName = (string)reader[1];
                        obj.Price = (double)reader[2];
                    }
                    reader.Close();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                if (obj == null)
                {
                    return null;
                }
                return obj;
            }
        }

        public void UpdateProduct(Product obj)
        {
            using (SqlConnection connection = new(connectionString))
            {
                if (obj.ProductId == 0)
                {
                    //create
                    queryString = "INSERT INTO Products Values(@ProductName, @Price);";
                    SqlCommand command = new(queryString, connection);
                    command.Parameters.AddWithValue("@ProductName", obj.ProductName);
                    command.Parameters.AddWithValue("@Price", obj.Price);

                    connection.Open();
                    command.ExecuteNonQuery();

                }
                else
                {
                    //update
                    queryString = "UPDATE Products " +
                        "SET ProductName = @ProductName, " +
                        "Price = @Price, " +
                        "WHERE ProductId = @id; ";
                    SqlCommand command = new(queryString, connection);
                    command.Parameters.AddWithValue("@id", obj.ProductId);
                    command.Parameters.AddWithValue("@ProductName", obj.ProductName);
                    command.Parameters.AddWithValue("@Price", obj.Price);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            return;
        }
        public bool DeleteProduct(int? id)
        {
            Product obj = new Product();
            using (SqlConnection connection = new(connectionString))
            {
                bool isObjectFound = false;
                queryString = "SELECT * " +
                            "FROM Products " +
                            "WHERE ProductId = @id;";

                SqlCommand findCommand = new(queryString, connection);
                findCommand.Parameters.AddWithValue("@id", id);

                connection.Open();
                SqlDataReader reader = findCommand.ExecuteReader();
                while (reader.Read())
                {
                    //obj.ProductId = (int)reader[0];
                    obj.ProductName = (string)reader[1];
                    obj.Price = (double)reader[2];
                }
                reader.Close();
                if (obj == null)
                {
                    return isObjectFound;
                }
                isObjectFound = true;

                //delete
                queryString = "DELETE FROM Products " +
                            "WHERE ProductId = @id;";

                SqlCommand deleteCommand = new(queryString, connection);
                deleteCommand.Parameters.AddWithValue("@id", id);
                deleteCommand.ExecuteNonQuery();

                return isObjectFound;
            }
        }
    }
}
