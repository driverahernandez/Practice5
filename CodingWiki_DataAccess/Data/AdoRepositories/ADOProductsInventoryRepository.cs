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
    public class ADOProductsInventoryRepository : AdoRepository, IRepositoryProductsInventory
    {
        // Provide the query string with a parameter placeholder.

        public List<ProductInventory> GetProductsInventory()
        {
            queryString = "Select * FROM ProductsInventory;";
            List<ProductInventory> ProductInventoryList = new List<ProductInventory>();

            using (SqlConnection connection = new(connectionString))
            {
                SqlCommand command = new(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ProductInventoryList.Add(new ProductInventory
                    {
                        ProductId = (int)reader[0],
                        Amount = (int)reader[1]
                    });
                }
                reader.Close();
            }
            return ProductInventoryList;
        }

        public ProductInventory GetProductInventoryById(int id)
        {
            queryString = "SELECT TOP(1) * " +
                        "FROM ProductsInventory " +
                        "WHERE ProductId = @id;";
            ProductInventory obj = new ProductInventory();

            using (SqlConnection connection = new(connectionString))
            {
                SqlCommand command = new(queryString, connection);
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    obj.ProductId = (int)id;
                    obj.Amount = (int)reader[1];
                }
                reader.Close();
            }
            return obj;
        }

        public void CreateProductInventory(ProductInventory obj)
        {
            using (SqlConnection connection = new(connectionString))
            {
                //create
                queryString = "INSERT INTO ProductsInventory Values(@Amount);";
                SqlCommand command = new(queryString, connection);
                command.Parameters.AddWithValue("@Amount", obj.Amount);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public ProductInventory UpdateProductInventory(int? id)
        {
            ProductInventory obj = new ProductInventory();
            if (id == null || id == 0)
            {
                //create 
                return obj;
            }
            //edit 
            queryString = "SELECT TOP(1) * " +
                        "FROM ProductsInventory " +
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
                        obj.Amount = (int)reader[1];
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

        public void UpdateProductInventory(ProductInventory obj)
        {
            using (SqlConnection connection = new(connectionString))
            {
                if (obj.ProductId == 0)
                {
                    //create
                    queryString = "INSERT INTO ProductsInventory Values(@Amount);";
                    SqlCommand command = new(queryString, connection);
                    command.Parameters.AddWithValue("@Amount", obj.Amount);
                    

                    connection.Open();
                    command.ExecuteNonQuery();

                }
                else
                {
                    //update
                    queryString = "UPDATE ProductsInventory " +
                        "SET Amount = @Amount " +
                        "WHERE ProductId = @id; ";
                    SqlCommand command = new(queryString, connection);
                    command.Parameters.AddWithValue("@id", obj.ProductId);
                    command.Parameters.AddWithValue("@Amount", obj.Amount);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            return;
        }
        public bool DeleteProductInventory(int? id)
        {
            ProductInventory obj = new ProductInventory();
            using (SqlConnection connection = new(connectionString))
            {
                bool isObjectFound = false;
                queryString = "SELECT * " +
                            "FROM ProductsInventory " +
                            "WHERE ProductId = @id;";

                SqlCommand findCommand = new(queryString, connection);
                findCommand.Parameters.AddWithValue("@id", id);

                connection.Open();
                SqlDataReader reader = findCommand.ExecuteReader();
                while (reader.Read())
                {
                    obj.Amount = (int)reader[1];
                }
                reader.Close();
                if (obj == null)
                {
                    return isObjectFound;
                }
                isObjectFound = true;

                //delete
                queryString = "DELETE FROM ProductsInventory " +
                            "WHERE ProductId = @id;";

                SqlCommand deleteCommand = new(queryString, connection);
                deleteCommand.Parameters.AddWithValue("@id", id);
                deleteCommand.ExecuteNonQuery();

                return isObjectFound;
            }
        }
    }
}
