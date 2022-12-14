using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Tryitter.Autentication;
using Tryitter.Models;
using Tryitter.Schemas;

namespace Tryitter.Test
{
    public class TestAccountControllerFail : IClassFixture<ContextToTest<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public TestAccountControllerFail(ContextToTest<Program> factory)
        {
            _factory = factory;
        }

        [Trait("Category", "Criar Endpoint para cadastro")]
        [Fact(DisplayName = "Teste para rota post /account com status 400")]
        public async Task CreateUserWithFail()
        {
            var user = new Register
            {
                Email = "aline10@gmail.com",
                Nome = "Aline Souza",
                Modulo = "Back End",
                Status = "Finalizando",
                Senha = "senha123"
            };

            var client = _factory.CreateClient();
            using HttpResponseMessage response = await client.PostAsJsonAsync(
                "account", user
                );
            using HttpResponseMessage responseTwo = await client.PostAsJsonAsync(
               "account", user
                );

            responseTwo.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Trait("Category", "Criar Endpoint para atualização cadastral")]
        [Theory(DisplayName = "Teste para rota put /account com status 400")]
        [InlineData("Front End", "Finalizando")]
        public async Task UpdateAccountWithFail(string modulo, string status)
        {
            var user = new Register
            {
                Email = "maria10@gmail.com",
                Nome = "Maria Silva",
                Modulo = "Back End",
                Status = "Finalizando",
                Senha = "senha123"
            };

            var client = _factory.CreateClient();

            using HttpResponseMessage response = await client.PostAsJsonAsync(
                "account", user
                );

            var content = await response.Content.ReadFromJsonAsync<User>();

            var token = new TokenGenerator().Generate(user.Nome, user.Email, content.Id);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var studentUpdate = new UpdateAccount
            {
                Id = 99,
                Modulo = modulo,
                Status = status,
            };

            using HttpResponseMessage responseUpdate = await client.PutAsJsonAsync(
                "account", studentUpdate);

            responseUpdate.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Trait("Category", "Criar Endpoint para busca de contas")]
        [Fact(DisplayName = "Teste para rota get /account/id com status 404")]
        public async Task GetAccoutWithFail()
        {

            var client = _factory.CreateClient();

            using HttpResponseMessage responseGet = await client.GetAsync("account/99");

            responseGet.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }


        [Trait("Category", "Criar Endpoint para deletar conta")]
        [Fact(DisplayName = "Teste para rota delete /account/id com status 400")]
        public async Task DeleteAccoutWithSucces()
        {
            var user = new Register
            {
                Email = "eduardo10@gmail.com",
                Nome = "Eduardo Pereira",
                Modulo = "Back End",
                Status = "Finalizando",
                Senha = "senha123"
            };

            var client = _factory.CreateClient();

            using HttpResponseMessage response = await client.PostAsJsonAsync(
                "account", user
                );

            var content = await response.Content.ReadFromJsonAsync<User>();

            var token = new TokenGenerator().Generate(user.Nome, user.Email, content.Id);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            using HttpResponseMessage responseDelete = await client.DeleteAsync($"account/99");

            responseDelete.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Trait("Category", "Criar Endpoint para fazer login")]
        [Fact(DisplayName = "Teste para rota post /account/login com status 400")]
        public async Task LoginAccoutWithSucces()
        {
            var user = new Register
            {
                Email = "carol10@gmail.com",
                Nome = "Carol Ferreira",
                Modulo = "Back End",
                Status = "Finalizando",
                Senha = "senha123"
            };

            var client = _factory.CreateClient();

            using HttpResponseMessage response = await client.PostAsJsonAsync(
                "account", user
                );

            using HttpResponseMessage responseLogin = await client.PostAsJsonAsync(
                "account/login", new Login { email = user.Email, senha = "123456" });

            responseLogin.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }
    }
}