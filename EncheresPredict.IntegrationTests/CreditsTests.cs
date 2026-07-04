using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace EncheresPredict.IntegrationTests;

public class CreditsTests
{
    [Fact]
    public async Task Register_Should_Create_Account_With_Three_Welcome_Credits()
    {
        var factory = new CustomWebApplicationFactory();
        var client = factory.CreateClient();

        var token = await client.RegisterAndLoginAsync($"credits-{Guid.NewGuid():N}@test.com");
        client.Authorize(token);

        var response = await client.GetAsync("/api/credits");
        response.EnsureSuccessStatusCode();

        var payload = await response.Content.ReadFromJsonAsync<JsonElement>();

        Assert.Equal(3, payload.GetProperty("balance").GetInt32());

        var transactions = payload.GetProperty("recentTransactions");
        Assert.Equal(1, transactions.GetArrayLength());
        Assert.Equal("welcome_beta", transactions[0].GetProperty("reason").GetString());
        Assert.Equal(3, transactions[0].GetProperty("amount").GetInt32());
        Assert.Equal("Grant", transactions[0].GetProperty("type").GetString());
    }

    [Fact]
    public async Task Consume_On_Zero_Balance_Should_Return_409_Insufficient_Credits()
    {
        var factory = new CustomWebApplicationFactory();
        var client = factory.CreateClient();

        var email = $"consume-{Guid.NewGuid():N}@test.com";
        var token = await client.RegisterAndLoginAsync(email);
        client.Authorize(token);

        var userId = await GetUserIdAsync(client, token);

        for (var i = 0; i < 3; i++)
        {
            var consumeResponse = await client.PostAsJsonAsync("/api/credits/consume", new
            {
                userId,
                reason = "CCV analysis"
            });

            consumeResponse.EnsureSuccessStatusCode();
        }

        var failedConsume = await client.PostAsJsonAsync("/api/credits/consume", new
        {
            userId,
            reason = "CCV analysis"
        });

        Assert.Equal(HttpStatusCode.Conflict, failedConsume.StatusCode);

        var error = await failedConsume.Content.ReadFromJsonAsync<JsonElement>();
        Assert.Equal("INSUFFICIENT_CREDITS", error.GetProperty("erreur").GetString());
    }

    private static async Task<string> GetUserIdAsync(HttpClient client, string token)
    {
        client.Authorize(token);
        var meResponse = await client.GetAsync("/api/auth/me");
        meResponse.EnsureSuccessStatusCode();

        var payload = await meResponse.Content.ReadFromJsonAsync<JsonElement>();
        return payload.GetProperty("id").GetString()!;
    }
}
