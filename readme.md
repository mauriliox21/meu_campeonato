# Simulador de resultado de campeonatos C#

API para simular resultado campeonatos de com 8 times.

## Preparando o Ambiente

### Banco de Dados

Banco de dados: o banco de dados utilizado foi o SQL Server, o arquivo físico do banco está no diretório /banco/data/meuCampeonato.mdf, as cargas já foram executadas e o banco está pronto para uso.


### Construção da Aplicação

A aplicação foi criada utilizando a estrutura de camadas API, BLL (Bussiness Logic Layer), DAL (Data Acess Layer).

Padrão de escrita de código: Todos os nomes de tabelas colunas e procedures foram escritos com letras maiúsculas, onde os nomes compostos são separados por “_”, além disso todos tem prefixos em seus nomes relacionados a seus tipos (ex: Nomes de tabelas se iniciam com TB, storage procedures com STP). Nomes de coluna e variáveis tem o prefixo dos dados que vão armazenar (ex: Colunas com o prefixo SQ armazenam Sequências, colunas com o prefixo NM armazenam Nomes).

Para a conexão do banco de dados com a aplicação foram utilizados Entity Framework e SqlClient.

### Interface da API e exemplos de parâmetros

A coleção utilizada para testar a aplicação está disponível em [https://www.getpostman.com/collections/13d4f0d634c0190b7fea](https://www.getpostman.com/collections/13d4f0d634c0190b7fea)

Cadastro de Campeonatos 
| Método | Url                                | Parâmetros                               |
| ------ | ---------------------------------- | ---------------------------------------- |
| GET    | localhost:44307/api/campeonatos    |                                          |
| GET    | localhost:44307/api/campeonatos/id |                                          |
| POST   | localhost:44307/api/campeonatos    | {"nomeCampeonato": "1° Campeonato"}      |

- `GET localhost:44307/api/campeonatos/id` busca um único campeonato e os times que estão participando dele 


Cadastro de Times
| Método | Url                          | Parâmetros                                       |
| ------ | ---------------------------- | ------------------------------------------------ |
| POST   | localhost:44307/api/times    | { "sqCampeonato": "11","nomeTime": "Manchester"} |

- `POST localhost:44307/api/times` cria um time e o inclui em um campeonato, caso o time já exista criado apenas o inclui no campeonato

Simulações 
| Método | Url                                | Parâmetros                                    |
| ------ | ---------------------------------- | --------------------------------------------- |
| GET    | localhost:44307/api/simulacoes/id  | localhost:44307/api/simulacoes/{sqCampeonato} |
| POST   | localhost:44307/api/simulacoes/id  | localhost:44307/api/simulacoes/{sqCampeonato} |

- `GET localhost:44307/api/simulacoes/id` recebe o sequencial de um campeonato e busca sua simulação
- `POST localhost:44307/api/simulacoes/id` recebe o sequencial de um campeonato e gera sua simulação *obs: cada campeonato pode ter apenas uma simulação*