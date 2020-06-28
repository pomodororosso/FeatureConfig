using FeatureConfig.Web.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FeatureConfig.Web.Contracts
{
    public interface IFeatureRepository
    {
        Task<int> Create(FeatureValue featureValue);
        Task<int> Delete(int Id);
        Task<int> Count(string search);
        Task<int> Update(FeatureValue featureValue);
        Task<FeatureValue> GetById(int Id);
        Task<List<FeatureValue>> ListAll(int skip, int take, string orderBy, string direction, string search);
    }
}
