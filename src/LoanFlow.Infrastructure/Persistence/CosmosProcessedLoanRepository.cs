// LoanFlow.Infrastructure/Repositories/CosmosProcessedLoanRepository.cs
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using LoanFlow.Application.Models;
using LoanFlow.Application.Repositories;
using Microsoft.Azure.Cosmos;

namespace LoanFlow.Infrastructure.Repositories
{
    public class CosmosProcessedLoanRepository : IProcessedLoanRepository
    {
        private readonly Container _container;
        public CosmosProcessedLoanRepository(CosmosClient client)
            => _container = client.GetDatabase("LoanFlow")
                                   .GetContainer("ProcessedLoans");

        public async Task<IEnumerable<ProcessedLoanDto>> GetAllAsync()
        {
            var query = _container.GetItemQueryIterator<ProcessedLoanDto>(
                "SELECT * FROM c");
            var list = new List<ProcessedLoanDto>();
            while (query.HasMoreResults)
                list.AddRange((await query.ReadNextAsync()).Resource);
            return list;
        }

        public async Task<ProcessedLoanDto?> GetByIdAsync(Guid id)
        {
            try
            {
                var resp = await _container.ReadItemAsync<ProcessedLoanDto>(
                    id.ToString(), new PartitionKey(id.ToString()));
                return resp.Resource;
            }
            catch (CosmosException e) when (e.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
        }
    }
}
