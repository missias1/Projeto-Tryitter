# API para aplicativo de rede social 

Esse projeto foi realizado para o desafio final da aceleração de C#

- Listar informações da conta do cliente e dos posts
- Realizar operações de postar, atualizar e deletar post
- Realizar login e criar conta;
- Ser utilizada para se comunicar com o front de um aplicativo de rede social

## Lista de conteúdo :page_facing_up:
- Tecnologias Utilizadas e tomada de decisão
- Instalação
- Executando aplicação
- Executando testes
- Deploy no Azure
- Documentação Swagger
- Diagramas
- 3 Aprendizados

## Tecnologias utilizadas e tomada de decisão ✔️


## Instalação ✔️

Para instalar o projeto

```bash
git clone git@github.com:missias1/Projeto-Tryitter.git
```
```bash
cd Projeto-Tryitter
```
```bash
dotnet restore
```

## Executando aplicação ✔️

Rode o comando abaixo para inicializar a aplicação.

```bash
  dotnet run
```

## Executando os testes ✔️

Para rodar os testes, rode o seguinte comando

```bash
  dotnet test
```
Os testes estão com `84%` de cobertura.

<img src="https://user-images.githubusercontent.com/87737714/207664240-c07467d7-1427-4949-9d0c-df4dc3046698.png" width= "900">

## [Deploy no Azure]() ✔️

Para testar os endpoints, é necessário utilizar uma API Client como o [Insomnia](https://insomnia.rest/) ou [Postman](https://www.postman.com/). 

Assim é possível enviar requisições de POST, PUT, DELETE e acessar as rotas que exigem autenticação.

<img src="https://img.shields.io/badge/Insomnia-5849be?style=for-the-badge&logo=Insomnia&logoColor=white" width= "100"> <img src="https://img.shields.io/badge/Postman-FF6C37?style=for-the-badge&logo=Postman&logoColor=white" width= "100">

## [Documentação com Swagger]() ✔️



## Diagramas ✔️
Foi feita uma abordagem mais simples para lidar com o banco de dados. O foco foi listar as atividades que o usuário pode realizar em sua conta, bem como as ações que pode tomar para realizar determinada publicação.


#### Ações que o usuário pode realizar
<img src="https://user-images.githubusercontent.com/87737714/207675050-18106f33-cb5f-442b-857c-c4682b73bbf0.png" width= "700">


#### Relação das tabelas
<img src="https://user-images.githubusercontent.com/87737714/207674846-dfe37096-5fae-4318-a2eb-47a52f7112e6.png" width= "700">

## 3 Aprendizados
- O debugger do Visual Studio é seu melhor amigo
- Não há a forma mais correta e sim a forma mais adequada de se fazer algo
- Se deparar com erros são importante para o seu desenvolvimento
