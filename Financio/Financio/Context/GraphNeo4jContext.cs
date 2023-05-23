using Neo4j.Driver;

namespace Financio
{
    public class GraphNeo4jContext
    {
        private readonly IDriver driver;

        public GraphNeo4jContext(IConfiguration configuration)
        {
            var neo4jUri = configuration["Neo4j:Uri"];
            var neo4jUser = configuration["Neo4j:User"];
            var neo4jPassword = configuration["Neo4j:Password"];

            driver = GraphDatabase.Driver(neo4jUri, AuthTokens.Basic(neo4jUser, neo4jPassword));
        }

        public async Task<List<string>> GetNeighborsWithConnectionCount(List<string> nodeIDs)
        {
            var cypherQuery = $"MATCH (source)-[r]-(neighbor) " +
                              $"WHERE source.nodeID IN {ToCypherList(nodeIDs)} " +
                              $"  AND NOT neighbor.nodeID IN {ToCypherList(nodeIDs)} " +
                              $"RETURN neighbor.nodeID AS Neighbor, COUNT(r) AS ConnectionCount " +
                              $"ORDER BY ConnectionCount DESC";

            var results = new List<string>();

            using (var session = driver.AsyncSession())
            {
                var result = await session.RunAsync(cypherQuery);

                while (await result.FetchAsync())
                {
                    var neighbor = result.Current["Neighbor"].As<string>();
                    var connectionCount = result.Current["ConnectionCount"].As<long>();
                    results.Add(neighbor);
                }
            }

            return results;
        }

        private string ToCypherList(List<string> values)
        {
            return $"[{string.Join(", ", values.Select(v => $"'{v}'"))}]";
        }
    }
}
