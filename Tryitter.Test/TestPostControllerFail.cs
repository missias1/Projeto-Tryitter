using FluentAssertions;
using FluentAssertions.Equivalency;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Tryitter.Autentication;
using Tryitter.Models;
using Tryitter.Schemas;

namespace Tryitter.Test
{
    public class TestPostControllerFail : IClassFixture<ContextToTest<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public TestPostControllerFail(ContextToTest<Program> factory)
        {
            _factory = factory;
        }

        [Trait("Category", "Criar Endpoint para publicação de posts")]
        [Fact(DisplayName = "Teste para rota post /post com status 400")]
        public async Task CreatePostWithFail()
        {
            var user = new Register
            {
                Email = "mayara1@gmail.com",
                Nome = "Mayara",
                Modulo = "Fundamentos",
                Status = "Em andamento",
                Senha = "senha123"
            };

            var client = _factory.CreateClient();

            using HttpResponseMessage response = await client.PostAsJsonAsync(
                "account", user
                );

            var content = await response.Content.ReadFromJsonAsync<User>();

            var token = new TokenGenerator().Generate(user.Nome, user.Email, content.Id);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var post = new NewPost
            {
                Mensagem = "Minha primeira publicação",
                Imagem = "",
                UserId = 99,
            };

            using HttpResponseMessage responsePost = await client.PostAsJsonAsync(
                "post", post);

            responsePost.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Trait("Category", "Criar Endpoint para listagem de posts por usuário")]
        [Fact(DisplayName = "Teste para rota get /post com status 404")]
        public async Task ListAllPostsByIdWithFail()
        {
            var client = _factory.CreateClient();

            using HttpResponseMessage responseGet = await client.GetAsync("post/99");

            responseGet.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        [Trait("Category", "Criar Endpoint para listagem do último post do usuário")]
        [Fact(DisplayName = "Teste para rota get /post/{id}/last com status 404")]
        public async Task ListLastPostsByIdWithFail()
        {
            var client = _factory.CreateClient();

            using HttpResponseMessage responseGet = await client.GetAsync("post/99/last");

            responseGet.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        [Trait("Category", "Criar Endpoint para deletar post")]
        [Fact(DisplayName = "Teste para rota delete /post/{id} com status 400")]
        public async Task DeletePostWithFail()
        {
            var user = new Register
            {
                Email = "eduardo12@gmail.com",
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

            var post = new NewPost
            {
                Mensagem = "Minha primeira publicação",
                Imagem = "",
                UserId = content.Id
            };

            using HttpResponseMessage responsePost = await client.PostAsJsonAsync("post", post);

            var contentPost = await responsePost.Content.ReadFromJsonAsync<Post>();

            using HttpResponseMessage responseDelete = await client.DeleteAsync("post/99");

            responseDelete.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Trait("Category", "Criar Endpoint para atualização do post")]
        [Fact(DisplayName = "Teste para rota put /post com status 400")]
        public async Task UpdatePostWithFail()
        {
            var user = new Register
            {
                Email = "joao@gmail.com",
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

            var token = new TokenGenerator().Generate(user.Nome, user.Email, content.Id);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var post = new NewPost
            {
                Mensagem = "Minha primeira publicação",
                Imagem = "",
                UserId = content.Id,
            };

            using HttpResponseMessage responsePost = await client.PostAsJsonAsync("post", post);

            var jsonresponse = await responsePost.Content.ReadAsStringAsync();

            var postUpdate = new UpdatePost
            {
                Mensagem = "Minha publicação alterada",
                Imagem = "",
                Id = jsonresponse[0],
                UserId = 99,
            };

            using HttpResponseMessage responseUpdate = await client.PutAsJsonAsync("post", postUpdate);

            responseUpdate.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }
    }
}
