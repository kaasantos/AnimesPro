# AnimesPro
    Esta é uma API RESTful para realizar operações CRUD (Create, Read, Update, Delete) em uma coleção de animes, ao qual todas as operações serão logadas e salvas em banco de dados. A API permite que você gerencie informações sobre diferentes animes, incluindo seus nomes, diretor e resumo.

# Recursos Disponíveis
    Animes
    Listar Animes
    GET /animes - Retorna uma lista de todos os animes cadastrados.
    Obter um Anime Específico
    GET /animes/{id} - Retorna os detalhes de um anime específico, identificado pelo seu ID.
    GET /animes/{key}/{filter} - Retorna os detalhes de um anime específico, identificado pela sua chave e tipo de filtro, podendo ser ele por nome, diretor, resumo ou todos.
    Paginar os dados - Retorna uma lista animes cadastrados podendo controlar a quantidade de registros por página.
    GET /animes/Pagination - Retorna os detalhes de um animes dentro das epecificações e filtros.
    Adicionar um Novo Anime
    POST /animes - Adiciona um novo anime à coleção.
    Atualizar um Anime Existente
    PUT /animes - Atualiza os detalhes de um anime existente.
    Remover um Anime
    DELETE /animes/{id} - Remove um anime da coleção, identificado pelo seu ID.

# Estrutura de Dados
    Um anime é representado por um objeto JSON com os seguintes campos:

    id: Identificador único do anime (gerado automaticamente).
    nome: O nome do anime, sendo ele único e obrigatório.
    diretor: Diretor do anime, sendo ele obrigatório.
    resumo: Sinopse do anime.

    Exemplo de um objeto anime:
    {
    "id": 1,
    "nome": "Naruto",
    "diretor" : "Masashi Kishimoto",
    "resumo": "Naruto Uzumaki é um jovem ninja que sonha em se tornar o Hokage, líder da sua vila, e ser reconhecido como o mais forte de todos."
    }