# 🔐 Projeto - Tela de Login

Este projeto apresenta uma aplicação de login completa, desenvolvida com .NET 8 no backend e Flutter no frontend. Ele foca em segurança e robustez, com recursos como autenticação segura, cadastro com verificação de senha, controle de logs detalhados e validação de cadastro por e-mail.

---

🚀 Tecnologias Utilizadas
- Backend: .NET 8

- Frontend: Flutter

- Banco de Dados: SQL Server

- Mensageria: RabbitMQ

- Observabilidade: ElasticSearch + Kibana (com Serilog para logging)

- Testes: Unitários, de Integração e E2E

- DevOps: Docker Compose (para orquestração de serviços)

---

## 🧩 Arquitetura

🔙 Backend

 Backend
O backend é construído com foco em escalabilidade e manutenção, seguindo princípios de design robustos:

- Clean Architecture: Estruturado em camadas de Domínio, Aplicação e Infraestrutura, promovendo a separação de responsabilidades.

- Domain-Driven Design (DDD): Utiliza Value Objects para garantir um domínio rico, validado e seguro.

- Separação por Ambiente: Configurações específicas para Development, Staging e Production, facilitando o deploy e a gestão.

- Versionamento de API: Permite a adição de novas funcionalidades sem impactar clientes que utilizam versões anteriores.

- Ports and Adapters (Arquitetura Hexagonal): Facilita a comunicação com a infraestrutura externa (ex: banco de dados), permitindo a troca de tecnologias (ex: SQL Server para PostgreSQL) e simplificando os testes (via mocks).

- Middleware Global: Responsável por tratar exceções de forma centralizada e capturar dados para envio ao Serilog e ElasticSearch.

- Módulos (Bounded Contexts): Cada funcionalidade principal é encapsulada em seu próprio módulo; por exemplo, o AuthModule gerencia todas as operações de autenticação e registro de usuários.

- Módulo Compartilhado (Shared Kernel): Contém utilitários e classes reutilizáveis em toda a aplicação, como exceções personalizadas, infraestrutura de banco de dados (SQL), RabbitMQ, e serviços de hashing.

- Autenticação JWT: A API é configurada para usar JSON Web Tokens (JWT), permitindo a proteção de rotas com o atributo [Authorize].

---

### 💻 Frontend

Desenvolvido em Flutter, o frontend também adere a princípios de uma arquitetura limpa:

- Clean Architecture: Organizado para manter a lógica de negócios desacoplada da interface do usuário.

- Separação por Ambiente no Flavor: Configurações específicas para Development, Staging e Production, facilitando o deploy e a gestão.

- Ports and Adapters: Serviços como Http Service e Local Storage são abstraídos, garantindo que a lógica de aplicação não dependa de implementações específicas.

- Gerenciamento de Estado: Utiliza Change Notifier para um controle eficiente da interação do usuário e da atualização da interface.

- Controle de Sessão: As informações de sessão recebidas da API são persistidas no Local Storage do cliente e gerenciadas globalmente por uma classe auxiliar, SessionNotifier.

- Testes Unitários e de Integração: Implementados para validar a estrutura de JSONs de entrada e as mudanças de estado das ações da UI.


## ✅ Funcionalidades

🔑 Tela de Login

- Autenticação segura via API.

- Validação de campos do formulário (frontend e backend).

- Feedback visual com mensagens claras de erro ou sucesso.

---

 📝 Cadastro de Usuário

- Cadastro com nome, e-mail e senha.

- Validação robusta da força da senha (frontend e backend).

- Criptografia de senha antes do armazenamento.

- Confirmação por E-mail: Envio de e-mail de confirmação. O login só é liberado após a verificação do e-mail.

---

📨 Envio de E-mail com RabbitMQ

- Após o cadastro, a API publica os dados do usuário em uma fila do RabbitMQ.

- Um Email Worker Service dedicado consome essa fila e, de forma assíncrona, envia o e-mail de confirmação via SMTP.

- Essa abordagem garante o desacoplamento e a resiliência do processo de envio de e-mails, sem impactar a performance da API.

---

🛡️ Logs e Exceções

- Logs detalhados com:
  - Body enviado pelo usuário.
  - Respostas retornadas.
  - Exceções, Stack Trace, Ambiente, entre outras informações.
- Uso de Serilog para ambos: API e Worker Service.
- Visualização via Kibana conectada ao ElasticSearch.

---

🧪 Estratégia de Testes

Adotei uma estratégia de testes que segue os princípios F.I.R.S.T (Fast, Independent, Repeatable, Self-validating, Timely), garantindo testes rápidos, independentes e confiáveis:

- Testes Unitários: Focados na camada de domínio, validando o comportamento de entidades e value objects.

- Testes de Integração: Divididos em:

- Narrow: Testes mais rápidos que utilizam mocks para dependências externas, focando em use cases e repositórios.

- Broad: Testes mais abrangentes que interagem com infraestruturas reais (ex: banco de dados), validando a integração entre componentes.

- Testes E2E (End-to-End): Um teste abrangente que simula um fluxo de usuário completo (ex: cadastro de usuário via controller), validando a aplicação de ponta a ponta. Embora mais lentos, garantem a funcionalidade integrada de todo o sistema.

---

📦 Execução com Docker Compose

Este projeto utiliza Docker Compose para orquestrar e subir todos os serviços necessários em um único comando, simplificando o ambiente de desenvolvimento.

Serviços Contidos:
- SQL Server (com scripts de inicialização automática)

- API (.NET 8)

- Worker Service (para envio de e-mails)

- RabbitMQ

- ElasticSearch

- Kibana

- Flutter SDK (para build do frontend)

- Nginx (para servir o frontend)

---

### Como executar o projeto:

**1. Clone o Repositório:**

- git clone https://github.com/gianhp12/labs-challenge-api.git

**2. Inicie os Serviços com Docker Compose:**

 Agora, suba todos os serviços definidos no arquivo docker-compose.yml. O --build garante que as imagens serão construídas caso não existam ou tenham sido atualizadas:

- docker compose up --build

**3. Acessando a Aplicação**:  

 Uma vez que todos os serviços estejam em execução:

- O Frontend da Aplicação estará disponível em: http://localhost:8080

- A Documentação Swagger da API pode ser acessada em: http://localhost:8081

**4. Acessando os Logs da Aplicação:**

- Os logs da aplicação podem ser acessados pelo Kibana em: http://localhost:5601

- Ao acessar o Kibana, na seção de criar data views, estará disponivel dois indices de logs:

  -emailserviceworker-staging-(ano/mes)
  
  -labschallengeapi-staging-(ano/mes)
