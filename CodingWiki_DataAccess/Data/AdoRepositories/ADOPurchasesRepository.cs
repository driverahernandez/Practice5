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

    public class ADOPurchasesRepository : AdoRepository, IRepositoryPurchases
    {
        // Provide the query string with a parameter placeholder.
        string queryString;

        public List<Purchase> GetPurchases()
        {
            queryString = "Select * FROM Purchases;";
            List<Purchase> PurchaseList = new List<Purchase>();

            using (SqlConnection connection = new(connectionString))
            {
                SqlCommand command = new(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    PurchaseList.Add(new Purchase
                    {
                        PurchaseId = (int)reader[0],
                        ProductId = (int)reader[1],
                        Total = (double)reader[2],
                        PurchaseDate = DateOnly.FromDateTime((DateTime)reader[3])
                    });
                }
                reader.Close();
            }
            return PurchaseList;
        }
        public Purchase GetPurchaseById(int id)
        {
            queryString = "SELECT TOP(1) * " +
                        "FROM Purchases " +
                        "WHERE PurchaseId = @id;";
            Purchase obj = new Purchase();

            using (SqlConnection connection = new(connectionString))
            {
                SqlCommand command = new(queryString, connection);
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    obj.PurchaseId = (int)id;
                    obj.ProductId = (int)reader[1];
                    obj.Total = (double)reader[2];
                    obj.PurchaseDate = DateOnly.FromDateTime((DateTime)reader[3]);
                }
                reader.Close();
            }
            return obj;
        }

        public void CreatePurchase(Purchase obj)
        {
            using (SqlConnection connection = new(connectionString))
            {
                //create
                queryString = "INSERT INTO Purchases Values(@ProductId, @Total, @PurchaseDate);";
                SqlCommand command = new(queryString, connection);
                command.Parameters.AddWithValue("@ProductId", obj.ProductId);
                command.Parameters.AddWithValue("@Total", obj.Total);
                command.Parameters.AddWithValue("@PurchaseDate", obj.PurchaseDate);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public Purchase UpdatePurchase(int? id)
        {
            Purchase obj = new Purchase();
            if (id == null || id == 0)
            {
                //create 
                return obj;
            }
            //edit 
            queryString = "SELECT TOP(1) * " +
                        "FROM Purchases " +
                        "WHERE PurchaseId = @id;";

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
                        obj.PurchaseId = (int)id;
                        obj.ProductId = (int)reader[1];
                        obj.Total = (double)reader[2];
                        obj.PurchaseDate = DateOnly.FromDateTime((DateTime)reader[3]);
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

        public void UpdatePurchase(Purchase obj)
        {
            using (SqlConnection connection = new(connectionString))
            {
                if (obj.PurchaseId == 0)
                {
                    //create
                    queryString = "INSERT INTO Purchases Values(@ProductId, @Total, @PurchaseDate);";
                    SqlCommand command = new(queryString, connection);
                    command.Parameters.AddWithValue("@ProductId", obj.ProductId);
                    command.Parameters.AddWithValue("@Total", obj.Total);
                    command.Parameters.AddWithValue("@PurchaseDate", obj.PurchaseDate);

                    connection.Open();
                    command.ExecuteNonQuery();

                }
                else
                {
                    //update
                    queryString = "UPDATE Purchases " +
                        "SET ProductId = @ProductId, " +
                        "Total = @Total, " +
                        "PurchaseDate = @PurchaseDate " +
                        "WHERE PurchaseId = @id; ";
                    SqlCommand command = new(queryString, connection);
                    command.Parameters.AddWithValue("@id", obj.PurchaseId);
                    command.Parameters.AddWithValue("@ProductId", obj.ProductId);
                    command.Parameters.AddWithValue("@Total", obj.Total);
                    command.Parameters.AddWithValue("@PurchaseDate", obj.PurchaseDate);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            return;
        }
        public bool DeletePurchase(int? id)
        {
            Purchase obj = new Purchase();
            using (SqlConnection connection = new(connectionString))
            {
                bool isObjectFound = false;
                queryString = "SELECT * " +
                            "FROM Purchases " +
                            "WHERE PurchaseId = @id;";

                SqlCommand findCommand = new(queryString, connection);
                findCommand.Parameters.AddWithValue("@id", id);

                connection.Open();
                SqlDataReader reader = findCommand.ExecuteReader();
                while (reader.Read())
                {
                    //obj.PurchaseId = (int)reader[0];
                    obj.ProductId = (int)reader[1];
                    obj.Total = (double)reader[2];
                    obj.PurchaseDate = DateOnly.FromDateTime((DateTime)reader[3]);
                }
                reader.Close();
                if (obj == null)
                {
                    return isObjectFound;
                }
                isObjectFound = true;

                //delete
                queryString = "DELETE FROM Purchases " +
                            "WHERE PurchaseId = @id;";

                SqlCommand deleteCommand = new(queryString, connection);
                deleteCommand.Parameters.AddWithValue("@id", id);
                deleteCommand.ExecuteNonQuery();

                return isObjectFound;
            }
        }
    }
}
