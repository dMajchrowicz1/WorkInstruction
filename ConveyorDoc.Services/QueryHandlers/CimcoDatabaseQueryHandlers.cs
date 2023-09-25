using ConveyorDoc.Business.Model;
using ConveyorDoc.Business.Queries;
using ConveyorDoc.Core.Database;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorDoc.Services.QueryHandlers
{
    public class CimcoDatabaseQueryHandlers : IGetModuleProgramsQuery
    {
        private readonly IDbConnection _connection;

        public CimcoDatabaseQueryHandlers(ICimcoConnectionFactory connectionFactory)
        {
            _connection = connectionFactory.GetOpenConnection();
        }


        public IEnumerable<NcProgram> GetModulePrograms(string moduleNumber)
        {
            var result = Enumerable.Empty<NcProgram>();

            string query = @$"SELECT 
                                data3.dataname as ProgramNumber,
                                data3.datafilename as ProgramPath,
                                CASE
	     	                                WHEN data2.dataname = 'GurutzpeT1' THEN 'T1'
	     	                                WHEN data2.dataname = 'GurutzpeT3' THEN 'T3'
	     	                                WHEN data2.dataname = 'GurutzpeS1' THEN 'S1'
	     	                                WHEN data2.dataname = 'GurutzpeS2' THEN 'S2'
	     	                                WHEN data2.dataname = 'GurutzpeS4' THEN 'S4'
	     	                                WHEN data2.dataname = 'GurutzpeC7' THEN 'C7'
	     	                                WHEN data2.dataname = 'GurutzpeC9' THEN 'C9'
	     	                                WHEN data2.dataname = 'GurutzpeC14' THEN 'C14'
	     	                                WHEN data2.dataname = 'GurutzpeC14L' THEN 'C14L'
	     	                                WHEN data2.dataname = 'GurutzpeC15' THEN 'C15'
	     	                                WHEN data2.dataname = 'DMC80_1' THEN 'D1'
	     	                                WHEN data2.dataname = 'DMC80_2' THEN 'D2'
	     	                                WHEN data2.dataname = 'DMC80_3' THEN 'D3'
	     	                                WHEN data2.dataname = 'DMC80_4' THEN 'D4'
	     	                                END AS Machine,
	     	                    CASE 
	     	                                WHEN data3.dataname COLLATE SQL_Latin1_General_CP1_CI_AS like '%nc' THEN 'Fanuc'
	     	                                WHEN data3.dataname COLLATE SQL_Latin1_General_CP1_CI_AS like '%mpf' THEN 'Sinumerik'
	     	                                END AS ControlType 
                                FROM dbo.leveldata data1
                                LEFT JOIN dbo.leveldata data2 ON data1.dataid = data2.dataparentid
                                LEFT JOIN dbo.leveldata data3 ON data2.dataid = data3.dataparentid
                                WHERE data1.datalevelid = '2' 
                                    AND data2.datalevelid = '3' 
                                    AND data3.datalevelid = '6'
                                    AND data1.dataname = '{moduleNumber}'";

            result = _connection.Query<NcProgram>(query);
       
            return result;
        }
    }
}
