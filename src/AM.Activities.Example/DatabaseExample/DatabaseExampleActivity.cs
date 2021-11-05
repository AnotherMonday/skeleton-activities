using System;
using System.Activities;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using AM.Activities.Common.BaseActivities;
using AM.ComposerActivitiesBridge.Attributes;
using AM.Logging;

namespace AM.Activities.Example.DatabaseExample
{
    /// <summary>
    ///     Example on how to interact with the SQL server from the Composer Environment Settings.
    ///     This example is not SQL Injection safe and should not be used in production and just a very basic example.
    /// </summary>
    public class DatabaseExampleActivity : AbstractDatabaseActivity<DataTable>
    {
        /// <summary>
        ///     Input Argument of a SQL Query to select data by.
        /// </summary>
        [Category("Input")]
        [VariablePopup]
        public InArgument<string> Query { get; set; }

        /// <summary>
        ///     Output Argument to return the requested data as a DataTable
        /// </summary>
        [Category("Output")]
        [VariablePopup]
        public OutArgument<DataTable> ResultTable { get; set; }

        /// <summary>
        ///     Asynchronously execute the activity
        /// </summary>
        /// <param name="context">The execution context for an asynchronous activity.</param>
        /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
        /// <returns>Represents an asynchronous operation that returns DataTable.</returns>
        protected override Task<DataTable> ExecuteAsyncWithResult(AsyncCodeActivityContext context,
            CancellationToken cancellationToken)
        {
            string query = context.GetValue(Query);

            // Connection string from the Composer's Environment Settings.
            string connectionString = GetConnectionString(context);

            return Task.Factory.StartNew(
                () =>
                {
                    try
                    {
                        DataTable dataTable = new DataTable();

                        // Create a SQL connection to the Server
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            // Execute SQL Query
                            using (SqlCommand sqlCommand = new SqlCommand(query, connection))
                            {
                                connection.Open();

                                // Fill the Datable with the request data
                                using (SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand))
                                {
                                    dataAdapter.Fill(dataTable);
                                    connection.Close();
                                }
                            }
                        }

                        // After a successful execution return the DataTable
                        return dataTable;
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Error(ex.ToString());
                        throw;
                    }
                },
                cancellationToken);
        }

        /// <summary>
        ///     Set the Output Argument after Task completion.
        /// </summary>
        /// <param name="context">The execution context for an asynchronous activity.</param>
        /// <param name="result">The result of the Database interaction as DataTable object.</param>
        protected override void OutputResult(AsyncCodeActivityContext context, DataTable result)
        {
            ResultTable.Set(context, result);
        }
    }
}