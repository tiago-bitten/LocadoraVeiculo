# Projeto: Sistema de Locadora de Carros

## Propósito do Sistema
Desenvolver uma aplicação para gestão de locadora de carros, dividida em três microsserviços (Clientes, Veículos e Aluguéis). O sistema permitirá o gerenciamento de clientes, veículos disponíveis para locação e os registros de aluguéis, promovendo integração eficiente entre os microsserviços.

## Estrutura do Projeto
O sistema será composto por três microsserviços independentes:

1. **Clientes**: Responsável por gerenciar os dados dos clientes.
2. **Veículos**: Responsável por gerenciar os dados dos veículos, incluindo disponibilidade.
3. **Aluguéis**: Atua como o orquestrador, sendo responsável por registrar aluguéis e integrar-se aos microsserviços de Clientes e Veículos.

## Requisitos Funcionais
1. Gerenciamento de clientes:
   - Criar, listar, atualizar e remover clientes.
2. Gerenciamento de veículos:
   - Criar, listar, atualizar e remover veículos.
   - Consultar disponibilidade dos veículos.
3. Gestão de aluguéis:
   - Registrar novos aluguéis.
   - Consultar informações sobre clientes e veículos.
   - Alterar o status de veículos para "indisponível" ao confirmar um aluguel.

## Descritivo Técnico

### Estrutura de IDs
- Os IDs de todas as entidades serão do tipo `string`, formatados como `<nome_da_entidade>_<número_aleatório>`. Exemplos:
  - Cliente: `cliente_949838`
  - Veículo: `veiculo_583729`
  - Aluguel: `aluguel_293847`

### Microsserviços

#### 1. Clientes
- **Descrição**: Gerencia os dados dos clientes.
- **Requisitos**:
  - Exposição de endpoints REST para operações CRUD.
  - Armazenamento de dados utilizando SQLite.

#### 2. Veículos
- **Descrição**: Gerencia os dados dos veículos e sua disponibilidade.
- **Requisitos**:
  - Exposição de endpoints REST para operações CRUD e consulta de disponibilidade.
  - Alteração do status de veículos para "indisponível" após confirmação de um aluguel.
  - Armazenamento de dados utilizando SQLite.

#### 3. Aluguéis
- **Descrição**: Orquestra o sistema, realizando integração com os microsserviços de Clientes e Veículos.
- **Requisitos**:
  - Exposição de endpoints REST para registro e consulta de aluguéis.
  - Validações:
    - Consulta ao microsserviço de Clientes para verificar a existência do cliente.
    - Consulta ao microsserviço de Veículos para verificar a disponibilidade do veículo.
  - Alteração do status do veículo para "indisponível" ao registrar um aluguel.
  - Utilização do `HttpClient` para comunicação com os microsserviços de Clientes e Veículos.
  - Armazenamento de dados utilizando SQLite.

### Integrações

1. **Consulta de Clientes (Aluguéis → Clientes)**:
   - O microsserviço de Aluguéis consulta o microsserviço de Clientes para verificar a existência e status do cliente antes de registrar um aluguel.

2. **Consulta de Veículos (Aluguéis → Veículos)**:
   - O microsserviço de Aluguéis consulta o microsserviço de Veículos para verificar a disponibilidade do veículo antes de registrar um aluguel.

### Tecnologias Utilizadas
- **Framework**: .NET 8
- **Banco de Dados**: SQLite
- **Comunicação entre microsserviços**: REST API utilizando `HttpClient`
- **Gerenciamento de jobs**: Hangfire

### Fluxo do Processo de Aluguel
1. O cliente faz uma solicitação de aluguel.
2. O microsserviço de Aluguéis:
   - Consulta o microsserviço de Clientes para validar o cliente.
   - Consulta o microsserviço de Veículos para verificar a disponibilidade do veículo.
3. Se todas as validações forem aprovadas:
   - O aluguel é registrado.
   - Uma requisição é enviada ao microsserviço de Veículos para alterar o status do veículo para "Alugado".

### Estrutura de Diretórios
