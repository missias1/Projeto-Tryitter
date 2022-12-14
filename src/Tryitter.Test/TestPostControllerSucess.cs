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
    public class TestPostControllerSucess : IClassFixture<ContextToTest<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public TestPostControllerSucess(ContextToTest<Program> factory)
        {
            _factory = factory;
        }

        [Trait("Category", "Criar Endpoint para publicação de posts")]
        [Fact(DisplayName = "Teste para rota post /post com status 201")]
        public async Task CreatePostWithSucess()
        {
            var user = new Register
            {
                Email = "mayara12@gmail.com",
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
                UserId = content.Id,
            };
            //quickwatcher
            using HttpResponseMessage responsePost = await client.PostAsJsonAsync(
                "post", post);

            responsePost.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
        }

        [Trait("Category", "Criar Endpoint para listagem de posts por usuário")]
        [Fact(DisplayName = "Teste para rota get /post com status 200")]
        public async Task ListAllPostsByIdWithSucess()
        {
            var user = new Register
            {
                Email = "aline@gmail.com",
                Nome = "Aline Souza",
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

            using HttpResponseMessage responseGet = await client.GetAsync($"post/{content.Id}");

            responseGet.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Trait("Category", "Criar Endpoint para listagem do último post do usuário")]
        [Fact(DisplayName = "Teste para rota get /post/{id}/last com status 200")]
        public async Task ListLastPostsByIdWithSucess()
        {
            var user = new Register
            {
                Email = "carol@gmail.com",
                Nome = "Carol Ferreira",
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

            using HttpResponseMessage responseGet = await client.GetAsync($"post/{content.Id}/last");

            responseGet.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Trait("Category", "Criar Endpoint para deletar post")]
        [Fact(DisplayName = "Teste para rota delete /post/{id} com status 204")]
        public async Task DeletePostWithSucess()
        {
            var user = new Register
            {
                Email = "eduardo@gmail.com",
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

            using HttpResponseMessage responseDelete = await client.DeleteAsync($"post/{contentPost.Id}");

            responseDelete.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
        }

        [Trait("Category", "Criar Endpoint para atualização do post")]
        [Fact(DisplayName = "Teste para rota put /post com status 201")]
        public async Task UpdatePostWithSucess()
        {
            var user = new Register
            {
                Email = "joaozinho@gmail.com",
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

            var jsonresponse = await responsePost.Content.ReadFromJsonAsync<Post>();

            var postUpdate = new UpdatePost
            {
                Mensagem = "Minha publicação alterada",
                Imagem = "",
                Id = jsonresponse.Id,
                UserId = content.Id,
            };

            using HttpResponseMessage responseUpdate = await client.PutAsJsonAsync("post", postUpdate);

            responseUpdate.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
        }
    }
}
