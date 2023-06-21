# FrameworkWebDesk

## Agosto/2009 - Tema:
   Criação de um framework em C# Web Forms e Windows Forms e NHibernate para sistemas CRUD. Ao invés do uso da arquitetura Table Module de Martin Fowler, foi adotado Domain Model.

## Requisito: 
O desenvolvedor precisa aumentar sua produtividade para a construção de telas de cadastros, consultas e classes de domínio com persistência (Scaffold) para frontends Desktop e Web.

## Solução: 
Mediante o esquema de dados do Oracle para um determinado sistema (tabelas, views, etc) já criado pelo Oracle Designer 6i pelo analista de sistemas, o desenvolvedor usará um framework que lê todas as tabelas do Oracle, gerando telas de cadastros e consultas de forma automática, incluindo classes de domínios com responsabilidades próprias, com validações inclusas e considerando FKs para a montagem de telas. O mapeamento é automático e as operações CRUD são geradas prontas para uso, mantendo o mesmo backend em C# tanto para frontend Web Forms quanto para Windows Forms.

Framework CRUD construído em C# 2.0 Windows Forms, Web Forms e NHibernate para a produtividade de operações repetitivas, com componentes com binds diretos para seus entities em modo designer, para input com validations e output de dados com poucas linhas de código.
