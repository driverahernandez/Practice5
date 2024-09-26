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
    public class ADOSalesRepository : AdoRepository, IRepositorySales
    {

        public List<Sale> GetSales()
        {
            queryString = "Select * FROM Sales;";
            List<Sale> SaleList = new List<Sale>();

            using (SqlConnection connection = new(connectionString))
            {
                SqlCommand command = new(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    SaleList.Add(new Sale
                    {
                        SaleId = (int)reader[0],
                        ProductId = (int)reader[1],
                        Total = (double)reader[2],
                        SaleDate = DateOnly.FromDateTime((DateTime)reader[3])
                    });
                }
                reader.Close();
            }
            return SaleList;
        }
        public Sale GetSaleById(int id)
        {
            queryString = "SELECT TOP(1) * " +
                        "FROM Sales " +
                        "WHERE SaleId = @id;";
            Sale obj = new Sale();

            using (SqlConnection connection = new(connectionString))
            {
                SqlCommand command = new(queryString, connection);
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    obj.SaleId = (int)id;
                    obj.ProductId = (int)reader[1];
                    obj.Total = (double)reader[2];
                    obj.SaleDate = DateOnly.FromDateTime((DateTime)reader[3]);
                }
                reader.Close();
            }
            return obj;
        }

        public void CreateSale(Sale obj)
        {
            using (SqlConnection connection = new(connectionString))
            {
                //create
                queryString = "INSERT INTO Sales Values(@ProductId, @Total, @SaleDate);";
                SqlCommand command = new(queryString, connection);
                command.Parameters.AddWithValue("@ProductId", obj.ProductId);
                command.Parameters.AddWithValue("@Total", obj.Total);
                command.Parameters.AddWithValue("@SaleDate", obj.SaleDate);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public Sale UpdateSale(int? id)
        {
            Sale obj = new Sale();
            if (id == null || id == 0)
            {
                //create 
                return obj;
            }
            //edit 
            queryString = "SELECT TOP(1) * " +
                        "FROM Sales " +
                        "WHERE SaleId = @id;";

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
                        obj.SaleId = (int)id;
                        obj.ProductId = (int)reader[1];
                        obj.Total = (double)reader[2];
                        obj.SaleDate = DateOnly.FromDateTime((DateTime)reader[3]);
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

        public void UpdateSale(Sale obj)
        {
            using (SqlConnection connection = new(connectionString))
            {
                if (obj.SaleId == 0)
                {
                    //create
                    queryString = "INSERT INTO Sales Values(@ProductId, @Total, @SaleDate);";
                    SqlCommand command = new(queryString, connection);
                    command.Parameters.AddWithValue("@ProductId", obj.ProductId);
                    command.Parameters.AddWithValue("@Total", obj.Total);
                    command.Parameters.AddWithValue("@SaleDate", obj.SaleDate);

                    connection.Open();
                    command.ExecuteNonQuery();

                }
                else
                {
                    //update
                    queryString = "UPDATE Sales " +
                        "SET ProductId = @ProductId, " +
                        "Total = @Total, " +
                        "SaleDate = @SaleDate " +
                        "WHERE SaleId = @id; ";
                    SqlCommand command = new(queryString, connection);
                    command.Parameters.AddWithValue("@id", obj.SaleId);
                    command.Parameters.AddWithValue("@ProductId", obj.ProductId);
                    command.Parameters.AddWithValue("@Total", obj.Total);
                    command.Parameters.AddWithValue("@SaleDate", obj.SaleDate);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            return;
        }
        public bool DeleteSale(int? id)
        {
            Sale obj = new Sale();
            using (SqlConnection connection = new(connectionString))
            {
                bool isObjectFound = false;
                queryString = "SELECT * " +
                            "FROM Sales " +
                            "WHERE SaleId = @id;";

                SqlCommand findCommand = new(queryString, connection);
                findCommand.Parameters.AddWithValue("@id", id);

                connection.Open();
                SqlDataReader reader = findCommand.ExecuteReader();
                while (reader.Read())
                {
                    //obj.SaleId = (int)reader[0];
                    obj.ProductId = (int)reader[1];
                    obj.Total = (double)reader[2];
                    obj.SaleDate = DateOnly.FromDateTime((DateTime)reader[3]);
                }
                reader.Close();
                if (obj == null)
                {
                    return isObjectFound;
                }
                isObjectFound = true;

                //delete
                queryString = "DELETE FROM Sales " +
                            "WHERE SaleId = @id;";

                SqlCommand deleteCommand = new(queryString, connection);
                deleteCommand.Parameters.AddWithValue("@id", id);
                deleteCommand.ExecuteNonQuery();

                return isObjectFound;
            }
        }
    }
}
