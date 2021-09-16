using EnergyAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;


namespace EnergyAPI.DAO
{
    public class EnergyApi
    {
        private static string connectionString = @"Data Source=DESKTOP-GRIKV9R\SQLEXPRESS01;Initial Catalog=energy;Integrated Security=True;Pooling=False";

        public SqlConnection Connection { get; set; }
        public void Connect()
        {
            Connection = new SqlConnection(connectionString);
            Connection.Open();
        }

        public void Disconnect() => Connection.Close();

        public List<Measuring> GetMeasurings()
        {
            Connect();
            List<Measuring> result = new List<Measuring>();

            try
            {
                SqlCommand command = new SqlCommand("SELECT * FROM measuring_values;", Connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    Measuring record = new Measuring();
                    record.id = Convert.ToInt32(reader["id"]);
                    record.meter_id = Convert.ToInt32(reader["meter_id"]);
                    record.parameter_id = Convert.ToInt32(reader["parameter_id"]);
                    record.value = Convert.ToSingle(reader["value"]);
                    record.value_dt = Convert.ToDateTime(reader["value_dt"]);
                    result.Add(record);
                }

                reader.Close();
            }
            catch (Exception) { }
            finally { Disconnect(); }
            return result;
        }

        public List<ViewMeasuring> GetMeasurings(int MetersId, DateTime DateValue)
        {
            Connect();
            List<ViewMeasuring> result = new List<ViewMeasuring>();
            string dt = DateValue.ToString("d");

            try
            {
                SqlCommand command = new SqlCommand("SELECT * FROM (SELECT[meter_id], [value_dt], [value], [parameter_id] FROM[dbo].[measuring_values]) AS SourceTable PIVOT(AVG([value]) FOR[parameter_id] IN([1],[2],[3],[4])) AS PivotTable WHERE meter_id = '" + MetersId + "' AND CONVERT(date, value_dt, 3) = '" + dt + "';", Connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ViewMeasuring record = new ViewMeasuring();
                    record.meterId = Convert.ToInt32(reader["meter_id"]);
                    record.actO = Convert.ToSingle(reader["2"]);
                    record.actR = Convert.ToSingle(reader["1"]);
                    record.reactO = Convert.ToSingle(reader["4"]);
                    record.reactR = Convert.ToSingle(reader["3"]);
                    record.value_dt = Convert.ToDateTime(reader["value_dt"]);
                    result.Add(record);
                }

                reader.Close();
            }
            catch (Exception) { }
            finally { Disconnect(); }
            return result;
        }

        public List<Meters> GetMeters()
        {
            Connect();
            List<Meters> result = new List<Meters>();

            try
            {
                SqlCommand command = new SqlCommand("SELECT * FROM meters;", Connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    Meters record = new Meters();
                    record.id = Convert.ToInt32(reader["id"]);
                    record.caption = Convert.ToString(reader["caption"]);
                    result.Add(record);
                }

                reader.Close();
            }
            catch (Exception) { }
            finally { Disconnect(); }
            return result;

        }

        public List<Parameters> GetParametrs()
        {
            Connect();
            List<Parameters> result = new List<Parameters>();

            try
            {
                SqlCommand command = new SqlCommand("SELECT * FROM parameters;", Connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    Parameters record = new Parameters();
                    record.id = Convert.ToInt32(reader["id"]);
                    record.caption = Convert.ToString(reader["caption"]);
                    record.measure_units = Convert.ToString(reader["measure_units"]);
                    result.Add(record);
                }

                reader.Close();
            }
            catch (Exception) { }
            finally { Disconnect(); }
            return result;

        }
    }   
}
