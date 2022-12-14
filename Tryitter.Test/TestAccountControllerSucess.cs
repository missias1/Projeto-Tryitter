using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Tryitter.Autentication;
using Tryitter.Models;
using Tryitter.Schemas;

namespace Tryitter.Test
{
    public class TestAccountControllerSucess : IClassFixture<ContextToTest<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public TestAccountControllerSucess(ContextToTest<Program> factory)
        {
            _factory = factory;
        }

        [Trait("Category", "Criar Endpoint para cadastro")]
        [Fact(DisplayName = "Teste para rota post /account com status 201")]
        public async Task CreateUserWithSucess()
        {
            var user = new Register
            {
                Email = "aline100@gmail.com",
                Nome = "Aline Souza",
                Modulo = "Back End",
                Status = "Finalizando",
                Senha = "senha123"
            };

            var client = _factory.CreateClient();
            using HttpResponseMessage response = await client.PostAsJsonAsync(
                "account", user
                );

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
        }

        [Trait("Category", "Criar Endpoint para atualização cadastral")]
        [Theory(DisplayName = "Teste para rota put /account com status 201")]
        [InlineData("Front End", "Finalizando")]
        public async Task UpdateAccountWithSucess(string modulo, string status)
        {
            var user = new Register
            {
                Email = "maria100@gmail.com",
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
                Id = content.Id,
                Modulo = modulo,
                Status = status,
            };

            using HttpResponseMessage responseUpdate = await client.PutAsJsonAsync(
                "account", studentUpdate);

            responseUpdate.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
        }

        [Trait("Category", "Criar Endpoint para busca de contas")]
        [Fact(DisplayName = "Teste para rota get /account/id com status 200")]
        public async Task GetAccoutWithSucces()
        {
            var user = new Register
            {
                Email = "joao100@gmail.com",
                Nome = "Joao Pedro",
                Modulo = "Back End",
                Status = "Finalizando",
                Senha = "senha123"
            };

            var client = _factory.CreateClient();

            using HttpResponseMessage response = await client.PostAsJsonAsync(
                "account", user
                );

            var content = await response.Content.ReadFromJsonAsync<User>();

            using HttpResponseMessage responseGet = await client.GetAsync($"account/{content.Id}");

            responseGet.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }


        [Trait("Category", "Criar Endpoint para deletar conta")]
        [Fact(DisplayName = "Teste para rota delete /account/id com status 204")]
        public async Task DeleteAccoutWithSucces()
        {
            var user = new Register
            {
                Email = "eduardo100@gmail.com",
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

            using HttpResponseMessage responseDelete = await client.DeleteAsync($"account/{content.Id}");

            responseDelete.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
        }

        [Trait("Category", "Criar Endpoint para fazer login")]
        [Fact(DisplayName = "Teste para rota post /account/login com status 201")]
        public async Task LoginAccoutWithSucces()
        {
            var user = new Register
            {
                Email = "carol100@gmail.com",
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
                "account/login", new Login { email = user.Email, senha = user.Senha });

            responseLogin.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
        }
    }
}