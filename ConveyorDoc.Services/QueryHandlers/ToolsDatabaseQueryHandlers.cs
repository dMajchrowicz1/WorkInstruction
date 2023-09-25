using ConveyorDoc.Business.Queries;
using ConveyorDoc.Core.Database;
using Dapper;
using Slapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorDoc.Services.QueryHandlers
{
    public class ToolsDatabaseQueryHandlers : IGetAllToolsQuery, IGetToolQuery
    {
        private readonly IDbConnection _connection;


        public ToolsDatabaseQueryHandlers(IToolsConnectionFactory toolsConnection)
        {
            _connection = toolsConnection.GetOpenConnection();

            //Mapping config of multilevel tool entity
            AutoMapper.Configuration.AddIdentifier(typeof(ToolDto), "ItemId");
            AutoMapper.Configuration.AddIdentifier(typeof(ToolPartsDto), "CuttingPart");
            AutoMapper.Configuration.AddIdentifier(typeof(ToolDimensionsDto), "Zvalue");
        }

        public ToolDto GetTool(string offset, string machine)
        {
            string query = @$"SELECT 
                                     tool.[ItemId]
                                    ,tool.[Position]
                                    ,tool.[Offset]
                                    ,tool.[Machine]
                                    ,item.[Name]
                                    ,item.[Type] 
                                    ,item.[PDF]
                                    ,item.[Zvalue] as Dimensions_Zvalue
                                    ,item.[Xvalue] as Dimensions_Xvalue
                                    ,item.[CuttingPart] as Parts_CuttingPart
                                    ,item.[Item2] as Parts_Item2
                                    ,item.[Item3] as Parts_Item3
                                    ,item.[Item4] as Parts_Item4
                                    ,item.[Item5] as Parts_Item5
                                    ,item.[Item6] as Parts_Item6                                    
                            FROM [ToolList$] tool 
                            LEFT JOIN [ItemList$] item ON tool.ItemId = item.ID
                            WHERE [Offset] = @Offset AND [Machine] = @Machine";

            var result = _connection.QuerySingle<dynamic>(query, new { Offset = offset, Machine = machine });

            return Slapper.AutoMapper.MapDynamic<ToolDto>(result);
        }

        public IEnumerable<ToolDto> GetAllTools()
        {
            var result = Enumerable.Empty<ToolDto>();

            string query = @$"SELECT 
                                     tool.[ItemId]
                                    ,tool.[Position]
                                    ,tool.[Offset]
                                    ,tool.[Machine]
                                    ,item.[Name]
                                    ,item.[Type] 
                                    ,item.[PDF]
                                    ,item.[Zvalue] as Dimensions_Zvalue
                                    ,item.[Xvalue] as Dimensions_Xvalue
                                    ,item.[CuttingPart] as Parts_CuttingPart
                                    ,item.[Item2] as Parts_Item2
                                    ,item.[Item3] as Parts_Item3
                                    ,item.[Item4] as Parts_Item4
                                    ,item.[Item5] as Parts_Item5
                                    ,item.[Item6] as Parts_Item6                                    
                            FROM [ToolList$] tool 
                            LEFT JOIN [ItemList$] item ON tool.ItemId = item.ID";


            var tools = _connection.Query<dynamic>(query);

            result = AutoMapper.MapDynamic<ToolDto>(tools);

            return result;
        }
    }
}
