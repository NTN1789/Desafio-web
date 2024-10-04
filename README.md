## Description
Projeto teste de webScapping 

### 📋 Pré-requisitos para executar o projeto

* Linguagem: C#
* freamwork: .NET6
*  Html Agility Pack
* banco de dados usado foi o  sqlite3



## funcionalidade do projeto 
```
extrair dados de uma página html e armazenar em um banco de dados. 
```
## depois de clonar o projeto usar as funcionalidades abaixo
```bash
$ dotnet clean
```
```bash
$ dotnet build
```

```bash
$ dotnet run
```

## detalhes que tem que esperar o sqlite criar o banco de dados e fazer as requisição que necessita e retornar uma mensagem no terminal de :
```bash
 Dados salvos com sucesso no banco de dados SQLite.
 ID: 1, Código: , Nome: , Nome Científico: , Grupo: 
ID: 2, Código: BRC0001C, Nome: Abacate, polpa, in natura, Brasil, Nome Científico: Persea americana Mill, Grupo: Frutas e derivados
ID: 3, Código: BRC0195C, Nome: Abacaxi, cozido, caramelado, Nome Científico: , Grupo: Frutas e derivados
ID: 4, Código: BRC0300C, Nome: Abacaxi, grelhado, c/ cobertura de chocolate, Nome Científico: , Grupo: Frutas e derivados
ID: 5, Código: BRC0054C, Nome: Abacaxi, polpa, congelada, Brasil, Nome Científico: Ananas comosus L., Grupo: Frutas e derivados
ID: 6, Código: BRC0264C, Nome: Abacaxi, polpa, desidratada, Brasil, Nome Científico: Ananas comosus, Grupo: Frutas e derivados
ID: 7, Código: BRC0218C, Nome: Abacaxi, polpa, grelhado, c/ canela, Nome Científico: , Grupo: Frutas e derivados

```

vai retornar os dados , só tá dando erro para buscar os components , mas está no composicao.db 