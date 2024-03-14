# AnimesPro
Esta é uma API RESTful para realizar operações CRUD (Create, Read, Update, Delete) em uma coleção de animes, 
    ao qual todas as operações serão logadas e salvas em banco de dados. 
    A API permite que você gerencie informações sobre diferentes animes, incluindo seus nomes, diretor e resumo.

## Recursos Disponíveis
   - **Listar Animes**
    - GET /animes - Retorna uma lista de todos os animes cadastrados.
   - **Obter um Anime Específico por Id**
    - GET /animes/{id} - Retorna os detalhes de um anime específico, identificado pelo seu ID.
   - **Obter um Anime Específico por Key**
    - GET /animes/{key}/{filter} - Retorna os detalhes de um anime específico, identificado pela sua chave e tipo de filtro, 
    podendo ser ele por nome, diretor, resumo ou todos.
   - **Paginar Dados** 
    - GET /animes/Pagination - Retorna os detalhes de um animes dentro das epecificações e filtros.
   - **Adicionar um Novo Anime**
    - POST /animes - Adiciona um novo anime à coleção.
   - **Atualizar um Anime Existente**
    - PUT /animes - Atualiza os detalhes de um anime existente.
   - **Remover um Anime**
    - DELETE /animes/{id} - Remove um anime da coleção, identificado pelo seu ID.  
- **Listar Logs** - GET /logSistema - Retorna uma lista de todos os logs registrados.
- **Listar Logs em um Intervalo** - GET /logSistema/ChecarLogPorData - Retorna uma lista de todos os logs registrados dentor de um intervalo.


## Estrutura de Dados
 Um anime é representado por um objeto JSON com os seguintes campos:
   - **id**
    : Identificador único do anime (gerado automaticamente).
   - **nome**
    : O nome do anime, sendo ele único e obrigatório.
   - **diretor**
    : Diretor do anime, sendo ele obrigatório.
   - **resumo**
    : Sinopse do anime.


 Exemplo de um objeto anime:

    
    {
        "id": 1,
        "nome": "Naruto",
        "diretor" : "Masashi Kishimoto",
        "resumo": "Naruto Uzumaki é um jovem ninja que sonha em se tornar o Hokage, líder da sua vila, e ser reconhecido como o mais forte de todos."
    }

## Banco de dados
A aplicação foi projetada para criar automaticamente o banco de dados no SQL Server, caso o banco não seja detectado. Para configurar o banco de dados, siga estas etapas:
   - **Adicionar Credenciais no appsettings.json:** Abra o arquivo appsettings.json na pasta Api Anime Pro e adicione as credenciais necessárias para se conectar ao SQL Server. Você encontrará um trecho semelhante ao destacado abaixo. Substitua "nomeDoServidor", "seuUsuario", "NomeDoUsuario" e "suaSenha" pelos detalhes da sua configuração do SQL Server.
       - "DataBase": "Data Source=nomeDoServidor; Initial Catalog=DB_AnimesPro; User Id=seuUsuario; Password=suaSenha;TrustServerCertificate=True;"
- **Executar a Aplicação:** Ao iniciar a aplicação, o sistema verificará se o banco de dados especificado já existe. Se não existir, será criado automaticamente com base nas credenciais fornecidas no appsettings.json.
- **Confirmação da Criação do Banco de Dados:** Após a execução bem-sucedida da aplicação, você pode verificar no SQL Server Management Studio ou em qualquer outra ferramenta de gerenciamento de banco de dados se o banco de dados foi criado corretamente.
- **Utilização da Aplicação:** Com o banco de dados devidamente configurado e criado, você pode agora utilizar a aplicação normalmente.

