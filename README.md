# Virtual Shop School

Um sistema de e-commerce especializado na venda de produtos escolares, desenvolvido com uma arquitetura modular baseada em microsserviços e segurança via tokens.

## Sobre o Projeto

O Virtual Shop School é uma plataforma robusta de comércio eletrônico focada em gerenciar o ciclo de vida de produtos, carrinhos de compra e autenticação segura de usuários.

### Estrutura e Funcionalidades

*   **ProductAPI**: Implementação de um CRUD completo (Create, Read, Update, Delete) para o catálogo de materiais escolares.
*   **CartAPI**: Gerenciamento do carrinho de compras do usuário.
*   **CouponAPI**: Sistema integrado para aplicação de cupons de desconto.
*   **Identity Server**: Servidor centralizado para autenticação de usuários, utilizando **JWT (JSON Web Token)** para garantir a segurança e a comunicação autenticada entre as diferentes APIs.
*   **Web**: Interface desenvolvida seguindo o padrão **MVC (Model-View-Controller)**, com sintaxe Razor, para garantir uma navegação organizada e uma melhor separação de responsabilidades.

## Tecnologias Utilizadas

*   **Backend**: .NET 7, ASP .NET, C#, EntityFramework, AutoMapper, Swagger
*   **Arquitetura**: API REST, MVC
*   **Segurança**: Identity Server, JWT (JSON Web Tokens)
*   **Banco de Dados**: MySQL

## Funcionalidades Principais

*   Catálogo de produtos escolares.
*   Gerenciamento de carrinho de compras com suporte a descontos.
*   Sistema de login e autenticação segura com tokens.
*   Interface intuitiva seguindo o padrão Model-View-Controller.
