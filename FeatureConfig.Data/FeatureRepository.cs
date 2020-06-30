using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using FeatureConfig.Data.Entities;

namespace FeatureConfig.Data
{
    public class FeatureRepository : IFeatureRepository
    {
        private readonly IDapperManager _dapperManager;

        public FeatureRepository(IDapperManager dapperManager)
        {
            _dapperManager = dapperManager;
        }

        public Task<int> Create(FeatureValue featureValue)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("Name", featureValue.Name, DbType.String);
            dbPara.Add("Value", featureValue.Value, DbType.String);
            var featureValueId = Task.FromResult(_dapperManager.Insert<int>("[dbo].[SP_Add_FeatureValue]",
                            dbPara,
                            commandType: CommandType.StoredProcedure));
            return featureValueId;
        }

        public Task<FeatureValue> GetById(int id)
        {
            var featureValue = Task.FromResult(_dapperManager.Get<FeatureValue>($"select * from [FeatureValue] where ID = {id}", null,
                    commandType: CommandType.Text));
            return featureValue;
        }

        public Task<int> Delete(int id)
        {
            var deleteFeatureValue = Task.FromResult(_dapperManager.Execute($"Delete [FeatureValue] where ID = {id}", null,
                    commandType: CommandType.Text));
            return deleteFeatureValue;
        }

        public Task<int> Count(string search)
        {
            var totFeatureValue = Task.FromResult(_dapperManager.Get<int>($"select COUNT(*) from [FeatureValue] WHERE Name like '%{search}%'", null,
                    commandType: CommandType.Text));
            return totFeatureValue;
        }

        public Task<List<FeatureValue>> ListAll(int skip, int take, string orderBy, string direction = "DESC", string search = "")
        {
            var featureValues = Task.FromResult(_dapperManager.GetAll<FeatureValue>
                ($"SELECT * FROM [FeatureValue] WHERE Name like '%{search}%' ORDER BY {orderBy} {direction} OFFSET {skip} ROWS FETCH NEXT {take} ROWS ONLY; ", null, commandType: CommandType.Text));
            return featureValues;
        }

        public Task<int> Update(FeatureValue featureValue)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("Id", featureValue.ID);
            dbPara.Add("Name", featureValue.Name, DbType.String);
            dbPara.Add("Value", featureValue.Value, DbType.String);

            var updateFeatureValue = Task.FromResult(_dapperManager.Update<int>("[dbo].[SP_Update_FeatureValue]",
                            dbPara,
                            commandType: CommandType.StoredProcedure));
            return updateFeatureValue;
        }
    }
}
