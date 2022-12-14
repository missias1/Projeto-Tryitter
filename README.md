# API para aplicativo de rede social :chart_with_upwards_trend:

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

O projeto foi desenvolvido com a arquitetura MSC (Model, Service, Controller), possibilitando que as funções desempenhem um papel específico de acordo com sua camada.


As tecnologias utilizadas foram as aprendidas até aqui como o JavaScript, NodeJS, ExpressJS e MySQL. Esta última foi optada, ao invés de um ORM, por dar maior controle na manipulação do banco de dados e permitir escrever as queries. Utilizando o MySQL, pude trabalhar tendo mais domínio das saídas esperadas pelo banco, além de possibilitar maior entendimento ao lidar com o banco de dados.

Para realizar os testes foram utilizadas as bibliotecas aprendidas até aqui: Sinon, Mocha e Chai.

Outras tecnologias utilizadas foram o JWT para fazer autenticação nas rotas e Swagger para documentar os endpoints.

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



Obs2: Selecione o servidor do deploy para que não ocorra erro de conexão com a rede.

## Diagramas ✔️
Foi feita uma abordagem mais simples para lidar com o banco de dados. O foco foi listar as atividades que o usuário pode realizar em sua conta, bem como as ações que pode tomar para realizar determinada publicação.


#### Ações que o usuário pode realizar
<img src="![image](https://user-images.githubusercontent.com/87737714/207670451-86267047-f473-4a9f-9bcc-f4af5619ef79.png)
" width= "800">


#### Relação das tabelas
<img src="![image](https://user-images.githubusercontent.com/87737714/207674282-fe4d5b69-6788-4be8-838e-a5cf148f491b.png)
" width= "800">

## 3 Aprendizados
- O debugger do Visual Studio é seu melhor amigo
- Não há a forma mais correta e sim a forma mais adequada de se fazer algo
- Se deparar com erros são importante para o seu desenvolvimento
